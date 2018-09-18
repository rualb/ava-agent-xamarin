using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AvaExt.Settings;
using System.Net.Sockets;
using AvaExt.Common;

using AvaAgent.Common;
using System.Net;
using AvaExt.Translating.Tools;
using System.Threading;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Formating;
using AvaExt.MyException;

namespace AvaAgent.Services
{
    public class AgentData : IServerAgent
    {

        IEnvironment environment;
        TcpClient client;
        Stream stream;

        int timeout = CurrentVersion.ENV.getTimeOut();

        bool useHttp = false;

        public AgentData(IEnvironment pEnv)
        {
            environment = pEnv;
            client = new TcpClient();

            useHttp = ((CurrentVersion.ENV.getServerPort() / 1000) == 8);

            client.SendTimeout = client.ReceiveTimeout = timeout;
            client.Client.SendTimeout = client.Client.ReceiveTimeout = timeout;
        }

        void startConnectionManager()
        {
            WebRequest req = null;
            WebResponse res = null;
            try
            {

                req = HttpWebRequest.Create("http://127.0.0.1:54321");
                req.Timeout = 1;
                res = req.GetResponse();

            }
            catch { }
            finally
            {
                if (req != null)
                    req.Abort();
                if (res != null)
                    res.Close();
            }
        }

        //protected virtual int getPort(bool pForSending)
        //{
        //    int port_ = CurrentVersion.ENV.getServerPort();

        //    //if (pForSending)
        //    //{
        //    //    int firmNr_ = environment.getSysSettings().getInt(SettingsSysMob.MOB_SYS_FIRM, 1);
        //    //    port_ = CurrentVersion.getPortByFirmNr(firmNr_);
        //    //}

        //    return port_;
        //}

        bool connect(bool pForSending)
        {

            // Activate connections
            startConnectionManager();

            //

            string server = CurrentVersion.ENV.getServerIp();// environment.getAppSettings().getString(SettingsAvaAgent.MOB_SERVER_S);
            int port = CurrentVersion.ENV.getServerPort();// getPort(pForSending);//CurrentVersion.ENV.getServerPort();// environment.getAppSettings().getInt(SettingsAvaAgent.MOB_PORT_I, 6000);


            ToolMobile.setRuntimeMsg("Connection: to server [" + server + "] via port [" + port + "]");

            IPAddress ip = IPAddress.Parse(server);
            //Thread thread = new Thread(new ThreadStart(this._close));
            //thread.Start();

            System.Threading.Tasks.Task t = client.ConnectAsync(ip, port);
            try
            {
                t.Wait(timeout);

            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(t.Exception);
            }

            if (!client.Client.Connected)
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_CONNECTION);

            //thread.Abort();
            stream = client.GetStream();

            return true;

        }
        void disconnect()
        {
            try
            {
                stream.Close();
            }
            catch
            {
            }
            try
            {
                client.Client.Close();
            }
            catch
            {
            }
            try
            {
                client.Close();
            }
            catch
            {
            }

        }
        bool login()
        {

            BinaryWriter writer = null;
            try
            {

                writer = new BinaryWriter(stream);

                string agentNr = CurrentVersion.ENV.getAgentNr();// environment.getAppSettings().getString(SettingsAvaAgent.MOB_NR_S);
                string agentId = CurrentVersion.ENV.getAgentId(); //environment.getAppSettings().getString(SettingsAvaAgent.MOB_ID_S);

                writer.Write(agentNr);
                writer.Write(agentId);
                writer.Flush();

                if (receiveResult() == ConstProtResult.ok)
                    return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return false;
        }
        ConstProtResult receiveResult()
        {

            try
            {
                BinaryReader reader = new BinaryReader(stream);
                int res = reader.ReadInt32();
                return (ConstProtResult)res;
            }
            catch
            {
            }
            return ConstProtResult.undef;

        }
        bool init(bool pForSending)
        {


            bool res = false;
            res = connect(pForSending);
            if (res)
            {
                res = login();
                if (res)
                {
                    return true;
                }
            }
            return false;
        }
        ConstProtResult sendCmd(ConstProtCmd pCmd)
        {

            BinaryWriter writer = null;
            ConstProtResult res = ConstProtResult.undef;
            try
            {

                writer = new BinaryWriter(stream);
                writer.Write((int)pCmd);
                writer.Flush();

                res = receiveResult();

            }
            catch //(Exception exc)
            {
            }
            return res;
        }

        public virtual ConstProtResult sendData(byte[] pData)
        {
            ConstProtResult sendRes = ConstProtResult.undef;

            if (useHttp)
            {
                connectHttp(pData, "to_main");
                sendRes = ConstProtResult.ok;
            }
            else
            {

                try
                {
                    if (init(true))
                    {
                        try
                        {
                            if (sendCmd(ConstProtCmd.dataOutput) == ConstProtResult.ok)
                            {
                                _write(pData);
                                sendRes = receiveResult();
                            }
                            sendCmd(ConstProtCmd.exit);
                        }
                        catch //(Exception e)
                        {
                            sendRes = ConstProtResult.innerErrorClient;
                        }
                    }
                }
                finally
                {
                    disconnect();
                }

            }
            return sendRes;
        }

        public virtual ServerResult resiveData()
        {
            byte[] arr = null;
            ConstProtResult protRes = ConstProtResult.undef;


            if (useHttp)
            {
                arr = connectHttp(null, "from_main");
                protRes = ConstProtResult.ok;
            }
            else
            {
                try
                {
                    if (init(false))
                    {
                        try
                        {
                            if ((protRes = sendCmd(ConstProtCmd.dataInput)) == ConstProtResult.ok)
                            {

                                arr = _read();

                                sendCmd(ConstProtCmd.exit);
                            }
                        }
                        catch //(Exception e)
                        {
                            protRes = ConstProtResult.innerErrorClient;
                        }
                    }
                }
                finally
                {
                    disconnect();
                }
            }

            return new ServerResult(arr, protRes);
        }

        byte[] _read()
        {
            BinaryReader reader = new BinaryReader(stream);
            int len = reader.ReadInt32();
            byte[] arrTmp = new byte[len];
            int count = 0;
            int pos = 0;
            int tolLen = len;
            while (tolLen > 0)
            {
                count = reader.Read(arrTmp, pos, tolLen);
                pos += count;
                tolLen -= count;
            }
            return arrTmp;

        }

        void _write(byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((int)data.Length);
            writer.Write(data);
            writer.Flush();
        }


        static byte[] connectHttp(byte[] pPostData, string pController)
        {
            string server = CurrentVersion.ENV.getServerIp();
            int port = CurrentVersion.ENV.getServerPort();

            string agentNr = CurrentVersion.ENV.getAgentNr();
            string agentId = CurrentVersion.ENV.getAgentId();

            ToolMobile.setRuntimeMsg("Connection:http: agent [" + agentNr + "] to server [" + server + "] via port [" + port + "]");

            pPostData = pPostData ?? new byte[] { };
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(agentNr + ":" + agentId)));
                //System.Net.WebException
                byte[] pageData = client.UploadData("http://" + server + ":" + port + "/" + pController, "POST", pPostData);
                return pageData;
            }

        }


        public virtual string sendText(string pText)
        {
            if (useHttp)
            {

                byte[] data_ = Encoding.UTF8.GetBytes(pText);
                byte[] res_ = connectHttp(data_, "process_xml");
                var xml_ = Encoding.UTF8.GetString(res_);

                return xml_;
            }
            else
            {

                try
                {
                    if (init(true))
                    {
                        ConstProtResult sendRes = ConstProtResult.undef;

                        if (sendCmd(ConstProtCmd.dataIOXml) == ConstProtResult.ok)
                        {
                            byte[] data_ = Encoding.UTF8.GetBytes(pText);
                            _write(data_);
                            sendRes = receiveResult();
                        }

                        if (sendRes == ConstProtResult.ok)
                        {
                            byte[] data_ = _read();
                            sendCmd(ConstProtCmd.exit);
                            return Encoding.UTF8.GetString(data_);
                        }

                    }
                }
                finally
                {
                    disconnect();
                }
            }

            return null;

        }


        public void chackOperationResult(object pRetResult)
        {
            if (pRetResult != null)
            {
                if (pRetResult.GetType() == typeof(ConstProtResult))
                    switch ((ConstProtResult)pRetResult)
                    {
                        case ConstProtResult.innerErrorClient:
                            throw new  MyExceptionError(MessageCollection.T_MSG_ERROR_INNER_CLIENT);
                        case ConstProtResult.innerErrorSerrver:
                            throw new  MyExceptionError(MessageCollection.T_MSG_ERROR_INNER_SERVER);
                        case ConstProtResult.noData:
                            throw new  MyExceptionError(MessageCollection.T_MSG_ERROR_NO_DATA);
                        case ConstProtResult.loginError:
                            throw new  MyExceptionError(MessageCollection.T_MSG_ERROR_LOGINING);
                        case ConstProtResult.no:
                            throw new  MyExceptionError(MessageCollection.T_MSG_OPERATION_FAILED);
                        case ConstProtResult.undef:
                            throw new  MyExceptionError(MessageCollection.T_MSG_ERROR_UNDEF);
                    }
                else
                    if (pRetResult.GetType() == typeof(ServerResult))
                    {
                        ServerResult res = (ServerResult)pRetResult;
                        if (res.data == null)
                            chackOperationResult(res.protResult);
                    }


            }
            else
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INNER);

        }

    }
}

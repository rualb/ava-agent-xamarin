using Android.Content;
using AvaExt.AndroidEnv.ApplicationBase;
using AvaExt.FileSystem;
using AvaExt.Formating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvaExt.Common
{
    public class CurrentVersion
    {

        static CurrentVersion()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ToolMobile.setExceptionInner(e.ExceptionObject as Exception);
        }

        public static int getPortByFirmNr(int pNr)
        {
            return 6000 + (pNr - 1);
        }

        public class ENV
        {

            static Dictionary<string, ENV.ENVITM> envKeys_;
            const string fileName = "ava.config";

            static string _nsPerfix;
            public static string nsPerfix
            {
                get
                {

                    var curNs_ = getEnvString(NS, string.Empty);
                    var nsList_ = getEnvString(NSLIST, string.Empty);
                    if (!string.IsNullOrEmpty(nsList_) && string.IsNullOrEmpty(curNs_))
                    {
                        curNs_ = ToolString.explodeList(nsList_)[0];
                        nsPerfix = curNs_;
                    }


                    return string.IsNullOrEmpty(curNs_) ? string.Empty : curNs_ + "_";
                }
                set
                {

                    setEnv(NS, value);
                    SAVE();

                }


            }

            public static void initNs(EventHandler pHandler)
            {
                string val_ = ENV.getEnvString(NSLIST, string.Empty);
                if (string.IsNullOrEmpty(val_))
                    return;

                var arr_ = ToolString.explodeList(val_);

                ToolMsg.askList(null, arr_, (s, e) =>
                {

                    if (pHandler != null)
                    {

                        var ns_ = arr_[e.Which];
                        nsPerfix = ns_;
                        pHandler.Invoke(s, EventArgs.Empty);
                    }
                });

            }







            public const string LOCAL = "LOCAL";
            public const string MAXDATASIZE = "MAXDATASIZE";
            public const string SERVER = "SERVER";
            public const string PORT = "PORT";
            public const string AGENTNR = "AGENTNR";
            public const string AGENTID = "AGENTID";
            public const string TIMEOUT = "TIMEOUT";
            public const string DEBUG = "DEBUG";
            public const string PRINTER = "PRINTER";
            public const string FIRMS = "FIRMS";
            public const string ZERODOC = "ZERODOC";
            public const string NS = "NS";
            public const string NSLIST = "NSLIST";

            //public const string PRINTERTYPE = "PRINTERTYPE";
            //public const string PRINTERENC = "PRINTERENC";

            static object debug = null;
            static bool isdebug;
            public static bool isZeroDocAllowed()
            {
                return getEnvBool(ZERODOC, false);
            }
            public static string getServerIp()
            {
                return getEnvString(SERVER, string.Empty);
            }


            public static string getPrinter()
            {
                return getEnvString(PRINTER, string.Empty);
            }
            public static string getAgentNr()
            {
                return getEnvString(nsPerfix + AGENTNR, string.Empty);
            }
            public static string getAgentId()
            {
                return getEnvString(nsPerfix + AGENTID, string.Empty);
            }
            public static string getFirms()
            {
                return getEnvString(FIRMS, string.Empty);
            }
            public static int getServerPort()
            {
                var portVal = getEnvInt(nsPerfix + PORT, -1);
                if (portVal > 0)
                {
                    return portVal;
                }

                return getEnvInt(PORT, 6000);//
            }
            public static int getMaxDataSize()
            {
                return getEnvInt(MAXDATASIZE, 1000000);
            }
            public static int getTimeOut()
            {
                return getEnvInt(TIMEOUT, 15) * 1000;
            }
            public static int getDsLimit()
            {
                return getEnvInt("DSLIMIT", 500);
            }
            public static bool isLocalExim()
            {
                return getEnvBool(LOCAL, false);
            }
            public static bool isDebugMode()
            {
                //#if DEBUG
                //                return true;
                //#endif

                if (debug == null)
                {
                    isdebug = getEnvBool(DEBUG, false);
                }

                return isdebug;
            }
            public static int getEnvInt(string pName, int pDef)
            {
                string val_ = getEnv(pName, string.Empty).Trim();
                return val_ == string.Empty ? pDef : XmlFormating.helper.parseInt(val_);
            }
            public static double getEnvDouble(string pName, int pDef)
            {
                string val_ = getEnv(pName, string.Empty).Trim();
                return val_ == string.Empty ? pDef : XmlFormating.helper.parseDouble(val_);
            }
            public static bool getEnvBool(string pName, bool pDef)
            {
                string val_ = getEnv(pName, string.Empty).Trim();
                return val_ == XmlFormating.BoolValue.boolTrue;
            }


            public static string getEnvString(string pName, string pDef)
            {
                return getEnv(pName, pDef);

            }
            public static void setEnv(string pName, string pVal)
            {
                LOAD();

                ENV.ENVITM.ADD(pName, pVal);

            }
            public static void setPrinter(string pVal)
            {
                setEnv(PRINTER, pVal);

            }
            public static void SAVE()
            {
                LOAD();

                StringBuilder sb = new StringBuilder();
                foreach (ENVITM itm in envKeys_.Values)
                    sb.AppendLine(itm.ToString());

                ToolMobile.writeFileText(fileName, sb.ToString());
            }

            public static void LOAD()
            {
                if (envKeys_ == null)
                {
                    envKeys_ = new Dictionary<string, ENV.ENVITM>();

                    if (ToolMobile.existsFile(fileName))
                    {
                        StringReader sr = new StringReader(ToolMobile.readFileText(fileName));

                        string line_ = null;
                        while ((line_ = sr.ReadLine()) != null)
                            ENV.ENVITM.PARSE(line_);

                    }
                }
            }


            public static string getEnv(string pName, string pDef)
            {
                ENV.LOAD();

                if (envKeys_.ContainsKey(pName))
                    return envKeys_[pName].val;

                return pDef;

            }


            internal class ENVITM
            {
                public static void ADD(string pName, string pVal)
                {
                    PARSE(pName + "," + pVal);

                    ENV.SAVE();
                }
                public static void PARSE(string pLine)
                {
                    if (pLine == null)
                        return;

                    ENVITM itm = new ENVITM();
                    ++indx_;

                    if (!pLine.StartsWith("//"))
                    {
                        string[] arr_ = ToolString.breakList(pLine);

                        itm.key = (arr_.Length > 0 ? arr_[0] : string.Empty).Trim();
                        itm.val = (arr_.Length > 1 ? arr_[1] : string.Empty);

                    }
                    else
                    {
                        itm.key = "dummy_" + indx_;
                        itm.val = pLine;
                        itm.isComment = true;
                    }

                    if (itm.key != string.Empty)
                    {
                        envKeys_[itm.key] = itm;
                    }
                }
                static int indx_ = 0;
                public string key = "dummy_" + indx_;
                public string val;
                public bool isComment = false;

                public override string ToString()
                {
                    return isComment ? val : key + "," + val;
                }
            }


        }

        public string dummy = "dummy";

        //public static string getName()
        //{
        //    return "AvaAgent";
        //}




        public static bool isDebug()
        {
            var res_ = false;
#if DEBUG
            res_ = true;
#endif

            return res_;
        }
    }
}

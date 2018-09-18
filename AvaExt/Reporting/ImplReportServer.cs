using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.PagedSource;
using System.IO;
using AvaExt.TableOperation;
using AvaExt.Common;
using System.Xml;
using System.Net;

namespace AvaExt.Reporting
{
    public class ImplReportServer : IReport
    {
        IEnvironment _environment;
        string _cmd;
        string content;

        public ImplReportServer(IEnvironment pEnv, string pCmd)
        {
            _environment = pEnv;
            _cmd = pCmd;



         
            refresh();

        }

        void refresh()
        {
            try
            {
                //ToolMsg.progressStart(null, "T_LOAD");
                var agent = _environment.getServerAgent();

                if (agent == null)
                    throw new Exception("Connection agent is null");

                var res = agent.sendText(string.Format(@"
<CMD>
<arr cmd='REPSERVER' arg='{0}' />
</CMD>

", WebUtility.HtmlEncode(_cmd)));

                string err = null;
                string rep = null;

                if (res != null)
                {
                    var docXml = new XmlDocument();
                    docXml.LoadXml(res);

                    var nodeRoot = docXml["CMD"];

                    var nodeRep = nodeRoot.FirstChild;

                    rep = ToolXml.getAttribValue(nodeRep, "value", "");

                    err = ToolXml.getAttribValue(nodeRep, "err", "");

                }
                else
                {
                    throw new Exception("T_MSG_ERROR_CONNECTION");
                }


                var hasErr = err != "";

                if (hasErr)
                    throw new Exception(err);

                //if (!string.IsNullOrEmpty(rep))
                //    ToolMobile.writeFileText(_file, rep);

                content = rep;
            }
            finally
            {

                //ToolMsg.progressStop( );
            }

        }



        public void refreshSource()
        {

        }

        public void setDataSource(object pDataSource)
        {

        }



        public DataSet getDataSet()
        {
            throw new NotImplementedException();
        }



        public virtual object getResult()
        {
            return content;
        }




        public virtual RenderingInfo getRenderingInfo()
        {
            return new RenderingInfo()
            {
                isDirect = false
            };
        }


    }
}

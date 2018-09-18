using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.SQL.Dynamic;
using System.Data;
using System.IO;
using AvaExt.Filter;
using AvaExt.PagedSource;
using AvaExt.SQL;
using AvaExt.MobControl.Reporting.XmlReport;
using AvaExt.TableOperation;
using System.Net;
using System.Xml;

namespace AvaExt.Reporting
{

    public class ImplReportServerSource : IReportSource
    {

        IEnvironment _environment;
        string _cmd;
        //string _file;
     //   string _dir;
        //const string dirReports = "cache/reports";

      
        string content = null;

        public ImplReportServerSource(IEnvironment pEnv, string pCmd)
        {


            _environment = pEnv;
            _cmd = pCmd;

            //_dir = Path.Combine(dirReports, CurrentVersion.ENV.getAgentNr());

           // if (!ToolMobile.existsDir(_dir))
           //     ToolMobile.createDir(_dir);

           // _file = Path.Combine(_dir, ToolObjectName.getArgValue(pCmd, "loc"));

            //auto clean cache before render
            //var cache = ToolObjectName.getArgValue(pCmd, "cache") == "1";
            ////
            //if (nocache && ToolMobile.existsFile(_file))
            //    ToolMobile.deleteFile(_file);

            //if (!ToolMobile.existsFile(_file))
            //    refresh();
 
          //  content = ToolMobile.readFileText(_file);

             

        }

   



        public DataSet get()
        {
            throw new NotImplementedException();

        }

        public DataSet getFiltersValues()
        {
            throw new NotImplementedException();
        }


        public void addFilter(IFilter pFilter)
        {

        }

        public IReport[] getReports()
        {
            return new IReport[]{
            new ImplReportServer(  _environment,   _cmd)
            };
        }


        public void Dispose()
        {

        }

    }
}

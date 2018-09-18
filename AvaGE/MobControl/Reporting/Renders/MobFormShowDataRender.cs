using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Reporting;
using AvaExt.Common;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common.Const;

namespace AvaGE.MobControl.Reporting.Renders
{
    public class MobFormShowDataRender : IReportRender
    {
        IReport _report;
        //  UnicodeEncoding _enc = new UnicodeEncoding();
        string _data = string.Empty;
       
        IEnvironment environment;

        ReportRenderUtil util = new ReportRenderUtil();
        public MobFormShowDataRender(IEnvironment pEnv)
        {
            environment = pEnv;

           
        }
        public void setReport(IReport pReport)
        {
            _report = pReport;
            util.renderingInfo = _report.getRenderingInfo();
        }

         
        public object done()
        {

            try
            {
                _report.refreshSource();

                util.renderingData = _report.getResult() as string;

                if (util.renderingData == null)
                    util.renderingData = string.Empty;

                if (util.renderingInfo.isDirect)
                    util.renderTo(null);
                else
                {
                    //  ToolMobile.startForm(typeof(MobFormShowData), new string[] { ConstCmdLine.value }, new string[] { _data });

                    ToolMobile.startForm(typeof(MobFormShowData), new string[] { MobFormShowData.PRM_RENDER }, new Java.IO.ISerializable[] { util });
                }
            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);

            }
            return null;
        }


        public void Dispose()
        {

        }




    }
}

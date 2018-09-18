using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.Common;
using MobExt.ControlOperation;
using MobExt.Settings;
using MobExt.Reporting;
using MobExt.TableOperation.RowsSelector;
using System.Data;
using MobGE.MobControl.Reporting;
using MobExt.Filter;
using MobGE.MobControl.Reporting.Renders;

namespace MobGE.MobControl
{
    public class MobMenuItemInfo : MobMenuItem
    {
        public event EventHandler InfoChildActivityDone;
        public IRowSource rowSource = null;
        public override void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            base.globalRead(pEnv, pSettings);

            //string infoList = pSettings.getString(InfoSource);
            //if (infoList != string.Empty)
            //{
            //    IDictionary<string, string> dic = ToolString.explodeForParameters(infoList);
            //    IEnumerator<string> enumer = dic.Keys.GetEnumerator();
            //    enumer.Reset();
            //    while (enumer.MoveNext())
            //    {
            //        MobMenuItem item = new MobMenuItem();
            //        item.Text = pEnv.translate(dic[enumer.Current]);
            //  item.activity = ReportFactory.createActivity(pEnv, pSettings, enumer.Current, rowSource );

            //        this.MenuItems.Add(item);
            //    }
            //}
            //else
            //    this.Enabled = false;

            string infoList = pSettings.getString(InfoSource);
            if (infoList != string.Empty)
            {

                string[] arr = ToolString.explodeList(pSettings.getString(InfoSource));
                foreach (string srcName in arr)
                {
                    //check
                    //MobMenuItem item = new MobMenuItem();
                    //item.Text = pEnv.translate(pSettings.getStringAttr(srcName, "name"));
                    //string location = pSettings.getStringAttr(srcName, "location");
                    //string[] arrParm = ToolString.explodeList(pSettings.getStringAttr(srcName, "params"));
                    //string[] arrCols = ToolString.explodeList(pSettings.getStringAttr(srcName, "cols"));
                    //item.activity = new NodeActivity(pEnv, rowSource, location, arrParm, arrCols);
                    //item.Click += new EventHandler(item_Click);
                    //this.MenuItems.Add(item);
                }
            }
            else
                this.Enabled = false;
        }

        void item_Click(object sender, EventArgs e)
        {
            if (this.InfoChildActivityDone != null)
                this.InfoChildActivityDone.Invoke(sender, EventArgs.Empty);
        }


        string _InfoSource = "INFO_SOURCE";
        public string InfoSource
        {
            get
            {
                return _InfoSource;
            }
            set
            {
                _InfoSource = value;
            }
        }


    }


    class NodeActivity : IActivity
    {
        IEnvironment _environment;
        IRowSource _valSource;
        string _location;
        string[] _params;
        string[] _cols;
        public NodeActivity(IEnvironment pEnv, IRowSource pValSource, string pLocation, string[] pParams, string[] pCols)
        {
            _environment = pEnv;
            _valSource = pValSource;
            _location = pLocation;
            _params = pParams;
            _cols = pCols;
        }



        public void done()
        {
            IReportRender render = null;
            try
            {
                IReportSource repSource = new ImplReportSource(_environment, _location);
                for (int i = 0; i < _params.Length; ++i)
                    if (_params[i] != string.Empty && _cols[i] != string.Empty)
                    {
                        IFilter filter = new ImplFilter(_environment, repSource, FilterInfo.getConstFilterInfo(_params[i], _valSource.get()[_cols[i]]));
                        repSource.addFilter(filter);
                    }
                repSource.getReports()[0].setDataSource(repSource.get());
                render = new MobFormShowDataStub(_environment);
                render.setReport(repSource.getReports()[0]);
                render.done();

            }
            catch (Exception exc)
            {
                _environment.getExceptionHandler().setException(exc);
            }
            finally
            {
                if (render != null)
                    render.Dispose();
            }
        }


    }
}

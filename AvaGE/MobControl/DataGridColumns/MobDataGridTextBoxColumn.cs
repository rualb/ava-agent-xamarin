using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.ControlOperation;
using MobExt.Common;
using MobExt.Settings;
using MobGE.MobControl.Tools.Formating;
using System.Data;
using MobExt.TableOperation;
using MobExt.Manual.Table;

namespace MobGE.MobControl.DataGridColumns
{
    public class MobDataGridTextBoxColumn : DataGridTextBoxColumn, IControlGlobalInit
    {

        public string Name = string.Empty; 

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            NullText = string.Empty;
            InitForGlobal.read(this, getGlobalObjactName(),  pEnv, pSettings);

            const string attrFormatSet = "FormatSet";
            string formatSet = pSettings.getStringAttr(this.Name, attrFormatSet);
            if (formatSet != null && formatSet != string.Empty)
            {
                string formatSetData = pSettings.getString(formatSet);
                if (formatSetData != null && formatSetData != string.Empty)
                {
                    DataTable tab = ToolString.explodeForTable(formatSetData, new string[] { TableDUMMY.LOGICALREF, TableDUMMY.VALUE });
                    pEnv.translate(tab);
                    //ToolColumn.changeType(tab, TableDUMMY.LOGICALREF, this.ValueType);
                    tab.PrimaryKey = new DataColumn[] { tab.Columns[TableDUMMY.LOGICALREF] };
                    FormatInfo = new FormatFromTable( this.Name, tab, TableDUMMY.VALUE);
                }
            }
        }
        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
        {
            
            base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
        }
        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(),   pEnv, pSettings);
        }

        public virtual string getGlobalObjactName()
        {
            return Name;
        }

   





        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using AvaExt.SQL;

namespace AvaExt.Adapter.Tools
{
    public class ToolStockRemain
    {
        //DataTable _tableIO;
        //IPagedSource _psRemain;

        IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }


        public ToolStockRemain(IEnvironment pEnv)
        {
            environment = pEnv;
            //  _psRemain = new PagedSourceMatsIO(environment);
        }


        //  int indxIOAmount;
        // int indxIOStockRef;
        public void refreshIOTable()
        {
            //_tableIO = _psRemain.get();
            //_tableIO.PrimaryKey = new DataColumn[] { _tableIO.Columns[TableDUMMY.STOCKREF] };
            //indxIOAmount = _tableIO.Columns.IndexOf(TableDUMMY.AMOUNT);
            //indxIOStockRef = _tableIO.Columns.IndexOf(TableDUMMY.STOCKREF);
        }

        public double getMatDBIO(object id)
        {
            //if (_tableIO == null)
            //    refreshIOTable();
            //DataRow rowIO = _tableIO.Rows.Find(id);
            //if (rowIO != null && !rowIO.IsNull(indxIOAmount))
            //    return (double)rowIO[indxIOAmount];
            //return 0;
            if (!ToolCell.isNull(id))
                return Convert.ToDouble(ToolCell.isNull(SqlExecute.executeScalar(environment, "SELECT ONHANDIO FROM LG_$FIRM$_ITEMS WHERE LOGICALREF = @P1", new object[] { id }), 0));
            return 0;
        }
        public double getMatDocIO(object id)
        {
            DataSet ds = environment.getCurDoc();
            if (ds != null && ToolSlip.isDsSlip(ds))
                return itemAmountInDoc(id, ds.Tables[TableSTLINE.TABLE]);
            return 0;
        }

        double itemAmountInDoc(object id, DataTable docLines)
        {
            double fix = 0;
            foreach (DataRow lRow in docLines.Rows)
            {
                if (lRow.RowState == DataRowState.Deleted ||
                    lRow.RowState == DataRowState.Unchanged ||
                    lRow.RowState == DataRowState.Modified)
                    fix -= getFix(id, lRow, DataRowVersion.Original);

                if (lRow.RowState == DataRowState.Added ||
                    lRow.RowState == DataRowState.Modified)
                    fix += getFix(id, lRow, DataRowVersion.Current);
            }
            return fix;
        }

        double getFix(object lref, DataRow lRow, DataRowVersion vers)
        {
            double fix = 0;
            if ((string)lref == (string)lRow[TableSTLINE.STOCKREF, vers])
            {
                ConstBool isCancelled = (ConstBool)(short)ToolCell.isNull(lRow[TableSTLINE.CANCELLED, vers], (short)ConstBool.yes);
                if (isCancelled == ConstBool.not)
                {
                    ConstLineType lineType = (ConstLineType)(short)ToolCell.isNull(lRow[TableSTLINE.LINETYPE, vers], (short)ConstLineType.undef);
                    double amount = (double)ToolCell.isNull(lRow[TableSTLINE.AMOUNT, vers], (double)0.0);
                    ConstIOCode ioCode = (ConstIOCode)(short)ToolCell.isNull(lRow[TableSTLINE.IOCODE, vers], (short)0);
                    if (lineType == ConstLineType.material || lineType == ConstLineType.promotion)
                        fix += (amount * getIOSign(ioCode));
                }
            }
            return fix;
        }
        double getIOSign(ConstIOCode ioCode)
        {

            // double sign = 0;
            switch (ioCode)
            {
                case ConstIOCode.input:
                case ConstIOCode.inputFromWarehouse:
                    return +1;
                case ConstIOCode.output:
                case ConstIOCode.outputFromWarehouse:
                    return -1;
            }
            return 0;
        }





    }
}

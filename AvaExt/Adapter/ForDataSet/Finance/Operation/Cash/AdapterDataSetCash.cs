using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.InfoClass;
using AvaExt.TableOperation;
using AvaExt.Database.GL;
using System.Collections;
using AvaExt.Database.Tools;


namespace AvaExt.Adapter.ForDataSet.Finance.Operation.Cash
{
    public class AdapterDataSetCash : AdapterDataSetDocument
    {
 
        protected short docSign;

        public AdapterDataSetCash(IEnvironment pEnv, IAdapterTable[] pAdapterCol)
            : base(
            pEnv,
            pAdapterCol
            )
        {
            docNumModule = (short)ConstDocNumModule.cashTrans;
            docNumDocType = 1; //undef
        }

        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableKSLINES.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        docId = row[TableKSLINES.LOGICALREF];
                        ToolCell.set(row, TableKSLINES.FICHENO,getDocNr()); 
                    }
                    else
                    {
                        docId =  row[TableKSLINES.LOGICALREF];
                    }
                    ToolCell.set(row, TableKSLINES.TRCODE, docTrCode);
                    ToolCell.set(row, TableKSLINES.SIGN, docSign);
                }
            }
        }


        protected override object getReturnResult()
        {
            return docId;
        }


        public override string getCode()
        {
            return _constAdpNamePreix + TableKSLINES.TABLE;
        }
 
    }
}

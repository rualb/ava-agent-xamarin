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


namespace AvaExt.Adapter.ForDataSet
{
    public class AdapterDataSetSlip : AdapterDataSetDocument
    {

        protected virtual string getHeaderName()
        {
            return TableINVOICE.TABLE;
        }
        protected virtual string getHeaderNameLong() 
        {
            return TableINVOICE.TABLE_FULL_NAME;
        }
        protected virtual string getHeaderNameSeq() 
        {
            return TableINVOICE.TABLE_RECORD_ID;
        }
        protected virtual string getLinesName()
        {
            return TableSTLINE.TABLE;
        }
        protected virtual string getLinesNameLong()
        {
            return TableSTLINE.TABLE_LONG;
        }
        protected virtual string getLinesNameSeq()
        {
            return TableSTLINE.TABLE_RECORD_ID;
        }
        
        protected short docGroupCode;
        protected short docIOCode;

        protected short lineTrCode;
        protected short lineGroupCode;
        protected short lineIOCode;

        public AdapterDataSetSlip(IEnvironment pEnv)
            : base(
            pEnv,
                        new IAdapterTable[]
                {   
                    new  AdapterTableSlip ( pEnv ,TableINVOICE.LOGICALREF ),
                    new AdapterTableMaterialTrans ( pEnv, TableSTLINE.STDOCREF) ,
 
                }
            )
        {
            
        }
        public override string getCode()
        {
            return _constAdpNamePreix + TableINVOICE.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData) 
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableINVOICE.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        docId = row[TableINVOICE.LOGICALREF];
                        ToolCell.set(row, TableINVOICE.FICHENO, getDocNr()); 
 
 
                    }
                    else
                    {
                        docId = row[TableINVOICE.LOGICALREF];
 
  
                    }
                    ToolCell.set(row, TableINVOICE.GRPCODE, docGroupCode);
                    ToolCell.set(row, TableINVOICE.TRCODE, docTrCode);
                    ToolCell.set(row, TableINVOICE.IOCODE, docIOCode);
                }
            }
            tab = dataSet.Tables[TableSTLINE.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if ((row.RowState != DataRowState.Deleted)) // || isSlipLinesRow(row))
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        ToolCell.set(row, TableSTLINE.STDOCREF, docId);
                    }
                    else
                    {
                    }
                    //ToolSlip.setLineMaterialInfo(row, environment);
                    ToolCell.set(row, TableSTLINE.TRCODE, lineTrCode);
                    ToolCell.set(row, TableSTLINE.IOCODE, lineIOCode);
                    ToolCell.set(row, TableSTLINE.STDOCLNNO, i + 1);
                }
            }
        }



    
        protected override object getReturnResult()
        {
            return docId;
        }


    
    }


}

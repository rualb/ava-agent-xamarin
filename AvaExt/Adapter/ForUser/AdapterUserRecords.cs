using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.TableOperation;
using AvaExt.TableOperation.CellAutomation;

namespace AvaExt.Adapter.ForUser
{
    public class AdapterUserRecords : ImplAdapterUser
    {
        String linesTableName;
        DataRow lastLine;
      


        List<DataRow> linesList = new List<DataRow>();

        DataTable getLinesTable()
        {
            return getTable(linesTableName);
        }

        public AdapterUserRecords(IEnvironment pEnv, IAdapterDataSet pAdapter, String pLinesTableName)
            : base(pEnv, pAdapter)
        {
            linesTableName = pLinesTableName;
        }
   

        public virtual void resetSchema()
        {
            lastLine = null;
            linesList.Clear();
            DataTable tab = getLinesTable();
            for (int i = 0; i < tab.Rows.Count; ++i)
                if (isValidForEnumerationRow(tab.Rows[i]))
                    linesList.Add(tab.Rows[i]);
        }
        public virtual int getLinesCount()
        {
            return linesList.Count;
        }

        public virtual bool next()
        {
                if ((lastLine == null) && (linesList.Count > 0))
                {
                    lastLine = linesList[0];
                    return true;
                }
                int indx = linesList.IndexOf(lastLine);
                if (indx < (linesList.Count - 1))
                {
                    lastLine = linesList[indx + 1];
                    return true;
                }    
                return false;
        }



        public virtual void addLine()
        {
            lastLine = addRowToTable(getLinesTable());
        }
        public virtual void insertLine(int pos)
        {
            DataTable tab = getLinesTable();
            pos = Math.Max(0, pos);
            int counter = -1;
            int realPos = tab.Rows.Count;
            for (int i = 0; i < tab.Rows.Count; ++i)
                if (isValidForEnumerationRow(tab.Rows[i]))
                {
                    ++counter;
                    if (counter == pos)
                    {
                        realPos = i;
                        break;
                    }
                }
            lastLine = insertRowIntoTable(tab, realPos);
        }

        public virtual DataRow getCurrentLine()
        {
            return lastLine;
        }
        public virtual object getLine(String col)
        {
            if (col != null)
                return lastLine[col];
            return null;
        }
        public virtual void setLine(string col, object val)
        {
            if (col != null && val != null)
                lastLine[col] = val;
        }
        public virtual void setLine(string[] colArr, object[] valArr)
        {
            if (colArr != null && valArr != null)
                for (int i = 0; i < colArr.Length; ++i)
                    setLine(colArr[i], valArr[i]);
        }

        protected virtual bool isUsedRow(DataRow pRow)
        {
            return !ToolRow.isDeleted(pRow);
        }
        protected virtual bool isValidForEnumerationRow(DataRow pRow)
        {
            return !ToolRow.isDeleted(pRow);
        }
         
    }
}

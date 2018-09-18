using AvaExt.TableOperation;
using System;
using System.Collections.Generic;
using System.Text;
 

namespace AvaExt.Database
{


    public class ColumnDescriptor
    {
        public string name;
        public int size;
        public Type type;
        public object data;

        public ColumnDescriptor copy()
        {
            ColumnDescriptor d = new ColumnDescriptor();
            d.name = name;
            d.size = size;
            d.type = type;
            return d;
        }

        public override string ToString()
        {
            return name + ' ' + size + ' ' + type;
        }
    }

    public class ColumnDescriptorExt
    {
        public readonly string table = string.Empty;
        public readonly string column = string.Empty;

        public int size;
        public Type type;
        public object data;

        public ColumnDescriptorExt(string pName)
        {
            if (ToolColumn.isColumnFullName(pName))
            {
                table = ToolColumn.extractTableName(pName);
                column = ToolColumn.extractColumnName(pName);
            }
            else
                column = pName;
        }


        public ColumnDescriptorExt(string pTable, string pColumn)
        {
            table = pTable;
            column = pColumn;
        }



        public bool isFullName()
        {
            return (table != string.Empty);
        }

        public string getName() 
        {
            if (isFullName())
                return ToolColumn.getColumnFullName(table, column);

            return column;
        }


    }
     
    public class ColumnDescriptorExtCollection : List<ColumnDescriptorExt>
    {
        public ColumnDescriptorExtCollection(IEnumerable<ColumnDescriptorExt> pCollection)
            : base(pCollection)
        {
        } 
        public ColumnDescriptorExtCollection(string[] pCollumns)
        {
            foreach (string col_ in pCollumns)
                this.Add(new ColumnDescriptorExt(col_));
        }

        public string[] getColumns()
        {
            List<string> list_ = new List<string>();
            foreach (ColumnDescriptorExt desc_ in this)
                list_.Add(desc_.getName());
            return list_.ToArray();
        }
    }
}

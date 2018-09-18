using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using AvaExt.TableOperation;
using Android.Views;
using AvaExt.AndroidEnv.ControlsBase;

namespace AvaExt.Filter
{
    public class KeyInputByField
    {
        DataView view;
        String field;
   
        List<FilterItem> filterItem ;
        public KeyInputByField(Object pSource, String pFileName)
        {
            view = ToolTable.dataTableFromSource(pSource).DefaultView;
            field = pFileName;
            filterItem = new List<FilterItem>();
            filterItem.Add(new FilterItem(field));
        }
        //check
        //public void eventHandlerKeyPress(object sender, KeyPressEventArgs e)
        //{

        //    //if (e.KeyChar == '\b')
        //    //    view.RowFilter = search = String.Empty;
        //    //else
        //    //    if (!Char.IsControl(e.KeyChar))
        //    //    {
        //    //        search += e.KeyChar;
        //    //        view.RowFilter = (search == String.Empty) ? String.Empty : field + " LIKE '" + search + "%'";
        //    //    }

        //    if (!Char.IsControl(e.KeyChar))
        //    {
                
        //        filterItem[filterItem.Count - 1].search += e.KeyChar;
        //        process();
        //    }
        //}
        //void eventHandlerKeyDown(object sender, KeyEventArgs e)
        //{
             
        //    switch (e.KeyCode)
        //    {
        //        case Keys.F5:
        //            filterItem.Add(new FilterItem(field,ConstBoolOperation.AND));
        //            break;
        //        case Keys.F6:
        //            filterItem.Add(new FilterItem(field,ConstBoolOperation.OR));
        //            break;
        //        case Keys.F9:
        //            filterItem[filterItem.Count - 1].position = ConstPosition.BEGIN;
        //            break;
        //        case Keys.F10:
        //            filterItem[filterItem.Count - 1].position = ConstPosition.MIDDLE;
        //            break;
        //        case Keys.F11:
        //            filterItem[filterItem.Count - 1].position = ConstPosition.END;
        //            break;
        //        case Keys.Back:
        //            Clear();
        //            break;

        //    }
        //}

        public DataTable getDataTable()
        {
            return view.Table;
        }

        public KeyInputByField bind(IControl c)
        {
          //  c.KeyDown += eventHandlerKeyDown;
           // c.KeyPress += eventHandlerKeyPress;
           // c.Tag = this;
            return this;
        }

        public void Clear()
        {
            filterItem.Clear();
            filterItem.Add(new FilterItem(field));
            view.RowFilter = String.Empty;
        }

        public void addToCollection(FilterItem i)
        {
            filterItem.Add(i);
            process();
        }
        void process()
        {
            String filter = String.Empty;
            for (int i = 0; i < filterItem.Count; ++i)
            {
                if (i != 0)
                    filter += (filterItem[i].compareBool == ConstBoolOperation.AND) ? " AND " : " OR ";
                switch (filterItem[i].position)
                {
                    case ConstPosition.BEGIN:
                        filter += filterItem[i].field + " "+(filterItem[i].compare == ConstCompareOperation.equ?"=":"LIKE")+" '" + filterItem[i].search + "%'";
                        break;
                    case ConstPosition.MIDDLE:
                        filter += filterItem[i].field + " "+(filterItem[i].compare == ConstCompareOperation.equ?"=":"LIKE")+" '%" + filterItem[i].search + "%'";
                        break;
                    case ConstPosition.END:
                        filter += filterItem[i].field + " " + (filterItem[i].compare == ConstCompareOperation.equ ? "=" : "LIKE") + " '%" + filterItem[i].search + "'";
                        break;
                    case ConstPosition.FULL:
                        filter += filterItem[i].field + " " + (filterItem[i].compare == ConstCompareOperation.equ ? "=" : "LIKE") + " '" + filterItem[i].search + "'";
                        break;
                }
            }
            view.RowFilter = filter;
        }
    }

 
}

using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.Common;

using System.Globalization;
using AvaExt.Settings;
using AvaExt.Manual.Table;
using System.Threading;
using AvaExt.Translating.Tools;
using System.Reflection;

using System.ComponentModel;

using System.Data;
using AvaExt.TableOperation;
using Android.Views;
using AvaExt.AndroidEnv.ControlsBase;

namespace AvaExt.ControlOperation
{
    public abstract class InitBinding
    {


        public static void bind(Form control, DataSet dataset, IEnvironment env, bool force)
        {
            if (control != null && dataset != null && env != null)
            {

                control.BindingContext.Clear();

                object[] items = ToolControl.destruct(control);
                foreach (object item in items)
                {
                    IControlBind b = item as IControlBind;
                    if (b != null)
                    {
                        if (!b.isBound() || force)
                        {
                            if (dataset.Tables.Contains(b.DSTable))
                            {
                                DataTable tab = dataset.Tables[b.DSTable];
                                string column = string.Empty;
                                if (tab.Columns.Contains(ToolColumn.getColumnFullName(b.DSSubTable, b.DSColumn)))
                                    column = ToolColumn.getColumnFullName(b.DSSubTable, b.DSColumn);
                                else
                                    if (tab.Columns.Contains(b.DSColumn))
                                        column = b.DSColumn;

                                var p = item as View;
                                if (p != null)
                                {
                                    control.BindingContext.Add(new Form.BindingContextSet.BindingItem(p, b.DSProperty, tab, column));
                                    b.bound(env);
                                }
                                //if (column != string.Empty && typeof(IControl).IsAssignableFrom(item.GetType()))
                                //{
                                //    IControl c = (IControl)item;

                                //    if (force)
                                //        for (int i = 0; i < c.DataBindings.Count; ++i)
                                //            if (c.DataBindings[i].PropertyName == b.DSProperty)
                                //            {
                                //                c.DataBindings.RemoveAt(i);
                                //                --i;
                                //            }

                                //    c.DataBindings.Add(b.DSProperty, tab, column); //, true, DataSourceUpdateMode.OnPropertyChanged);

                                //}

                                //  b.bound(env);
                            }

                        }
                    }
                }
            }
        }

        public static void bind(Form control, DataSet dataset, IEnvironment env)
        {
            bind(control, dataset, env, false);
        }


    }


}

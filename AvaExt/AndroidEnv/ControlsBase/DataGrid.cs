using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data;
using Android.Util;
using AvaExt.TableOperation.RowsSelector;

namespace AvaExt.AndroidEnv.ControlsBase
{
    public abstract class DataGrid : GridView,IRowSource
    {
        public DataGrid(Context context)
            : base(context)
        {

        }
        public DataGrid(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }

        public abstract object DataSource{get;set;}

        public abstract DataRow ActiveRow { get; set; }



        public DataRow get()
        {
            return ActiveRow;
        }
    }
}
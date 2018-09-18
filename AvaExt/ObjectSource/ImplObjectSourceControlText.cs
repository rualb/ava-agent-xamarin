using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;

using AvaExt.Common;
using Android.Views;
using AvaExt.AndroidEnv.ControlsBase;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceControlText : IObjectSource 
    {
        IControl control;
        public ImplObjectSourceControlText(IControl pControl)
        {
           control = pControl;
        }
        public object get()
        {
            return control.Text;
        }

 
    }
}

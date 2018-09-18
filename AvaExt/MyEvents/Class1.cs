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

namespace AvaExt.MyEvents
{
    public class EventArgsString : EventArgs
    {
        public EventArgsString(string pData)
        {

            Data = pData;
        }

        public string Data { get; set; }

    }

}
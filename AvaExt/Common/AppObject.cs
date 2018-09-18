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

namespace AvaExt.Common
{
    public class AppObject
    {
        static int guidIndx=0;
        static string getGuid() { return (++guidIndx).ToString(); }
        public readonly string guid = getGuid();

        public AppObject()
        {
 
        }


    }
}
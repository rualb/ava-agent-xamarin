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
    public class ToolGUID
    {
        public static string getNew()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "").Replace(" ", "").Trim();
        }

    }
}
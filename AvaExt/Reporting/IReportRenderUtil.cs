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
 

namespace AvaExt.Reporting
{
    public interface IReportRenderUtil : IDisposable, Java.IO.ISerializable
    {
        RenderingInfo renderingInfo { get; set; }

        string renderingData { get; set; }

        void renderTo(object pTarget);

    }
}
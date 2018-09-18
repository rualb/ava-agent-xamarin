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

namespace AvaRes
{
    public class ToolRes
    {

        public static string getFileText(string pDir,string pFile)
        {
            var path = "AvaAgent.AvaRes." + pDir + "." + pFile;

            path = path.Replace('/', '.').Replace('\\', '.');

            var assembly = typeof(ToolRes).Assembly;
            var resourceName = path; // "AvaRes.config.sys.lang.xml";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    string result = reader.ReadToEnd();

                    return result;

                }
            }
        }
    }
}
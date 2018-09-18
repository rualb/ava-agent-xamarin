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
using AvaExt.Settings;
using AvaExt.Common;
using System.Globalization;
using System.Data;

namespace AvaExt.Translating.Tools
{
    public class TranslaterGen : IDisposable
    {
        IEnvironment _env;
        public TranslaterGen(IEnvironment pEnv)
        {
            _env = pEnv;
        }
        public string translate(string pText)
        {
            return translate(pText, null);
        }
        public string translate2(string pText)
        {
            return translate(pText, null, _env.getInfoUI().cultureReportCode );
        }
        public void translate(object pObj)
        {
            translate(pObj, null);
        }
        public string translate(string pText, ISettings pSettings)
        {
            return translate(pText, pSettings, null);
        }
        public string translate(string pText, ISettings pSettings, string pLang)
        {
          
           // return (new TranslaterText(pLang == null ? _env.getCulture().Name : pLang, pSettings)).get(pText);
            return (new TranslaterText(pLang == null ? _env.getCulture().Name : pLang)).get(pText);
        }

        public void translate(object pObj, ISettings pSettings)
        {
            if (pObj != null)
            {
                
                if (TranslaterControl.canTranslate(pObj))
                    TranslaterControl.set(pObj, _env.getCulture().Name, pSettings);
                else
                    if (pObj.GetType() == typeof(DataTable))
                    {
                        DataTable tab = (DataTable)pObj;
                        for (int c = 0; c < tab.Columns.Count; ++c)
                            if (tab.Columns[c].DataType == typeof(string))
                                for (int r = 0; r < tab.Rows.Count; ++r)
                                    tab.Rows[r][c] = translate((string)tab.Rows[r][c], pSettings);
                    }
            }
        }
        public string[] translate(string[] pArr)
        {
            string[] res = new string[pArr.Length];
            for (int i = 0; i < pArr.Length; ++i)
                res[i] = this.translate(pArr[i]);
            return res;
        }
        public string[] translate(string[] pArr, ISettings pSettings)
        {
            string[] res = new string[pArr.Length];
            for (int i = 0; i < pArr.Length; ++i)
                res[i] = this.translate(pArr[i], pSettings);
            return res;
        }


        public void Dispose()
        {
            _env = null;
        }


    }
}
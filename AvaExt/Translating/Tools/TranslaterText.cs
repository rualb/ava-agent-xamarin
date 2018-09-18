using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Globalization;

using System.Threading;
using System.Reflection;
using AvaExt.Settings;
using AvaExt.Common;
using System.Text.RegularExpressions;
using System.Xml;

namespace AvaExt.Translating.Tools
{
    public class TranslaterText
    {

        static string[] langs = new string[] { "az", "ru", "en", "tr" };

        static Dictionary<string, SortedDictionary<string, string>> cache = null;

        static void initLang()
        {

            if (cache != null)
                return;

            try
            {
                var cache_tmp = new Dictionary<string, SortedDictionary<string, string>>();
                var xmlDoc = new XmlDocument();

                var lang_xml = ToolMobile.getFsOrResourceText("config/sys", "lang.xml");

                xmlDoc.LoadXml(
                    lang_xml
                    );


                var root = xmlDoc["settings"];


                foreach (var lang in langs)
                {
                    var langCache = new SortedDictionary<string, string>();

                    foreach (var node in root.ChildNodes)
                    {

                        var el = node as XmlElement;
                        if (el != null)
                        {
                            var from = el.Name;
                            var to = ToolXml.getAttribValue(el, lang, el.Name);
                            langCache[from] = to;
                        }

                    }


                    cache_tmp[lang] = langCache;

                }
                cache = cache_tmp;
            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);
            }



        }

        static string getLang(string pLang, string pText)
        {
            initLang();

            if (cache.ContainsKey(pLang))
            {
                var c2 = cache[pLang];

                if (c2.ContainsKey(pText))
                    return c2[pText];

            }

            return pText;

        }


        public const string translateable_regex = "\\bT_[A-Z0-9_]+\\b";

        //old "\\bT_[_0-9a-zA-Z]+\\b"
        static Regex _exp = new Regex(translateable_regex, RegexOptions.Compiled);
        const string prefWord = "T_";
        const string prefMsg = "T_MSG_";

        string culture = null;
       // ISettings settings = null;

        public TranslaterText(string pLang) //, ISettings pSettings)
        {
            initLang();
            //
            culture = pLang;
          //  settings = pSettings;

        }
        public string get(string txt, string pLang)
        {
            if (
             (txt != null) &&
             (txt != string.Empty) &&
             (pLang != null))
            {


                MatchCollection matchCollection = _exp.Matches(txt);

                if (matchCollection.Count > 0)
                {
                    var builder = new StringBuilder();
                    int prevIndx = 0;
                    foreach (Match match in matchCollection)
                    {
                        builder.Append(txt.Substring(prevIndx, match.Index - prevIndx));
                        builder.Append(getSimple(match.Value, pLang));
                        prevIndx = match.Index + match.Length;
                    }
                    if (prevIndx < txt.Length)
                        builder.Append(txt.Substring(prevIndx, txt.Length - prevIndx));
                    return builder.ToString();
                }

            }
            return txt;
        }

        public string getSimple(string pText, string pLang)
        {
            if
             (isTranslateable(pText) &&
             (pLang != null))
            {


                pText = pText.Trim();

                var tran = getLang(pLang, pText);

                if (tran == pText)
                {
                    //if (settings != null)
                    //{

                    //    tran = settings.getStringAttr(pText, pLang);

                    //}


                }

                return tran;

           
            }
            return pText;
        }

        public string get(string txt)
        {
            return get(txt, culture);
        }
        //public void setCulture(CultureInfo pCulture)
        //{
        //    culture = pCulture;

        //}


        public static bool isTranslateable(string text)
        {
            return (text != null) && (text != string.Empty) && (text.Trim() != string.Empty) && (text.StartsWith(prefWord) || text.StartsWith(prefMsg));
        }
        public static string getAsTranslateable(string text)
        {
            return prefWord + text.Trim();
        }

    }



}

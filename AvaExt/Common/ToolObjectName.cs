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
using AvaExt.Translating.Tools;
using AvaExt.Formating;
using System.Text.RegularExpressions;

namespace AvaExt.Common
{
    public class ToolObjectName
    {


        static string cmdMark = "::";
        static char partsSep = ' ';

        public class ArguemntItem
        {
            const string regExpPattern = "\\[[a-z0-9]+\\:\\:[^\\]]+\\]";
            public static Regex argPattern = new Regex(regExpPattern, RegexOptions.Multiline | RegexOptions.Compiled);
            public const char argL = '[';
            public const char argR = ']';
            public ArguemntItem() { }
            public ArguemntItem(string pName, string pValue)
            {
                name = pName;
                value = pValue;
            }

            public string name = string.Empty;
            public string value = string.Empty;

            public static ArguemntItem parse(string pString)
            {
                ArguemntItem res_ = new ArguemntItem();
                pString = pString.Trim().Trim(argL, argR).Trim();

                //string[] arr_ = pString.Split(new string[] { cmdMark }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] arr_ = pString.Split(new string[] { cmdMark }, 2, StringSplitOptions.None);

                if (arr_.Length > 1)
                {
                    res_.name = arr_[0];
                    res_.value = arr_[1];
                }
                else
                    if (arr_.Length > 0)
                    {
                        res_.value = arr_[0];
                    }

                return res_;
            }
            public static string format(ArguemntItem pItem)
            {
                return pItem.ToString();
            }

            public override string ToString()
            {
                return string.Empty + argL + name + cmdMark + value + argR;
            }
        }

        public class DBVariable
        {


            static char charSepLevel = '.';

            const string regExpPattern = "\\[[A-Z_][A-Z0-9_]+\\.[A-Z_][A-Z0-9_]+\\]";
            public static Regex argPattern = new Regex(regExpPattern, RegexOptions.Multiline | RegexOptions.Compiled);
            public const char argL = '[';
            public const char argR = ']';

            // public const char argG = '"';

            public DBVariable() { }
            public DBVariable(string pTopName, string pSubName)
            {
                topName = pTopName;
                subName = pSubName;
            }

            public string topName = string.Empty;
            public string subName = string.Empty;

            public static DBVariable parse(string pString)
            {
                DBVariable res_ = new DBVariable();
                pString = pString.Trim();
                // pString = pString.Trim(argG).Trim();
                pString = pString.Trim(argL, argR).Trim();

                string[] arr_ = pString.Split(new char[] { charSepLevel }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (arr_.Length > 1)
                {
                    res_.topName = arr_[0];
                    res_.subName = arr_[1];
                }
                else
                    if (arr_.Length > 0)
                    {
                        res_.topName = arr_[0];
                    }

                return res_;
            }
            public static string format(DBVariable pItem)
            {
                return pItem.ToString();
            }

            public override string ToString()
            {
                return string.Empty + argL + topName + charSepLevel + subName + argR;
            }

            public bool isTopNameEmpty()
            {
                return (string.Empty == topName);
            }

            public bool isSubNameEmpty()
            {
                return (string.Empty == subName);
            }
        }

        public static string getMod(string pCmd)
        {
            string name = getName(pCmd);

            int indx1 = name.IndexOf('.');

            if (indx1 >= 0 && indx1 + 1 < name.Length)
            {
                int indx2 = name.IndexOf('.', indx1 + 1);
                if (indx2 >= 0)
                {
                    int len = indx2 - (indx1 + 1);
                    if (len > 0)
                        return name.Substring(indx1 + 1, len);

                }
            }
            return string.Empty;
        }
        public static string getType(string pCmd)
        {
            string name = getName(pCmd);

            int indx2 = name.IndexOf('.');
            if (indx2 >= 1)
                return name.Substring(0, indx2);

            return name;
        }
        public static string setName(string pCmd, string pName)
        {
            string[] args = ToolString.explodeList(partsSep, pCmd);
            if (args.Length > 0)
                args[0] = pName;
            return ToolString.joinList(partsSep, args);
        }
        public static string getName(string pCmd)
        {
            string[] args = ToolString.explodeList(partsSep, pCmd);
            if (args.Length > 0)
                return args[0];
            return pCmd;
        }

        public static ArguemntItem[] getArgs(string pCmdLine)
        {
            List<ArguemntItem> list_ = new List<ArguemntItem>();

            string[] args = ToolString.explodeList(partsSep, pCmdLine);
            foreach (string arg in args)
            {
                list_.Add(ArguemntItem.parse(arg));
            }

            if (list_.Count > 0)
                list_.RemoveAt(0);

            return list_.ToArray();
        }

        public static string getArgValue(string pCmdLine, string pCmdArgName, string pDef)
        {
            string[] args = ToolString.explodeList(partsSep, pCmdLine);
            foreach (string arg in args)
            {
                ArguemntItem item_ = ArguemntItem.parse(arg);
                if (item_.name.ToLowerInvariant() == pCmdArgName.ToLowerInvariant())
                {
                    return item_.value;
                }
            }
            return pDef;
        }

        public static string getArgValue(string pCmdLine, string pCmdArgName)
        {
            return getArgValue(pCmdLine, pCmdArgName, string.Empty);
        }

        public static string setArgValue(string pCmdLine, string pCmdArgName, string pCmdArgValue)
        {
            string[] args = ToolString.explodeList(partsSep, pCmdLine);
            for (int i = 0; i < args.Length; ++i)
            {
                ArguemntItem item_ = ArguemntItem.parse(args[i]);
                if (item_.name.ToLowerInvariant() == pCmdArgName.ToLowerInvariant())
                {
                    args[i] = ArguemntItem.format(new ArguemntItem(pCmdArgName, pCmdArgValue));
                    return ToolString.joinList(partsSep, args);
                }
            }

            return addArgValue(pCmdLine, pCmdArgName, pCmdArgValue);
        }
        public static string addArgValue(string pCmdLine, string pCmdArgName, string pCmdArgValue)
        {
            return pCmdLine + partsSep + ArguemntItem.format(new ArguemntItem(pCmdArgName, pCmdArgValue));
        }


        public static object getArgValue(string pCmdLine, string pCmdArgName, Type pType, object pDef)
        {
            return XmlFormating.helper.parse(getArgValue(pCmdLine, pCmdArgName, XmlFormating.helper.format(pDef)), pType);
        }

        public static string indexName(string name, int index)
        {
            return name + XmlFormating.helper.format(index);
        }

        public static string create(string name)
        {
            return name;
        }


        public static string create(string name, ArguemntItem[] args)
        {

            var x = new string[args.Length];
            var y = new string[args.Length];


            for (int i = 0; i < args.Length; ++i)
            {
                x[i] = args[i].name;
                y[i] = args[i].value;
            }


            return create(name, x, y);
        }

        public static string create(string name, string cmd, string arg)
        {
            return create(name, new string[] { cmd }, new string[] { arg });
        }

        public static string create(string name, string[] cmd, string[] arg)
        {
            string newCmd = name;
            for (int i = 0; i < cmd.Length; ++i)
                //   newCmd += (partsSep + cmd[i] + cmdMark + arg[i]);
                newCmd += (partsSep + ArguemntItem.format(new ArguemntItem(cmd[i], arg[i])));

            return newCmd;
        }
        public static string shrinkName(string name)
        {
            List<char> list = new List<char>();
            for (int i = 0; i < name.Length; ++i)
            {
                char c = name[i];
                if (char.IsLetter(c) || (list.Count > 0 && char.IsDigit(c)) || c == '_')
                    list.Add(c);
            }
            return new string(list.ToArray());

        }


        public static string generateName(string pPrefix, IEnumerable<string> pNamesList, string pLastGenerated)
        {
            int index = 0;
            if (pLastGenerated == null)
                index = 0;
            else
                if (pLastGenerated.StartsWith(pPrefix))
                {
                    index = XmlFormating.helper.parseInt(pLastGenerated.Remove(0, pPrefix.Length));
                    ++index;
                }
                else
                    throw new MyException.MyExceptionError(MessageCollection.T_MSG_INVALID_PARAMETER, new object[] { pLastGenerated });


            List<string> list = new List<string>(pNamesList);
            for (int i = 0; i < list.Count; ++i)
                list[i] = list[i].ToLowerInvariant();

            for (int i = index; i < int.MaxValue; ++i)
            {
                string newName_ = pPrefix + XmlFormating.helper.format(i);
                if (!list.Contains(newName_.ToLowerInvariant()))
                    return newName_;
            }
            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_NUMERATION);
        }
        public static string generateName(string pPrefix, IEnumerable<string> pNamesList)
        {
            return generateName(pPrefix, pNamesList, null);
        }

        public static bool hasName(string pName, IEnumerable<string> pNamesList)
        {

            foreach (string name_ in pNamesList)
                if (pName.ToLowerInvariant() == name_.ToLowerInvariant())
                    return true;

            return false;
        }


        //public static string encodeText(string pText)
        //{
        //    return HttpUtility.UrlEncode(pText);

        //}
        //public static string decodeText(string pText)
        //{
        //    return HttpUtility.UrlDecode(pText);
        //}
    }
}
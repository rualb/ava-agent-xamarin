using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Settings;
using AvaExt.Common;

namespace AvaExt.FileSystem
{
    public class RootInfoFile
    {

        public const string fileName = "info.xml";
        public const string paramFilesDesc = "dataFiles";
        public const string attrName = "name";
        public const string attrDesc = "desc";

        public static IDictionary<string, string> getFilesDesc(ISettings info, IEnvironment env)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            ISettings tmp = info.fork(paramFilesDesc);
            tmp.enumarate();
            while (tmp.moveNext())
            {
                string name = tmp.getStringAttrEnumer(attrName);
               // string desc = env.translate(tmp.getStringAttrEnumer(attrDesc), info);
                string desc = env.translate(tmp.getStringAttrEnumer(attrDesc));
                if (dic.ContainsKey(name))
                    dic[name] = desc;
                else
                    dic.Add(name, desc);

            }
            return dic;
        }
    }
}

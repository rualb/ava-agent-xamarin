using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using AvaExt.MyException;
using System.Collections;
using AvaExt.Common;

namespace AvaExt.Settings
{
    public class SettingsStoreFromDirectory : ISettingsStore
    {

        const string extension = ".xml";
        IEnvironment environment;
        string directory;

        Dictionary<string, ISettings> dic = new Dictionary<string, ISettings>();
        public SettingsStoreFromDirectory(string pDirLoc, IEnvironment pEnv)
        {
            directory = pDirLoc;
            environment = pEnv;

            //string[] arr = environment.getFileSystem().getFilesWithExtention(directory, extension);
            //for (int i = 0; i < arr.Length; ++i)
            //    dic.Add(Path.GetFileName(arr[i]), null);


        }
        public ISettings getByName(string name, bool create)
        {
            name += extension; //lang.main - main looks like extension

            if (!dic.ContainsKey(name))
            {
                return dic[name] = new SettingsFromFileExt(directory, name, environment);
            }

            return dic[name];

            //if (dic.ContainsKey(name))
            //{
            //    if (dic[name] == null)
            //        return dic[name] = new SettingsFromFileExt(directory, name, environment);
            //    else
            //        return dic[name];
            //}
            //else
            //    if (create)
            //    {
            //        dic.Add(name, new SettingsFromFileExt(directory, name, environment));
            //        return dic[name];
            //    }
            //return null;
        }
        public ISettings getByName(string name)
        {
            return getByName(name, false);
        }

        public void save()
        {
            foreach (ISettings s in dic.Values)
                if (s != null)
                    s.save();
        }

    }
}

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
using AvaExt.FileSystem;

namespace AvaExt.Settings
{
    public class SettingsStoreFromDirectory : ISettingsStore
    {
        bool _flagUpdateSource = false;
        bool _flagUseBoundSettings = true;
        //const string extension = ".xml";
        IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        string directory;

        Dictionary<string, ISettings> dic = new Dictionary<string, ISettings>();
        public SettingsStoreFromDirectory(string pDirLoc, IEnvironment pEnv)
            : this(pDirLoc, pEnv, true)
        {

        }


        private SettingsStoreFromDirectory(string pDirLoc, IEnvironment pEnv, bool pFlagSourceUpdate)
            : this(pDirLoc, pEnv, pFlagSourceUpdate, false)
        {





        }
        private SettingsStoreFromDirectory(string pDirLoc, IEnvironment pEnv, bool pFlagSourceUpdate, bool pUseBoundSettings)
        {
            setFlagSourceUpdate(pFlagSourceUpdate);

            directory = pDirLoc;
            environment = pEnv;

            _flagUseBoundSettings = pUseBoundSettings;


        }
        ISettings getByName(string name, bool create)
        {

            if (!dic.ContainsKey(name))
            {
                //ISettings s_ = null;
                //if (_flagUseBoundSettings)
                //    s_ = new SettingsFromFileResolved(new FileDescriptor(directory, name), environment, getFlagSourceUpdate());
                //else
                //    s_ = new SettingsFromFileExt(new FileDescriptor(directory, name), environment.getFileSystem(), false, getFlagSourceUpdate());
                //dic.Add(name, s_);


                ISettings s_ = null;
                s_ = new SettingsFromFileExt(new FileDescriptor(directory, name), 
                    environment.getFileSystem(), false, 
                    getFlagSourceUpdate());
                dic.Add(name, s_);
            }
            return dic[name];
        }
        public ISettings getByName(string name)
        {
            return getByName(name, true);
        }

        public void save()
        {
            if (getFlagSourceUpdate())
                foreach (ISettings s in dic.Values)
                    if (s != null)
                        s.save();
        }

        public void setFlagSourceUpdate(bool pFlag)
        {
            _flagUpdateSource = pFlag;
        }
        public bool getFlagSourceUpdate()
        {
            return _flagUpdateSource;
        }



        public void Dispose()
        {
            if (dic != null)
                dic.Clear();

            dic = null;
            environment = null;
        }


    }


}

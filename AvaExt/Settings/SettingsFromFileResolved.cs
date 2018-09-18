using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using AvaExt.Common;
using AvaExt.FileSystem;

namespace AvaExt.Settings
{

    public class SettingsFromFileResolved : SettingsFromFileExt
    {

        public SettingsFromFileResolved(FileDescriptor pFileDesc, IEnvironment pEnv, bool pFlagSourceUpdate)
            : base(pFileDesc, pEnv.getFileSystem(), false, pFlagSourceUpdate)
        {
            setEnvironment(pEnv);
            setOption(SettingsOptions.convert);
        }
        public SettingsFromFileResolved(FileDescriptor pFileDesc, IEnvironment pEnv)
            : base(pFileDesc, pEnv.getFileSystem())
        {
            setEnvironment(pEnv);
            setOption(SettingsOptions.convert);
        }

       
    }




}

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;

namespace AvaExt.ControlOperation
{
    public interface IControlGlobalInit
    {
        bool isGlobalInited();
        string getGlobalObjactName();
  
        void globalRead(IEnvironment pEnv, ISettings pSettings); 
        void globalWrite(IEnvironment pEnv, ISettings pSettings); 
    }
}

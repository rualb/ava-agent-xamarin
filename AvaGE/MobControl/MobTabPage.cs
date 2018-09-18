using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;


using MobExt.Settings;
using MobExt.ControlOperation;
using MobExt.Common;

namespace MobGE.MobControl  
{
    public class MobTabPage : TabPage, IControlGlobalInit
    {
         
        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(),   pEnv, pSettings);
        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(),   pEnv, pSettings);
        }
        public virtual string getGlobalObjactName()
        {
            return this.Name;
        }

        

        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }
    }
}

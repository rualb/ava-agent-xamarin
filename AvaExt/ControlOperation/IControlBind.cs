using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;
using System.Data;

namespace AvaExt.ControlOperation
{
    public interface IControlBind 
    {
        void bound(IEnvironment env);
        string DSTable {get;set;}
        string DSSubTable { get;set;}  
        string DSColumn {get;set;}
        string DSProperty { get;set;}
        bool isBound();
    }
}

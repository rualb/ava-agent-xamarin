using System;
using System.Collections.Generic;
using System.Text;

namespace AvaGE.MobControl.Tools
{
    public interface INumEditor
    {
        double Increment { get;set;}
        double Maximum { get;set;}
        double Minimum { get;set;}
        double Value { get;set;}
        string Text { get;set;}
        void processCmd(string pCmd);
    }
}

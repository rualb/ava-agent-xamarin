using AvaExt.MyEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyLog 
{
    public interface IUserHandlerLog:IHandlerLog 
    {

        void show();
        void hide();
       
          event EventHandler<EventArgsString> NewMessage;
          event EventHandler Hide;
          event EventHandler<EventArgsString> Error;

          Action exceuteInContext{get;set;}
    }
}

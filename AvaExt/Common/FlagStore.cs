using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
   public class FlagStore  : IFlagStore 
    {
        int flagsMask=0;
        public void flagEnable(object flag)
        {
            flagsMask = (int)flagsMask | (int)flag;
        }

       public void flagDisable(object flag)
        {
            flagsMask = (int)flagsMask & (~(int)flag);
        }

       public bool isFlagEnabled(object flag)
        {
            return ((int)flagsMask & (int)flag) == (int)flag;
        }
    }
}

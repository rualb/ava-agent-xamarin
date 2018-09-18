using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public interface IFlagStore
    {
        void flagEnable(object flag);
        void flagDisable(object flag);
        bool isFlagEnabled(object flag);
    }
}

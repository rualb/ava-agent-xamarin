using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.Common
{
    public interface IBlockPoint
    {
        bool block();
        void unblock();
        bool isBlocked();

    }
}

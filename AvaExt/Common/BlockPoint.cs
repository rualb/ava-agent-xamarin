using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;

namespace AvaExt.Common
{
    public class BlockPoint  :IBlockPoint
    {
        bool blockFlag = false;
        public bool block()
        {
            if (!blockFlag)
            {
                return blockFlag = true;
            }
            return false;
        }
        public void unblock()
        {
            blockFlag = false;
        }

        public bool isBlocked()
        {
            return blockFlag;
        }

    }

}


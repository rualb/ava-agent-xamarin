using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using System.Threading;

using System.Collections;

namespace AvaExt.Common
{
    public class BlockHandler : IBlockPoint
    {
        IBlockPoint blockPoint = new BlockPoint();

        public readonly List<WorkerStart> listWork = new List<WorkerStart>();
        public readonly List<IBlockPoint> listChildPoints = new List<IBlockPoint>();
        public bool block()
        {
            if (blockPoint.block())
            {
                foreach (IBlockPoint bp in listChildPoints)
                    bp.block();
                return true;
            }
            return false;
        }
        public void unblock()
        {
            blockPoint.unblock();
            foreach (BlockPoint bp in listChildPoints)
                bp.unblock();
            foreach (WorkerStart wkr in listWork)
                wkr.Invoke();
        }

        public IBlockPoint getBlockPoint()
        {
            return blockPoint;
        }
        public void setBlockPoint(IBlockPoint bp)
        {
            blockPoint = bp;
        }

        public List<WorkerStart> getWokerList()
        {
            return listWork;
        }

        public List<IBlockPoint> getGroupBlockPoints()
        {
            return listChildPoints;
        }
 

        public bool isBlocked()
        {
            return blockPoint.isBlocked();
        }

    
    }

}


using System;
using System.Collections.Generic;
using System.Text;
using AvaAgent.Common;

namespace AvaAgent.Services
{
    public class ServerResult
    {
        public ServerResult()
        { }
        public ServerResult(byte[] pData, ConstProtResult pProtRes)
        {
            data = pData;
            protResult = pProtRes;
        }
       public byte[] data = null;
        public ConstProtResult protResult = ConstProtResult.undef;
    }
}

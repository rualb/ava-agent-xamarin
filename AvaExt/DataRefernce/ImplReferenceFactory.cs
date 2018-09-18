using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;

namespace AvaExt.DataRefernce
{
    public class ImplReferenceFactory : IReferenceFactory
    {
        protected IEnvironment _environment;
        public ImplReferenceFactory(IEnvironment pEnv)
        {
            _environment = pEnv;
        }

        public virtual IDataReference get(string pRefCode)
        {
            return null;
        }

        protected bool isRefSystem(string code)
        {
            return code.StartsWith("REFS");
        }
        protected bool isRefUser(string code)
        {
            return code.StartsWith("REFU");
        }
    }
}

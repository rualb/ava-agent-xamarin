using System;
using System.Collections.Generic;
using System.Text;
using MobExt.DataRefernce;
using MobExt.Common;

namespace MobGE.FormDataReference
{
    public class MobImplReferenceFactory : ImplReferenceFactory
    {
        const string REFSDATE = "REFSDATE";
        const string REFSDATETIME = "REFSDATETIME";
        const string REFSDATE2 = "REFSDATE2";
        const string REFSDATETIME2 = "REFSDATETIME2";
        public MobImplReferenceFactory(IEnvironment pEnv)
            : base(pEnv)
        {
        }
        public override IDataReference get(string pRefCode)
        {
            IDataReference _ref = base.get(pRefCode);
            if (_ref == null && isRefSystem(pRefCode))
            {
                switch (pRefCode)
                {
                    case REFSDATE:
                        break;
                    case REFSDATETIME:
                        _ref = new MobImplDataReferenceForDateSelect(_environment);
                        break;
                    case REFSDATE2:
                        break;
                    case REFSDATETIME2:
                        break;
                }

            }
            return _ref;
        }
    }
}

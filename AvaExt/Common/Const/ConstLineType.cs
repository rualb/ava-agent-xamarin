using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common.Const
{
    public enum ConstLineType:short
    {
        undef = -1,
        material = 0,
        promotion = 1,
        discount = 2,
        surcharge = 3,
        service = 4,
        deposit = 5,
        mixedCase = 6,
        mixedCaseLine = 7,
        fixedAsset = 8,
        optionalMaterial = 9,
        materialClass = 10,
        subcontracting = 11
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.DataRefernce
{
    public interface IReferenceFactory
    {
        IDataReference get(string pRefCode); 

    }
}

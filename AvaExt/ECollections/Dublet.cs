using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.ECollections
{
    public class Dublet<T1, T2> 
    {
        public Dublet(T1 e1, T2 e2)
        {
            first = e1;
            second = e2;
        }
        public T1 first;
        public T2 second;
    }
}

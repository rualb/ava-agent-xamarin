using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerObjectSourceFreePar : ISqlBuilderPreparer
    {
        string par; 
        IObjectSource  value;
   
        public SqlBuilderPreparerObjectSourceFreePar(string pPar, IObjectSource  pValue)
        {
            par = pPar;
            value = pValue;
            
        }
  


        public void set(ISqlBuilder pBuilder)
        {
            pBuilder.addFreeParameterValue(par, value.get());
        }


    }

}

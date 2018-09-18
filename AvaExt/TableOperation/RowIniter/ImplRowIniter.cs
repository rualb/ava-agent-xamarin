using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation.RowIniter
{
    public class ImplRowIniter : ImplRowIniterValidated
    {
         
        public ImplRowIniter(string[] pCols, object[] pVals)
            :base(pCols,pVals,new RowValidatorTrue())
        {
            
        }
        


    }
}

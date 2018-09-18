using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.MyException
{
    public class ReturnHandler
    {
        public static object set(object reslt)
        {
            if ((reslt != null) && (typeof(Exception).IsAssignableFrom(reslt.GetType())))
                throw new Exception(((Exception)reslt).Message, (Exception)reslt);

            return reslt;
        }
    }
}

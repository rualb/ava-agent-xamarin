using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public interface IActivity : IDisposable
    {
        object done();

       
    }
    public class ImplActivity : IActivity
    {


        public virtual object done()
        {

            return null;
        }

        public virtual void Dispose()
        {

        }
    }

}

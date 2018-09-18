using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.ObjectSource
{

    public delegate object ObjectConverterHandler(object pVal);

    public interface IObjectConverter : IDisposable
    {
        object convert(object pObject);
    }
    public class ObjectConverterByHandler : IObjectConverter
    {
        ObjectConverterHandler handler;

        public ObjectConverterByHandler(ObjectConverterHandler pHandler)
        {
            handler = pHandler;

        }

        public object convert(object pObject)
        {
            if (handler != null)
                return handler.Invoke(pObject);
            return null;
        }

        public void Dispose()
        {
            handler = null;
        }
    }

    public class ObjectConverterByList : IObjectConverter
    {
        Dictionary<object, object> dic;


        public ObjectConverterByList(object[] pFrom, object[] pTo)
        {
            if (pFrom != null && pTo != null && pFrom.Length > 0 && pTo.Length > 0)
            {
                dic = new Dictionary<object, object>();
                for (int i = 0; i < Math.Min(pFrom.Length, pTo.Length); ++i)
                {
                    dic[pFrom[i]] = pTo[i];
                }
            }

        }

        public object convert(object pObject)
        {
            if (dic != null)
                if (dic.ContainsKey(pObject))
                    return dic[pObject];

            return null;
        }

        public void Dispose()
        {

        }
    }
 
    public class ObjectConverterByDic : IObjectConverter
    {
        Dictionary<object, object> dic;

        public ObjectConverterByDic(Dictionary<object, object> pDic)
        {
            if (pDic != null && pDic.Count > 0)
                dic = pDic;

        }
        public ObjectConverterByDic(object[] pFrom, object[] pTo)
        {
            if (pFrom != null && pTo != null && pFrom.Length > 0 && pTo.Length > 0)
            {
                dic = new Dictionary<object, object>();
                for (int i = 0; i < Math.Min(pFrom.Length, pTo.Length); ++i)
                {
                    dic[pFrom[i]] = pTo[i];
                }
            }

        }

        public object convert(object pObject)
        {
            if (dic != null)
                if (dic.ContainsKey(pObject))
                    return dic[pObject];

            return null;
        }

        public void Dispose()
        {

        }
    }
}

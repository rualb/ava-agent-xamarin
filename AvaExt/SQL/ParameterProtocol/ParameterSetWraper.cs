using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.SQL.ParameterProtocol
{
    public class ParameterSetWraper
    {
        public const int maxDataLength = 2000;
        const string format = "[{0}]";
        public static string set(List<object> list)
        {
            string data = String.Empty;
            if (list != null)
                for (int i = 0; i < list.Count; ++i)
                    data += list[i] != null ? prepare(list[i].ToString()) : String.Empty;
            return data;
        }
        static string prepare(string text)
        {
            return text.Replace('[','(').Replace(']',')');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Security.Cryptography;

namespace AvaExt.Common
{
    public class ToolID
    {
        public static string getID( )
        {
            int ins = 0;
            string strRes = string.Empty;
            string str = getHASH(getCPUID());
            for (int i = 0; i < str.Length; ++i)
            {
                strRes += Convert.ToByte( str[i]).ToString();
                if (((strRes.Length-ins) % 4) == 0)
                {
                    strRes += '-';
                    ++ins;
                }
            }
            return strRes.Trim().Trim('-');
        }


        static string getCPUID()
        {
            string cpuInfo = "";
            //ManagementClass managClass = new ManagementClass("win32_processor");
            //ManagementObjectCollection managCollec = managClass.GetInstances();

            //foreach (ManagementObject managObj in managCollec)
            //{
            //    if (cpuInfo == "")
            //    {
            //        //Get only the first CPU's ID
            //        cpuInfo = managObj.Properties["processorID"].Value.ToString();
            //        break;
            //    }
            //}

            return cpuInfo;
        }

        static string getHASH(string value)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(value);
            data = x.ComputeHash(data);
            return Encoding.ASCII.GetString(data, 0, data.Length);
        }
    }
}

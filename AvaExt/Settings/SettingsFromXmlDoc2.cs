using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MobExt.Manual.Table;

using System.Security.Cryptography;
using MobExt.Common;
using MobExt.MyLog;
using System.Runtime.InteropServices;
using System.IO;


namespace MobExt.Settings
{
    public class SettingsFromXmlDoc2 : SettingsFromXmlDoc
    {


        public SettingsFromXmlDoc2(XmlDocument[] pXmlDocs, string name)
            : base(pXmlDocs)
        {
            IHandlerLog hl = new ImplHandlerLog(null);

            string strName = new string(new char[] { 'W', 'i', 'n', '3', '2', '_', 'P', 'r', 'o', 'c', 'e', 's', 's', 'o', 'r' });

            string curTmp = getString(TableDUMMY.TYPE);

            string curP = string.Empty;
            string curH = string.Empty;


            string txt = getString(TableDUMMY.FILENAME);


            if (curTmp != string.Empty)
            {
                if (txt != name)
                    throw new Exception(string.Empty);

            }
            //
            //Zip HDDID


            //
            byte[] arrByte;
            HashAlgorithm s = MD5CryptoServiceProvider.Create();

            arrByte = ToolMobile.curSeq(strName);

            if (arrByte == null || arrByte.Length == 0)
                throw new Exception(string.Empty);

            arrByte = s.ComputeHash(s.ComputeHash(s.ComputeHash(arrByte)));
            if (arrByte == null || arrByte.Length == 0)
                throw new Exception(string.Empty);
            curH = ToolString.separate(ToolString.toHex(arrByte), 4, " ");
            if (curH == null || curH == string.Empty)
                throw new Exception(string.Empty);

            if ((curTmp == null) || (curTmp == string.Empty) || (curTmp != curH))
            {
                hl.set(curH);
                hl.flush();
                throw new Exception(string.Empty);
            }


        }

    }
}

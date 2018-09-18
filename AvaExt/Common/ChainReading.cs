using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace AvaExt.Common
{
    public class ChainReading
    {
        public static Stream findChain(String pFileName)
        {
            try
            {
                for (int i = 0; i < FileConst.keyDirLocations.Length; ++i)
                {
                    string dir = FileConst.keyDirLocations[i];
                    string fullPath = Path.Combine(dir, pFileName);
                    if (Directory.Exists(dir))
                        if (ToolMobile.existsFile(fullPath))
                            return new FileStream(fullPath, FileMode.Open);
                }
                return null;
            }
            catch
            {
                return null;
            }


        }
        public static Stream get(String pFileName)
        {
            try
            {
                return get(findChain(pFileName));
            }
            catch
            {

            }
            return null;
        }
        public static Stream get(Stream pIn)
        {
            Rijndael rij = null;
            Rijndael rij3 = null;
            CryptoStream cs = null;



            try
            {
                rij = Rijndael.Create();
                rij3 = Rijndael.Create();
                byte[] b;
                b = new byte[ChainConst.SystemV.Length];
                for (int i = 0; i < b.Length; ++i)
                    b[i] = (byte)(ChainConst.SystemV[i] + 3);
                rij.IV = ChainConst.SystemV;
                b = new byte[ChainConst.SystemK.Length];
                for (int i = 0; i < b.Length; ++i)
                    b[i] = (byte)(ChainConst.SystemK[i] + 3);
                rij.Key = ChainConst.SystemK;

                pIn.Read(ChainConst.tempbuf, 0, ChainConst.tempbuf.Length);
                pIn.Read(ChainConst.tempbuf, 0, ChainConst.tempbuf.Length);
                pIn.Read(ChainConst.tempbuf, 0, ChainConst.tempbuf.Length);

                rij3.IV = new byte[ChainConst.SystemV.Length];
                rij3.Key = new byte[ChainConst.SystemK.Length];

                b = new byte[ChainConst.SystemV.Length];
                pIn.Read(b, 0, b.Length);
                rij3.IV = b;
                b = new byte[ChainConst.SystemK.Length];
                pIn.Read(b, 0, b.Length);
                rij3.Key = b;

                int count = pIn.ReadByte();
                count = (count % 13) + 1;
                for (int i = 0; i < count; ++i)
                {
                    pIn.Read(ChainConst.tempbuf, 0, ChainConst.tempbuf.Length);
                }




                cs = new CryptoStream(pIn, rij.CreateDecryptor(), CryptoStreamMode.Read);
                cs = new CryptoStream(cs, rij.CreateDecryptor(), CryptoStreamMode.Read);
                cs = new CryptoStream(cs, rij3.CreateDecryptor(), CryptoStreamMode.Read);
                cs = new CryptoStream(cs, rij.CreateDecryptor(), CryptoStreamMode.Read);



                return cs;

            }
            catch //(Exception exc)
            {
                return null;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using MobExt.Common;
using MobExt.Formating;


namespace MobExt.FileSystem
{
    public class ToolPath
    {
        const int MAXFILNAMELENTH = 150;

        public static bool isFileNameValid(string pName)
        {
            if (pName == string.Empty)
                return false;

            if (pName.Length > MAXFILNAMELENTH)
                return false;

            if (pName.IndexOfAny(new char[] { '/', '\\' }) >= 0)
                return false;

            if (pName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                return false;

            if (pName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return false;

            return true;
        }





        public static string tmpLocationDir()
        {
            string rootDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            return Path.Combine(rootDir, "temp");

            // return Path.GetTempPath();
        }


        static string getDalyTempDir()
        {

            string d_ = CurrentVersion.getName().ToUpper() + "_SYS_TMP_" + DateTime.Now.ToString("yyyy_MM_dd");

            d_ = Path.Combine(tmpLocationDir(), d_);

            if (!Directory.Exists(d_))
                Directory.CreateDirectory(d_);

            return d_;
        }

        public static string createTempDir()
        {
            //string d_ = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            //Directory.CreateDirectory(d_);
            //return d_;



            for (int i = 10; i < 30; ++i)
            {
                //ToolGUID.getNew() 
                string d_ = generateTempName(CurrentVersion.getName() + "_", i);

                if (!Directory.Exists(d_))
                {
                    Directory.CreateDirectory(d_);
                    return d_;
                }
            }
            throw new MyException.MyExceptionError("I/O Exception, Cant generate tmp directory");
        }

        public static string createTempFile()
        {
            return createTempFile(string.Empty);
        }

        public static string createTempFile(string pExt)
        {
            for (int i = 10; i < 30; ++i)
            {

                string d_ = generateTempName(CurrentVersion.getName() + "_", i);
                if (pExt != string.Empty)
                    d_ = Path.ChangeExtension(d_, pExt);

                if (!File.Exists(d_))
                {
                    File.WriteAllBytes(d_, new byte[] { });
                    return d_;
                }
            }
            throw new MyException.MyExceptionError("I/O Exception, Cant generate tmp file");
        }

        static string generateTempName(string pPrefix, int pIndx)
        {
            string prefix_ = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffffff");
            string rand_ = XmlFormating.helper.format(pIndx).PadLeft(2, '0'); //Path.GetRandomFileName()
            string res_ = Path.Combine(getDalyTempDir(), pPrefix.ToUpper() + prefix_ + rand_);
            // res_ = res_.Replace(' ', '_').Replace('-', '_');
            return res_;
        }

        public static string generateSufix()
        {
            return "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffffff");

        }
    }
}

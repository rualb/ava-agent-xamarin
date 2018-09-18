using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AvaExt.FileSystem
{
    public class FileDescriptor
    {

        public const string simpleMark = "+";

        public FileDescriptor(string pLoc, string pName)
        {
            location = pLoc;
            name = pName;
        }

        public string location = string.Empty;
        public string name = string.Empty;
        public string oldContent = string.Empty;
        public bool hasContentChanged(string pContent)
        {
            return (oldContent != pContent);
        }

        public bool hasSimpleMark()
        {
            return name.StartsWith(simpleMark);
        }

        public static FileDescriptor[] wrap(string pLocation, string[] pFiles)
        {
            List<FileDescriptor> list = new List<FileDescriptor>();
            foreach (string file in pFiles)
                list.Add(new FileDescriptor(pLocation, file));
            return list.ToArray();
        }

        public FileDescriptor copy()
        {
            FileDescriptor res_ = new FileDescriptor(this.location, this.name);
            res_.oldContent = this.oldContent;
            return res_;
        }

        public override string ToString()
        {
            return Path.Combine(location, name);
        }



        public bool isCurrentOrGeneralCulture()
        {
            string l_ = getFileCulture();
            return (l_ == string.Empty || l_ == System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);

        }

        public string getFileCulture()
        {
            string n1_ = Path.GetFileNameWithoutExtension(this.name);
            string n2_ = Path.GetFileNameWithoutExtension(n1_);


            if ((n1_.Length - n2_.Length) == (1 + 2)) //+dot
                return n1_.Substring(n1_.Length - 2, 2);

            return string.Empty;

        }

        public string getDescriptionWithoutCultureAndMark()
        {

            string n1_ = Path.GetFileNameWithoutExtension(this.name);
            string n2_ = Path.GetFileNameWithoutExtension(n1_);

            if ((n1_.Length - n2_.Length) == (1 + 2)) //+dot
                return n2_.TrimStart(simpleMark[0]);

            return n1_.TrimStart(simpleMark[0]);

        }



        public bool isMarkedWithTag(string[] pTagArr)
        {

            string name_ = this.getDescriptionWithoutCultureAndMark().ToLowerInvariant();

            foreach (string keyWord_ in pTagArr)
                if (keyWord_ != string.Empty)
                {
                    if (name_.EndsWith(keyWord_.ToLowerInvariant()))
                        return true;
                }

            return false;

        }
    }
}

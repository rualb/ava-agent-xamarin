using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 
using System.Reflection;
using AvaExt.Formating;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
 

namespace AvaExt.FileSystem
{
    public abstract class ImplFileSystemBase : IFileSystem
    {


        protected virtual string correctPath(string pDir, string pName)
        {
            return pDir;
        }

        public string getCombinedPath(string dir, string name)
        {
            return Path.Combine(dir, name);
        }


        public virtual byte[] getFile(string dir, string name)
        {
            return null;
        }


        public virtual void setFile(string dir, string name, byte[] data)
        {

        }



        public virtual string getFsOrResourceText(string dir, string name)
        {
            return null;
        }

        public virtual string getFileText(string dir, string name)
        {
            return null;
        }


        public virtual void setFileText(string dir, string name, string text)
        {

        }


  
        public virtual string[] getFiles(string dir)
        {
            return null;
        }
        public string[] getFilesPrefixed(string dir, string namePrefix)
        {
            string path = correctPath(dir, string.Empty);
            //
            List<string> list = new List<string>(getFiles(path));

            for (int i = 0; i < list.Count; ++i)
                if (!list[i].StartsWith(namePrefix))
                {
                    list.RemoveAt(i);
                    --i;
                }
            return list.ToArray();
        }
        public string[] getFilesWithExtention(string dir, string ext)
        {
            string path = correctPath(dir, string.Empty);
            //
            List<string> list = new List<string>(getFiles(path));

            for (int i = 0; i < list.Count; ++i)
                if (Path.GetExtension(list[i]) != ext)
                {
                    list.RemoveAt(i);
                    --i;
                }
            return list.ToArray();
        }





        //public string getTempFileName()
        //{

        //    return ToolPath.createTempFile(); //Path.GetTempFileName();
        //}


     

        public virtual bool exists(string dir, string name)
        {
            return false;
        }




        public virtual void delete(string dir, string name)
        {

        }
        public virtual void rename(string dir, string name, string newName)
        {

        }
        public virtual void copy(string dir, string name, string destName)
        {

        }




        public string getNewName(string dir, string ext)
        {

            //
            for (int i = 1; i < int.MaxValue; ++i)
            {
                string newName = Path.ChangeExtension(XmlFormating.helper.format(i), ext);
                if (!exists(dir, newName))
                    return newName;
            }
            throw new MyExceptionInner(MessageCollection.T_MSG_ERROR_NUMERATION);
        }









        public virtual void createDir(string pDir)
        {
            
        }
    }
}

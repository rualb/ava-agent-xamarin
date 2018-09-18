using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Reflection;
using AvaExt.Common;


namespace AvaExt.FileSystem
{
    public class ImplFileSystem : ImplFileSystemBase
    {


        string rootDir = string.Empty;


        public ImplFileSystem()
        {

            rootDir = ToolMobile.getRootPath();


            // createDir(rootDir);


        }

        //public ImplFileSystemLocalRooted(string pRoot)
        //{
        //    rootDir = pRoot;
        //}



        protected override string correctPath(string pDir, string pName)
        {
            string res_ = pDir;

            if (pName != null && pName != string.Empty)
                res_ = Path.Combine(res_, pName);

            res_ = ToolMobile.correctPath(res_);


            return res_;


        }

        public override byte[] getFile(string dir, string name)
        {
            string path = correctPath(dir, name);
            //
            return ToolMobile.readFileData(path);
        }


        public override void setFile(string dir, string name, byte[] data)
        {
            string path = correctPath(dir, name);
            //
            ToolMobile.writeFileData(path, data);
        }

        public override string getFileText(string dir, string name)
        {
            string path = correctPath(dir, name);
            //
            return ToolMobile.readFileText(path);
        }

        public override string getFsOrResourceText(string dir, string name)
        {
            string path = correctPath(dir, name);
            //
            return ToolMobile.getFsOrResourceText(dir, name);
        }
        
        public override void setFileText(string dir, string name, string text)
        {
            string path = correctPath(dir, name);
            //
            ToolMobile.writeFileText(path, text);
        }




        public override string[] getFiles(string dir)
        {
            string path = correctPath(dir, string.Empty);
            //
            string[] arr = ToolMobile.getFiles(path);
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = Path.GetFileName(arr[i]);
            return arr;
        }








        public override bool exists(string dir, string name)
        {
            string path = correctPath(dir, name);
            //
            return ToolMobile.existsFile(path);
        }




        public override void delete(string dir, string name)
        {
            string path = correctPath(dir, name);
            //
            ToolMobile.deleteFile(path);
        }
        public override void rename(string dir, string name, string newName)
        {
            string path = correctPath(dir, name);
            string path2 = correctPath(dir, newName);
            //
            File.Move(path, path2);
        }
        public override void copy(string dir, string name, string destName)
        {
            string path = correctPath(dir, name);
            string path2 = correctPath(dir, destName);
            //
            File.Copy(path, path2);
        }


        public override void createDir(string pDir)
        {
            pDir = correctPath(pDir, string.Empty);
            ToolMobile.createDir(pDir);
        }

        //public string getTempDir()
        //{
        //    return tmpDir;
        //}
    }
}

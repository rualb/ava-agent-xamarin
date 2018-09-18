using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MobExt.Settings;
using MobExt.Common;

namespace MobExt.FileSystem
{
    public class ImplFileSystem : IFileSystem
    {

        public ImplFileSystem()
        {


            if (Directory.Exists(getTempDir()))
                Directory.Delete(getTempDir(), true);
            Directory.CreateDirectory(getTempDir());

        }


        public string getFileText(string path)
        {
            return ToolFile.readAllText(getFullPath(path));
        }

        public void setFileText(string path, string text)
        {
            ToolFile.writeAllText(getFullPath(path), text, false);
        }


        public string getFileText(string dir, string name)
        {
            return getFileText(getCombinedPath(getFullPath(dir), name));
        }

        public string getCombinedPath(string dir, string name)
        {
            return Path.Combine(dir, name);
        }

        public void setFileText(string dir, string name, string text)
        {
            setFileText(getCombinedPath(getFullPath(dir), name), text);
        }


        public string[] getFiles(string dir)
        {
            string[] arr = Directory.GetFiles(getFullPath(dir));
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = Path.GetFileName(arr[i]);
            return arr;
        }
        public string[] getFilesPrefixed(string dir, string namePrefix)
        {
            List<string> list = new List<string>(getFiles(dir));

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
            List<string> list = new List<string>(getFiles(dir));

            for (int i = 0; i < list.Count; ++i)
                if (Path.GetExtension(list[i]) != ext)
                {
                    list.RemoveAt(i);
                    --i;
                }
            return list.ToArray();
        }





        public string getTempFileName()
        {
            return Path.Combine(getTempDir(), Guid.NewGuid().ToString());
        }

        public string getTempDir()
        {
            return getFullPath("temp");
        }


        string getFullPath(string path)
        {
            return Path.Combine(ToolMobile.curDir(), path);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Settings;

namespace AvaExt.FileSystem
{
    public interface IFileSystem
    {
 
        //string getFileText(string path);
        string getFileText(string dir, string name);
        string getFsOrResourceText(string dir, string name);
 
        string getCombinedPath(string dir, string name);
       // void setFileText(string path, string text);
        void setFileText(string dir, string name, string text);
        string[] getFiles(string dir);
        string[] getFilesPrefixed(string dir,string namePrefix);
        string[] getFilesWithExtention(string dir, string ext); 
        // void setFileText(string dir, string name, string text, string desc);
       // ISettings getRootInfoFile(string dir);
      //  string getTempFileName();
      //  string getTempDir();

        void createDir(string pDir);
    }
}

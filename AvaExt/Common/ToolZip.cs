using System;
using System.Collections.Generic;
using System.Text;

//using System.IO;
//using ICSharpCode.SharpZipLib.Zip;
using Java.IO;
using Java.Util.Zip;
using System.IO;

namespace AvaExt.Common
{
    public class ToolZip
    {

        //check

        //public static void compress(string outputFile, string rootPath, string file)
        //{

        //    FastZip fz = new FastZip();
        //    fz.CreateZip(outputFile, rootPath, false, file);
        //}

        //public static void decompress(string zipFilePath, string outputFolder)
        //{
        //    FastZip fz = new FastZip();
        //    fz.ExtractZip(zipFilePath, outputFolder, null);

        //}


        public static void compress(string outputFile, string file)
        {
            new Compress(new string[] { file }, outputFile);

        }

        public static void decompress(string zipFilePath, string outputFolder)
        {

            new Decompress(zipFilePath, outputFolder);
        }
        public class Compress
        {
            private static readonly int BUFFER = 2048;

            private string[] _files;
            private string _zipFile;

            public Compress(String[] files, String zipFile)
            {
                _files = files;
                _zipFile = zipFile;

                zip();
            }

            public void zip()
            {
                System.IO.FileStream dest = null;
                ZipOutputStream out_ = null;
                try
                {

                    dest = new System.IO.FileStream(_zipFile, System.IO.FileMode.Create);

                    out_ = new ZipOutputStream(new System.IO.BufferedStream(dest));


                    byte[] data = new byte[BUFFER];

                    for (int i = 0; i < _files.Length; i++)
                    {
                        System.IO.FileStream fi = null;
                        BufferedInputStream origin = null;
                        try
                        {
                            fi = new System.IO.FileStream(_files[i], System.IO.FileMode.Open);
                            origin = new BufferedInputStream(fi, BUFFER);

                            ZipEntry entry = new ZipEntry(Path.GetFileName(_files[i]));
                            out_.PutNextEntry(entry);
                            int count;
                            //for (int b = origin.Read(); b != -1; b = origin.Read())
                            //{
                            //    out_.Write(b);
                            //}

                            while ((count = origin.Read(data, 0, BUFFER)) != -1)
                            {

                                out_.Write(data, 0, count);
                            }

                        }
                        finally
                        {
                            if (origin != null) origin.Close();
                            if (fi != null) fi.Close();

                        }
                    }


                }
                finally
                {
                    if (out_ != null) out_.Close(); //top first
                    if (dest != null) dest.Close();

                }

            }

        }


        public class Decompress
        {
            private String _zipFile;
            private String _location;

            public Decompress(String zipFile, String location)
            {
                _zipFile = zipFile;
                _location = location;

                _dirChecker("");

                unzip();
            }

            void unzip()
            {
                System.IO.FileStream fin = null;
                ZipInputStream zin = null;
                try
                {
                    fin = new System.IO.FileStream(_zipFile, System.IO.FileMode.Open);
                    zin = new ZipInputStream(fin);
                    ZipEntry ze = null;

                    byte[] buf = new byte[1024*4];

                    while ((ze = zin.NextEntry) != null)
                    {

                        if (ze.IsDirectory)
                            _dirChecker(ze.Name);
                        else
                        {
                            FileOutputStream fout = null;
                            try
                            {

                                fout = new FileOutputStream(Path.Combine(_location, ze.Name));

                                while (true)
                                {

                                    int count = zin.Read(buf);
                                    if (count < 0)
                                        break;

                                    fout.Write(buf, 0, count);
                                }

                                //for (int c = zin.Read(); c != -1; c = zin.Read())
                                //    fout.Write(c);



                            }
                            finally
                            {
                                if (fout != null) fout.Close();
                            }
                        }

                        zin.CloseEntry();
                    }
                }
                finally
                {
                    if (zin != null) zin.Close(); //top first
                    if (fin != null) fin.Close();
                }

            }

            private void _dirChecker(String dir)
            {
                dir = Path.Combine(_location, dir);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
        }

    }
}

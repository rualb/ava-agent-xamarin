using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using AvaExt.Common;

namespace AvaExt.Settings
{

    public class SettingsFromFileExt : SettingsFromXmlDoc
    {

        //

        IEnvironment environment;


        List<FileDescriptor> filesDescriptorsList = new List<FileDescriptor>();



        public SettingsFromFileExt(string dir, string fileName, IEnvironment pEnv)
            : base()
        {
            environment = pEnv;
            setXmlDocs(getDocs(dir, fileName));
        }



        XmlDocument[] getDocs(string dir, string fileName)
        {
            try
            {
                List<XmlDocument> docs = new List<XmlDocument>();

                string[] files = environment.getFileSystem().getFilesPrefixed(dir, fileName);

                if (files.Length == 0)
                {
                    docs.Add(getNewDoc());
                    filesDescriptorsList.Add(null);
                }
                else
                    foreach (string fileNameNew in files)
                    {
                        XmlDocument doc = new XmlDocument();
                        FileDescriptor desc = new FileDescriptor();

                        desc.document = doc;
                        desc.dir = dir;
                        desc.fileName = fileNameNew;
                        doc.LoadXml(environment.getFileSystem().getFileText(dir, fileNameNew));
                        desc.oldContent = doc.InnerXml; //saved xml doc looks like formatted against InnerXml

                        if (fileName == fileNameNew)
                        {
                            docs.Insert(0, doc);
                            filesDescriptorsList.Insert(0, desc);
                        }
                        else
                        {
                            docs.Add(doc);
                            filesDescriptorsList.Add(desc);
                        }
                    }
                return docs.ToArray();

            }
            catch (Exception exc)
            {
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INNER, new object[] { dir, fileName }, exc);

            }
        }



        public override void save()
        {
            foreach (FileDescriptor desc in filesDescriptorsList)
                if (desc != null)
                    if (desc.oldContent != desc.document.InnerXml)
                    {
                        StringWriter sw = new StringWriter();
                        desc.document.Save(sw);
                        environment.getFileSystem().setFileText(desc.dir, desc.fileName, sw.GetStringBuilder().ToString());
                        desc.oldContent = desc.document.InnerXml;
                    }
        }






        XmlDocument getNewDoc()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
            doc.AppendChild(doc.CreateElement(rootElementName));
            return doc;
        }
    }


    class FileDescriptor
    {
        public string dir = string.Empty;
        public string fileName = string.Empty;

        public string oldContent = string.Empty;
        public XmlDocument document = null;
    }

}

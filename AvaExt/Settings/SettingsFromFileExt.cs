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
using AvaExt.FileSystem;



namespace AvaExt.Settings
{

    public class SettingsFromFileExt : SettingsFromXmlDoc
    {


        class FileDescriptorXml
        {

            public FileDescriptorXml(FileDescriptor pDesc, XmlDocument pDocument)
            {
                desc = pDesc;
                document = pDocument;
            }
            public FileDescriptor desc;
            public XmlDocument document;

        }
        //
        IFileSystem _fileSystem;



        List<FileDescriptorXml> filesDescriptorsList = new List<FileDescriptorXml>();

        public SettingsFromFileExt(string dir, string fileName, IEnvironment pEnv)
            : this(new FileDescriptor(dir, fileName), pEnv.getFileSystem(), false, false)
        {

        }

        public SettingsFromFileExt(FileDescriptor pFileDesc, IFileSystem pFs, bool pFileNameIsPattern, bool pFlagSourceUpdate)
        {
            setFlagSourceUpdate(pFlagSourceUpdate);
            _fileSystem = pFs;
            setXmlDocs(getDocs(pFs, pFileDesc, pFileNameIsPattern));
        }

        public SettingsFromFileExt(FileDescriptor pFileDesc, IFileSystem pFs)
            : this(pFileDesc, pFs, false, true)
        {
        }

        //public SettingsFromFileExt(FileDescriptor pFileDesc, IFileSystem pFs, string pData)
        //    : this(pFileDesc, pFs, getDoc(pData))
        //{

        //}


        protected virtual string changeInputData(string pInput)
        {
            return pInput;
        }
        protected virtual string changeOutputData(string pOutput)
        {
            return pOutput;
        }

        bool hasWellNownExtension(string pName)
        {

            string ext_ = Path.GetExtension(pName);

            if (ext_ != null && ext_ != string.Empty)
            {
                if (
                    (ext_ == FileExt.extSRS) ||
                    (ext_ == FileExt.extXLRS) ||
                    (ext_ == FileExt.extMDBRS) ||
                    (ext_ == FileExt.extDOCBM) ||
                    (ext_ == FileExt.extXLSBM) ||
                    (ext_ == FileExt.extXml)
                    )
                    return true;

            }

            return false;

        }
        XmlDocument[] getDocs(IFileSystem pFs, FileDescriptor pDesc, bool pPatternt)
        {
            try
            {
                FileDescriptor desc_ = pDesc.copy();


                List<XmlDocument> docs = new List<XmlDocument>();

                string[] files = new string[] { };

                if (pPatternt)
                    files = pFs.getFilesPrefixed(desc_.location, desc_.name);
                else
                {
                    if (!hasWellNownExtension(desc_.name))
                        desc_.name += FileExt.extXml;

                    // if (ToolMobile.existsFile(desc_.location, desc_.name))
                    files = new string[] { desc_.name };
                }
                if (files.Length == 0)
                {
                    throw new MyExceptionError(MessageCollection.T_MSG_INVALID_PARAMETER, new object[] { pDesc });
                    //docs.Add(getNewDoc());
                    //filesDescriptorsList.Add(null);
                }
                else
                    foreach (string fileNameNew in files)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(changeInputData(pFs.getFsOrResourceText(desc_.location, fileNameNew)));

                        FileDescriptorXml desc = new FileDescriptorXml(new FileDescriptor(desc_.location, fileNameNew), doc);
                        desc.desc.oldContent = getDocContent(doc);

                        if (desc_.name == Path.GetFileNameWithoutExtension(fileNameNew))
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
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INNER, new object[] { pDesc.ToString() }, exc);
            }
        }



        public override void save()
        {
            if (getFlagSourceUpdate())
                foreach (FileDescriptorXml desc in filesDescriptorsList)
                    if (desc != null && desc.desc.name != null)
                        if (desc.desc.hasContentChanged(desc.document.InnerXml))
                        {
                            _fileSystem.setFileText(desc.desc.location, desc.desc.name, changeOutputData(format(desc.document)));
                            desc.desc.oldContent = getDocContent(desc.document);
                        }
        }





        /*
        static XmlDocument getNewDoc()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
            doc.AppendChild(doc.CreateElement(rootElementName));
            return doc;
        }
        */

        static string getDocContent(XmlDocument pDoc)
        {
            return pDoc.InnerXml;
        }



        //static XmlDocument getDoc(string pData)
        //{
        //    XmlDocument doc = null;


        //    if (pData == null || pData == string.Empty)
        //        doc = getNewDoc();
        //    else
        //    {
        //        doc = new XmlDocument();
        //        doc.LoadXml(pData);
        //    }
        //    return doc;
        //}
    }




}

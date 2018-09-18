using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using System.Data;

namespace AvaExt.Settings
{
    public class SettingsFromTable : SettingsFromXmlDoc
    {

        //




        List<string> xmlDocsFilesLong = new List<string>();
 
        public SettingsFromTable(DataTable table, string colName, string colParm)
            : base()
        {
            setXmlDocs(new XmlDocument[] { createDoc() });
            foreach (DataRow row in table.Rows)
                set((string)row[colName], (string)row[colParm]);
        }







        new XmlDocument createDoc()
        {
            XmlDocument doc = new XmlDocument();
            StringWriter ss = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(ss);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlWriter.WriteStartElement(rootElementName);
            xmlWriter.Close();
            doc.LoadXml(ss.ToString());


            return doc;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace AvaExt.Common
{
    public class ToolXml
    {
        public static string getAttribValue(XmlNode node, string attr, string defVal)
        {
            if (node.Attributes[attr] != null)
                return node.Attributes[attr].Value;
            return defVal;
        }
    }
}

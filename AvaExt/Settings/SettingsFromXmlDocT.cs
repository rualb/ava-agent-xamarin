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
using AvaExt.Formating;

namespace AvaExt.Settings
{
    public class SettingsFromXmlDoc : ISettings
    {
        XmlElement lastEnumeratedNode;
        IEnumerator<XmlElement> enumeration;


        string _code = string.Empty;
        string _desc = string.Empty;

        XmlFormating _formating = new XmlFormating();
        ToolTypeSet _typeSet = new ToolTypeSet();

        protected const string nodeArr = "arr";

        protected const string cmd_ref = "ref::";
        protected const string cmd_file = "file::";
        protected const string cmd_objtxt = "objtxt::";
        protected const string cmd_mark = "::";
        protected const string cmd_sep = ".";

        protected const string rootElementName = "settings";
        protected const string settingsValue = "value";
        protected bool changed = false;
        //




        List<XmlElement> xmlNodes;

        protected void setXmlDocs(XmlDocument[] pDocs)
        {

            xmlNodes = new List<XmlElement>();
            foreach (XmlDocument doc in pDocs)
                xmlNodes.Add(doc.DocumentElement);

        }
        protected XmlDocument[] getXmlDocs()
        {
            List<XmlDocument> xmlDocs = new List<XmlDocument>();
            foreach (XmlElement node in xmlNodes)
                xmlDocs.Add(node.OwnerDocument);
            return xmlDocs.ToArray();
        }

        public SettingsFromXmlDoc(XmlElement pXmlNode)
        {

            xmlNodes = new List<XmlElement>();
            xmlNodes.Add(pXmlNode);

        }
        public SettingsFromXmlDoc(XmlElement pXmlNode, XmlElement pXmlNodeEnumerated)
        {

            xmlNodes = new List<XmlElement>();
            xmlNodes.Add(pXmlNode);
            lastEnumeratedNode = pXmlNodeEnumerated;

        }
        public SettingsFromXmlDoc(XmlDocument[] pXmlDocs)
        {

            setXmlDocs(pXmlDocs);

        }
        public SettingsFromXmlDoc()
        {

            setXmlDocs(new XmlDocument[] { });

        }




        string getValue(string name, string attr)
        {
            XmlElement param = getElement(name);
            return getValue(attr, param);
        }
        string getValue(string attr, XmlElement param)
        {
            if ((attr != null) && (attr != string.Empty) && (param != null) && (param.HasAttribute(attr)))
            {
                string val = param.GetAttribute(attr);
                if (val.StartsWith(cmd_ref))
                    return getValue(getValParam(val), getValParamAttr(val));
                return val;
            }
            return null;
        }

        XmlElement getElement(string name)
        {
            XmlElement res = null;
            if (name != null && name != string.Empty)
                foreach (XmlElement node in xmlNodes)
                    if ((res = node[name]) != null)
                        return res;
            return res;
        }


        string getValParam(string txt)
        {
            int indx1 = txt.IndexOf(cmd_mark);
            if (indx1 < 0)
                indx1 = 0;
            else
                indx1 = indx1 + cmd_mark.Length;
            int indx2 = txt.IndexOf(cmd_sep);
            if (indx2 < 0)
                indx2 = txt.Length - 1;
            return txt.Substring(indx1, indx2 - indx1 + 1);
        }
        string getValParamAttr(string txt)
        {
            int indx1 = txt.IndexOf(cmd_sep);
            if (indx1 < 0)
                return settingsValue;
            int indx2 = txt.Length - 1;
            return txt.Substring(indx1, indx2 - indx1 + 1);
        }


        void setInner(string name, string attr, string value)
        {
            XmlElement param = getElement(name);
            if (param == null)
                param = createElement(name);
            setInner(attr, value, param);
        }
        void setInner(string attr, string value, XmlElement param)
        {
            param.SetAttribute(attr, value);
            changed = true;
        }
        XmlElement createElement(string name, XmlElement node)
        {
            XmlElement param = node.OwnerDocument.CreateElement(name);
            node.AppendChild(param);
            return param;
        }
        XmlElement createElement(string name)
        {
            changed = true;
            return createElement(name, getMainNode());
        }
        protected virtual XmlElement getMainNode()
        {
            return xmlNodes[0];
        }
        string valueToString(object value)
        {
            return _formating.format(value);

        }

        #region Enumeration Methods

        public string getNameEnumer()
        {
            return lastEnumeratedNode.Name;
        }
        public string getInnerStringEnumer()
        {
            return lastEnumeratedNode.InnerText;

        }
        public void setInnerStringEnumer(string value)
        {
            lastEnumeratedNode.InnerText = value;
        }

        public void deleteEnumer()
        {
            changed = true;
            lastEnumeratedNode.ParentNode.RemoveChild(lastEnumeratedNode);
        }
        public void clearEnumer()
        {
            changed = true;
            lastEnumeratedNode.RemoveAll();
        }
        public void setEnumer(string attr, object value)
        {
            if ((value != null) && (attr != null))
                if ((attr != string.Empty))
                    setInner(attr, valueToString(value), lastEnumeratedNode);
        }
        public void setEnumer(object value)
        {
            setEnumer(settingsValue, value);
        }

        public object getEnumer(Type type)
        {
            return getAttrEnumer(settingsValue, type, null);
        }
        public string getStringEnumer()
        {
            return getStringAttrEnumer(settingsValue, string.Empty);
        }
        public Type getTypeEnumer()
        {
            return getTypeAttrEnumer(settingsValue, typeof(object));
        }
        public int getIntEnumer()
        {
            return getIntAttrEnumer(settingsValue, 0);
        }
        public short getShortEnumer()
        {
            return getShortAttrEnumer(settingsValue, 0);
        }
        public DateTime getDateTimeEnumer()
        {
            return getDateTimeAttrEnumer(settingsValue, DateTime.Now);
        }
        public double getDoubleEnumer()
        {
            return getDoubleAttrEnumer(settingsValue, 0);
        }
        public bool getBoolEnumer()
        {
            return getBoolAttrEnumer(settingsValue, false);
        }

        public object getEnumer(Type type, object defVal)
        {
            return getAttrEnumer(settingsValue, type, defVal);
        }
        public string getStringEnumer(string defVal)
        {
            return getStringAttrEnumer(settingsValue, defVal);
        }
        public Type getTypeEnumer(Type defVal)
        {
            return getTypeAttrEnumer(settingsValue, defVal);
        }
        public int getIntEnumer(int defVal)
        {
            return getIntAttrEnumer(settingsValue, defVal);
        }
        public short getShortEnumer(short defVal)
        {
            return getShortAttrEnumer(settingsValue, defVal);
        }
        public DateTime getDateTimeEnumer(DateTime defVal)
        {
            return getDateTimeAttrEnumer(settingsValue, defVal);
        }
        public double getDoubleEnumer(double defVal)
        {
            return getDoubleAttrEnumer(settingsValue, defVal);
        }
        public bool getBoolEnumer(bool defVal)
        {
            return getBoolAttrEnumer(settingsValue, defVal);
        }

        public object getAttrEnumer(string attr, Type type)
        {
            return getAttrEnumer(attr, type, null);
        }
        public string getStringAttrEnumer(string attr)
        {
            return getStringAttrEnumer(attr, string.Empty);
        }
        public Type getTypeAttrEnumer(string attr)
        {
            return getTypeAttrEnumer(attr, typeof(object));
        }
        public int getIntAttrEnumer(string attr)
        {
            return getIntAttrEnumer(attr, 0);
        }
        public short getShortAttrEnumer(string attr)
        {
            return getShortAttrEnumer(attr, 0);
        }
        public DateTime getDateTimeAttrEnumer(string attr)
        {
            return getDateTimeAttrEnumer(attr, DateTime.Now);
        }
        public double getDoubleAttrEnumer(string attr)
        {
            return getDoubleAttrEnumer(attr, 0);
        }
        public bool getBoolAttrEnumer(string attr)
        {
            return getBoolAttrEnumer(attr, false);
        }

        public object getAttrEnumer(string attr, Type type, object defVal)
        {
            string val = getValue(attr, lastEnumeratedNode);
            if (val == null)
                return defVal;
            return _formating.parse(val, type);
        }
        public string getStringAttrEnumer(string attr, string defVal)
        {
            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseString(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public Type getTypeAttrEnumer(string attr, Type defVal)
        {
            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseType(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public int getIntAttrEnumer(string attr, int defVal)
        {

            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseInt(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public short getShortAttrEnumer(string attr, short defVal)
        {

            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseShort(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public DateTime getDateTimeAttrEnumer(string attr, DateTime defVal)
        {

            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseDateTime(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public double getDoubleAttrEnumer(string attr, double defVal)
        {

            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseDouble(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public bool getBoolAttrEnumer(string attr, bool defVal)
        {

            string res;
            if ((res = getValue(attr, lastEnumeratedNode)) != null)
                return _formating.parseBool(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }


        public string[] getListAttrEnumer(string attr)
        {
            return ToolString.explodeList(getStringAttrEnumer(attr));
        }
        public string[] getListAttrEnumer(string attr, string[] def)
        {
            return ToolString.explodeList(getStringAttrEnumer(attr, ToolString.joinList(def)));
        }

        public void setListEnumer(string[] vals)
        {
            setEnumer(ToolString.joinList(vals));
        }
        public void setListAttrEnumer(string attr, string[] vals)
        {
            setEnumer(attr, ToolString.joinList(vals));
        }


        public string[] getAllAttrEnumer()
        {
            List<string> list = new List<string>();
            XmlElement elm = lastEnumeratedNode;
            if (elm != null)
                foreach (XmlAttribute attr in elm.Attributes)
                    list.Add(attr.Name);
            return list.ToArray();
        }
        public ISettings forkEnumer()
        {
            return new SettingsFromXmlDoc(lastEnumeratedNode);
        }
        #endregion

        public void set(string name, string attr, object value)
        {
            if ((name != null) && (value != null) && (attr != null))
                if ((name != string.Empty) && (attr != string.Empty))
                    setInner(name, attr, valueToString(value));
        }
        public void set(string name, object value)
        {
            set(name, settingsValue, value);
        }


        public object get(string name, Type type)
        {
            return _formating.parse(name, type);

        }
        public string getString(string name)
        {
            return getString(name, string.Empty);
        }
        public Type getType(string name)
        {
            return getType(name, typeof(object));
        }
        public int getInt(string name)
        {
            return getInt(name, 0);
        }
        public short getShort(string name)
        {
            return getShort(name, 0);
        }
        public DateTime getDateTime(string name)
        {
            return getDateTime(name, DateTime.Now);
        }
        public double getDouble(string name)
        {
            return getDouble(name, 0);
        }
        public bool getBool(string name)
        {
            return getBool(name, false);
        }

        public object get(string name, Type type, object defVal)
        {
            return _formating.parse(name, type, defVal);
        }
        public string getString(string name, string defVal)
        {
            return getStringAttr(name, settingsValue, defVal);
        }
        public Type getType(string name, Type defVal)
        {
            return getTypeAttr(name, settingsValue, defVal);
        }
        public int getInt(string name, int defVal)
        {
            return getIntAttr(name, settingsValue, defVal);
        }
        public short getShort(string name, short defVal)
        {
            return getShortAttr(name, settingsValue, defVal);
        }
        public DateTime getDateTime(string name, DateTime defVal)
        {
            return getDateTimeAttr(name, settingsValue, defVal);
        }
        public double getDouble(string name, double defVal)
        {
            return getDoubleAttr(name, settingsValue, defVal);
        }
        public bool getBool(string name, bool defVal)
        {
            return getBoolAttr(name, settingsValue, defVal);
        }




        public object getAttr(string name, string attr, Type type)
        {
            return getAttr(name, attr, type, null);
        }
        public string getStringAttr(string name, string attr)
        {
            return getStringAttr(name, attr, string.Empty);
        }
        public Type getTypeAttr(string name, string attr)
        {
            return getTypeAttr(name, attr, typeof(object));
        }
        public int getIntAttr(string name, string attr)
        {
            return getIntAttr(name, attr, 0);
        }
        public short getShortAttr(string name, string attr)
        {
            return getShortAttr(name, attr, 0);
        }
        public DateTime getDateTimeAttr(string name, string attr)
        {
            return getDateTimeAttr(name, attr, DateTime.Now);
        }
        public double getDoubleAttr(string name, string attr)
        {
            return getDoubleAttr(name, attr, 0);
        }
        public bool getBoolAttr(string name, string attr)
        {
            return getBoolAttr(name, attr, false);
        }


        public object getAttr(string name, string attr, Type type, object defVal)
        {
            string val = getValue(name, attr);
            if (val == null)
                return defVal;
            return _formating.parse(val, type);
        }
        public string getStringAttr(string name, string attr, string defVal)
        {
            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseString(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public Type getTypeAttr(string name, string attr, Type defVal)
        {
            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseType(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public int getIntAttr(string name, string attr, int defVal)
        {

            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseInt(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public short getShortAttr(string name, string attr, short defVal)
        {

            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseShort(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public DateTime getDateTimeAttr(string name, string attr, DateTime defVal)
        {

            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseDateTime(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public double getDoubleAttr(string name, string attr, double defVal)
        {

            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseDouble(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }
        public bool getBoolAttr(string name, string attr, bool defVal)
        {

            string res;
            if ((res = getValue(name, attr)) != null)
                return _formating.parseBool(res);
            //if (addIfNew)
            //    set(name, attr, defVal);
            return defVal;
        }

        public string[] getAllAttr(string name)
        {
            List<string> list = new List<string>();
            foreach (XmlElement node in xmlNodes)
            {
                XmlElement elm = node[name];
                if (elm != null)
                {
                    foreach (XmlAttribute attr in elm.Attributes)
                        list.Add(attr.Name);
                    break;
                }
            }
            return list.ToArray();
        }

        public virtual string[] getAllSettings()
        {
            List<string> list = new List<string>();

            foreach (XmlNode nodeRoot in xmlNodes)
                foreach (XmlNode node in nodeRoot)
                    if (node.NodeType == XmlNodeType.Element && !list.Contains(node.Name))
                        list.Add(node.Name);
            return list.ToArray();
        }



        public static XmlDocument createDoc()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
            doc.AppendChild(doc.CreateElement(rootElementName));

            return doc;
        }

        Type[] checkAttrTypesArr(Type[] arr, int len)
        {
            if (arr == null || arr.Length == 0)
                return ToolArray.create<Type>(len, typeof(string));
            return arr;
        }
        public object[][] getArr(string name, Type[] type)
        {
            return getArrAttr(name, new string[] { settingsValue }, type);
        }
        public object[][] getArr(string name, string arrName, Type[] type)
        {
            return getArrAttr(name, arrName, new string[] { settingsValue }, type);
        }
        public object[][] getArrAttr(string name, string[] attr, Type[] type)
        {
            return getArrAttr(name, nodeArr, attr, type);
        }
        public object[][] getArrAttr(string name, string arrName, string[] attr, Type[] type)
        {
            Type[] attrTypes = checkAttrTypesArr(type, attr.Length);
            List<object[]> list = new List<object[]>();
            XmlElement root = getElement(name);
            if (root != null)
                foreach (XmlNode node in root)
                    if (node.Name == arrName)
                        if (typeof(XmlElement).IsAssignableFrom(node.GetType()))
                        {
                            XmlElement elem = (XmlElement)node;
                            object[] arr = new object[attr.Length];

                            for (int i = 0; i < attr.Length; ++i)
                                arr[i] = _formating.parse(elem.GetAttribute(attr[i]), attrTypes[i]);
                            list.Add(arr);
                        }
            return list.ToArray();
        }

        public string[][] getArr(string name)
        {
            return ToolArray.stringArray(getArr(name, new Type[0]));
        }
        public string[][] getArr(string name, string arrName)
        {
            return ToolArray.stringArray(getArr(name, arrName, new Type[0]));
        }
        public string[][] getArrAttr(string name, string[] attr)
        {
            return ToolArray.stringArray(getArrAttr(name, attr, new Type[0]));
        }
        public string[][] getArrAttr(string name, string arrName, string[] attr)
        {
            return ToolArray.stringArray(getArrAttr(name, arrName, attr, new Type[0]));
        }

        public void setArr(string name, object[] value)
        {
            setArrAttr(name, new string[] { settingsValue }, new object[][] { value });
        }
        public void setArr(string name, string arrName, object[] value)
        {
            setArrAttr(name, arrName, new string[] { settingsValue }, new object[][] { value });
        }
        public void setArrAttr(string name, string[] attr, object[][] value)
        {
            setArrAttr(name, nodeArr, attr, value);
        }
        public void setArrAttr(string name, string arrName, string[] attr, object[][] value)
        {
            XmlElement root = getElement(name);
            if (root == null)
                root = createElement(name);

            //clear
            XmlElement param;
            while ((param = root[arrName]) != null)
                root.RemoveChild(param);
            //root.InnerXml = string.Empty;
            //add
            foreach (object[] arr in value)
            {
                XmlElement newChild = root.OwnerDocument.CreateElement(arrName);
                for (int i = 0; i < attr.Length; ++i)
                    newChild.SetAttribute(attr[i], valueToString(arr[i]));
                root.AppendChild(newChild);
            }
            changed = true;
        }








        public string[] getList(string name)
        {
            return ToolString.explodeList(this.getString(name));
        }
        public string[] getListAttr(string name, string attr)
        {
            return ToolString.explodeList(this.getStringAttr(name, attr));
        }
        public string[] getList(string name, string[] def)
        {
            return ToolString.explodeList(this.getString(name, ToolString.joinList(def)));
        }
        public string[] getListAttr(string name, string attr, string[] def)
        {
            return ToolString.explodeList(this.getStringAttr(name, attr, ToolString.joinList(def)));
        }

        public void setList(string name, string[] vals)
        {
            this.set(name, ToolString.joinList(vals));
        }
        public void setListAttr(string name, string attr, string[] vals)
        {
            this.set(name, attr, ToolString.joinList(vals));
        }




        public string getCode()
        {
            return _code;
        }

        public void setCode(string code)
        {
            _code = code;
        }

        public string getDescription()
        {
            return _desc;
        }

        public void setDescription(string desc)
        {
            _desc = desc;
        }

        public void delete(string name)
        {
            changed = true;
            XmlElement element = null;
            while ((element = getElement(name)) != null)
                element.ParentNode.RemoveChild(element);
        }
        public void clear()
        {
            changed = true;
            getMainNode().RemoveAll();
        }
        public bool hasParameter(string name)
        {
            return (getElement(name) != null);
        }


        public string getInnerString(string name)
        {
            XmlElement param = getElement(name);
            if (param != null)
                return param.InnerText;
            return string.Empty;
        }
        public void setInnerString(string name, string value)
        {
            XmlElement param = getElement(name);
            if (param == null)
                param = createElement(name);

            param.InnerText = value;

        }
        /// <summary>
        /// Fork for enumerated node
        /// </summary>
        /// <returns></returns>

        public ISettings fork(string name)
        {
            XmlElement elm = getElement(name);
            if (elm == null)
                elm = createElement(name);
            return new SettingsFromXmlDoc(elm);
        }
        public ISettings fork()
        {
            return new SettingsFromXmlDoc(getMainNode(), lastEnumeratedNode);
        }

        public bool isEnumerValid()
        {
            return (lastEnumeratedNode != null);
        }

        public void enumarate()
        {
            enumarate(null);
        }
        public void enumarateFirst(string name)
        {
            lastEnumeratedNode = getElement(name);
            if (lastEnumeratedNode != null)
                enumeration = (new List<XmlElement>(new XmlElement[] { lastEnumeratedNode })).GetEnumerator();
        }
        public void enumarate(string name)
        {
            lastEnumeratedNode = null;
            List<XmlElement> list = new List<XmlElement>();
            foreach (XmlNode nodeRoot in xmlNodes)
                foreach (XmlNode node in nodeRoot)
                    if ((name == null) || (name == node.Name))
                        if (node.NodeType == XmlNodeType.Element)
                            list.Add((XmlElement)node);
            if (list.Count > 0)
                lastEnumeratedNode = list[0];
            enumeration = list.GetEnumerator();
        }

        public bool moveNext()
        {
            if (enumeration.MoveNext())
            {
                lastEnumeratedNode = enumeration.Current;
                return true;
            }
            lastEnumeratedNode = null;
            return false;
        }
        public void add()
        {
            add(nodeArr);
        }
        public void add(string name)
        {
            getMainNode().AppendChild(lastEnumeratedNode = getMainNode().OwnerDocument.CreateElement(name));
        }

        public virtual void save()
        {

        }


    }
}

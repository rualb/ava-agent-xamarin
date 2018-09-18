using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using AvaExt.Expression;
using AvaExt.Common;
using AvaExt.Reporting;
using System.IO;
using AvaExt.SQL.Dynamic;
using AvaExt.TableOperation;
using AvaExt.Formating;


namespace AvaExt.MobControl.Reporting.XmlReport
{
    public class ImplXmlReport : ImplReport
    {
        UnicodeEncoding _enc = new UnicodeEncoding();
        XmlDocument _doc = new XmlDocument();
        RenderingInfo _rendInfo = new RenderingInfo();

        SimpleScript _script;
        IEnvironment _environment;
        const string _constNodeCmdName = "cmd";


        const string _constCmdText = "text";
        const string _constCmdTableAggr = "aggr";
        const string _constCmdTable = "table";
        const string _constCmdTableFirstRow = "tableFR";
        const string _constCmdColumn = "addColumn";
        const string _constCmdGroup = "group";
        const string _constCmdFilter = "filter";
 

        const string _constAttrTable = "table";
        const string _constAttrFilter = "filter";
        const string _constAttrSort = "sort";
        const string _constAttrLineRepeat = "lineRepeat";
        const string _constAttrName = "name";
        const string _constAttrList = "list";
        const string _constAttrExp = "exp";
        const string _constAttrType = "type";
        const string _constAttrReplace = "replace";
        const string _constAttrCount = "count";
        const string _constAttrDirect = "direct";
        const string _constAttrCol = "col";
        const string _constAttrEnc = "enc";

        public ImplXmlReport(string pFile, IEnvironment pEnv)
        {
            _environment = pEnv;
            _script = new SimpleScript(_environment);

           // var fileContent = ToolMobile.readFileTextByCache(pFile);
            var fileContent = ToolMobile.getFsOrResourceText(Path.GetDirectoryName(pFile),Path.GetFileName(pFile));
            _doc.LoadXml(fileContent);
            //
            _rendInfo.count = XmlFormating.helper.parseInt(ToolXml.getAttribValue(_doc.DocumentElement, _constAttrCount, XmlFormating.helper.format(_rendInfo.count)));
            _rendInfo.isDirect = XmlFormating.helper.parseBool(ToolXml.getAttribValue(_doc.DocumentElement, _constAttrDirect, XmlFormating.helper.format(_rendInfo.isDirect)));
            _rendInfo.encoding = XmlFormating.helper.parseString(ToolXml.getAttribValue(_doc.DocumentElement, _constAttrEnc, XmlFormating.helper.format(_rendInfo.encoding)));
            _rendInfo.replace = XmlFormating.helper.parseString(ToolXml.getAttribValue(_doc.DocumentElement, _constAttrReplace, XmlFormating.helper.format(_rendInfo.replace)));
            
            //
        }

       

        void eval(XmlNode pNode, DataSet pDataSet, StringBuilder pBuilder)
        {

            foreach (XmlNode node in pNode.ChildNodes)
                if (node.NodeType == XmlNodeType.Element && node.Name == _constNodeCmdName)
                {
                    switch (node.Attributes[_constAttrType].Value)
                    {
                        case _constCmdText:
                            exeCmdText(pBuilder, node);
                            break;
                        case _constCmdTable:
                            exeCmdTable(pBuilder, node, pDataSet);
                            break;
                        case _constCmdTableFirstRow:
                            exeCmdTable(pBuilder, node, pDataSet, 1);
                            break;
                        case _constCmdColumn:
                            exeCmdColumn(node, pDataSet);
                            break;
                        case _constCmdGroup:
                            exeCmdGroup(pBuilder, node, pDataSet);
                            break;
                        case _constCmdTableAggr:
                            exeCmdAggr(node, pDataSet);
                            break;
                        case _constCmdFilter:
                            exeCmdFilter(node, pDataSet);
                            break;

                    }

                }


        }

        private void exeCmdAggr(XmlNode pNode, DataSet pDataSet)
        {
            string name = pNode.Attributes[_constAttrName].Value;
            DataTable table = pDataSet.Tables[pNode.Attributes[_constAttrTable].Value];
            string exp = pNode.Attributes[_constAttrExp].Value;
            string filter = ToolXml.getAttribValue(pNode, _constAttrFilter, string.Empty);
            string col = ToolXml.getAttribValue(pNode, _constAttrCol, string.Empty); ;
            string sort = ToolXml.getAttribValue(pNode, _constAttrSort, string.Empty);
            string lineCount = ToolXml.getAttribValue(pNode, _constAttrLineRepeat, string.Empty);
            DataRow[] rows = table.Select(filter, sort);


            switch (exp.ToLower())
            {
                case "sum":
                    double res = 0;
                    foreach (DataRow row in rows)
                        res += Convert.ToDouble(row[col]);
                    setVar(name, res);
                    break;
                case "count":
                    setVar(name, rows.Length);
                    break;
                case "distinct":
                    List<object> list = new List<object>();
                    foreach (DataRow row in rows)
                        if (!list.Contains(row[col]))
                            list.Add(row[col]);
                    setVar(name, list.Count);
                    break;

            }






        }
        private void exeCmdFilter(XmlNode pNode, DataSet pDataSet)
        {

            DataTable table = pDataSet.Tables[pNode.Attributes[_constAttrTable].Value];
            string exp = pNode.Attributes[_constAttrExp].Value;
            table.DefaultView.RowFilter = exp;

        }

        string eval(XmlNode pNode, DataSet pDataSet)
        {
            StringBuilder strBuilder = new StringBuilder(1000);
            _script.setVarStack();
            eval(pNode, pDataSet, strBuilder);
            return strBuilder.ToString();
        }

        private void exeCmdGroup(StringBuilder pBuilder, XmlNode pNode, DataSet pDataSet)
        {
            string tabName = pNode.Attributes[_constAttrTable].Value;
            string filter = ToolXml.getAttribValue(pNode, _constAttrFilter, string.Empty);
            DataTable table = pDataSet.Tables[tabName];
            table.DefaultView.RowFilter = filter;
            string[] colsArr = ToolString.explodeList(pNode.Attributes[_constAttrList].Value);


            DataTable[] grpTabs = ToolTable.explodeForGroups(table, colsArr);
            foreach (DataTable curTab in grpTabs)
            {
                if (curTab.DefaultView.Count > 0)
                {
                    pDataSet.Tables.Remove(tabName);
                    pDataSet.Tables.Add(curTab);
                    setVars(curTab.DefaultView[0].Row);
                    eval(pNode, pDataSet, pBuilder);
                }
            }


            pDataSet.Tables.Remove(tabName);
            pDataSet.Tables.Add(table);


        }



        private void exeCmdColumn(XmlNode pNode, DataSet pDataSet)
        {
            DataTable table = pDataSet.Tables[pNode.Attributes[_constAttrTable].Value];
            string newColumnName = pNode.Attributes[_constAttrName].Value;
            string newColumnExp = pNode.Attributes[_constAttrExp].Value;
            if (!table.Columns.Contains(newColumnName))
                table.Columns.Add(newColumnName, typeof(object), newColumnExp);
        }

        private void exeCmdTable(StringBuilder pBuilder, XmlNode pNode, DataSet pDataSet)
        {
            exeCmdTable(pBuilder, pNode, pDataSet, int.MaxValue - 1);
        }

        private void exeCmdTable(StringBuilder pBuilder, XmlNode pNode, DataSet pDataSet, int pMaxCount)
        {
            DataTable table = pDataSet.Tables[pNode.Attributes[_constAttrTable].Value];
            exeCmdTable(pBuilder, pNode, table, pMaxCount);
        }
        private void exeCmdTable(StringBuilder pBuilder, XmlNode pNode, DataTable pTable, int pMaxCount)
        {
            try
            {

                pTable.DefaultView.RowFilter = ToolXml.getAttribValue(pNode, _constAttrFilter, string.Empty);
                pTable.DefaultView.Sort = ToolXml.getAttribValue(pNode, _constAttrSort, string.Empty);

                var repeatCol_ = ToolXml.getAttribValue(pNode, _constAttrLineRepeat, string.Empty);
               

                int counter = 0;
                foreach (DataRowView rowView in pTable.DefaultView)
                {
                    ++counter;
                    if (counter > pMaxCount)
                        break;


                    var repeat_ = 1;
                    if (!string.IsNullOrEmpty(repeatCol_))
                    {
                        repeat_ = Convert.ToInt16(Convert.ToDouble(rowView[repeatCol_]));
                        repeat_ = Math.Max(repeat_, 1);
                    }

                  

                    for (int i = 1; i <= repeat_; ++i)
                    {
                        setVars(rowView.Row);
                        string newData = _script.eval(pNode.InnerText);
                        pBuilder.Append(newData);
                    }

                }

            }
            finally
            {
                if (pTable != null)
                    pTable.DefaultView.Sort = pTable.DefaultView.RowFilter = string.Empty;
            }
        }
        void setVars(DataRow dataRow)
        {
            foreach (DataColumn col in dataRow.Table.Columns)
            {
                string varName = string.Format("{0}.{1}", dataRow.Table.TableName, col.ColumnName);
                setVar(varName, dataRow[col]);
            }
        }
        void setVar(string name, object val)
        {
            _script.getVarOperator().setVar(name, val);
        }
        private void exeCmdText(StringBuilder pBuilder, XmlNode pNode)
        {
            string newData = _script.eval(pNode.InnerText);
            pBuilder.Append(newData);
        }






        public string getData()
        {
            return eval(_doc.DocumentElement, getDataSet());
        }

        public override object getResult()
        {
            string data = eval(_doc.DocumentElement, getDataSet());
            data = _environment.translate(data);
            return data;
            //StringBuilder strBuilder = new StringBuilder(data);
            //string replace = ToolXml.getAttribValue(_doc.DocumentElement, _constAttrReplace, string.Empty);
            //if (replace != string.Empty)
            //{
            //    string[][] arrItems = ToolString.explodeGroupList(replace);
            //    foreach (string[] arr in arrItems)
            //        if (arr.Length == 2)
            //            strBuilder.Replace(arr[0], arr[1]);
            //}
            //return strBuilder.ToString();
            
        }
        public override RenderingInfo getRenderingInfo()
        {
            return _rendInfo;
        }
    }
}

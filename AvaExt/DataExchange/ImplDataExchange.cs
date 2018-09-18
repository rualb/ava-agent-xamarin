using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowValidator;
using System.Collections;
using AvaExt.Adapter.ForUser;
using System.Data;
using System.Xml;
using AvaExt.TableOperation;
using AvaExt.Common;
using AvaExt.Formating;
using System.IO;
using AvaExt.Translating.Tools;
using AvaExt.SQL;

namespace AvaExt.DataExchange
{
    public class ImplDataExchange : IDataExchangeExt
    {
        IDictionary<string, IRowValidator> _validatorCol = new Dictionary<string, IRowValidator>();
        IRowValidator _defValidator = new RowValidatorTrue();
        IAdapterUser _userAdapter;
        const string _nodeRootName = "DATA";
        const string _attrRootSep = "ei_sep";
        const string _attrRootCode = "ei_code";
        const string _nodeItemName = "ITEM";
        const string _attrItemTableCols = "ei_cols";
        const string _attrItemTableTable = "ei_table";
        const string _attrRootSepData = "sepData";
        XmlFormating _formating = new XmlFormating();
        IDictionary<string, string[]> _expDesc = null;
        IEnvironment _environment;
        public ImplDataExchange(IEnvironment pEnv, IAdapterUser pUserAdapter)
        {
            _environment = pEnv;
            _userAdapter = pUserAdapter;

        }
        protected virtual void initExportDescriptor(IDataExchangeExt pDataExchange, IAdapterUser pUserAdapter)
        {
            string attrExpDesc = "expdesc";
            string attrTab = "table";
            string attrCols = "cols";
            string[] arrDesc = ToolString.explodeList(_environment.getAppSettings().getStringAttr(pUserAdapter.getAdapterDataSet().getCode(), attrExpDesc));
            IDictionary<string, string[]> data = new Dictionary<string, string[]>();
            foreach (string desc in arrDesc)
            {
                data.Add(
                    _environment.getAppSettings().getStringAttr(desc, attrTab),
                    ToolString.explodeList(_environment.getAppSettings().getStringAttr(desc, attrCols))
                 );
            }
            pDataExchange.setExportDescriptor(data);
        }
        public void setRowValidator(string pTable, IRowValidator pRowValidator)
        {
            if (_validatorCol.ContainsKey(pTable))
                _validatorCol[pTable] = pRowValidator;
            else
                _validatorCol.Add(pTable, pRowValidator);
        }

        IRowValidator getValidator(string tableName)
        {
            if (_validatorCol.ContainsKey(tableName))
                return _validatorCol[tableName];
            return _defValidator;
        }
        public string export()
        {

            return string.Empty;
        }
        public void export(XmlDocument doc)
        {
            if (this.getExportDescriptor() == null)
                initExportDescriptor(this, _userAdapter);
            correctDoc(doc);
            DataSet dataSet = _userAdapter.getDataSet();
            XmlElement xmlExpRoot = doc.CreateElement(_nodeItemName);
            xmlExpRoot.SetAttribute(_attrRootCode, dataSet.DataSetName);
            char sepChar = '\t';
            string expTable;
            string[] expCols;
            IEnumerator<KeyValuePair<string, string[]>> expTabsEnumer = _expDesc.GetEnumerator();
            expTabsEnumer.Reset();
            while (expTabsEnumer.MoveNext())
            {
                expTable = expTabsEnumer.Current.Key;
                expCols = expTabsEnumer.Current.Value;

                DataTable table = dataSet.Tables[expTable];
                IRowValidator validator = getValidator(expTable);
                //
                XmlElement xmlExpTab = doc.CreateElement(expTable);
                xmlExpTab.SetAttribute(_attrItemTableCols, ToolString.joinList(sepChar, expCols));
                xmlExpRoot.AppendChild(xmlExpTab);
                //
                StringWriter strData = new StringWriter();
                strData.WriteLine();
                foreach (DataRow expRow in table.Rows)
                    if (validator.check(expRow))
                    {
                        bool isFirstVal = true;
                        foreach (string expCol in expCols)
                        {
                            if (isFirstVal)
                                isFirstVal = false;
                            else
                                strData.Write(sepChar);
                            strData.Write(_formating.format(expRow[expCol]));
                        }
                        strData.WriteLine();
                    }
                xmlExpTab.InnerText = strData.GetStringBuilder().ToString();
            }
            doc[_nodeRootName].AppendChild(xmlExpRoot);
        }

        void correctDoc(XmlDocument doc)
        {
            if (doc.InnerText == string.Empty)
            {
                doc.AppendChild(doc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'"));
                doc.AppendChild(doc.CreateElement(_nodeRootName));
            }
        }
        public void import(XmlDocument doc)
        {
            XmlElement rootNode = doc[_nodeRootName];
            char sepChar = '\t';
            string adpName = _userAdapter.getAdapterDataSet().getCode();
            foreach (XmlNode itemNode in rootNode.ChildNodes)
                if ((itemNode.Name == _nodeItemName) && (itemNode.Attributes[_attrRootCode].Value == adpName))
                {
                    _userAdapter.add();
                    DataSet dataSet = _userAdapter.getDataSet();
                    foreach (XmlNode itemNodeTable in itemNode.ChildNodes)
                    {
                        string colsList = itemNodeTable.Attributes[_attrItemTableCols].Value;
                        string[] colsArr = ToolString.explodeList(sepChar, colsList);
                        string tableName = itemNodeTable.Name;
                        DataTable tableD = dataSet.Tables[tableName];
                        if (tableD == null)
                            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INNER, new object[] { tableName });
                        tableD.Clear();
                        string data = itemNode.InnerText;
                        //
                        StringReader reader = new StringReader(data);
                        var lineIndx = 0;
                        var commitCounter = 0;

                        string line;

                        var listCode = new List<string>();

                        while ((line = reader.ReadLine()) != null)
                        {
                            ++lineIndx;

                            try
                            {


                                if (line != string.Empty)
                                {



                                    string[] arrData = ToolString.explodeList(sepChar, line); //line.Split(sepChar);

                                    if (arrData == null || arrData.Length != colsArr.Length)
                                        throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT, new object[] { line });


                                  //  if (lineIndx > 10)
                                    //    break;

                                   


                                    var rowDic = new Dictionary<string, string>();

                                    for (int indxColD = 0; indxColD < colsArr.Length; ++indxColD)
                                    {
                                        var col = colsArr[indxColD];
                                        var colVal = arrData[indxColD];

                                       

                                        if (col == "CODE")
                                        {
                                            var keyCode = colVal.ToUpperInvariant();

                                            if (keyCode == "")
                                            {
                                                //throw new Exception("Record CODE is empty: index: " + lineIndx);
                                                colVal = "EMPTYCODE" + lineIndx;
                                            }
                                            else
                                                if (listCode.Contains(keyCode))
                                                {
                                                    throw new Exception("Record dublicate: " + keyCode);
                                                }
                                                else
                                                    listCode.Add(keyCode);



                                        }

                                        rowDic[col] = colVal;

                                    }

                                    switch (tableName)
                                    {
                                        //case "FIRMPARAMS":
                                        //    SqlExecute.executeNonQuery(_environment, "INSERT INTO L_FIRMPARAMS (LOGICALREF,CODE,VALUE) VALUES (@P1,@P2,@P3 )",
                                        //  new object[] { rowDic["CODE"], rowDic["CODE"], rowDic["VALUE"] });
                                        //    break;
                                        //case "CLCARD":
                                        //    SqlExecute.executeNonQuery(_environment, "INSERT INTO LG_$FIRM$_CLCARD (LOGICALREF,CODE,CLGRPCODE,CLGRPCODESUB) VALUES (@P1,@P2,@P3,@P4)",
                                        //  new object[] { rowDic["LOGICALREF"], rowDic["CODE"], rowDic["CLGRPCODE"], rowDic["CLGRPCODESUB"] });
                                        //    break;
                                        //case "ITEMS":
                                        //    SqlExecute.executeNonQuery(_environment, "INSERT INTO LG_$FIRM$_ITEMS (LOGICALREF,CODE,STGRPCODE,STGRPCODESUB) VALUES (@P1,@P2,@P3,@P4)",
                                        //  new object[] { rowDic["LOGICALREF"], rowDic["CODE"], rowDic["STGRPCODE"], rowDic["STGRPCODESUB"] });
                                        //    break;
                                        default:
                                            {


                                                DataRow newRowD = _userAdapter.addRowToTable(tableD);


                                                foreach (var pair in rowDic)
                                                {
                                                    DataColumn dCol = tableD.Columns[pair.Key];
                                                    if (dCol == null)
                                                        throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INNER, new object[] { pair.Key });

                                                    var value_ = _formating.parse(pair.Value, dCol.DataType);

                                                    newRowD[dCol] = value_;

                                                }

                                            }
                                            break;
                                    }

                                    
                                    

                                    ++commitCounter;


                                    //if (commitCounter > 1)
                                    //{

                                    //    _userAdapter.update();
                                    //    _userAdapter.clear();
                                    //    tableD.Clear();
                                    //    //
                                    //    //

                                    //    commitCounter = 0;
                                    //}
                                }

                            }
                            catch (Exception exc)
                            {
                                ToolMobile.setRuntimeMsg(line);
                                ToolMobile.setRuntimeMsg(exc.ToString());


                                throw new Exception(exc.Message, exc);

                            }
                        }

                    }

             

                    _userAdapter.update();
                }
        }
        public void import(string pData)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pData);
            import(doc);
        }
        public IDictionary<string, string[]> getExportDescriptor()
        {
            return _expDesc;
        }
        public void setExportDescriptor(IDictionary<string, string[]> pExpDesc)
        {
            _expDesc = pExpDesc;
        }


    }
}

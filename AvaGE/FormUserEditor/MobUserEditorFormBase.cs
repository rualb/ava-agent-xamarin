using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.Adapter.ForUser;
using System.Reflection;
using System.IO;
using AvaExt.Adapter.Const;
using AvaExt.SQL.Dynamic;
using AvaExt.PagedSource;
using System.Collections;
using System.Threading;
using AvaGE.Common;
using AvaGE.MobControl;
using AvaExt.DataRefernce;
using AvaExt.Adapter;
using AvaExt.Manual.Table;
using AvaExt.Formating;
using AvaExt.TableOperation;
using AvaExt.Translating.Tools;
using AvaExt.ControlOperation;
using AvaExt.Drivers;
using AvaExt.SQL;
using AvaAgent;

namespace AvaGE.FormUserEditor
{
    public class MobUserEditorFormBase : AvaGE.Common.MobFormBase, IAdapterUserForm
    {


        bool isEditorFormSavingStarted = false; //look like some times android generate two click ???

        //static Assembly _plugin = null;
        const string _docStatusSave = "_saveDoc";
        const string _docStatusBeginDoc = "_beginDoc";
        protected ConstMarkingCodesGroup innerVerSpecodeGroup;
        protected ConstMarkingCodesType innerVerSpecodeType;
        protected ConstMarkingCodesGroup innerVerCyphGroup;
        protected ConstMarkingCodesType innerVerCyphType;

        protected EditingTools editingTools;
        protected IAdapterUser userAdapter { get { return editingTools != null ? editingTools.adapter : null; } }

        public virtual void setAdapter(EditingTools pTool)
        {
            editingTools = pTool;

        }
        protected virtual string[][] getDefaultValues()
        {
            List<string[]> list = new List<string[]>();
            string PARM = "MOB_DEFAULTVALS_" + getId();
            string listStr = environment.getSysSettings().getString(PARM);
            // listStr = "INVOICE,READONLY,1;INVOICE,READONLY,1;xxxx,yyyy,1";
            DataTable table = ToolString.explodeForTable(listStr, new string[] { TableDUMMY.TABLE, TableDUMMY.COLUMN, TableDUMMY.VALUE });

            foreach (DataRow row in table.Rows)
                list.Add(new string[] { row[TableDUMMY.TABLE].ToString(), row[TableDUMMY.COLUMN].ToString(), row[TableDUMMY.VALUE].ToString() });
            return list.ToArray();
        }
        public virtual void reinitEditingForData()
        {
            initChanger();

            //init defaults
            if (userAdapter.getAdapterWorkState() == AdapterWorkState.stateAdd)
            {
                DataSet ds = userAdapter.getDataSet();

                string[][] arrDefaults = getDefaultValues();
                foreach (string[] arrTmp in arrDefaults)
                {
                    string tableName = arrTmp[0];
                    string tableColName = arrTmp[1];
                    string valueStr = arrTmp[2];

                    DataTable tableTmp = ds.Tables[tableName];
                    if (tableTmp != null)
                    {
                        DataColumn colTmp = tableTmp.Columns[tableColName];
                        if (colTmp != null)
                        {
                            string val_ = getValue(valueStr);
                            object value = XmlFormating.helper.parse(val_, colTmp.DataType);
                            ToolColumn.setColumnValue(tableTmp, colTmp.ColumnName, value);
                        }
                    }

                }
            }
        }

        string getValue(string valueStr)
        {
            if (valueStr.Length > 0 && valueStr[0] == ('/'))
            {
                return environment.getSysSettings().getString(valueStr.TrimStart('/'));
            }

            return valueStr;
        }

        DriverStub _changer = null;


        MobButton cBtnOk { get { return FindViewById<MobButton>(Resource.Id.cBtnOk); } }
        MobButton cBtnCancel { get { return FindViewById<MobButton>(Resource.Id.cBtnCancel); } }


        public MobUserEditorFormBase(IEnvironment pEnv, int pLayer)
            : base(pEnv, pLayer)
        {



        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);



            _changer = null;
            editingTools = null;
        }


        protected bool isViewMode()
        {
            return Intent.GetStringExtra("view") == XmlFormating.helper.format(true);
        }

        protected override void initBeforeSettings()
        {
            EditingTools tools = environment.getAdapter(globalStoreName());
            if (tools != null)
                setAdapter(tools);

            base.initBeforeSettings();
        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();

            reinitBindingProperties();
            bindToData();

            reinitEditingForData();




            if (isViewMode())
                cBtnOk.Visible = false;
            else
                cBtnOk.Click += cBtnOk_Click;

            cBtnCancel.Click += cBtnCancel_Click;
        }
        void cBtnCancel_Click(object sender, EventArgs e)
        {
            userRequireCancel();
        }

        void cBtnOk_Click(object sender, EventArgs e)
        {
            userRequireCommit();
        }

        protected virtual void reinitBindingProperties()
        {

        }

        protected virtual void bindToData()
        {
            InitBinding.bind(this, userAdapter.getDataSet(), environment, true);
        }
        protected override void userRequireCommit()
        {
            if (isViewMode())
                userRequireCancel();
            else
            {

                if (!isEditorFormSavingStarted)
                {
                    try
                    {
                        isEditorFormSavingStarted = true;

                        try
                        {
                            FinishDataEditing();

                            startSave();

                            editingTools.save(this);
                        }
                        catch (Exception exc)
                        {
                            environment.getExceptionHandler().setException(exc);
                        }

                    }
                    finally
                    {
                        isEditorFormSavingStarted = false;
                    }

                }

            }
        }

        protected override void userRequireCancel()
        {
            editingTools.cancel(this);
        }




        void initChanger()
        {

            //try
            //{
            if (_changer == null)
                _changer = DriverStub.createInstanse(environment);//, ToolMobile.getAsmRootPath(), "AvaPlugin.dll"

            callChanger(_docStatusBeginDoc);

             
        }

        void callChanger(string docState)
        {
            if (_changer != null &&
                (
                userAdapter.getAdapterWorkState() == AdapterWorkState.stateAdd ||
                userAdapter.getAdapterWorkState() == AdapterWorkState.stateEdit ||

                // added for _beginDoc , chech doc count limit
                userAdapter.getAdapterWorkState() == AdapterWorkState.stateCopy 
                )
                )
            {
                _changer.call(new object[] { "_dataSet", userAdapter.getDataSet() });
                _changer.call(new object[] { "_activity", new WaitCallback(this.cmdHandler) });

                _changer.call(new object[] { docState });

            }

        }


       


        public virtual void startSave()
        {
            callChanger(_docStatusSave);


            //if (!callChanger(_docStatusCheck))
            //    throw getChangerExc();


        }


        void cmdHandler(object pArgs)
        {
            object res = null;
            object[] args = pArgs as object[];
            if (args != null && args.Length > 0)
            {
                string cmd = args[0] as string;
                switch (cmd)
                {
                    case "sql":
                        res = executeSql(args[1].ToString(), args[2] as object[]);
                        break;
                    case "translate":
                        res = environment.translate((string)args[1]);
                        break;
                    case "ref":
                        {
                            IDataReference ref_ = environment.getReference(args[1] as string);
                            if (ref_ != null)
                                ref_.begin(null, null, false, args[2] as EventHandler);

                            //inside handler sender(IDataReference) handel "sender.getSelected()"
                        }
                        break;
                    case "stockIO":
                        res = environment.getMatIOAmount(args[1]);
                        break;

                    case "settings":
                        res = environment.getSysSettings().getString(args[1] as string, null);
                        break;


                }
            }
            _changer.call(new object[] { "_return", res });

        }
        DataTable executeSql(string sql, object[] parameters)
        {

            return SqlExecute.execute(environment, sql, parameters);


        }

        //void setFieldValue(object obj, string field, object val)
        //{
        //    Type type = obj.GetType();
        //    FieldInfo fi = type.GetField(field);
        //    if (fi != null)
        //        fi.SetValue(obj, val);
        //}
        //object getFieldValue(object obj, string field)
        //{
        //    Type type = obj.GetType();
        //    FieldInfo fi = type.GetField(field);
        //    if (fi != null)
        //        return fi.GetValue(obj);
        //    return null;
        //}


        public Form getFormObject()
        {
            return this;
        }
        protected virtual string getPrefix()
        {
            return string.Empty;
        }

        protected virtual string getId()
        {
            return string.Empty;
        }

    }



}


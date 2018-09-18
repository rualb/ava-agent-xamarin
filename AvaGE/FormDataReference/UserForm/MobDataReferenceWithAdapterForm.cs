using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.ControlOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.Common.Const;
using AvaExt.Manual.Table;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Translating.Tools;

using AvaExt.ObjectSource;
using AvaExt.TableOperation;
using AvaExt.Adapter;
using AvaGE.FormUserEditor;

namespace AvaGE.FormDataReference.UserForm
{
    public class MobDataReferenceWithAdapterForm : MobDataReferenceGridFormBase
    {
        //
        protected virtual void onNewAdded(EditingTools pTool, object pLref)
        {


        }
        protected virtual void handlerReferenceInformer(EditingTools pTool, object pLref)
        {
            if (!object.ReferenceEquals(pTool, _editor))
                return;

            try
            {
                if (
                    (pTool.adapter.getAdapterWorkState() == AvaExt.Adapter.Const.AdapterWorkState.stateDelete) ||
                    (pTool.adapter.getAdapterWorkState() == AvaExt.Adapter.Const.AdapterWorkState.stateAdd))
                    refresh();//all refresh
                else
                    refresh(pLref);


                if (
                    (pTool.adapter.getAdapterWorkState() == AvaExt.Adapter.Const.AdapterWorkState.stateAdd) ||
                    (pTool.adapter.getAdapterWorkState() == AvaExt.Adapter.Const.AdapterWorkState.stateCopy)
                    )
                    onNewAdded(pTool, pLref);
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
        }

        EditingTools _editor;
        void initAdapter()
        {
            var code = getAdapterCode();
            _editor = environment.getAdapter(code);

            if (_editor == null)
                throw new Exception("Cant find editor for:" + code);

            _editor.handlerReferenceInformer = handlerReferenceInformer; //make editor bound to this reference
        }
        //
        protected virtual string getAdapterCode()
        {
            return string.Empty;
        }
        public MobDataReferenceWithAdapterForm(int pLayout)
            : base(pLayout)
        {
            initAdapter();

            Closed += MobDataReferenceWithAdapterForm_Closed;
        }

        void MobDataReferenceWithAdapterForm_Closed(object sender, EventArgs e)
        {
            if (_editor != null)
                _editor.handlerReferenceInformer = null;

            _editor = null;
        }



        protected override object add()
        {
            _editor.adapter.add();

            // return _editor.edit();

            _editor.edit();
            return null;


        }
        protected override void view(DataRow dataRow)
        {
            _editor.adapter.edit(dataRow[TableDUMMY.LOGICALREF]);
            _editor.view();

        }
        protected override object edit(DataRow dataRow)
        {
            _editor.adapter.edit(dataRow[TableDUMMY.LOGICALREF]);
            //  return _editor.edit();

            _editor.edit();
            return null;

        }
        protected override object copy(DataRow dataRow)
        {
            _editor.adapter.edit(dataRow[TableDUMMY.LOGICALREF]);
            _editor.adapter.setAdded();
            _editor.adapter.resetRefs();
            _editor.adapter.initCopy();
            //  return _editor.edit();
            _editor.edit();
            return null;

        }
        protected override void delete(DataRow dataRow)
        {
            _editor.adapter.edit(dataRow[TableDUMMY.LOGICALREF]);
            _editor.delete();

        }





    }
}


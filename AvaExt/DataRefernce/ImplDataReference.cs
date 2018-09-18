using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic;
using AvaExt.TableOperation;
using AvaExt.Expression;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.DataRefernce
{
    public class ImplDataReference : AppObject, IDataReference, IDataReferenceHelper
    {
        ReferenceMode referenceMode;
  
        IFlagStore flagStore = new FlagStore();
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        protected IPagedSource source;
        protected List<DataRow> rows = new List<DataRow>();
        protected bool isMultiSelect = false;
        protected IRowValidator validator = new RowValidatorTrue();

        protected string cmd;
        protected string name;
        public ImplDataReference(string pCmd, string pName)
        {
            //if null
            cmd = pCmd;
            name = pName;

            if (pName != null)
                source = new ImplPagedSourceFromDs(environment, pName, ToolObjectName.getName(pCmd));
        }

        public ReferenceMode getReferenceMode()
        {
            return referenceMode;
        }


        public IFlagStore getFlagStore()
        {
            return flagStore;
        }
        public virtual void clear()
        {
            referenceMode.handlerOk = null;

            if (rows != null)
                rows.Clear();
        }
        public virtual bool begin(string pColumn, object pValue, ReferenceMode pMode, EventHandler pOkHandler)
        {
            referenceMode = pMode;
            referenceMode.handlerOk = pOkHandler;

            if (pMode.showMode)
                this.getFlagStore().flagEnable(ReferenceFlags.showMode);
            else
                this.getFlagStore().flagDisable(ReferenceFlags.showMode);

            if (pMode.formBatchMode)
                this.getFlagStore().flagEnable(ReferenceFlags.formBatchMode);
            else
                this.getFlagStore().flagDisable(ReferenceFlags.formBatchMode);

            
            rows.Clear();
            show(pColumn, pValue);
            return isDataSelected();
        }
        public virtual bool begin(string pColumn, object pValue, bool pShowMode, EventHandler pOkHandler)
        {
            return begin(pColumn, pValue, new ReferenceMode { showMode = pShowMode }, pOkHandler);
        }
        //public virtual bool begin()
        //{
        //    // isMultiSelect = allowMultible;
        //    return begin(null, null, true, null);
        //}
        //public virtual void beginShow()
        //{
        //    try
        //    {
        //        this.getFlagStore().flagEnable(ReferenceFlags.showMode);
        //        rows.Clear();
        //        show();
        //    }
        //    finally
        //    {
        //        this.getFlagStore().flagDisable(ReferenceFlags.showMode);
        //    }

        //}

        protected virtual void show(string pColumn, object pValue)
        {

        }

        public virtual bool isDataSelected()
        {
            return (rows.Count > 0);
        }

        public virtual DataRow[] getSelectedAll()
        {
            return rows.ToArray();
        }

        public virtual DataRow getSelected()
        {
            if (isDataSelected())
                return rows[0];
            return null;
        }

        public void setRecordValidator(IRowValidator pValidator)
        {
            validator = pValidator;
        }

        public bool addData(DataRow row)
        {
            try
            {
                if (row != null)
                    if (validator.check(row))
                    {
                        rows.Add(ToolRow.createRowCopy(row));

                        EventHandler h_ = referenceMode.handlerOk; //make backup
                        referenceMode.handlerOk = null;

                        if (h_ != null)
                            h_.Invoke(this, EventArgs.Empty);//

                        if (this.getFlagStore().isFlagEnabled(ReferenceFlags.formBatchMode))
                        {
                            this.clear(); //clear data and handler
                            referenceMode.handlerOk = h_; //will be re-used

                            return false; //dont close
                        }

                        return true;
                    }
                return false;

            }
            finally
            {
                referenceMode.lastBatchModeIndex = -1;
            }
        }


        //public virtual void setFilter(string pColumn, object pValue)
        //{

        //}

        public IPagedSource getPagedSource()
        {
            return source;
        }



        public virtual void refresh()
        {

        }

        public virtual void refresh(object id)
        {

        }



        public void Dispose()
        {

        }
    }
}

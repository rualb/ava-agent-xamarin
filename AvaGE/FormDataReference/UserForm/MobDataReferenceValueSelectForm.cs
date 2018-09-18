using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.DataRefernce;
using AvaExt.PagedSource;
using AvaGE.MobControl.ControlsTools;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaGE.Common;
using AvaExt.Common.Const;
using AvaGE.MobControl;
using AvaExt.Manual.Table;
using Android.Views;
using AvaAgent;

namespace AvaGE.FormDataReference.UserForm
{
    public class MobDataReferenceValueSelectForm : MobFormBase
    {
        public bool showMode { get { return reference == null ? true : reference.getFlagStore().isFlagEnabled(ReferenceFlags.showMode); } } //only show no select
        //  protected IDataReferenceHelper helper;
        protected ImplDataReference reference;
        // protected IPagedSource source;
        //public IPagedDataAction extender;
        //  protected IUserTopMessanger topMessanger;
        protected DataTable _table = new DataTable();


        public MobDataReferenceValueSelectForm(int pLayout)
            : base(null, pLayout)
        {



            //
            initTable();

     


        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            //helper = null;
            reference = null;
            //   source = null;
            // extender = null;
            //  topMessanger = null;
            _table = null;

            getBtnCancel().Click -= BtnCancel_Click;
            getBtnOk().Click -= BtnOk_Click;
        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();



            ImplDataReference ref_ = ToolMobile.getEnvironment().getReference(this.Intent.GetStringExtra(ConstCmdLine.cmd)) as ImplDataReference;
            if (ref_ != null)
            {

                reference = ref_;
            }

            getBtnCancel().Click += BtnCancel_Click;
            getBtnOk().Click += BtnOk_Click;


            getBtnOk().Visibility = showMode ? ViewStates.Invisible : ViewStates.Visible;


            string col_ = Intent.GetStringExtra(TableDUMMY.COLUMN);
            string val_ = Intent.GetStringExtra(TableDUMMY.VALUE);
            if (col_ != null && !string.IsNullOrEmpty(val_))
            {
                setValue(val_);
            }
        }

        void BtnOk_Click(object sender, EventArgs e)
        {
            if (!showMode)
                returnData(getData());
        }

        void BtnCancel_Click(object sender, EventArgs e)
        {
            reference.clear();
            Close();
        }

        public MobButton getBtnOk()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnOk);
        }

        public MobButton getBtnCancel()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnCancel);
        }


  


        public virtual void setHelper(IDataReferenceHelper pHelper)
        {
            //helper = pHelper;
        }

        public virtual void setValue(object value)
        {
        }

        protected virtual void initTable()
        {


        }
        protected virtual DataRow getData()
        {

            return null;
        }
        protected virtual void returnData(DataRow row)
        {
            try
            {
                if (row != null && reference != null)
                {
                    if (reference.addData(row))
                        Close();
                }

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }

    }
}


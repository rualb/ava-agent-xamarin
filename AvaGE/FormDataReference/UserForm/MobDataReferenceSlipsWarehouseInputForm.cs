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
using AvaGE.FormUserEditor;

using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.TableOperation;
using AvaExt.Translating.Tools;
using AvaExt.ObjectSource;
using AvaExt.Adapter.ForDataSet.Material.Operation.Slip;
using AvaExt.Adapter.ForUser.Material.Operation.Slip;

using AvaGE.FormUserEditor.Const;
using Android.App;


namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceSlipsWarehouseInputForm : MobDataReferenceForMoveForm
    {
        protected override string globalStoreName()
        {
            return ConstRefCode.slipWarehouseIn;// "ref.mm.warehouse.slip.input";
        }
        public MobDataReferenceSlipsWarehouseInputForm()
            : base(0)
        {


        }

        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.GRPCODE, ConstDocGroupCode.materialManagement));
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.TRCODE, ConstDocTypeMaterial.marerialDefinedInput15));
           
        }

         

        protected override string getAdapterCode()
        {
            return ConstAdapterNames.adp_mm_doc_slip_warehouse_input;
        }

        

     
    }
}


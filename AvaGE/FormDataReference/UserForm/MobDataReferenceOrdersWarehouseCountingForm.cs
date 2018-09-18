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
using AvaExt.Adapter.ForUser;
using AvaExt.Manual.Table;
using AvaGE.FormUserEditor;

using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForUser.Sale.Operation.Order;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Order;
 
using AvaExt.Translating.Tools;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Common.Const;
using AvaExt.ObjectSource;
using AvaExt.Adapter.ForDataSet.Material.Operation.Order;
using AvaExt.Adapter.ForUser.Material.Operation.Order;
 
using AvaExt.TableOperation;
using AvaGE.FormUserEditor.Const;
using Android.App;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceOrdersWarehouseCountingForm : MobDataReferenceForOrderForm
    {

        protected override string globalStoreName()
        {
            return ConstRefCode.orderWarehouseCounting;// "ref.mm.warehouse.order.input";
        }
        public MobDataReferenceOrdersWarehouseCountingForm()
            : base(0)
        {


        }

        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.GRPCODE, ConstDocGroupCode.materialManagement));
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.TRCODE, ConstDocTypeMaterial.marerialDefinedInput19));
   
        }
        
        protected override string getAdapterCode()
        {
            return ConstAdapterNames.adp_mm_doc_order_warehouse_counting;
        }

      
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using AvaExt.Common.Const;
using AvaExt.Translating.Tools;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterialUnitMagic : PagedSourceMaterialUnits
    {
        delegate DataTable unitTrans(DataTable table);
        unitTrans action;
        public PagedSourceMaterialUnitMagic(IEnvironment pEnv, ConstMaterialUnitPrefered prefUnit)
            : base(pEnv)
        {
            switch (prefUnit)
            {
                case ConstMaterialUnitPrefered.main:
                    action = new unitTrans(ToolMaterial.getMainUnit);
                    break;
                case ConstMaterialUnitPrefered.small:
                    action = new unitTrans(ToolMaterial.getSmallUnit);
                    break;
                case ConstMaterialUnitPrefered.large:
                    action = new unitTrans(ToolMaterial.getLargeUnit);
                    break;
                default :
                    throw new MyException.MyExceptionError(MessageCollection.T_MSG_INVALID_PARAMETER);
            }
        }
        public override DataTable getFirst()
        {
            return get();
        }
        public override DataTable getFirst(bool prepareForWhere)
        {
            return get();
        }
        public override DataTable getNext()
        {
            return get();
        }
        public override DataTable getPreviose()
        {
            return get();
        }
        public override DataTable getCurrent()
        {
            return get();
        }
        public override DataTable getLast()
        {
            return get();
        }
        public override DataTable getAll()
        {
            return get();
        }
        public override DataTable get()
        {
            return action(base.get());
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.DataRefernce;

using AvaExt.Common;
using AvaExt.AndroidEnv.ControlsBase;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.ObjectSource;
using AvaExt.TableOperation.RowValidator;

namespace AvaGE.MobControl.ControlsTools
{
    public class BindDataRefenceAsActivitySet : IActivity
    {
        IActivity[] activity;
        IRowSource rowSource;
        IRowValidator[] validator;


        public BindDataRefenceAsActivitySet(IRowSource pRowSource, IActivity[] pActivity, IRowValidator[] pValidator)
        {
            activity = pActivity;
            rowSource = pRowSource;
            validator = pValidator;
        }


        public object done()
        {
            DataRow row = rowSource.get();
            for (int i = 0; i < validator.Length; ++i)
                if (validator[i].check(row))
                {
                    activity[i].done();
                    break;
                }

            return null;
        }



        public void Dispose()
        {
            activity = null;
            rowSource = null;
            validator = null;
        }
    }
}

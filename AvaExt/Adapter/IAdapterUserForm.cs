using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser;
using AvaExt.AndroidEnv.ControlsBase;


namespace AvaExt.Adapter
{
    public interface IAdapterUserForm
    {
        void initForm();
        void setAdapter(EditingTools pTool);
        void reinitEditingForData();
        void startSave();
        Form getFormObject();
    
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.SQL.Dynamic;
using AvaExt.PagedSource;
using AvaExt.TableOperation.RowValidator;
using AvaExt.Common;

namespace AvaExt.DataRefernce
{
    public interface IDataReference : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>is selection isnt null</returns>
        /// 
        void refresh();
        void refresh(object id);
        //bool begin(); //only dialog mode
        bool begin(string pColumn, object pValue, bool pShowMode, EventHandler pOkHandler); //only dialog mode,search
        bool begin(string pColumn, object pValue, ReferenceMode pMode, EventHandler pOkHandler);
        // void beginShow(); 
        IFlagStore getFlagStore();
        // void setFilter(string pColumn, object pValue);
        bool isDataSelected();
        DataRow[] getSelectedAll();
        DataRow getSelected();
        void setRecordValidator(IRowValidator pValidator);
        IPagedSource getPagedSource();

        ReferenceMode getReferenceMode();
    }


    [Flags]
    public enum ReferenceFlags
    {
        multiSelect = 1,
        // dialog = 2, //olvays
        // mdi = 4,
        showMode = 8, //only show no select
        formBatchMode = 16
    }

    public class ReferenceMode
    {
        public EventHandler handlerOk;

        public int lastBatchModeIndex = -1;

        public string[] batchModeIndexes;

        public bool showMode = true;
        public bool formBatchMode = false; //show as not show mode but dont close on select, !!! check clear data 

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Settings
{
    public class SettingsNamesApp
    {
        /// <summary>
        /// B_T - B is name, T is type
        /// --------------------------
        /// Body
        /// DB - database
        /// LOGIN - login
        /// USER - user
        /// FIN - finance
        /// STOCK - material(stock)
        /// Types
        /// S - string
        /// B - bool
        /// I - int
        /// D - double
        /// DT - DateTime
        /// SH - short
        /// </summary>
        public const string DS_DATA_SOURCE_S = "DS_DATA_SOURCE_S";
        public const string DS_INITIAL_CATALOG_S = "DS_INITIAL_CATALOG_S";
        public const string DS_USER_S = "DS_USER_S";
        public const string DS_PASSWORD_S = "DS_PASSWORD_S";
        public const string DS_STRING_S = "DS_STRING_S";
        //
        public const string LOGIN_USER_NAME_S = "LOGIN_USER_NAME_S";
        public const string LOGIN_USER_PASSWORD_S = "LOGIN_USER_PASSWORD_S";
        public const string LOGIN_USER_FIRM_S = "LOGIN_USER_FIRM_S";
        public const string LOGIN_USER_PERIOD_S = "LOGIN_USER_PERIOD_S"; 
        //
        public const string USER_LANG_MAIN_S = "USER_LANG_MAIN_S";
        public const string USER_LANG_ADDITIONAL_S = "USER_LANG_ADDITIONAL_S";
        //public const string USER_RECORDS_ONCE_I = "USER_RECORDS_ONCE_I";
        //
        //public const string STOCK_MAIN_WAREHOUSE_I = "STOCK_MAIN_WAREHOUSE_I";
        //public const string STOCK_PREVENT_NEQATIV_STOCK_B = "STOCK_PREVENT_NEQATIV_STOCK_B";
        //public const string STOCK_PREFERED_UNIT_I = "STOCK_PREFERED_UNIT_I";
        //public const string STOCK_DEF_TYPE_I = "STOCK_DEF_TYPE_I";
        //
        //public const string FIN_DEFAULT_CASH_CODE_S = "FIN_DEFAULT_CASH_CODE_S"; 
        //
        //public const string APP_DB_TABLE_EXCHANGE_S = "APP_DB_TABLE_EXCHANGE_S";
        //public const string APP_USE_TIRM_B = "APP_USE_TIRM_B";
        //public const string APP_COM_PORT_S = "APP_COM_PORT_S";
       // public const string APP_INTERVAL_IN_DAYS_I = "APP_INTERVAL_IN_DAYS_I";
        //


        //public const string APP_LANGS_S = "APP_LANGS_S";
        public const string APP_SQL_CONST_S = "APP_SQL_CONST_S";

        public const string APP_UI_DECIMALS_I = "APP_UI_DECIMALS_I";

        public const string MOB_CORRECT_NEGATIVE_STOCK_B = "MOB_CORRECT_NEGATIVE_STOCK_B";
        public const string MOB_CONTROL_NEGATIVE_STOCK_B = "MOB_CONTROL_NEGATIVE_STOCK_B";

    }
}

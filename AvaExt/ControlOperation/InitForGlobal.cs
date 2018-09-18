using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.Common;

using System.Globalization;
using AvaExt.Settings;
using AvaExt.Manual.Table;
using System.Threading;
using AvaExt.Translating.Tools;
using System.Reflection;

using System.ComponentModel;
using AvaExt.MyException;


namespace AvaExt.ControlOperation
{
    public abstract class InitForGlobal
    {
        public static void readSettings(object control, IEnvironment env, ISettings settings)
        {
            if (control != null && settings != null && env != null)
            {
                
                object[] items = ToolControl.destruct(control);
                foreach (object item in items)
                {
                    var glob_ = item as IControlGlobalInit;
                    if (glob_ != null)
                    {
                        if (!glob_.isGlobalInited())
                            glob_.globalRead(env, settings);

                    }
                }

            }
        }

        //dont write settings  in mobile
        public static void writeSettings(object control, IEnvironment env, ISettings settings)
        {
            if (control != null && env != null && settings != null)
            {
                object[] items = ToolControl.destruct(control);
                foreach (object item in items)
                {
                    var glob_ = item as IControlGlobalInit;
                    if (glob_ != null)
                        glob_.globalWrite(env, settings);
                }
            }
        }
        public static void read(object pObj, string pName, IEnvironment pEnv, ISettings pSettings)
        {
            if (pObj == null)
                return;

            if (string.IsNullOrEmpty(pName))
                return;

            try
            {

               // ToolMobile.log("object read settings [" + pName + "]");
 
                pSettings.enumarateFirst(pName);
                if (pSettings.isEnumerValid())
                    foreach (string property in pSettings.getAllAttrEnumer())
                    {
                       // ToolMobile.log("object read settings property [" + pName + "]/[" + property + "]");

                        string propertyObj = property.TrimEnd('_');

                        PropertyInfo pinf = pObj.GetType().GetProperty(propertyObj);
                        if (pinf != null)
                        {
                            object value = pinf.GetValue(pObj, null);
                            if (pinf.PropertyType.IsEnum)
                                value = Convert.ToInt32(value);
                            value = pSettings.getAttrEnumer(property, (pinf.PropertyType.IsEnum ? typeof(int) : pinf.PropertyType), value);
                            pinf.SetValue(pObj, value, null);
                        }
                        else
                        {
                            FieldInfo finf = pObj.GetType().GetField(propertyObj);
                            if (finf != null)
                            {
                                object value = finf.GetValue(pObj);
                                if (finf.FieldType.IsEnum)
                                    value = Convert.ToInt32(value);
                                value = pSettings.getAttrEnumer(property, (finf.FieldType.IsEnum ? typeof(int) : finf.FieldType), value);
                                finf.SetValue(pObj, value);
                            }
                        }

                    }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_RUNTIME, new object[] { pName }, exc);

            }
        }
        public static void write(object pObj, string pName, IEnvironment pEnv, ISettings pSettings)
        {
            if (pObj == null)
                return;
            if (string.IsNullOrEmpty(pName))
                return;
            try
            {
 
              

             //   ToolMobile.log("object write settings [" + pName + "]");

                pSettings.enumarateFirst(pName);
                if (pSettings.isEnumerValid())
                    foreach (string property in pSettings.getAllAttrEnumer()) if (property.EndsWith("__"))
                        {
                            //ToolMobile.log("object write settings property [" + pName + "]/[" + property + "]");

                            string propertyObj = property.TrimEnd('_');
                            PropertyInfo pinf = pObj.GetType().GetProperty(propertyObj); 
                            if (pinf != null)
                            {
                                object value = pinf.GetValue(pObj, null);
                                if (pinf.PropertyType.IsEnum)
                                    value = Convert.ToInt32(value);
                                pSettings.setEnumer(property, value);
                            }
                            else
                            {
                                FieldInfo finf = pObj.GetType().GetField(propertyObj);
                                if (finf != null)
                                {
                                    object value = finf.GetValue(pObj);
                                    if (finf.FieldType.IsEnum)
                                        value = Convert.ToInt32(value);
                                    pSettings.setEnumer(property, value);
                                }
                            }

                        }

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_RUNTIME, new object[] { pName }, exc);

            }
        }


    }


}

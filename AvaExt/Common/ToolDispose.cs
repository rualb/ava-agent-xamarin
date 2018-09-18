using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;

namespace Ava_Ext.Common
{
    public class ToolDispose
    {


        public static void dispose(object pObj)
        {

            try
            {
                if (pObj == null)
                    return;



                try
                {
                    var vg = pObj as Android.Views.ViewGroup;

                    if (vg != null)
                        vg.RemoveAllViews();

                }
                catch (Exception exc)
                {

                }
                try
                {
                    var v = pObj as Android.Views.View;

                    if (v != null)
                        v.DestroyDrawingCache();

                }
                catch (Exception exc)
                {

                }


                try
                {
                    var d_ = pObj as IDisposable;

                    if (d_ != null) d_.Dispose();

                }
                catch (Exception exc)
                {

                }
                try
                {
                    var d_ = pObj as Android.App.Activity;

                    if (d_ != null && d_.Window != null)
                    {
                        d_.Window.Dispose();
                    }

                }
                catch (Exception exc)
                {

                }

               

            }
            catch (Exception exc)
            {

            }

            // cleanObject(pObj);

        }

        public static void disposeControl(object pControl)
        {
            if (pControl == null)
                return;

            try
            {
                //add, destructed orlinks will be lost
                object[] arr_ = ToolControl.destruct(pControl);
                foreach (object o_ in arr_)
                    ToolDispose.dispose(o_);
            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);
            }
        }


        //static void cleanObject(object pObj)
        //{
        //    try
        //    {

        //        return;


        //        if (pObj == null)
        //            return;

        //        var flags =
        //            System.Reflection.BindingFlags.FlattenHierarchy |
        //          System.Reflection.BindingFlags.Public |
        //           System.Reflection.BindingFlags.NonPublic |
        //             System.Reflection.BindingFlags.Instance
        //           ;

        //        var type = pObj.GetType();






        //        {
        //            var props = type.GetProperties(flags);

        //            foreach (var p in props)
        //                if (p.CanWrite && !p.PropertyType.IsValueType)
        //                {
        //                    try
        //                    {
        //                        p.SetValue(pObj, null);
        //                    }
        //                    catch
        //                    {
        //                    }
        //                }

        //        }

        //        {
        //            var fields = type.GetFields(flags);
        //            foreach (var f in fields)
        //            {
        //                try
        //                {
        //                    if (!f.FieldType.IsValueType)
        //                        f.SetValue(pObj, null);
        //                }
        //                catch
        //                {
        //                }
        //            }

        //        }

        //        //events is in fields list

        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser;
using AvaExt.Common;
using AvaExt.Adapter;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common.Const;
using AvaExt.Formating;
using AvaExt.Translating.Tools;
using AvaExt.TableOperation;

namespace AvaExt.Adapter
{
    public class EditingTools
    {

        public delegate void ReferenceInformer(EditingTools pTool, object pLref);

        public ReferenceInformer handlerReferenceInformer;

        public IAdapterUser adapter;
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        public Type form;

        public EditingTools(IEnvironment pEnv)
        {
        }
        public EditingTools(IEnvironment pEnv, IAdapterUser pAdapter, Type pForm)
        {

            environment = pEnv;
            adapter = pAdapter;
            form = pForm;
        }
        public object delete()
        {
            object res = null;
            bool ok = false;
            try
            {
                environment.docBegin(adapter.getDataSet());
                adapter.delete();
                environment.beginBatch();
                res = adapter.update();
                environment.commitBatch();
                ok = true;
            }
            catch (Exception exc)
            {

                environment.rollbackBatch();
                environment.getExceptionHandler().setException(exc);
            }
            finally
            {
                environment.docEnd();
            }

            if (ok)
            {
                if (handlerReferenceInformer != null)
                    handlerReferenceInformer.Invoke(this, res);

            }

            return res;
        }
        public void view()
        {
            // form.setAdapter(adapter);
            // form.initForm();
            // form.reinitEditingForData();

            try
            {

                //
                environment.docBegin(adapter.getDataSet());
                ToolMobile.startForm(form, new string[] { "view" }, new string[] { XmlFormating.helper.format(true) });
            }
            catch (Exception exc)
            {
                environment.docEnd();
                environment.getExceptionHandler().setException(exc);

            }

        }
        public void edit()
        {

            try
            {
                environment.docBegin(adapter.getDataSet());
                ToolMobile.startForm(form);
            }
            catch (Exception exc)
            {
                environment.docEnd();
                environment.getExceptionHandler().setException(exc);

            }

        }

        //user need save
        public object save(Form pForm)
        {
            object res = null;
            bool ok = false;

            try
            {
                environment.beginBatch();
                res = adapter.update();
                environment.commitBatch();
                ok = true;
            }
            catch (Exception exc)
            {
                environment.rollbackBatch();
                environment.getExceptionHandler().setException(exc);
            }

         

            if (ok)
            {
                environment.docEnd();

                if (pForm != null)
                    pForm.Close();

                if (handlerReferenceInformer != null)
                    handlerReferenceInformer.Invoke(this, res);


           //     onSaved();



            }


            if (ok)
            {
                var ds = adapter.getDataSet();
                var code = (ds != null && ds.Tables.Count > 0 ? ds.Tables[0].TableName : "DUMMY");
                if (ds != null)
                {
                    ToolMobile.logRuntime(string.Format(
                        "Doc commited by [{0}] with id [{1}] and worked as [{2}]", 
                        code, 
                        ToolCell.isNull(res, "null").ToString(), 
                        adapter.getAdapterWorkState().ToString()
                        ));
              
                 }
            }

            return res;
        }

        //void onSaved()
        //{
        //    try
        //    {
        //        if (CurrentVersion.ENV.getEnvBool("SENDONSAVE", false))
        //        {
        //            var a = environment.toActivity("tool.data.export", null);
        //            if (a != null)
        //                a.done();

        //        }


        //    }
        //    catch (Exception exc)
        //    {

        //        environment.getExceptionHandler().setException(exc);
        //    }
        //}

        //user need cancel
        public void cancel(Form pForm)
        {
            ToolMsg.confirm(pForm, MessageCollection.T_MSG_COMMIT_CLOSE, () =>
            {

                environment.docEnd();

                if (pForm != null)
                    pForm.Close();

            }, null);
        }

    }
}

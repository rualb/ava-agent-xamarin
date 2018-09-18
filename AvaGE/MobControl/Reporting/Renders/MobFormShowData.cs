using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Reporting;
using System.IO;
using AvaExt.Translating.Tools;

using AvaExt.Settings;
using System.Text.RegularExpressions;
using Android.Widget;
using AvaExt.Common.Const;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Content;
using AvaAgent;

namespace AvaGE.MobControl.Reporting.Renders
{

    //orintation by user
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.User)]
    public class MobFormShowData : MobForm
    {

        public const string PRM_RENDER = "render";

        ReportRenderUtil _renderUtil;
        ReportRenderUtil renderUtil
        {
            get
            {

                if (_renderUtil == null)
                {
                    _renderUtil = Intent.GetSerializableExtra(PRM_RENDER) as ReportRenderUtil;

                }


                return _renderUtil;

            }
        }

        protected override string globalStoreName()
        {
            return "tool.showdata";
        }

        string _data = string.Empty;

        public MobFormShowData()
            : base(null, Resource.Layout.MobFormShowData)
        {
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.User;

        }


       // MobLabel cData { get { return FindViewById<MobLabel>(Resource.Id.cData); } }

        MobButton cBtnOk { get { return FindViewById<MobButton>(Resource.Id.cBtnOk); } }
        MobButton cBtnCancel { get { return FindViewById<MobButton>(Resource.Id.cBtnCancel); } }
        MobButton cBtnMenu { get { return FindViewById<MobButton>(Resource.Id.cBtnMenu); } }
        MobPanel cContext { get { return FindViewById<MobPanel>(Resource.Id.cContext); } }
    
         
        

        protected override void initAfterSettings()
        {
            base.initAfterSettings();

            cBtnCancel.Click += cBtnCancel_Click;
            cBtnOk.Click += cBtnOk_Click;


            RegisterForContextMenu(cContext);
            cBtnMenu.Click += cBtnMenu_Click;

            renderTo(cContext);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            //  base.OnCreateContextMenu(menu, v, menuInfo);


            {

                menu.Add(0, 1, 0, translate(WordCollection.T_SEND));

            }


        }
        public override bool OnContextItemSelected(IMenuItem item)
        {

            doCmd( item.ItemId);

            return base.OnContextItemSelected(item);
        }

        void doCmd(int pCmd)
        {
            try
            {
                switch (pCmd)
                {
                    case 1:
                        {

                            renderTo("share");
                        }

                        break;
                }


            }
            catch (Exception exc)
            {

                ToolMobile.setException(exc);
            }
        }

        void cBtnMenu_Click(object sender, EventArgs e)
        {
            this.OpenContextMenu(cContext);
        }


        void cBtnCancel_Click(object sender, EventArgs e)
        {
            userRequireCancel();
        }

        void cBtnOk_Click(object sender, EventArgs e)
        {
            userRequireSave();
        }


        void renderTo(object pTarget)
        {
            if (renderUtil != null)
                renderUtil.renderTo(pTarget);
        }

        protected virtual void userRequireSave()
        {

            renderTo(null);
        }






        protected override void OnNewIntent(Android.Content.Intent intent)
        {

            base.OnNewIntent(intent);
        }







    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.SQL.Dynamic;
using AvaExt.Translating.Tools;
using AvaExt.PagedSource;
using AvaExt.Common.Const;
using AvaGE.FormUserEditor;
using Android.App;
using AvaExt.Formating;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateVisible)]
    public partial class MobDataReferenceValueSelectBarcodeMatForm : MobDataReferenceValueSelectBarcodeForm
    {
        string __AMOUNT = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.AMOUNT);
        string __UNIT = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UNIT);
        string __UNITREF = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UNITREF);
        string __UINFO1 = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UINFO1);
        string __UINFO2 = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UINFO2);

        protected override string globalStoreName()
        {
            return ConstRefCode.materialBarcode;
        }

        public MobDataReferenceValueSelectBarcodeMatForm()
            : base()
        {
            Created += MobDataReferenceValueSelectBarcodeMatForm_Created;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Created -= MobDataReferenceValueSelectBarcodeMatForm_Created;

        }
       
        void MobDataReferenceValueSelectBarcodeMatForm_Created(object sender, EventArgs e)
        {
            int len = short.MaxValue;
            len = Math.Min(len, environment.getColumnLen(TableITEMS.TABLE, TableITEMS.BARCODE1));
            //  len = Math.Min(len, environment.getColumnLen(TableITEMS.TABLE, TableITEMS.BARCODE2));
            //  len = Math.Min(len, environment.getColumnLen(TableITEMS.TABLE, TableITEMS.BARCODE3));
            cBarcode.MaxLength = len;
        }
        protected override void returnData(DataRow pRow)
        {

            reset();
            if (pRow != null)
            {
                string barcode = pRow[TableDUMMY.VALUE].ToString().Trim();
                if (barcode != string.Empty)
                {

                    var p = new PARSER(barcode, CurrentVersion.ENV.getEnvString("WEIGHTBARCODE", ""));

                    barcode = p.CODE1;


                    IPagedSource ps = reference.getPagedSource();
                    ps.getBuilder().reset();
                    ps.getBuilder().beginWhereGroup();
                    ps.getBuilder().addParameterValue(TableITEMS.BARCODE1, barcode, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
                    if (CurrentVersion.ENV.getEnvBool("BARCODEMULTI", false))
                    {
                        ps.getBuilder().addParameterValue(TableITEMS.BARCODE2, barcode, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
                        ps.getBuilder().addParameterValue(TableITEMS.BARCODE3, barcode, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
                    }
                    ps.getBuilder().endWhereGroup();
                    DataTable matTab = ps.getAll();
                    if (matTab != null && matTab.Rows.Count > 0)
                    {

                        DataRow matRow = matTab.Rows[0];
                        DataRow matRowExt = selectUnit(matRow, barcode, p.WEIGHT);

                        base.returnData(matRowExt);

                    }
                    else
                    {
                        ToolMobile.playAlarmAndVibrate();


                        ToolMsg.show(this, MessageCollection.T_MSG_INVALID_BARCODE, null);

                    }
                }


            }


        }





        public class PARSER
        {

            const char prmPriceNumber = 'P';
            const char prmPriceDecimal = 'Q';
            const char prmWeithNumber = 'W';
            const char prmWeithDecimal = 'X';
            const char prmCode1 = 'M';
            const char prmCode2 = 'N';

            public string BARCODE { get; private set; }
            public string FORMAT { get; private set; }




            public static bool ISMACH(string pBarcode, string pFormat)
            {
                if (pBarcode == null || pFormat == null)
                    return false;

                if (pBarcode.Length != pFormat.Length || pBarcode.Length == 0 || pFormat.Length == 0)
                    return false;

                for (int i = 0; i < pBarcode.Length; ++i)
                {
                    var b = pBarcode[i];
                    var p = pFormat[i];
                    if (char.IsDigit(b) && char.IsDigit(p))
                    {
                        if (b != p)
                            return false;

                    }

                }

                return true;

            }

            public PARSER(string pBarcode, string pFormat)
            {
                BARCODE = pBarcode;

                if (ISMACH(pBarcode, pFormat))
                    FORMAT = pFormat;
                else
                    FORMAT = "".PadLeft(pBarcode.Length, 'M');

                init();
            }




            void init()
            {
                try
                {
                    if (BARCODE == null || FORMAT == null || BARCODE.Length != FORMAT.Length || BARCODE.Length == 0)
                        throw new Exception();


                    StringBuilder sbCode1_ = new StringBuilder();
                    StringBuilder sbCode2_ = new StringBuilder();

                    StringBuilder sbWeightN_ = new StringBuilder();
                    StringBuilder sbWeightD_ = new StringBuilder();
                    StringBuilder sbPriceN_ = new StringBuilder();
                    StringBuilder sbPriceD_ = new StringBuilder();
                    for (int i = 0; i < FORMAT.Length; ++i)
                    {
                        char f = FORMAT[i];
                        char v = BARCODE[i];
                        switch (f)
                        {
                            case prmCode1:
                                sbCode1_.Append(v);
                                break;
                            case prmCode2:
                                sbCode2_.Append(v);
                                break;
                            case prmWeithDecimal:
                                sbWeightD_.Append(v);
                                break;
                            case prmWeithNumber:
                                sbWeightN_.Append(v);
                                break;
                            case prmPriceDecimal:
                                sbPriceD_.Append(v);
                                break;
                            case prmPriceNumber:
                                sbPriceN_.Append(v);
                                break;
                        }

                    }

                    _WEIGHT = XmlFormating.helper.parseDouble((sbWeightN_.Length > 0 ? sbWeightN_.ToString() : "0") + "." + (sbWeightD_.Length > 0 ? sbWeightD_.ToString() : "0"));
                    _PRICE = XmlFormating.helper.parseDouble((sbPriceN_.Length > 0 ? sbPriceN_.ToString() : "0") + "." + (sbPriceD_.Length > 0 ? sbPriceD_.ToString() : "0"));
                    _CODE1 = sbCode1_.ToString();//.TrimStart('0');
                    _CODE2 = sbCode2_.ToString();//.TrimStart('0');
                }
                catch
                {
                    throw new Exception("Incorrect barcode [" + BARCODE + "] format [" + FORMAT + "]");
                }

            }
            double _WEIGHT;
            public double WEIGHT
            {
                get { return _WEIGHT; }
            }

            double _PRICE;
            public double PRICE
            {
                get { return _PRICE; }
            }
            string _CODE1;
            public string CODE1
            {
                get { return _CODE1 == null ? "" : _CODE1; }
            }

            string _CODE2;
            public string CODE2
            {
                get { return _CODE2 == null ? "" : _CODE2; }
            }

            public bool ISEMPTY
            {
                get { return string.IsNullOrEmpty(CODE1) && string.IsNullOrEmpty(CODE2); }

            }

        }





        void test(string barcode)
        {
            //try
            //{
            //    if (barcode == "0001")
            //    {

            //        var c = ToolMobile.getContextLast();
            //        if (c == null)
            //            return;

            //        c.Window.AddFlags(Android.Views.WindowManagerFlags.TurnScreenOn);


            //    }
            //}
            //catch (Exception exc)
            //{
            //    ToolMobile.setExceptionInner(exc);
            //}
        }








        DataRow selectUnit(DataRow pMatRow, string barcode, double amonunt)
        {
            DataRow matRowExt = MobUserEditorFormMaterialDoc.extendMatRow(pMatRow);

            string barcode1 = matRowExt[TableITEMS.BARCODE1].ToString();
            string barcode2 = matRowExt[TableITEMS.BARCODE2].ToString();
            string barcode3 = matRowExt[TableITEMS.BARCODE3].ToString();

            if (barcode == barcode1)
                MobUserEditorFormMaterialDoc.extendMatRow(matRowExt, 1, amonunt);
            else
                if (barcode == barcode2)
                    MobUserEditorFormMaterialDoc.extendMatRow(matRowExt, 2, amonunt);
                else
                    if (barcode == barcode3)
                        MobUserEditorFormMaterialDoc.extendMatRow(matRowExt, 3, amonunt);

            return matRowExt;
        }

    }
}


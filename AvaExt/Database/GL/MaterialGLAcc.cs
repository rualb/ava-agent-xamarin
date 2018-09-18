using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.SQL;

using AvaExt.Adapter.Tools;
using System.Collections;
using System.Data;

namespace AvaExt.Database.GL
{
    class MaterialGLAcc
    {
        static object mutex = new object();
        static DataTable table = null;
        static string trCode = "trCode";
        static string lnType = "lnType";
        static string glTrCode = "glTrCode";
        static string glType = "glType";
        static void initTable()
        {
            if (table != null)
                return;

            table = new DataTable();
            DataColumn pk1 = table.Columns.Add(trCode, typeof(short));
            DataColumn pk2 = table.Columns.Add(lnType, typeof(short));
            table.Columns.Add(glTrCode, typeof(short));
            table.Columns.Add(glType, typeof(short));
            table.PrimaryKey = new DataColumn[] { pk1, pk2 };
            //
            //Mat
            table.Rows.Add(new object[] { ConstDocTypeMaterial.retailSale, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleAccount });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.wholeSale, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleAccount });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.retailSaleReturn, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleReturn });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.wholeSaleReturn, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleReturn });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialPurchase, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_purchaseAccount });

            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialScrap, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_scrap });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialUsage, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_usage });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialInputFromProd, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_inputFromProduction });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialOpening, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_otherInput});
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedInput15, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefindeInput});
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedInput16, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefindeInput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedInput17, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefindeInput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedInput18, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefindeInput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedInput19, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefindeInput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedOutput20, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefinedOutput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedOutput21, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefinedOutput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedOutput22, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefinedOutput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedOutput23, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefinedOutput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.marerialDefinedOutput24, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_userDefinedOutput });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialExcess, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_cycleCountExcess });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialDeficit, ConstLineType.material, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_cycleCountDeficit });
            //Discount
            table.Rows.Add(new object[] { ConstDocTypeMaterial.retailSale, ConstLineType.discount, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleDiscount });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.wholeSale, ConstLineType.discount, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_saleDiscount });
            table.Rows.Add(new object[] { ConstDocTypeMaterial.materialPurchase, ConstLineType.discount, ConstCardGlRelationTrcode.itemCard, ConstCardGlRelationType.m_purchaseDiscount });
            //Promo
            //Surch
        }



        public static short[] translateForMaterial(ConstDocTypeMaterial docType, ConstLineType lineType)
        {
            lock (mutex)
            {
                short[] res = null;
                initTable();
                DataRow row = table.Rows.Find(new object[] { docType, lineType });
                if (row != null)
                {
                    res = new short[2];
                    res[0] = (short)row[glTrCode];
                    res[1] = (short)row[glType];
                }
                return res;
            }
        }
    }
}

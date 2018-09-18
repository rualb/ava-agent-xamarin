using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
 
using System.Text.RegularExpressions;

namespace AvaExt.Common
{
    public class ToolBarcodeScaner
    {

        public static void scanSimple(Action<string> barcode)
        {

            startScan(barcode);
 
        }

        async static void startScan(Action<string> barcode)
        {
            try
            {
                var opt = new ZXing.Mobile.MobileBarcodeScanningOptions();
                opt.PossibleFormats.Clear();
                opt.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13);
                opt.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8);
                opt.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128);


                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan(opt);

                if (result != null && barcode != null)
                    barcode.Invoke(result.Text);

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }

    }
}
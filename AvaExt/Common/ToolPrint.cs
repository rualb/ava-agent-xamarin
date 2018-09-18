using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.Translating.Tools;

using System.Threading;
using System.Reflection;
using AvaExt.Drivers;
using Android.Bluetooth;
using System.IO;
using Java.Util;
using System.Threading.Tasks;
using AvaExt.MyException;
using Android.Runtime;
using System.Net.Sockets;

namespace AvaExt.Common
{
    public class ToolPrint
    {
        static PrintHelper handler = new PrintHelper();

        static bool isPrinting = false;


        public static void print(string pData, Encoding pEnc)
        {
            handler.print(pData, pEnc);




        }



        static string convert(string pData)
        {
            //#barcode,12345678#
            //#cut#
            //#clean#
            //#center#

            char xchar = '#';

            StringReader sr = new StringReader(pData);
            StringBuilder sb = new StringBuilder();

            string line = null;
            while ((line = sr.ReadLine()) != null)
            {

                if (line.StartsWith(xchar.ToString()) && line.EndsWith(xchar.ToString()))
                {
                    line = line.Trim(xchar);
                    var arr = ToolString.explodeList(line);
                    if (arr.Length > 0)
                    {
                        string cmd = arr.Length > 0 ? arr[0] : null;
                        string arg1 = arr.Length > 1 ? arr[1] : null;
                        string arg2 = arr.Length > 2 ? arr[2] : null;
                        string arg3 = arr.Length > 3 ? arr[3] : null;

                        MemoryStream ms = new MemoryStream();
                        BinaryWriter bw = new BinaryWriter(ms);
                        switch (cmd)
                        {
                            case "barcode":
                                if (arg1 != null)
                                {
                                    PosPrinter.Reinit(bw);
                                    PosPrinter.OutCenter(bw);
                                    PosPrinter.OutHeight(bw, 60);
                                    PosPrinter.Barcode(bw, arg1);
                                    PosPrinter.Text(bw, arg1);
                                    PosPrinter.Reinit(bw);
                                }
                                else
                                    sb.AppendLine("error");
                                break;
                            case "cut":
                                {
                                    PosPrinter.Cut(bw);
                                }
                                break;
                            case "clean":
                                {
                                    PosPrinter.Reinit(bw);
                                }
                                break;
                            case "center":
                                {
                                    PosPrinter.OutCenter(bw);
                                }
                                break;


                        }

                        bw.Flush();
                        var newdata = ms.ToArray();

                        var newline = Encoding.ASCII.GetString(newdata);

                        sb.AppendLine(newline);
                    }


                }
                else
                    sb.AppendLine(line);

            }




            return sb.ToString();
        }



        class PrintHelper
        {
            interface IPrinter
            {

                void open();
                void close();
                bool writeLine(string data, bool pThrowError);
                bool write(string data);

            }

            class PrintHandlerTcp : IPrinter
            {

                public PrintHandlerTcp(Encoding pEnc)
                {
                    encoding = pEnc;
                }

                Encoding encoding;

                TcpClient mmSocket;

                Stream mmOutputStream { get { return mmSocket.GetStream(); } }
                Stream mmInputStream { get { return mmSocket.GetStream(); } }



                public void close()
                {



                    try
                    {
                        if (mmSocket != null)// && mmSocket.IsConnected)
                        {
                            mmSocket.Close();
                            mmSocket = null;
                        }
                    }
                    catch
                    {

                    }


                }









                internal bool tryOpen()
                {
                    try
                    {
                        if (mmSocket == null)
                            mmSocket = new TcpClient();

                        if (mmSocket != null && !mmSocket.Connected)
                        {

                            string addr_ = CurrentVersion.ENV.getPrinter();

                            string[] arr_ = ToolString.breakList(':', addr_);

                            mmSocket.Connect(arr_[0], int.Parse(arr_[1]));

                            return writeLine(" ", false);
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                    return false;
                }
                internal bool isOk()
                {
                    try
                    {

                        if (mmSocket != null && mmSocket.Connected)
                            return true;
                    }
                    catch
                    {
                        return false;

                    }
                    return false;
                }
                internal bool write(string pData, bool pThrowError)
                {
                    bool ok_ = false;
                    try
                    {
                        pData = convert(pData);

                        byte[] arr_ = encoding.GetBytes(pData);
                        // byte[] arr_ = Encoding.ASCII.GetBytes(pData);
                        IAsyncResult res_ = mmOutputStream.BeginWrite(arr_, 0, arr_.Length, new AsyncCallback(writeFinished), null);
                        res_.AsyncWaitHandle.WaitOne(5000);
                        ok_ = res_.IsCompleted;
                        //if (!ok_ ))
                        //    mmOutputStream.EndWrite(res_); 

                    }
                    catch (Exception exc)
                    {

                    }
                    if (pThrowError && !ok_)
                        throw new IOException();

                    return ok_;
                }


                internal void writeFinished(IAsyncResult res)
                {

                }

                public bool writeLine(string data, bool pThrowError)
                {

                    return write(data + "\r\n", pThrowError);
                }
                public void open()
                {

                    tryOpen();

                }






                public bool write(string pData)
                {

                    writeLine(pData, true);



                    return true;
                }


            }



            class PrintHandlerBT : IPrinter
            {

                public PrintHandlerBT(Encoding pEnc)
                {
                    encoding = pEnc;
                }

                Encoding encoding;

                BluetoothSocket mmSocket;

                Stream mmOutputStream { get { return mmSocket.OutputStream; } }
                Stream mmInputStream { get { return mmSocket.InputStream; } }



                public void close()
                {
                    //try
                    //{
                    //    if (mmOutputStream != null)
                    //    {
                    //        mmOutputStream.Flush();
                    //        mmOutputStream.Close();
                    //        //  mmOutputStream = null;
                    //    }
                    //}
                    //catch
                    //{

                    //}
                    //try
                    //{
                    //    if (mmInputStream != null)
                    //    {
                    //        mmInputStream.Close();
                    //        // mmInputStream = null;
                    //    }
                    //}
                    //catch
                    //{

                    //}
                    try
                    {
                        if (mmSocket != null)// && mmSocket.IsConnected)
                        {
                            mmSocket.Close();
                            mmSocket = null;
                        }
                    }
                    catch
                    {

                    }


                }


                internal BluetoothAdapter getAdapter()
                {
                    return BluetoothAdapter.DefaultAdapter;

                }

                internal bool openBT()
                {
                    BluetoothAdapter mBluetoothAdapter = getAdapter();
                    BluetoothDevice mmDevice = null;


                    if (mBluetoothAdapter == null)
                    {
                        ToolMobile.log("no bt adapter");

                        ToolMsg.show(null, MessageCollection.T_MSG_ERROR_CONNECTION + " [BLUETOOTH]", null);
                        return false;
                    }



                    if (!mBluetoothAdapter.IsEnabled)
                    {
                        ToolMobile.log("enable bt adapter");

                        mBluetoothAdapter.Enable();

                        for (int i = 0; i < 15; ++i)
                        {
                            if (mBluetoothAdapter.IsEnabled)
                                break;
                            Thread.Sleep(100);
                        }


                        if (!mBluetoothAdapter.IsEnabled)
                        {
                            ToolMsg.show(null, MessageCollection.T_MSG_ERROR_CONNECTION + " [BLUETOOTH]", null);
                            return false;
                        }
                    }

                    List<string> list = new List<string>();
                    foreach (BluetoothDevice dev_ in mBluetoothAdapter.BondedDevices)
                    {
                        list.Add(dev_.Name);

                        if (dev_.Name == CurrentVersion.ENV.getPrinter())
                        {
                            mmDevice = dev_;
                            break;
                        }

                    }



                    if (mmDevice == null)
                    {
                        string[] arr_ = list.ToArray();
                        ToolMsg.askList(null, arr_, (o, e) =>
                        {

                            if (e.Which >= 0 && e.Which < arr_.Length)
                            {
                                CurrentVersion.ENV.setPrinter(arr_[e.Which]);
                            }

                        });

                        // ToolMsg.show(null, MessageCollection.T_MSG_ERROR_NO_DATA + " BLUETOOTH = [" + CurrentVersion.ENV.getPrinter() + "]", null);
                        return false;
                    }

                    try
                    {


                        //mBluetoothAdapter.StartDiscovery();
                        //for (int i = 0; i < 10; ++i)
                        //{
                        //    if (mBluetoothAdapter.IsDiscovering)
                        //        break;
                        //    Thread.Sleep(100);

                        //}

                        // UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");


                        IntPtr createRfcommSocket = JNIEnv.GetMethodID(mmDevice.Class.Handle, "createRfcommSocket", "(I)Landroid/bluetooth/BluetoothSocket;");

                        IntPtr _socket = JNIEnv.CallObjectMethod(mmDevice.Handle, createRfcommSocket, new Android.Runtime.JValue(1));

                        mmSocket = Java.Lang.Object.GetObject<BluetoothSocket>(_socket, JniHandleOwnership.TransferLocalRef);

                        mBluetoothAdapter.CancelDiscovery();

                        mmSocket.Connect();

                        //           Method m = mmDevice.GetType().GetMethod("CreateRfcommSocket", new Type[] {int.class});
                        //tmp = (BluetoothSocket) m.invoke(device, 1);

                        // mmSocket = mmDevice.CreateRfcommSocketToServiceRecord(uuid);


                        // mmSocket.Connect();

                        //time out not supported
                        // mmInputStream.ReadTimeout = mmInputStream.WriteTimeout = mmOutputStream.ReadTimeout = mmOutputStream.WriteTimeout = 5000;
                        return true;
                    }
                    catch (Exception exc)
                    {
                        ToolMobile.log(exc.ToString());
                        ToolMobile.setExceptionInner(exc);
                    }
                    finally
                    {



                    }

                    return false;
                }






                internal bool tryOpen()
                {
                    try
                    {
                        if (mmSocket != null && !mmSocket.IsConnected)
                        {
                            mmSocket.Connect();

                            return writeLine(" ", false);
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                    return false;
                }
                internal bool isOk()
                {
                    try
                    {

                        if (mmSocket != null && mmSocket.IsConnected)
                            return true;
                    }
                    catch
                    {
                        return false;

                    }
                    return false;
                }
                internal bool write(string pData, bool pThrowError)
                {
                    bool ok_ = false;
                    try
                    {
                        pData = convert(pData);

                        byte[] arr_ = encoding.GetBytes(pData);
                        // byte[] arr_ = Encoding.ASCII.GetBytes(pData);
                        IAsyncResult res_ = mmOutputStream.BeginWrite(arr_, 0, arr_.Length, new AsyncCallback(writeFinished), null);
                        res_.AsyncWaitHandle.WaitOne(5000);
                        ok_ = res_.IsCompleted;
                        //if (!ok_ ))
                        //    mmOutputStream.EndWrite(res_); 

                    }
                    catch (Exception exc)
                    {

                    }
                    if (pThrowError && !ok_)
                        throw new IOException();

                    return ok_;
                }

                internal void writeFinished(IAsyncResult res)
                {

                }

                public bool writeLine(string data, bool pThrowError)
                {

                    return write(data + "\r\n", pThrowError);
                }
                public void open()
                {

                    openBT();

                }


                public bool write(string pData)
                {
                    foreach (string str_ in explode(pData))
                    {


                        writeLine(str_, true);

                    }

                    return true;
                }




            }


            static string[] explode(string pData)
            {
                List<string> l = new List<string>();
                StringReader sr = new StringReader(pData);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                    l.Add(line);

                return l.ToArray();


            }

            public void print(string pData, Encoding pEnc)
            {
                IPrinter handler = null;

                string printer_ = CurrentVersion.ENV.getPrinter();

                if (printer_.Contains(":"))
                    handler = new PrintHandlerTcp(pEnc);
                else
                    handler = new PrintHandlerBT(pEnc);




                try
                {



                    handler.open();

                    handler.writeLine(" ", true);

                    handler.write(pData);

                    // handler.writeLine(" ", true);

                    //

                    //
                    if (CurrentVersion.ENV.getEnvBool("AUTOCUT", false))
                    {


                        string GS = Convert.ToString((char)29);
                        string ESC = Convert.ToString((char)27);

                        string COMMAND = "";
                        COMMAND = ESC + "@";
                        COMMAND += GS + "V" + (char)1;

                        handler.write(COMMAND);
                    }

                }
                catch (Exception exc)
                {
                    ToolMobile.setException(exc);
                }
                finally
                {
                    handler.close();
                }



            }
        }




        static class PosPrinter
        {
            public static void Enlarged(BinaryWriter bw, string text)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)33);
                bw.Write((byte)32);
                bw.Write(text);
                bw.Write(AsciiControlChars.Newline);
            }
            public static void High(BinaryWriter bw, string text)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)33);
                bw.Write((byte)16);
                bw.Write(text); //Width,enlarged
                bw.Write(AsciiControlChars.Newline);
            }
            public static void LargeText(BinaryWriter bw, string text)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)33);
                bw.Write((byte)48);
                bw.Write(text);
                bw.Write(AsciiControlChars.Newline);
            }
            public static void OutHeight(BinaryWriter bw, int height)
            {
                bw.Write(AsciiControlChars.GroupSeparator);
                bw.Write((byte)0x68);
                bw.Write((byte)height);


            }
            public static void OutLeft(BinaryWriter bw)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)0x61);
                bw.Write((byte)0x00);


            }
            public static void OutCenter(BinaryWriter bw)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)0x61);
                bw.Write((byte)0x01);


            }
            public static void OutRight(BinaryWriter bw)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)0x61);
                bw.Write((byte)0x02);


            }


            public static void FeedLines(BinaryWriter bw, int lines)
            {
                bw.Write(AsciiControlChars.Newline);
                if (lines > 0)
                {
                    bw.Write(AsciiControlChars.Escape);
                    bw.Write('d');
                    bw.Write((byte)lines - 1);
                }
            }



            public static void NormalFont(BinaryWriter bw, string text, bool line = true)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write((byte)33);
                bw.Write((byte)8);
                bw.Write(" " + text);
                if (line)
                    bw.Write(AsciiControlChars.Newline);
            }
            public static void Reinit(BinaryWriter bw)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write('@');
            }

            public static void Cut(BinaryWriter bw)
            {
                bw.Write(AsciiControlChars.GroupSeparator);
                bw.Write('V');
                bw.Write((byte)1);

            }


            public static void Text(BinaryWriter bw, string text, bool line = true)
            {
                bw.Write(text);
                if (line)
                    bw.Write(AsciiControlChars.Newline);
            }


            public static void Barcode(BinaryWriter bw, string text, string type = null)
            {


                if (type == null)
                    type = "code93"; //for thermal printers


                switch (type.ToLowerInvariant())
                {
                    case "code93":
                        {

                            byte[] contents = Encoding.ASCII.GetBytes(text);


                            byte[] formats = new byte[] { AsciiControlChars.GroupSeparator, (byte)0x6b, (byte)0x48, (byte)contents.Length };


                            byte[] bytes = new byte[formats.Length + contents.Length];


                            Array.Copy(formats, 0, bytes, 0, formats.Length);
                            Array.Copy(contents, 0, bytes, formats.Length, contents.Length);




                            bw.Write(bytes);

                        }
                        break;
                    case "code128":
                        {




                            byte[] contents = Encoding.ASCII.GetBytes(text);


                            byte[] formats = new byte[] { AsciiControlChars.GroupSeparator, (byte)0x6b, (byte)0x49, (byte)contents.Length };


                            byte[] bytes = new byte[formats.Length + contents.Length];


                            Array.Copy(formats, 0, bytes, 0, formats.Length);
                            Array.Copy(contents, 0, bytes, formats.Length, contents.Length);




                            bw.Write(bytes);



                        }
                        break;


                }





            }






            public static class AsciiControlChars
            {
                /// <summary>
                /// Usually indicates the end of a string.
                /// </summary>
                public const byte Nul = (byte)0x00;

                /// <summary>
                /// Meant to be used for printers. When receiving this code the 
                /// printer moves to the next sheet of paper.
                /// </summary>
                public const byte FormFeed = (byte)0x0C;

                /// <summary>
                /// Starts an extended sequence of control codes.
                /// </summary>
                public const byte Escape = (byte)0x1B;

                /// <summary>
                /// Advances to the next line.
                /// </summary>
                public const byte Newline = (byte)0x0A;

                /// <summary>
                /// Defined to separate tables or different sets of data in a serial
                /// data storage system.
                /// </summary>
                public const byte GroupSeparator = (byte)0x1D;

                /// <summary>
                /// A horizontal tab.
                /// </summary>
                public const byte HorizontalTab = (byte)0x09;


                /// <summary>
                /// Vertical Tab
                /// </summary>
                public const byte VerticalTab = (byte)0x11;


                /// <summary>
                /// Returns the carriage to the start of the line.
                /// </summary>
                public const byte CarriageReturn = (byte)0x0D;

                /// <summary>
                /// Cancels the operation.
                /// </summary>
                public const byte Cancel = (byte)0x18;

                /// <summary>
                /// Indicates that control characters present in the stream should
                /// be passed through as transmitted and not interpreted as control
                /// characters.
                /// </summary>
                public const byte DataLinkEscape = (byte)0x10;

                /// <summary>
                /// Signals the end of a transmission.
                /// </summary>
                public const byte EndOfTransmission = (byte)0x04;

                /// <summary>
                /// In serial storage, signals the separation of two files.
                /// </summary>
                public const byte FileSeparator = (byte)0x1C;

            }
        }

    }
}



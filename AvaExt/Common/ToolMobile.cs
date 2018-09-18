using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AvaExt.AndroidEnv.ApplicationBase;
using Android.Content;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.MyException;
using AvaExt.MyLog;
using AvaExt.ControlOperation;
using AvaExt.Translating.Tools;
using Android.App;
using AvaExt.Settings;
using Android.Views;
using AvaExt.Formating;
using Android.Media;

namespace AvaExt.Common
{
    public class ToolMobile
    {
        static IHandlerLog handlerLog = new ImplHandlerLog(null);
        static IHandlerException handlerException = new ImplHandlerException(null, handlerLog);
        static IHandlerException handlerExceptionInner = new ImplHandlerException(null, handlerLog, false);
        static IEnvironment _environment;


        public static void start()
        {
            try
            {



                string tmp = getTmpDir();

                if (Directory.Exists(tmp))
                    Directory.Delete(tmp, true);

                ToolMobile.createDir(tmp);



            }
            catch (Exception exc)
            {

                ToolMobile.setExceptionInner(exc);
            }
        }


        public static string getCurrentDataId()
        {
            return _environment.getSysSettings().getString(SettingsSysMob.MOB_USR_DATA_ID, string.Empty);
        }


        public static bool isEnvInited()
        {
            return _environment != null;

        }

        public static IEnvironment getEnvironment()
        {

            if (_environment == null)
            {
                log("Env is null");


                
                var app_ = Application.Context as ApplicationExt;
                if (app_ == null)
                    setExceptionInner(new Exception("Application not impliment ApplicationExt class"));
                else
                    app_.startEnv();

            }

            return _environment;
        }

        public static void restartEnvironment()
        {
            try
            {
                ToolMobile.setEnvironment(null);
            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);

            }
            try
            {
                getEnvironment();
            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);

            }

        }

        public static bool canPayment()
        {
            var e = getEnvironment();

            if (e == null)
                return false;

            return e.getSysSettings().getBool(SettingsSysMob.MOB_SYS_CANPAYMENT, true);
        }
        public static bool isReader()
        {

            var e = getEnvironment();

            if (e == null)
                return true;


            var nr_ = e.getSysSettings().getShort(SettingsSysMob.MOB_SYS_AGENT_ID, 0);

            if ((nr_ >= 900 && nr_ <= 999) || nr_ == 0)
                return true;

            var reader_ = e.getSysSettings().getBool(SettingsSysMob.MOB_SYS_ISREADER, false);

            if (reader_)
                return true;

            return false;

        }


        public static void setEnvironment(IEnvironment pEnv)
        {
            if (_environment != null)
            {
                var x = _environment;
                _environment = null;
                x.Dispose();
            }

            _environment = pEnv;

            if (_environment == null)
                ToolMobile.log("environment stoped");
            else
                ToolMobile.log("environment starting");
        }

        //static ActivityExt _context;
        //public static ActivityExt getContext()
        //{
        //    return _context;
        //}
        //public static void setContext(ActivityExt pEnv)
        //{
        //    _context = pEnv;
        //}
        public static ActivityExt getContextLast()
        {
            return ActivityExt.getActivityExtLast();
        }


        public static string curDir()
        {
            return getRootPath();
        }

        public static string getTmpDir()
        {

            return Path.Combine(curDir(), "tmp");
        }
        public static string getTmpFile(string pFileName = null)
        {
            if (string.IsNullOrEmpty(pFileName))
                pFileName = DateTime.Now.Ticks.ToString();

            return Path.Combine(getTmpDir(), pFileName);
        }

        public static string getFsOrResourceText(string pDir, string pFile)
        {

            var full = Path.Combine(pDir, pFile);

            full = full.Replace('\\', '/'); //has bug

            pDir = Path.GetDirectoryName(full);
            pFile = Path.GetFileName(full);

            try
            {


                if (existsFile(full))
                    return readFileText(full);

                return AvaRes.ToolRes.getFileText(pDir, pFile);


            }
            catch (Exception exc)
            {
                throw new Exception("Error on try use:" + full, exc);
            }
        }

        public static void writeFileData(string file, byte[] data)
        {
            File.WriteAllBytes(getFullPath(file), data);
        }
        public static byte[] readFileData(string file)
        {
            return File.ReadAllBytes(getFullPath(file));
        }

        public static void writeFileText(string dir, string file, string data)
        {
            writeFileText(Path.Combine(dir, file), data);
        }
        public static void writeFileText(string file, string data)
        {
            File.WriteAllText(getFullPath(file), data);
        }

        public static void appendFileText(string dir, string file, string data)
        {
            appendFileText(Path.Combine(dir, file), data);
        }
        public static void appendFileText(string file, string data)
        {
            File.AppendAllText(getFullPath(file), data);
        }
        public static string readFileText(string file)
        {
            return File.ReadAllText(getFullPath(file));
        }
        public static string readFileTextByCache(string file)
        {
            file = getFullPath(file);
            string fileId = "file:://" + file;

            string val_ = getEnvironment().getStateRuntime(fileId) as string;
            if (val_ == null)
            {
                val_ = File.ReadAllText(getFullPath(file));

                if (!CurrentVersion.isDebug())
                    getEnvironment().setStateRuntime(fileId, val_);
            }
            return val_;
        }


        public const string Name = "AvaAgent";



        public static string getRootPath()
        {
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Name);
        }

        public static string getAsmRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(typeof(ToolMobile)).Location);
        }

        public static string getFullPath(string pPath)
        {
            return correctPath(pPath);
        }
        public static string getFullPath(string pDir, string pName)
        {
            return getFullPath(Path.Combine(pDir, pName));
        }

        public static string correctPath(string pPath)
        {

            pPath = pPath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

            if (!Path.IsPathRooted(pPath))
                pPath = Path.Combine(getRootPath(), pPath);

            pPath = Path.GetFullPath(pPath);


            return pPath;
        }
        public static void deleteFile(string pFile)
        {
            File.Delete(getFullPath(pFile));

        }
        public static void deleteFile(string pDir, string pFile)
        {
            File.Delete(getFullPath(pDir, pFile));

        }
        public static bool existsFile(string pDir, string pFile)
        {
            return existsFile(Path.Combine(pDir, pFile));
        }
        public static bool existsFile(string pFile)
        {
            return File.Exists(getFullPath(pFile));

        }

        public static bool existsDir(string pDir)
        {
            return Directory.Exists(getFullPath(pDir));

        }

        public static void deleteDir(string pDir)
        {
            Directory.Delete(getFullPath(pDir), true);
        }

        public static string[] getFiles(string pDir)
        {
            string[] arr = Directory.GetFiles(getFullPath(pDir));
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = Path.GetFileName(arr[i]);
            return arr;
        }

        public static void createDir(string pDir)
        {

            Directory.CreateDirectory(getFullPath(pDir));
        }

        public static void startForm(Type type)
        {
            startForm(type, (string[])null, (string[])null);
        }

        public static void startForm(Type type, string[] args, string[] vals)
        {
            try
            {

                ActivityExt x = getContextLast();
                Intent intent = new Intent(x, type);
                if (args != null && vals != null)
                    for (int i = 0; i < Math.Min(args.Length, vals.Length); ++i)
                        intent.PutExtra(args[i], vals[i]);

                x.StartActivity(intent);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
                throw new MyExceptionInner(exc.Message, exc);

            }
        }
        public static void startForm(Type type, string[] args, Java.IO.ISerializable[] vals)
        {


            try
            {
                ActivityExt x = getContextLast();
                Intent intent = new Intent(x, type);
                if (args != null && vals != null)
                    for (int i = 0; i < Math.Min(args.Length, vals.Length); ++i)
                        intent.PutExtra(args[i], vals[i]);

                x.StartActivity(intent);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
                throw new MyExceptionInner(MessageCollection.T_MSG_ERROR_INNER, exc);

            }
        }
        public static void startFormForResult(Type type)
        {
            try
            {

                ActivityExt x = getContextLast();

                Intent intent = new Intent(x, type);

                x.StartActivityForResult(intent, 0);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
                throw new MyExceptionInner(MessageCollection.T_MSG_ERROR_INNER, exc);

            }
        }

        public static string getFromTag(Android.Views.View obj, string arg)
        {
            if (obj == null)
                throw new ArgumentNullException();

            string tag_ = ToolCell.isNull(obj.Tag, string.Empty).ToString();
            string val_ = string.Empty;
            if (tag_ != null)
                val_ = ToolObjectName.getArgValue(tag_, arg);

            return val_;
        }

        public static string getFromTagName(Android.Views.View obj)
        {
            string name = getFromTag(obj, ConstCmdLine.name);

            // if (string.IsNullOrEmpty(name))
            //    throw new Exception("Object with tag [" + ToolCell.isNull(obj.Tag, string.Empty) + "] hasnt name");

            return name == null ? string.Empty : name;
        }

        public static object[] getChilds(Android.Views.View pView)
        {
            List<object> list = new List<object>();
            if (pView != null)
                if (pView as IIndestructable == null)
                {
                  //  ToolMobile.log("getChilds form [" + pView.GetType().FullName + "]");

                    Android.Views.ViewGroup g = pView as Android.Views.ViewGroup;
                    if (g != null)
                    {
                        for (int i = 0; i < g.ChildCount; ++i)
                        {
                            View v = g.GetChildAt(i);

                         //   ToolMobile.log("getChilds added child [" + v.GetType().FullName + "]");

                            list.Add(v);

                        }
                    }
                    else
                    {
                        //ToolMobile.log("getChilds not for ViewGroup [" + pView.GetType().FullName + "]");
                    }
                }
            return list.ToArray();
        }
        public static void setException(Exception pExc)
        {
            if (pExc == null)
                return;

            if (getEnvironment() != null)
                getEnvironment().getExceptionHandler().setException(pExc);
            else
                handlerException.setException(pExc);

        }
        public static void setExceptionInner(Exception pExc)
        {
            if (pExc == null)
                return;

            handlerExceptionInner.setException(pExc);

        }

        public static void setRuntimeMsg(string pMsg)
        {
            if (pMsg == null)
                return;

            handlerLog.set(pMsg);

        }

        public static double getDpFromPx(int px)
        {

            return px / Application.Context.Resources.DisplayMetrics.Density;
        }

        public static int getPxFromDp(double dp)
        {
            return (int)(dp * Application.Context.Resources.DisplayMetrics.Density);
        }

        static int decimals_ = -1;
        public static double mathRound(double value)
        {
            int round_ = 3;

            if (decimals_ < 0) //notinited
                if (getEnvironment() != null) //try init
                    decimals_ = getEnvironment().getAppSettings().getInt(SettingsNamesApp.APP_UI_DECIMALS_I, 3);

            if (decimals_ >= 0) //inited
                round_ = decimals_;

            return Math.Round(value, round_);


        }

        public static void logRuntime(string msg)
        {

            handlerLog.set(msg);
        }

        public static void log(string msg)
        {

            if (!CurrentVersion.ENV.isDebugMode())
                return;

            handlerLog.set(msg);
        }
        public static void playAlarmAndVibrate()
        {
            playAlarm();
            vibrate();
        }
        public static void playAlarm()
        {
            try
            {
                var n = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var last_ = getContextLast();
                if (last_ != null)
                {
                    Ringtone r = RingtoneManager.GetRingtone(last_, n);
                    r.Play();
                }
            }
            catch (Exception e)
            {
                setExceptionInner(e);
            }
        }



        public static void vibrate()
        {
            try
            {
                var last_ = getContextLast();
                if (last_ != null)
                {
                    var v = last_.GetSystemService(Context.VibratorService) as Android.OS.Vibrator;
                    if (v != null)
                        v.Vibrate(500);
                }
            }
            catch (Exception e)
            {
                setExceptionInner(e);
            }
        }
    }
}

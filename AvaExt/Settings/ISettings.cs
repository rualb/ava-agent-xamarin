using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.FileSystem;
using AvaExt.Common;

namespace AvaExt.Settings
{
    [Flags]
    public enum SettingsOptions
    {
        none = 0,
        /// <summary>
        /// check for [var::*] [sys::*] [app::*] [lang::*]
        /// </summary>
        convert = 1,
        save = 2

    }

    public interface ISettings
    {

        void setEnvironment(IEnvironment pEnv);
        SettingsOptions getOptions();
        void setOptions(SettingsOptions pOptions);
        bool getOption(SettingsOptions pOption);
        void setOption(SettingsOptions pOption);

        bool enumarate();
        void enumarateFirst(string name);
        bool enumarate(string name);
        void enumarateFirstAttr(string attr, string value);
        void enumarateAttr(string attr, string value);

        bool moveNext();


        #region Enumeration Methods

        bool isEnumerValid();
        string getNameEnumer();

        void deleteEnumer();
        void clearEnumer();
        void deleteChildsEnumer(); 
       // string getInnerStringEnumer();
       // void setInnerStringEnumer(string value);

        void setEnumer(string attr, object value);
        void setEnumer(object value);

        object getEnumer(Type type);
        string getStringEnumer();
        Type getTypeEnumer();
        int getIntEnumer();
        short getShortEnumer();
        DateTime getDateTimeEnumer();
        double getDoubleEnumer();
        bool getBoolEnumer();

        object getEnumer(Type type, object defVal);
        string getStringEnumer(string defVal);
        Type getTypeEnumer(Type defVal);
        int getIntEnumer(int defVal);
        short getShortEnumer(short defVal);
        DateTime getDateTimeEnumer(DateTime defVal);
        double getDoubleEnumer(double defVal);
        bool getBoolEnumer(bool defVal);

        object getAttrEnumer(string attr, Type type);
        string getStringAttrEnumer(string attr);
        Type getTypeAttrEnumer(string attr);
        int getIntAttrEnumer(string attr);
        short getShortAttrEnumer(string attr);
        DateTime getDateTimeAttrEnumer(string attr);
        double getDoubleAttrEnumer(string attr);
        bool getBoolAttrEnumer(string attr);


        object getAttrEnumer(string attr, Type type, object defVal);
        string getStringAttrEnumer(string attr, string defVal);
        Type getTypeAttrEnumer(string attr, Type defVal);
        int getIntAttrEnumer(string attr, int defVal);
        short getShortAttrEnumer(string attr, short defVal);
        DateTime getDateTimeAttrEnumer(string attr, DateTime defVal);
        double getDoubleAttrEnumer(string attr, double defVal);
        bool getBoolAttrEnumer(string attr, bool defVal);

        string[] getListAttrEnumer(string attr);
        string[] getListAttrEnumer(string attr, string[] def);
        void setListEnumer(string[] vals);
        void setListAttrEnumer(string attr, string[] vals);

        string[] getAllAttrEnumer();
        ISettings forkEnumer();

        #endregion



        object get(string name, Type type);
        string getString(string name);
        Type getType(string name);
        int getInt(string name);
        short getShort(string name);
        DateTime getDateTime(string name);
        double getDouble(string name);
        bool getBool(string name);

        object get(string name, Type type, object defVal);
        string getString(string name, string defVal);
        Type getType(string name, Type defVal);
        int getInt(string name, int defVal);
        short getShort(string name, short defVal);
        DateTime getDateTime(string name, DateTime defVal);
        double getDouble(string name, double defVal);
        bool getBool(string name, bool defVal);



        object getAttr(string name, string attr, Type type);
        string getStringAttr(string name, string attr);
        Type getTypeAttr(string name, string attr);
        int getIntAttr(string name, string attr);
        short getShortAttr(string name, string attr);
        DateTime getDateTimeAttr(string name, string attr);
        double getDoubleAttr(string name, string attr);
        bool getBoolAttr(string name, string attr);

        object getAttr(string name, string attr, Type type, object defVal);
        string getStringAttr(string name, string attr, string defVal);
        Type getTypeAttr(string name, string attr, Type defVal);
        int getIntAttr(string name, string attr, int defVal);
        short getShortAttr(string name, string attr, short defVal);
        DateTime getDateTimeAttr(string name, string attr, DateTime defVal);
        double getDoubleAttr(string name, string attr, double defVal);
        bool getBoolAttr(string name, string attr, bool defVal);




        object[][] getArr(string name, Type[] type);
        object[][] getArr(string name, string arrName, Type[] type);
        object[][] getArrAttr(string name, string[] attr, Type[] type);
        object[][] getArrAttr(string name, string arrName, string[] attr, Type[] type);

        string[][] getArr(string name);
        string[][] getArr(string name, string arrName);
        string[][] getArrAttr(string name, string[] attr);
        string[][] getArrAttr(string name, string arrName, string[] attr);

        void setArr(string name, object[] value);
        void setArr(string name, string arrName, object[] value);
        void setArrAttr(string name, string[] attr, object[][] value);
        void setArrAttr(string name, string arrName, string[] attr, object[][] value);


        string[] getList(string name);
        string[] getListAttr(string name, string attr);
        string[] getList(string name, string[] def);
        string[] getListAttr(string name, string attr, string[] def);

        void setList(string name, string[] vals);
        void setListAttr(string name, string attr, string[] vals);

        void add();
        void add(string name);
        void addAfterEnum();
        void addAfterEnum(string name);
        void addFragment(string data);
        void set(string name, object value);
        void set(string name, string attr, object value);


        string[] getAllSettings();
        string[] getAllAttr(string name);

        void deleteEnumerAttr(string attr); 

        /// <summary>
        /// create name if not exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISettings fork(string name);
        ISettings fork();

        void clear();
        void deleteChilds(); 
        void delete(string name);
        bool hasParameter(string name);
        //string getInnerString(string name);
        //void setInnerString(string name, string text);

        string format();
        string formatEnumer();
        string formatTopNodes();

        void setFlagSourceUpdate(bool pFlag);
        bool getFlagSourceUpdate();
        void save();
    }
}

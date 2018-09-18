using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using System.CodeDom.Compiler;

using System.Reflection;
using AvaExt.Translating.Tools;

namespace AvaExt.Expression
{
    //public class ImplExpEvaluator : IEvaluator
    //{
    //    public const string varName = "var";
    //    public const string varName2 = "var2";
    //    public const string varName3 = "var3";
    //    public const string varName4 = "var4";
    //    const string colRESULT = "RESULT";
    //    string[] vars;
 
    //    object[] values;
     

    //    DataTable tableEval = new DataTable("EVAL");
    //    DataRow rowEval;


    //    int getIndex(string name)
    //    {

    //        return Array.IndexOf<string>(vars, name);
    //    }


    //    public ImplExpEvaluator(string pText, string[] pVars)
    //    {
    //        tableEval.Rows.Add(rowEval = tableEval.NewRow());
    //        vars = pVars;
    //        values = new object[vars.Length];
    //        for (int i = 0; i < vars.Length; ++i)
    //            tableEval.Columns.Add(vars[i], typeof(object));
    //        tableEval.Columns.Add(colRESULT, typeof(object), pText);
    //    }

    //    public object getResult()
    //    {
    //        try
    //        {
    //            for (int i = 0; i < vars.Length; ++i)
    //                rowEval[vars[i]] = values[i];
    //            return rowEval[colRESULT];
    //        }
    //        catch (Exception exc)
    //        {
    //            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
    //        }
    //    }

    //    public void setVar(string pName, object pValue)
    //    {
    //        try
    //        {
    //            values[getIndex(pName)] = pValue;
    //        }
    //        catch (Exception exc)
    //        {
    //            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
    //        }
    //    }
    //    public void setVar(string[] pNames, object[] pValues)
    //    {
    //        try
    //        {
    //            for (int i = 0; i < pNames.Length; ++i)
    //                values[getIndex(pNames[i])] = pValues[i];
    //        }
    //        catch (Exception exc)
    //        {
    //            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
    //        }
    //    }
    //    public void setVar(object pValue)
    //    {
    //        try
    //        {
    //            values[getIndex(varName)] = pValue;
    //        }
    //        catch (Exception exc)
    //        {
    //            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
    //        }
    //    }
    //    public void setVarAll(object[] pValues)
    //    {
    //        try
    //        {
    //            pValues.CopyTo(values, 0);
    //        }
    //        catch (Exception exc)
    //        {
    //            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
    //        }
    //    }
    //}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Expression;
using AvaExt.MyException;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.Common;

namespace AvaExt.TableOperation.CellAutomation
{
    //public class CellAutomationSpaceExp : CellAutomationSpace
    //{

    //    public CellAutomationSpaceExp(
    //        string[] pColumnsVars,
    //        object pTopOperDef,
    //        string pTopAnyExp,
    //        string pTopTotalExp,
    //        string pSubCorrectExp,
    //        string[] pSubCols,
    //        string[] pSubAnyExp,
    //        IRowsSelector pSelectorTop,
    //        IRowsSelector pSelectorBot)
    //        : base(
    //            pColumnsVars,
    //            pTopOperDef,
    //            null,
    //            null,
    //            null,
    //            pSubCols,
    //            null,
    //            pSelectorTop,
    //            pSelectorBot
    //            )
    //    {
    //        //
    //        evalOnTopAny = new ImplExpEvaluator(pTopAnyExp, columnsVars);
    //        evalOnTopAnyResult = new ImplExpEvaluator(
    //            pTopTotalExp,
    //            new string[] { ImplExpEvaluator.varName, ImplExpEvaluator.varName2 });
    //        evalOnSubCorrect = new ImplExpEvaluator(
    //            pSubCorrectExp,
    //            ToolArray.merge<string>(columnsVars, new string[] { ImplExpEvaluator.varName }));
    //        evalOnSubCalc = new IEvaluator[pSubAnyExp.Length];

    //        for (int i = 0; i < pSubAnyExp.Length; ++i)
    //            evalOnSubCalc[i] = new ImplExpEvaluator(
    //                pSubAnyExp[i],
    //                ToolArray.merge<string>(columnsVars, new string[] { ImplExpEvaluator.varName }));

    //    }



       


    //}

}


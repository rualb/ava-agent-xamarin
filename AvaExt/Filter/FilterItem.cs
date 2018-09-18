using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Filter
{
    public class FilterItem
    {
        public FilterItem(String pFiled)
        {
            compareBool = ConstBoolOperation.OR;
            position = ConstPosition.BEGIN;
            search = String.Empty;
            field = pFiled;
            compare = ConstCompareOperation.like;
        }
        public FilterItem(String pFiled,ConstBoolOperation oper)
        {
            compareBool = oper;
            position = ConstPosition.BEGIN;
            search = String.Empty;
            field = pFiled;
            compare = ConstCompareOperation.like;
        }
        public FilterItem(String pFiled,String pSearch,ConstCompareOperation pCompare, ConstBoolOperation pOper,ConstPosition pPos)
        {
            compareBool = pOper;
            position = pPos;
            search = pSearch;
            field = pFiled;
            compare = pCompare;
        }
        public ConstCompareOperation compare;
        public ConstBoolOperation compareBool;
        public ConstPosition position;
        public String search;
        public String field;
        
        
    }
}

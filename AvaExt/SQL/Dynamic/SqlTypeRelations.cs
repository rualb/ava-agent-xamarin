using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.SQL.Dynamic
{
    public enum SqlTypeRelations : int
    {
        undef = 0,
        equal = 1, // =
        greater = 2, // >
        less = 3, // <
        greaterEqual = 4,// >=
        lessEqual = 5,// <=
        notEqual = 6,// !=
        like = 7,// like

        boolAnd = 8, //AND
        boolOr = 9, //OR

        sortAsc = 10,  //ASC
        sortDesc = 11, //DESC

        groupBegin = 12, //  ( 
        groupEnd = 13 //  ) 

         
    }
}

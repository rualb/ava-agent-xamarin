using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;

namespace AvaExt.SQL.Dynamic
{
    public class SqlTypeRelationsString
    {
        public const string equal = "=";
        public const string greater = ">";
        public const string less = "<";
        public const string greaterEqual = ">=";
        public const string lessEqual = "<=";
        public const string notEqual = "<>";
        public const string like = "LIKE";
        public const string boolAnd = "AND";
        public const string boolOr = "OR";

        public const string sortAsc = "ASC";
        public const string sortDesc = "DESC";

        public const string groupBegin = "(";
        public const string groupEnd = ")";  

        public static string convert(SqlTypeRelations rel)
        {
            string res = string.Empty; 
            switch ((int)rel)
            {
                case (int)SqlTypeRelations.equal:
                    res = SqlTypeRelationsString.equal;
                    break;
                case (int)SqlTypeRelations.greaterEqual:
                    res = SqlTypeRelationsString.greaterEqual;
                    break;
                case (int)SqlTypeRelations.greater:
                    res = SqlTypeRelationsString.greater;
                    break;
                case (int)SqlTypeRelations.lessEqual:
                    res = SqlTypeRelationsString.lessEqual;
                    break;
                case (int)SqlTypeRelations.less:
                    res = SqlTypeRelationsString.less;
                    break;
                case (int)SqlTypeRelations.notEqual:
                    res = SqlTypeRelationsString.notEqual;
                    break;
                case (int)SqlTypeRelations.like:
                    res = SqlTypeRelationsString.like;
                    break;
                case (int)SqlTypeRelations.boolAnd:
                    res = SqlTypeRelationsString.boolAnd;
                    break;
                case (int)SqlTypeRelations.boolOr:
                    res = SqlTypeRelationsString.boolOr;
                    break;
                case (int)SqlTypeRelations.sortAsc:
                    res = SqlTypeRelationsString.sortAsc;
                    break;
                case (int)SqlTypeRelations.sortDesc:
                    res = SqlTypeRelationsString.sortDesc;
                    break;
                case (int)SqlTypeRelations.groupBegin:
                    res = SqlTypeRelationsString.groupBegin;
                    break;
                case (int)SqlTypeRelations.groupEnd:
                    res = SqlTypeRelationsString.groupEnd;
                    break;
            }
            return res;
        }
    }
}

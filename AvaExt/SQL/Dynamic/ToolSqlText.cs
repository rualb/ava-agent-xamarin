using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.SQL.Dynamic
{
    public class ToolSqlText
    {
        const char _listSep = ',';

        public static string relationToString(SqlTypeRelations rel)
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
        public static string getSortString(string[] cols, SqlTypeRelations[] rels)
        {
            string txt = string.Empty;
            for (int i = 0; i < cols.Length; ++i)
                txt += string.Format(@"{0} {1},", cols[i], relationToString(rels[i]));
            return txt.Trim().Trim(_listSep);
        }

        public static string getWhereStringOld(string[] cols, string[] values)
        {
            string txt = string.Empty;
            for (int i = 0; i < cols.Length; ++i)
            {
                txt += string.Format((i > 0 ? @" AND {0} = '{1}'" : @"{0} = '{1}'"), cols[i], values[i]);
            }
            return txt;
        }


        public static string getNewParamName(int indx)
        {
            if (indx > 0)
                return "@P" + indx;
            return string.Empty;
        }
    }
}

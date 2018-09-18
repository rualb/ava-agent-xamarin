using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.Manual.Table;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Translating.Tools;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Database.Tools;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.Tools
{
    public class ToolNumerator
    {

        const short constSeqTypeFixed = 0;
        const short constSeqTypeInc = 1;
        const short constSeqTypeIncGroup = 2;

        const short constSeqAttrUndef = 0;
        const short constSeqAttrFirm = 1;
        const short constSeqAttrDiv = 2;
        const short constSeqAttrWh = 3;
        const short constSeqAttrFabr = 4;
        const short constSeqAttrUser = 5;
        const short constSeqAttrRole = 6;
        const short constSeqAttrYear = 7;
        const short constSeqAttrMonth = 8;
        const short constSeqAttrDay = 9;
        const short constSeqAttrQuarter = 10;
        const short constSeqAttrPeriod = 11;
        const short constSeqAttrPresent = 12;

        const short constSeqFormDig = 0;
        const short constSeqFormText = 1;

        public static string getNext(IEnvironment env, DateTime date, short mod, short docType, short firm, short div, short factory, short wh, short group, short role, short user)
        {
            DataRow rowNum;

            DataTable tabNum = getData(env, date, mod, docType, firm, div, factory, wh, group, role, user);
            //////////////////////////////////
            DataTable tabCopy = tabNum.Copy();
            tabNum.Clear();
           
            tabCopy.DefaultView.Sort = "USERID DESC";
            tabNum.Load(tabCopy.DefaultView.ToTable().CreateDataReader());
            //////////////////////////////////
            rowNum = ToolRow.getFirstRealRow(tabNum);
            ToolSeq.lockByUpdate(env, TableDOCNUM.TABLE_REAL_NAME, rowNum[TableDOCNUM.LOGICALREF]);
            rowNum = ToolRow.getFirstRealRow(getData(env, (int)rowNum[TableDOCNUM.LOGICALREF]));
            //
            string newDocNum = getNewDocNum(rowNum, env, date, mod, docType, firm, div, factory, wh, group, role, user);
            //
            rowNum[TableDOCNUM.LASTASGND] = newDocNum;
            IAdapterTable adapter = new AdapterTableDocNum(env, TableDOCNUM.LOGICALREF);
            adapter.set(rowNum.Table);
            return newDocNum;
        }


        static DataTable getData(IEnvironment env, DateTime date, short mod, short docType, short firm, short div, short factory, short wh, short group, short role, short user)
        {
            IPagedSource source = new PagedSourceDocNum(env);
            source.getBuilder().addParameterValue(TableDOCNUM.APPMODULE, mod);
            source.getBuilder().addParameterValue(TableDOCNUM.DOCIDEN, docType);
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.FIRMID, firm);
            source.getBuilder().addParameterValue(TableDOCNUM.FIRMID, 0, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.EFFSDATE, date, SqlTypeRelations.lessEqual);
            source.getBuilder().addParameterValue(TableDOCNUM.EFFEDATE, date, SqlTypeRelations.greaterEqual);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.DIVISID, div);
            source.getBuilder().addParameterValue(TableDOCNUM.DIVISID, -1, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.WHID, wh);
            source.getBuilder().addParameterValue(TableDOCNUM.WHID, -1, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.FACTID, factory);
            source.getBuilder().addParameterValue(TableDOCNUM.FACTID, -1, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.GROUPID, group);
            source.getBuilder().addParameterValue(TableDOCNUM.GROUPID, 0, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.ROLEID, role);
            source.getBuilder().addParameterValue(TableDOCNUM.ROLEID, 0, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            source.getBuilder().beginWhereGroup();
            source.getBuilder().addParameterValue(TableDOCNUM.USERID, user);
            source.getBuilder().addParameterValue(TableDOCNUM.USERID, 0, SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            source.getBuilder().endWhereGroup();
            DataTable tabNum = source.getAll();
            if (ToolTable.isEmpty(tabNum))
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_NUMERATION);
            return tabNum;
        }
        static DataTable getData(IEnvironment env, int lref)
        {
            IPagedSource source = new PagedSourceDocNum(env);
            source.getBuilder().addParameterValue(TableDOCNUM.LOGICALREF, lref);
            DataTable tabNum = source.getAll();
            if (ToolTable.isEmpty(tabNum))
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_NUMERATION);
            return tabNum;
        }
        

        static string getNewDocNum(DataRow row, IEnvironment env, DateTime date, short mod, short docType, short firm, short div, short factory, short wh, short group, short role, short user)
        {
            string curNum = (string)row[TableDOCNUM.LASTASGND];
            string newNum = string.Empty;
            int startIndx = 0;
            bool additionalInc = true;
            for (int i = 1; i <= 16; ++i)
            {
                string segId = ToolString.shrincDigit(i.ToString());
                string ENUM_SEGMENTS_SEGSTART = string.Format(TableDOCNUM.ENUM_SEGMENTS_SEGSTART, segId); // varchar 17
                string ENUM_SEGMENTS_SEGEND = string.Format(TableDOCNUM.ENUM_SEGMENTS_SEGEND, segId); // varchar 17
                string ENUM_SEGMENTS_SEGLEN = string.Format(TableDOCNUM.ENUM_SEGMENTS_SEGLEN, segId); // smallint 2
                string ENUM_SEGMENTS_SEGATTRB = string.Format(TableDOCNUM.ENUM_SEGMENTS_SEGATTRB, segId); // smallint 2
                string ENUM_SEGMENTS_FILLCH = string.Format(TableDOCNUM.ENUM_SEGMENTS_FILLCH, segId); // smallint 2
                string ENUM_SEGMENTS_SEGFORM = string.Format(TableDOCNUM.ENUM_SEGMENTS_SEGFORM, segId); // smallint 2
                string ENUM_SEGMENTS_INCREM = string.Format(TableDOCNUM.ENUM_SEGMENTS_INCREM, segId); // smallint 2 
 
                string start = (string)row[ENUM_SEGMENTS_SEGSTART]; // varchar 17
                string end = (string)row[ENUM_SEGMENTS_SEGEND]; // varchar 17
                short len = (short)row[ENUM_SEGMENTS_SEGLEN]; // smallint 2
                short attr = (short)row[ENUM_SEGMENTS_SEGATTRB]; // smallint 2
                short fillChar = (short)row[ENUM_SEGMENTS_FILLCH]; // smallint 2
                short form = (short)row[ENUM_SEGMENTS_SEGFORM]; // smallint 2
                short incType = (short)row[ENUM_SEGMENTS_INCREM]; // smallint 2 
                
                
                if (len <= 0)
                    break;

                string seg = ToolString.substring(curNum, startIndx, len);

                if (incType == constSeqTypeFixed && attr == constSeqAttrUndef)
                {
                    seg = start;
                }
                else
                {
                    switch (attr)
                    {
                        case constSeqAttrUndef:
                            Int64 segDig = Int64.Parse(ToolString.shrincDigit(seg));
                            Int64 startDig = Int64.Parse(ToolString.shrincDigit(start));
                            Int64 endDig = Int64.Parse(ToolString.shrincDigit(end));
                            if (endDig == 0)
                                endDig = Int64.Parse(string.Empty.PadLeft(len, '9'));
                            //
                            if (segDig < startDig)
                                segDig = startDig;
                            if (additionalInc)
                                ++segDig;
                            if (segDig > endDig)
                            {
                                segDig = endDig;
                                additionalInc = true;
                            }

                            seg = ToolString.shrincDigit(segDig.ToString());
                            break;
                        case constSeqAttrFirm:
                            seg = ToolString.shrincDigit(firm.ToString());
                            break;
                        case constSeqAttrDiv:
                            seg = ToolString.shrincDigit(div.ToString());
                            break;
                        case constSeqAttrWh:
                            seg = ToolString.shrincDigit(wh.ToString());
                            break;
                        case constSeqAttrFabr:
                            seg = ToolString.shrincDigit(factory.ToString());
                            break;
                        case constSeqAttrUser:
                            seg = ToolString.shrincDigit(user.ToString());
                            break;
                        case constSeqAttrRole:
                            seg = ToolString.shrincDigit(role.ToString());
                            break;
                        case constSeqAttrYear:
                            seg = ToolString.shrincDigit(date.Year.ToString());
                            break;
                        case constSeqAttrMonth:
                            seg = ToolString.shrincDigit(date.Month.ToString());
                            break;
                        case constSeqAttrDay:
                            seg = ToolString.shrincDigit(date.Day.ToString());
                            break;
                        case constSeqAttrQuarter:
                            seg = ToolString.shrincDigit(((date.Month / 4) + 1).ToString());
                            break;
                        case constSeqAttrPeriod:
                            seg = ToolString.shrincDigit((env.getInfoApplication().periodId).ToString());
                            break;
                        case constSeqAttrPresent:
                            //seg = ToolString.shrincDigit(firm.ToString());
                            break;
                    }
                }
                seg = seg.PadLeft(len, Convert.ToChar(fillChar));
                startIndx += len;
                newNum += seg;
            }
            if (curNum == newNum)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_NUMERATION);
            return newNum;
        }


           
    }
}

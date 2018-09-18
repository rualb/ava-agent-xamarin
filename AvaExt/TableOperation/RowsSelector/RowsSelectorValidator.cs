using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Expression;
using AvaExt.TableOperation.Const;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation.RowsSelector
{
    public class RowsSelectorValidator : IRowsSelector 
    {
        IRowValidator validatorSelectT;
        IRowValidator validatorStopT; 
        IRowValidator validatorSelectB;
        IRowValidator validatorStopB;

        IRowValidator validator;

        public RowsSelectorValidator(DataTable pTable, IRowValidator pValidatorSelectT, IRowValidator pValidatorStopT, IRowValidator pValidatorSelectB, IRowValidator pValidatorStopB,  IRowValidator pValidator)
        {
            validatorSelectT = pValidatorSelectT;
            validatorStopT = pValidatorStopT;
            validatorSelectB = pValidatorSelectB;
            validatorStopB = pValidatorStopB;

            validator = pValidator;
        }

        public DataRow[] get(DataRow row)
        {
            List<DataRow> rows = new List<DataRow>();
            DataTable table = row.Table;
 
            for (int i = table.Rows.IndexOf(row); i >= 0; --i)
            {
                DataRow locRow = table.Rows[i];
                if (!ToolRow.isDeleted(locRow))
                    if (validator.check(locRow))
                    {
                        if (validatorSelectT.check(locRow))
                            rows.Insert(0, locRow);
                        if (validatorStopT.check(locRow)) 
                            break;
                    }
            }
            for (int i = table.Rows.IndexOf(row) + 1; i < table.Rows.Count; ++i)
            {
                DataRow locRow = table.Rows[i];
                if ((!ToolRow.isDeleted(locRow)))
                    if (validator.check(locRow))
                    {
                        if (validatorSelectB.check(locRow))
                            rows.Add(locRow);
                        if (validatorStopB.check(locRow))
                            break;
                    }
            }
            return rows.ToArray();
        }
 
 
    }
}

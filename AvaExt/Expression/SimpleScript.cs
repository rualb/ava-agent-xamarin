using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Formating;
using System.Text.RegularExpressions;
using AvaExt.Common;
using AvaExt.Translating.Tools;
using AvaExt.SQL.Dynamic;
using AvaExt.TableOperation;

namespace AvaExt.Expression
{
    public class SimpleScript
    {
        XmlFormating _formating = new XmlFormating();
        Regex _exp;
        VarsOperations _oper;
        IEnvironment _env;
        public SimpleScript(IEnvironment pEnv)
        {
            _env = pEnv;
            _exp = new Regex("\\$\\([^\\$\\)\\(]+\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        }
        public void setVarStack()
        {
            _oper = new VarsOperations(new Dictionary<string, object>());
        }
        public void setVarStack(IDictionary<string, object> pDic)
        {
            _oper = new VarsOperations(pDic);
        }

        public string eval(string pScript)
        {
            StringBuilder builder = new StringBuilder();

            MatchCollection mc = _exp.Matches(pScript);
            for (int i = 0; i < mc.Count; ++i)
            {

                object res = parse(mc[i].Value);
                if (res != null)
                    builder.Append(res);

            }

            return builder.ToString();
        }

        private object parse(string str)
        {
            char quote = '"';
            char sep = ',';
            String tmp = str.Trim('$', ')').Trim('(');
            List<string> list = new List<string>();

            while (tmp != string.Empty)
            {
                int fInx = tmp.IndexOf(quote);
                if (fInx < 0)
                    fInx = tmp.Length;
                string part;
                part = tmp.Substring(0, fInx).Trim().Trim(sep).Trim();
                if (part != string.Empty)
                    list.AddRange(ToolString.trim(part.Split(sep)));

                if (fInx != tmp.Length)
                {
                    int sInx = tmp.IndexOf(quote, fInx + 1);
                    part = tmp.Substring(fInx, sInx - fInx + 1).Trim();
                    list.Add(part);
                    tmp = tmp.Remove(0, sInx + 1).Trim().Trim(sep).Trim();
                }
                else
                    tmp = string.Empty;
            }
            return parse(list.ToArray());
        }
        private object parse(string[] str)
        {



            switch (str[0])
            {
                case "println":
                    return "\r\n";
                case "set":
                    _oper.setVar(str[1], parse(ToolArray.sub<string>(str, 2)));
                    break;
                case "get":
                    return _oper.getVar(str[1]);
                case "mult":
                    return _oper.mult(ToolArray.sub<string>(str, 1));
                case "sum":
                    return _oper.sum(ToolArray.sub<string>(str, 1));
                case "div":
                    return _oper.div(ToolArray.sub<string>(str, 1));
                case "format":
                    return _oper.format(ToolArray.sub<string>(str, 1));
                case "formatln":
                    return _oper.format(ToolArray.sub<string>(str, 1)) + "\r\n";
                case "toDouble":
                    return _oper.toDouble(str[1]);
                case "toInt32":
                    return _oper.toInt32(str[1]);
                case "toInt16":
                    return _oper.toInt16(str[1]);
                case "sys":
                    return _env.getSysSettings().getString(_oper.getVar(str[1]).ToString());
                case "printch":
                    return _oper.printch(ToolArray.sub<string>(str, 1));
                case "replace":
                    return _oper.replace(ToolArray.sub<string>(str, 1));
                case "formatlistarr":
                    return _oper.formatlistarr(ToolArray.sub<string>(str, 1));
            }
            if (str.Length == 1)
                return _oper.getVar(str[0]);
            return null;
        }

        public VarsOperations getVarOperator()
        {
            return _oper;
        }

    }


    public class VarsOperations
    {
        const string SYS_DATE = "SYS_DATE";
        IDictionary<string, object> _vars;
        IFormatProvider _numFormat = XmlFormating.getNumberFormat();
        public VarsOperations(IDictionary<string, object> pDic)
        {
            _vars = pDic;
        }
        bool isDigit(string name)
        {
            if (char.IsDigit(name[0]))
                return true;

            if (name.Length >= 2)
                if (name[0] == '-' || name[0] == '+')
                    if (char.IsDigit(name[1]))
                        return true;
            return false;
        }
        public object getVar(string name)
        {
            if (name[0] == '"' && name[name.Length - 1] == '"')
                return name.Trim('"');
            if (isDigit(name))
                return name;
            if (name[0] == '-' || name[0] == '+')
                return mult(new string[] { name[0].ToString() + "1", name.TrimStart('-', '+') });

            if (name[0] == '@')
            {
                switch (name)
                {
                    case "@newline":
                        return "\r\n";
                    case "@tab":
                        return "\t";
                    case "@comma":
                        return ",";
                    case "@dotcomma":
                        return ";";
                }
            }

            if (isInnerVar(name))
                return getVarInner(name);
            if (_vars.ContainsKey(name))
                return _vars[name];
            throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { name });
        }
        object getVarInner(string name)
        {
            switch (name)
            {
                case SYS_DATE:
                    return DateTime.Now;
            }
            return string.Empty;
        }
        bool isInnerVar(string name)
        {
            return name.StartsWith("SYS_");
        }
        public void setVar(string name, object value)
        {
            if (_vars.ContainsKey(name))
                _vars[name] = value;
            else
                _vars.Add(name, value);

        }
        public double mult(string[] vars)
        {
            if (vars.Length < 2)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);
            double res = 1;
            for (int i = 0; i < vars.Length; ++i)
                res *= toDouble(vars[i]);
            return res;
        }
        public double sum(string[] vars)
        {
            if (vars.Length < 1)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);
            double res = 0;
            for (int i = 0; i < vars.Length; ++i)
                res += toDouble(vars[i]);

            return res;
        }
        public double div(string[] vars)
        {
            if (vars.Length < 2)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);
            double res = toDouble(vars[0]);
            for (int i = 1; i < vars.Length; ++i)
                res /= toDouble(vars[i]);
            return res;
        }
        public string format(string[] vars)
        {
            if (vars.Length < 2)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);
            object[] varsData = new object[vars.Length - 1];
            for (int i = 1; i < vars.Length; ++i)
                varsData[i - 1] = getVar(vars[i]);
            return string.Format((string)getVar(vars[0]), varsData);
        }

        public double toDouble(string var)
        {
            return Convert.ToDouble(ToolCell.isNull(getVar(var), 0.0), _numFormat);
        }
        public double toInt32(string var)
        {
            return Convert.ToInt32(getVar(var), _numFormat);
        }
        public double toInt16(string var)
        {
            return Convert.ToInt16(getVar(var), _numFormat);
        }


        public string printch(string[] p)
        {
            char[] chArr = new char[p.Length];
            for (int i = 0; i < p.Length; ++i)
                chArr[i] = (char)int.Parse(p[i].Trim());
            return new string(chArr);
        }
        public string replace(string[] vars)
        {
            if (vars.Length < 3)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);

            string v1 = getVar(vars[0]).ToString();
            string v2 = getVar(vars[1]).ToString();
            string v3 = getVar(vars[2]).ToString();

            return v1.Replace(v2, v3);
        }

        object PARSE(string val, Type type)
        {
            if (ToolType.isNumber(type))
            {
                val = val.Trim();
                if (val == string.Empty)
                    val = "0";
            }else
                if (type == ToolTypeSet.helper.tDateTime)
                {
                    string pat_ = "1900-01-01 00-00-00";
                    val = val.Trim();
                    if (val.Length < pat_.Length)
                        val = val + ToolString.right(pat_, pat_.Length - val.Length);
                }
 
            return XmlFormating.helper.parse(val, type);

        }

        public string formatlistarr(string[] vars)
        {
            if (vars.Length < 3)
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_ARGS_COUNT);

            StringBuilder sb = new StringBuilder();


            string format_ = getVar(vars[0]).ToString();
            string arr_ = getVar(vars[1]).ToString();

            if (!string.IsNullOrEmpty(arr_))
            {
                string join_ = getVar(vars[2]).ToString();

                string[] typesTmp_ = ToolArray.sub<string>(vars, 3);
                Type[] types_ = new Type[typesTmp_.Length];

                for (int i = 0; i < types_.Length; ++i)
                    types_[i] = ToolType.parse(typesTmp_[i]);


                string[][] values_ = ToolString.explodeGroupList(arr_);


                for (int x = 0; x < values_.Length; ++x)
                {
                    string[] itms = values_[x];
                    object[] data_ = new object[itms.Length];

                    for (int i = 0; i < data_.Length; ++i)
                    {
                        Type type_ = i < types_.Length ? types_[i] : ToolTypeSet.helper.tString;

                        data_[i] = PARSE(itms[i], type_);


                    }

                    sb.Append(string.Format(format_, data_));
                    if (x < values_.Length - 1)
                        sb.Append(join_);
                }


            }

            return sb.ToString();
        }
    }
}

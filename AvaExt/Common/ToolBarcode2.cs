using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaExt.Common
{
    public class ToolBarcode2
    {
        public static string toEAN13(string chaine) 
        {
            chaine = chaine.Trim();
            if (!Regex.IsMatch(chaine, "^\\d{12}$"))
                return string.Empty;
            string __upper, __lower, __left, __right, __rcode, __lcode;
            __upper = __lower = __left = __right = __rcode = __lcode = string.Empty;

            chaine += of_getcheckdigit13(chaine);


            __upper = "ABCDEFGHIJ";
            __lower = __upper.ToLower();

            __left = mid(chaine, 2, 6);
            __right = mid(chaine, 8, 6);
            __rcode = "";

            for (int i = 1; i <= 6; ++i)
                __rcode += mid(__lower, integer(mid(__right, i, 1)) + 1, 1);


            switch (integer(left(chaine, 1)))
            {
                case 0:
                    __lcode = "#!" + left(__left, 1) + mid(__left, 2, 1) + mid(__left, 3, 1) + mid(__left, 4, 1) + mid(__left, 5, 1) + mid(__left, 6, 1);
                    break;
                case 1:
                    __lcode = "$!" + left(__left, 1) + mid(__left, 2, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__left, 4, 1) + mid(__upper, integer(mid(__left, 5, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 6, 1)) + 1, 1);
                    break;
                case 2:
                    __lcode = "%!" + left(__left, 1) + mid(__left, 2, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 4, 1)) + 1, 1) + mid(__left, 5, 1) + mid(__upper, integer(mid(__left, 6, 1)) + 1, 1);
                    break;
                case 3:
                    __lcode = "&!" + left(__left, 1) + mid(__left, 2, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 4, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 5, 1)) + 1, 1) + mid(__left, 6, 1);
                    break;
                case 4:
                    __lcode = "'!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__left, 3, 1) + mid(__left, 4, 1) + mid(__upper, integer(mid(__left, 5, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 6, 1)) + 1, 1);
                    break;
                case 5:
                    __lcode = "(!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__left, 4, 1) + mid(__left, 5, 1) + mid(__upper, integer(mid(__left, 6, 1)) + 1, 1);
                    break;
                case 6:
                    __lcode = ")!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 4, 1)) + 1, 1) + mid(__left, 5, 1) + mid(__left, 6, 1);
                    break;
                case 7:
                    __lcode = "*!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__left, 3, 1) + mid(__upper, integer(mid(__left, 4, 1)) + 1, 1) + mid(__left, 5, 1) + mid(__upper, integer(mid(__left, 6, 1)) + 1, 1);
                    break;
                case 8:
                    __lcode = "+!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__left, 3, 1) + mid(__upper, integer(mid(__left, 4, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 5, 1)) + 1, 1) + mid(__left, 6, 1);
                    break;
                case 9:
                    __lcode = ",!" + left(__left, 1) + mid(__upper, integer(mid(__left, 2, 1)) + 1, 1) + mid(__upper, integer(mid(__left, 3, 1)) + 1, 1) + mid(__left, 4, 1) + mid(__upper, integer(mid(__left, 5, 1)) + 1, 1) + mid(__left, 6, 1);
                    break;
            }


            return __lcode + '-' + __rcode + '!';

        }

        public static string toEAN8(string chaine)
        {
            chaine = chaine.Trim();
            if (!Regex.IsMatch(chaine, "^\\d{7}$"))
                return string.Empty;
            string ls_lower, ls_left, ls_right, ls_rcode, ls_res;
            ls_lower = ls_left = ls_right = ls_rcode = ls_res = string.Empty;

            chaine += of_getcheckdigit8(chaine);


            ls_lower = "abcdefghij";

            ls_left = mid(chaine, 1, 4);
            ls_right = mid(chaine, 5, 4);
            ls_rcode = mid(ls_lower, integer(mid(ls_right, 1, 1)) + 1, 1)
                        + mid(ls_lower, integer(mid(ls_right, 2, 1)) + 1, 1)
                        + mid(ls_lower, integer(mid(ls_right, 3, 1)) + 1, 1)
                        + mid(ls_lower, integer(mid(ls_right, 4, 1)) + 1, 1);

            ls_res = '!' + ls_left + '-' + ls_rcode + '!';

            return ls_res;

        }


        static string of_getcheckdigit8(string as_input)
        {
            as_input += "0";
            int li_chet = 0, li_nechet = 0, li_checkdigit = 0;
            for (int i = 1; i <= 4; ++i)
            {
                li_chet += integer(mid(as_input, 2 * i, 1));
                li_nechet += integer(mid(as_input, 2 * i - 1, 1));
            }

            li_nechet *= 3;

            li_checkdigit = 10 - mod(li_chet + li_nechet, 10);

            if (li_checkdigit == 10)
                li_checkdigit = 0;



            return li_checkdigit.ToString();
        }
        static string of_getcheckdigit13(string as_input)
        {
            as_input += "0";
            int li_chet = 0, li_nechet = 0, li_checkdigit = 0;
            for (int i = 1; i <= 6; ++i)
            {
                li_chet += integer(mid(as_input, 2 * i, 1));
                li_nechet += integer(mid(as_input, 2 * i - 1, 1));
            }
            li_nechet *= 3;

            li_checkdigit = 10 - mod(li_chet + li_nechet, 10);

            if (li_checkdigit == 10)
                li_checkdigit = 0;

            return li_checkdigit.ToString();

        }
        static string mid(string str, int start, int len)
        {
            return str.Substring(start - 1, len);
        }
        static string left(string str, int len)
        {
            return mid(str, 1, len);
        }
        static string right(string str, int len)
        {
            return mid(str, str.Length - len + 1, len);
        }
        static int mod(int x, int y)
        {
            return x % y;
        }
        static int integer(string str)
        {
            return int.Parse(str);
        }

    }
}

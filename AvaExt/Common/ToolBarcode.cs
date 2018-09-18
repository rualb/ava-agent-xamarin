using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaExt.Common
{
    public class ToolBarcode
    {
        public static string toENC13(string chaine)
        {

            int i;
            int first;
            int checksum = 0;
            string CodeBarre = "";
            bool tableA;

            if (Regex.IsMatch(chaine, "^\\d{12}$"))
            {

                for (i = 1; i < 12; i += 2)
                    checksum += Convert.ToInt32(chaine.Substring(i, 1));
                checksum *= 3;
                for (i = 0; i < 12; i += 2)
                    checksum += Convert.ToInt32(chaine.Substring(i, 1));
                chaine += (10 - checksum % 10) % 10;

                CodeBarre = chaine.Substring(0, 1) + (char)(65 + Convert.ToInt32(chaine.Substring(1, 1)));
                first = Convert.ToInt32(chaine.Substring(0, 1));
                for (i = 2; i <= 6; i++)
                {
                    tableA = false;
                    switch (i)
                    {
                        case 2:
                            if (first >= 0 && first <= 3) tableA = true;
                            break;
                        case 3:
                            if (first == 0 || first == 4 || first == 7 || first == 8) tableA = true;
                            break;
                        case 4:
                            if (first == 0 || first == 1 || first == 4 || first == 5 || first == 9) tableA = true;
                            break;
                        case 5:
                            if (first == 0 || first == 2 || first == 5 || first == 6 || first == 7) tableA = true;
                            break;
                        case 6:
                            if (first == 0 || first == 3 || first == 6 || first == 8 || first == 9) tableA = true;
                            break;
                    }

                    if (tableA)
                        CodeBarre += (char)(65 + Convert.ToInt32(chaine.Substring(i, 1)));
                    else
                        CodeBarre += (char)(75 + Convert.ToInt32(chaine.Substring(i, 1)));
                }
                CodeBarre += "*";

                for (i = 7; i <= 12; i++)
                {
                    CodeBarre += (char)(97 + Convert.ToInt32(chaine.Substring(i, 1)));
                }
                CodeBarre += "+";
            }
            return CodeBarre;
        }

        public static string toENC128(string chaine)
        {
            //int checksum = 0;
            //if (Regex.IsMatch(chaine, "^\\d{8}$"))
            //{
            //    int cs = 0;
            //    for (int i = chaine.Length - 1; i >= 0; i = i - 2)
            //    {
            //        checksum += Int32.Parse(chaine[i].ToString());
            //    }
            //    checksum *= 3;
            //    for (int i = chaine.Length - 2; i >= 0; i = i - 2)
            //    {
            //        checksum += Int32.Parse(chaine[i].ToString());
            //    }
            //    chaine += ((10 - (cs % 10)) % 10).ToString();
            //}
            //return checksum;
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBSqlToJpeg
{
   public static class StringVB
    {  
        public static string MidVB(this String str, int startIndex, int length)
        {
            if (startIndex > str.Length)
            {
                return "";
            }
            else
            {
                return str.Substring(startIndex, length);
            }
        }

        public static string LeftVB(this String str, int length)
        {
            if (str.Length == 0)
            {
                return "";
            }
            else
            {
                return str.Substring(0, length);
            }
        }        

        public static string RightVB(this String str, int length)
        {
            if (str.Length == 0)
            {
                return "";
            }
            else
            {
                return str.Substring(str.Length - length, length);
            }
        }
    }
}

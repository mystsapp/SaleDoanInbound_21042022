using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Utilities
{
    public static class GetDate
    {

        // ngay dau cua thang
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        // ngay cuoi cua thang
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public static string LoadTuNgayDenNgay(string tuThang1, string denThang1, string nam1)
        {
            if (tuThang1.Length == 1)
            {
                tuThang1 = "0" + tuThang1;
            }
            
            string searchFromDate = "01/" + tuThang1 + "/" + nam1;
            string searchToDate = "01/" + denThang1 + "/" + nam1;

            // thang co 31 ngay
            if (denThang1 == "1" || denThang1 == "3" || denThang1 == "5" || denThang1 == "7" || denThang1 == "8" || denThang1 == "10" || denThang1 == "12")
            {
                if(denThang1.Length == 1)
                {
                    searchToDate = "31/" + AddChar(denThang1) + "/" + nam1;
                }
                else
                {
                    searchToDate = "31/" + denThang1 + "/" + nam1;
                }
                
            }
            // thang co 30 ngay
            if (denThang1 == "4" || denThang1 == "6" || denThang1 == "9" || denThang1 == "11")
            {
                if (denThang1.Length == 1)
                {
                    searchToDate = "30/" + AddChar(denThang1) + "/" + nam1;
                }
                else
                {
                    searchToDate = "30/" + denThang1 + "/" + nam1;
                }
                
            }
            // kiem tra nam nhuan
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 == 0)) // chia het 400 => nam nhuan
            {
                if (denThang1.Length == 1)
                {
                    searchToDate = "29/" + AddChar(denThang1) + "/" + nam1;
                }
                else
                {
                    searchToDate = "29/" + denThang1 + "/" + nam1;
                }
                
            }
            if ((denThang1 == "2") && (int.Parse(nam1) % 400 != 0)) // ko phai nam nhuan
            {
                if (denThang1.Length == 1)
                {
                    searchToDate = "28/" + AddChar(denThang1) + "/" + nam1;
                }
                else
                {
                    searchToDate = "28/" + denThang1 + "/" + nam1;
                }
                
            }

            return searchFromDate + "-" + searchToDate;
        }

        private static string AddChar(string charactor)
        {
            return "0" + charactor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS
{
    public class GetTime
    {
        /// <summary>
        /// Get datetime vietnamese
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>string datetime</returns>
        public string GetDatetime(DateTime dt)
        {
            string[] thu = new string[7] { "Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy" };
            string[] thang = new string[12] { "một", "hai", "ba", "tư", "năm", "sáu", "bảy", "tám", "chín", "mười", "mười một", "mười hai" };
            string ngaygiohientai;
            ngaygiohientai = dt.Hour.ToString() + " giờ ";
            ngaygiohientai += dt.Minute.ToString() + " phút ";
            ngaygiohientai += dt.Second.ToString() + " giây. ";
            ngaygiohientai += thu[(int)dt.DayOfWeek];
            ngaygiohientai += ", ngày " + dt.Day.ToString() + " ";
            ngaygiohientai += "tháng " + thang[dt.Month - 1];
            ngaygiohientai += " năm " + dt.Year;
            return ngaygiohientai;
        }
    }
}

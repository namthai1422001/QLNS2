using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace QLNS.QLNS
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SearchKey : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string keyword;
            keyword = context.Request.QueryString["keyword"];
            if (keyword != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonString = serializer.Serialize(GetFilteredList(keyword));
                System.Threading.Thread.Sleep(1000);
                context.Response.Write(jsonString);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string[] GetFilteredList(string keyword)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            List<string> lstData = (from a in
                                        (from p in db.PB_Nhanviens
                                         where p.Tinhtrang == 1
                                         select new
                                         {
                                             Hoten = p.HoNV.ToUpper() + " " + p.TenNV.ToUpper()
                                         })
                                    where a.Hoten.Contains(keyword.ToUpper())
                                    select a.Hoten).ToList();
            return lstData.ToArray();
        }
    }
}

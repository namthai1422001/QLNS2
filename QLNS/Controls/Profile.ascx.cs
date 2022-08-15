using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Controls
{
    public partial class Profile : System.Web.UI.UserControl
    {
        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Session["UserID"].ToString()))
                    {
                        Guid userid = new Guid(Session["UserID"].ToString());
                        dbLinQDataContext db = new dbLinQDataContext();
                        SYS_Nguoidung nguoidungs = db.SYS_Nguoidungs.Where(p => p.ID == userid).FirstOrDefault();
                        hplEditProfile.Text = nguoidungs.Fullname;
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/Login"));
                    }
                }
                catch
                {
                    Response.Redirect(ResolveUrl("~/Login"));
                }
            }
        }
    }
}
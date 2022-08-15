using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 10 (Xem 5, Cap nhat 7)
    /// Chức năng: 
    /// Hành động: 
    /// </summary>
    public partial class Quanhegiadinh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quan hệ gia đình";
                loadRole();
                loadData();
            }
        }

        #region Security
        //Phan quyen tren trang dua vao day!
        private void loadRole()
        {
            if (Session["UserRolls"] == null)
            {
                Response.Redirect(ResolveUrl("~/Login"));
            }
            List<int> UserRolls = Session["UserRolls"] as List<int>;
            if (UserRolls.Where(p => p == 10).Count() == 1)
            {
                hplAdd.Visible = true;
                hdIsRoll.Value = "True";
            }
            else
            {
                hplAdd.Visible = false;
                hdIsRoll.Value = "False";
            }
        }
        #endregion

        #region Methods
        //Load du lieu cho Repeater
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            List<DIC_Quanhegiadinh> lst = db.DIC_Quanhegiadinhs.ToList();
            int stt = 1;
            var lstData = (from p in lst
                           select
                           new
                           {
                               STT = stt++,
                               p.Maquanhe,
                               p.Tenquanhe,
                               p.GhiChu,
                               p.IsActive
                           }).ToList();
            rpData.DataSource = lstData;
            rpData.DataBind();
        }
        #endregion
    }
}

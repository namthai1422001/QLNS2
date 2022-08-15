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
    public partial class Bacluong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Bậc lương";
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

        //Load du lieu cho Repeater Ngachluong
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstNgachluong = (from p in db.DIC_NgachLuongs
                                 select new { p.MaNgach, p.TenNgach }).ToList();
            rpData.DataSource = lstNgachluong;
            rpData.DataBind();
        }

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpChild = (Repeater)e.Item.FindControl("rpChild");
                rpChild.ItemDataBound += new RepeaterItemEventHandler(rpChild_ItemDataBound);

                dbLinQDataContext db = new dbLinQDataContext();

                int NgachID;
                NgachID = int.Parse(e.Item.DataItem.ToString().Replace("{", "").Split(',').FirstOrDefault().Substring(11));
                var lstBacluong = (from p in db.DIC_BacLuongs
                                   where p.MaNgach == NgachID
                                   select new { p.Bac, p.MaNgach, p.Tenbac, p.Heso, p.GhiChu }).ToList();
                rpChild.DataSource = lstBacluong;
                rpChild.DataBind();
            }
        }

        protected void rpChild_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                HyperLink hplEdit = (HyperLink)e.Item.FindControl("hplEdit");
                if (hdIsRoll.Value == "True")
                {
                    string[] objbacluong = e.Item.DataItem.ToString().Replace("{", "").Split(',');
                    string bacid = objbacluong.FirstOrDefault().Substring(7);
                    string ngachid = objbacluong[1].Substring(11);
                    hplEdit.NavigateUrl = "EditBacluong/edit/" + ngachid + "/" + bacid + "?height=360";
                }
                else
                {
                    hplEdit.Visible = false;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 9 (Xem 5, Cap nhat 7)
    /// Chức năng: 
    /// Hành động: 
    /// </summary>
    public partial class Tonhom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Tổ - nhóm";
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
            if (UserRolls.Where(p => p == 9).Count() == 1)
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
        //Load du lieu cho Repeater Ngachluong
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstPhong = (from p in db.PB_Phongbans
                                 select new { p.Maphong, p.Tenphong }).ToList();
            rpData.DataSource = lstPhong;
            rpData.DataBind();
        }
        #endregion

        #region EventHandler
        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpChild = (Repeater)e.Item.FindControl("rpChild");
                rpChild.ItemDataBound += new RepeaterItemEventHandler(rpChild_ItemDataBound);

                dbLinQDataContext db = new dbLinQDataContext();

                int PhongID;
                PhongID = int.Parse(e.Item.DataItem.ToString().Replace("{", "").Split(',').FirstOrDefault().Substring(11));
                var lstTonhom = (from p in db.PB_ToNhoms
                                   where p.Maphong == PhongID
                                   select new { p.MaToNhom, p.Maphong, p.TenToNhom, p.Tongsonhanvien, p.GhiChu }).ToList();
                rpChild.DataSource = lstTonhom;
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
                    string[] objtonhom = e.Item.DataItem.ToString().Replace("{", "").Split(',');
                    string tonhomid = objtonhom.FirstOrDefault().Substring(12);
                    hplEdit.NavigateUrl = "EditTonhom/edit/" + tonhomid + "?height=360";
                }
                else
                {
                    hplEdit.Visible = false;
                }
            }
        }
        #endregion
    }
}

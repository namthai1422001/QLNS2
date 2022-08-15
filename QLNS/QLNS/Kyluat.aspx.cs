using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 4
    /// Chức năng: 16
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Kyluat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quyết định kỷ luật nhân viên";
                loadRole();
                loadNgay();
                loadData(DateTime.Parse(txtFromDate.Value, new CultureInfo("vi-vn")), DateTime.Parse(txtToDate.Value, new CultureInfo("vi-vn")));
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
            if (UserRolls.Where(p => p == 4).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
        }

        //Ghi lai nhat ky he thong
        private void DiarySystem(int chucnang, int hanhdong, string doituong)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.ID == new Guid(Session["UserID"].ToString())).FirstOrDefault();
                if (objData != null)
                {
                    db.sp_SYS_Nhatkyhethong_Insert(objData.Username, chucnang, hanhdong, doituong);
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
            catch
            {
                Response.Redirect("Login");
            };
        }
        #endregion

        #region Methods
        //Load txtNgay
        private void loadNgay()
        {
            DateTime now = DateTime.Now;
            txtFromDate.Value = now.AddDays(-30).ToString("dd/MM/yyyy");
            txtToDate.Value = now.ToString("dd/MM/yyyy");
        }

        //Load du lieu cho Repeater
        //Ngay di: optFilter = 0, else 1
        private void loadData(DateTime fromDate, DateTime toDate)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstKyluat = (from nvien in db.PB_Nhanviens
                             join kyluat in db.PB_KyluatNhanviens
                             on nvien.MaNV equals kyluat.MaNV
                             where (kyluat.Ngaykyluat >= fromDate && kyluat.Ngaykyluat <= toDate)
                             select
                       new
                       {
                           nvien.MaNV,
                           HoTen = nvien.HoNV + " " + nvien.TenNV,
                           kyluat.Makyluat,
                           kyluat.Tenkyluat,
                           kyluat.Hinhthuckyluat,
                           kyluat.Ngayxayra,
                           kyluat.Ngaykyluat
                       }).ToList();
            int stt = 1;
            var lstData = (from p in lstKyluat
                           select
                           new
                           {
                               STT = stt++,
                               p.MaNV,
                               p.HoTen,
                               p.Makyluat,
                               p.Tenkyluat,
                               p.Hinhthuckyluat,
                               p.Ngayxayra,
                               p.Ngaykyluat
                           }).ToList();
            rpData.DataSource = lstData;
            rpData.DataBind();

            DiarySystem(16, 5, "");
        }
        #endregion

        #region EventHandler
        protected void btnXem_Click(object sender, EventArgs e)
        {
            loadData(DateTime.Parse(txtFromDate.Value, new CultureInfo("vi-vn")), DateTime.Parse(txtToDate.Value, new CultureInfo("vi-vn")));
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditKyluat");
        }
        #endregion
    }
}

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
    /// Chức năng: 15
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Khenthuong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quyết định khen thưởng nhân viên";
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
            var lstKhenthuong = (from nvien in db.PB_Nhanviens
                                join khenthuong in db.PB_KhenthuongNhanviens
                                on nvien.MaNV equals khenthuong.MaNV
                                where (khenthuong.Ngaykhenthuong >= fromDate && khenthuong.Ngaykhenthuong <= toDate)
                                select
                          new
                          {
                              nvien.MaNV,
                              HoTen = nvien.HoNV + " " + nvien.TenNV,
                              khenthuong.Makhenthuong,
                              khenthuong.Tenkhenthuong,
                              khenthuong.Hinhthuckhenthuong,
                              khenthuong.Ngaykhenthuong,
                              khenthuong.Sotien
                          }).ToList();
            int stt = 1;
            var lstData = (from p in lstKhenthuong
                           select
                           new
                           {
                               STT = stt++,
                               p.MaNV,
                               p.HoTen,
                               p.Makhenthuong,
                               p.Tenkhenthuong,
                               p.Hinhthuckhenthuong,
                               p.Ngaykhenthuong,
                               p.Sotien
                           }).ToList();
            rpData.DataSource = lstData;
            rpData.DataBind();

            DiarySystem(15, 5, "");
        }
        #endregion

        #region EventHandler
        protected void btnXem_Click(object sender, EventArgs e)
        {
            loadData(DateTime.Parse(txtFromDate.Value, new CultureInfo("vi-vn")), DateTime.Parse(txtToDate.Value, new CultureInfo("vi-vn")));
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditKhenthuong");
        }
        #endregion
    }
}

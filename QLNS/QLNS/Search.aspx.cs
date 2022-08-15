using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: Login
    /// Chức năng: 
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Tìm kiếm nhân viên";
            if (!IsPostBack)
            {
                loadRole();
                if (!string.IsNullOrEmpty(Request.QueryString["key"]))
                {
                    loadData(new DeEncodeBase64().DecodeFrom64(Request.QueryString["key"]));
                }
            }
        }

        #region Security
        //Phan quyen tren trang dua vao day!
        private void loadRole()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect(ResolveUrl("~/Login"));
            }
        }
        #endregion

        #region Methods
        private void loadData(string Hoten)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            var lstNhanvien = (from nvien in db.PB_Nhanviens
                           join cviecnvien in db.View_PB_Nhanvien_Thongtincobans
                               on nvien.MaNV equals cviecnvien.MaNV
                           where (nvien.HoNV + " " + nvien.TenNV).ToUpper().Contains(Hoten.ToUpper())
                           select new
                           {
                               nvien.MaNV,
                               Hoten = nvien.HoNV + " " + nvien.TenNV,
                               Gioitinh = ((nvien.Nu == true) ? "Nữ" : "Nam"),
                               Hinhanh = ((nvien.Hinhanh != null) ? nvien.Hinhanh : "chuaco.png"),
                               nvien.Ngaysinh,
                               nvien.Noisinh,
                               nvien.Diachi,
                               nvien.Tamtru,
                               nvien.Dienthoainha,
                               nvien.Dienthoaididong,
                               nvien.Email,
                               Trangthai = ((nvien.Tinhtrang == 1) ? "Đang làm việc"
                                                : (nvien.Tinhtrang == 2) ? "Đang thử việc"
                                                : (nvien.Tinhtrang == 3) ? "Tạm ngưng việc"
                                                : "Đã nghỉ việc"),
                               Tenphong = ((cviecnvien.Tenphong != null) ? cviecnvien.Tenphong : "Chưa được cài đặt"),
                               Tenchucvu = ((cviecnvien.Tenchucvu != null) ? cviecnvien.Tenchucvu : "Chưa được cài đặt"),
                               Tencongviec = ((cviecnvien.Tencongviec != null) ? cviecnvien.Tencongviec : "Chưa được cài đặt")
                           }).ToList();
            int stt = 1;
            var lstData = (from p in lstNhanvien
                           select new
                           {
                               STT = stt++,
                               p.MaNV,
                               p.Hoten,
                               p.Gioitinh,
                               p.Hinhanh,
                               p.Ngaysinh,
                               p.Noisinh,
                               p.Diachi,
                               p.Tamtru,
                               p.Dienthoainha,
                               p.Dienthoaididong,
                               p.Email,
                               p.Trangthai,
                               p.Tenphong,
                               p.Tenchucvu,
                               p.Tencongviec
                           }).ToList();

            ltrInfor.Text = lstData.Count.ToString();
            ltrKey.Text = Hoten;

            rpData.DataSource = lstData;
            rpData.DataBind();

            
        }
        #endregion

        #region EventHandler
        protected void txtHoten_TextChanged(object sender, EventArgs e)
        {
            loadData(txtHoten.Text.Trim());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData(txtHoten.Text.Trim());
        }
        #endregion
    }
}

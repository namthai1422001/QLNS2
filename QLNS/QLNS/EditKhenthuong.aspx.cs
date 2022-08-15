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
    /// Hành động: Tạo (6)
    /// </summary>
    public partial class EditKhenthuong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quyết định khen thưởng nhân viên";
                loadRole();
                loadNgay();
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
                    Response.Redirect("../Login");
                }
            }
            catch
            {
                Response.Redirect("../Login");
            };
        }
        #endregion

        #region Methods
        //Load Ngay thang mac dinh
        private void loadNgay()
        {
            txtNgaykhenthuong.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtNgayky.Value = txtNgaykhenthuong.Value;
        }

        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstPhong = (from p in db.PB_Phongbans
                            select new { p.Maphong, p.Tenphong }).ToList();
            var lstChucvu = (from p in db.DIC_Chucvus
                             where p.IsActive == true
                             select new { p.Machucvu, p.Tenchucvu }).ToList();

            //Clear Items
            cbPhong.Items.Clear();
            cbNhanvienduocthuong.Items.Clear();
            cbChucvunguoiky.Items.Clear();
            cbNguoiky.Items.Clear();

            //Disable
            cbNhanvienduocthuong.Enabled = false;
            cbNguoiky.Enabled = false;

            //Bind Item first (Vui long chon)
            cbPhong.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
            cbNhanvienduocthuong.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
            cbChucvunguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
            cbNguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));


            //Bind data cbPhong and cbChucvunguoiky
            foreach (var p in lstPhong)
            {
                cbPhong.Items.Add(new ListItem(p.Tenphong, p.Maphong.ToString()));
            }
            foreach (var p in lstChucvu)
            {
                cbChucvunguoiky.Items.Add(new ListItem(p.Tenchucvu, p.Machucvu.ToString()));
            }
        }

        private void loadNhanvientheoPhong(int maph)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstData = (from p in db.View_PB_Nhanvien_Thongtincobans
                           where p.Maphong == maph
                           select
                           new { p.MaNV, HoTen = p.HoNV + " " + p.TenNV }).ToList();

            //Clear Items
            cbNhanvienduocthuong.Items.Clear();

            //Bind Items
            cbNhanvienduocthuong.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));

            foreach (var p in lstData)
            {
                cbNhanvienduocthuong.Items.Add(new ListItem(p.HoTen, p.MaNV));
            }
        }

        private void loadNhanvientheoChucvu(int macv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstData = (from p in db.View_PB_Nhanvien_Thongtincobans
                           where p.Machucvu == macv
                           select
                           new { p.MaNV, HoTen = p.HoNV + " " + p.TenNV }).ToList();

            //Clear Items
            cbNguoiky.Items.Clear();

            //Bind Items
            cbNguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));

            foreach (var p in lstData)
            {
                cbNguoiky.Items.Add(new ListItem(p.HoTen, p.MaNV));
            }
        }

        #endregion

        #region EventHandler
        protected void cbPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhong.SelectedValue != "0")
            {
                loadNhanvientheoPhong(int.Parse(cbPhong.SelectedValue));
                cbNhanvienduocthuong.Enabled = true;
            }
            else
            {
                cbNhanvienduocthuong.Items.Clear();
                cbNhanvienduocthuong.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
                cbNhanvienduocthuong.Enabled = false;
                regPhong.IsValid = false;
            }
        }

        protected void cbChucvunguoiky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChucvunguoiky.SelectedValue != "0")
            {
                loadNhanvientheoChucvu(int.Parse(cbChucvunguoiky.SelectedValue));
                cbNguoiky.Enabled = true;
            }
            else
            {
                cbNguoiky.Items.Clear();
                cbNguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
                cbNguoiky.Enabled = false;
                regChucvunguoiky.IsValid = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    DateTime ngaykhenthuong = DateTime.Parse(txtNgaykhenthuong.Value, new CultureInfo("vi-vn"));
                    var objSalary = (from p in db.PB_Danhsachbangluongs
                                     where p.Nam == ngaykhenthuong.Year
                                         && p.Thang == ngaykhenthuong.Month
                                     select
                                         new
                                         {
                                             p.IsLock,
                                             p.IsFinish
                                         }).FirstOrDefault();
                    if (objSalary != null)
                    {
                        //Bang luong trong thang da duoc tao.
                        //Kiem tra trang thai cua bang luong do da hoan thanh hay bi khoa hay ko
                        if (objSalary.IsFinish)
                        {
                            string strMsg = "Bảng lương tháng " + ngaykhenthuong.Month + "/" + ngaykhenthuong.Year + " đã hoàn thành. Không thể thêm khen thưởng trong tháng này";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + strMsg + "'); window.location = 'Khenthuong';", true);
                            return;
                        }
                        else
                        {
                            if (objSalary.IsLock)
                            {
                                string strMsg = "Bảng lương tháng " + ngaykhenthuong.Month + "/" + ngaykhenthuong.Year + " đã bị khóa. Hãy mở khóa bảng lương tháng này trước khi thêm";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + strMsg + "'); window.location = 'Khenthuong';", true);
                                return;
                            }
                        }
                    }
                    //Bang luong trong thang cho phep them.
                    PB_KhenthuongNhanvien objData = new PB_KhenthuongNhanvien();

                    objData.Makhenthuong = Guid.NewGuid();
                    objData.MaNV = cbNhanvienduocthuong.SelectedValue;
                    objData.Tenkhenthuong = txtTenkhenthuong.Text;
                    objData.Hinhthuckhenthuong = txtHinhthuckhenthuong.Text;
                    objData.LyDo = txtLyDo.Text;
                    objData.Ngaykhenthuong = ngaykhenthuong;
                    objData.Sotien = int.Parse(txtTienkhenthuong.Text.Replace(",", ""));
                    objData.Nguoiky = cbNguoiky.SelectedItem.Text;
                    objData.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    objData.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.PB_KhenthuongNhanviens.InsertOnSubmit(objData);
                    db.SubmitChanges();

                    DiarySystem(15, 6, cbNhanvienduocthuong.SelectedItem.Text + " ngày " + ngaykhenthuong.ToString("dd/MM/yyyy"));

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lưu quyết định khen thưởng nhân viên thành công'); window.location = 'Khenthuong';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Khenthuong';", true);
                };
            }
        }
        #endregion
    }
}

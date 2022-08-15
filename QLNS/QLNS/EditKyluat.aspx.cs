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
    /// Hành động: Tạo (6)
    /// </summary>
    public partial class EditKyluat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quyết định kỷ luật nhân viên";
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
            txtNgaykyluat.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtNgayxayra.Value = txtNgaykyluat.Value;
            txtNgayky.Value = txtNgaykyluat.Value;
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
            cbNhanvienbikyluat.Items.Clear();
            cbChucvunguoiky.Items.Clear();
            cbNguoiky.Items.Clear();

            //Disable
            cbNhanvienbikyluat.Enabled = false;
            cbNguoiky.Enabled = false;

            //Bind Item first (Vui long chon)
            cbPhong.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
            cbNhanvienbikyluat.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
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
            cbNhanvienbikyluat.Items.Clear();

            //Bind Items
            cbNhanvienbikyluat.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));

            foreach (var p in lstData)
            {
                cbNhanvienbikyluat.Items.Add(new ListItem(p.HoTen, p.MaNV));
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
                cbNhanvienbikyluat.Enabled = true;
            }
            else
            {
                cbNhanvienbikyluat.Items.Clear();
                cbNhanvienbikyluat.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
                cbNhanvienbikyluat.Enabled = false;
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

                    DateTime ngayxayra = DateTime.Parse(txtNgayxayra.Value, new CultureInfo("vi-vn"));
                    DateTime ngaykyluat = DateTime.Parse(txtNgaykyluat.Value, new CultureInfo("vi-vn"));

                   
                    PB_KyluatNhanvien objData = new PB_KyluatNhanvien();

                    objData.Makyluat = Guid.NewGuid();
                    objData.MaNV = cbNhanvienbikyluat.SelectedValue;
                    objData.Tenkyluat = txtTenkyluat.Text.Trim();
                    objData.Hinhthuckyluat = txtHinhthuckyluat.Text.Trim();
                    objData.LyDo = txtLyDo.Text.Trim();
                    objData.Diadiem = txtDiadiem.Text.Trim();
                    objData.Nguoichungkien = txtNguoichungkien.Text.Trim();
                    objData.Motasuviec = txtMotasuviec.Text.Trim();
                    objData.Nguoibikyluatgiaithich = txtNguoibikyluatgiaithich.Text.Trim();
                    objData.Ngayxayra = ngayxayra;
                    objData.Ngaykyluat = ngaykyluat;
                    objData.Nguoiky = cbNguoiky.SelectedItem.Text;
                    objData.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    objData.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.PB_KyluatNhanviens.InsertOnSubmit(objData);
                    db.SubmitChanges();

                    DiarySystem(16, 6, cbNhanvienbikyluat.SelectedItem.Text + " ngày " + ngaykyluat.ToString("dd/MM/yyyy"));

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lưu quyết định kỷ luật nhân viên thành công'); window.location = 'Kyluat';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Kyluat';", true);
                };
            }
        }
        #endregion
    }
}

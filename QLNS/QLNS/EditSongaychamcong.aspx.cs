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
    /// Quyền: 3
    /// Chức năng: 36
    /// Hành động: Tạo (6)
    /// </summary>
    public partial class EditSongaychamcong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Cài đặt số ngày chấm công trong tháng";
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
            if (UserRolls.Where(p => p == 3).Count() == 0)
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

        #region Load du lieu
        //Load du lieu
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Thaydoitongsongaychamcong objdata = (from p in db.PB_Thaydoitongsongaychamcongs
                                        where p.IsCurrent == true
                                        orderby p.Ngayapdung descending
                                        select p).FirstOrDefault();

            if (objdata != null)
            {
                loadOldData(objdata);
                loadNewData();
            }

            cbChucvunguoiky.Items.Clear();
            cbChucvunguoiky.Items.Add(new ListItem("Vui lòng chọn", "0"));
            cbChucvunguoiky.SelectedIndex = 0;

            var chucvu = (from p in db.DIC_Chucvus
                          where p.IsActive == true
                          select new
                          {
                              p.Machucvu,
                              p.Tenchucvu
                          }).ToList();
            foreach (var cv in chucvu)
            {
                cbChucvunguoiky.Items.Add(new ListItem(cv.Tenchucvu, cv.Machucvu.ToString()));
            }

            cbNguoiky.Items.Clear();
            cbNguoiky.Items.Add(new ListItem("Vui lòng chọn", "0"));
            cbNguoiky.SelectedIndex = 0;
            cbNguoiky.Enabled = false;

            DateTime now = DateTime.Now;
            lblBatdauapdung.Text = "Tháng " + now.Month.ToString("#,##0") + "/" + now.Year.ToString();
            txtNgayky.Value = now.ToString("dd/MM/yyyy");
        }

        //Gan du lieu cho cau hinh cong thuc cu~
        private void loadOldData(PB_Thaydoitongsongaychamcong objdata)
        {
            lblValue.Text = objdata.Tongsongaychamcong.ToString();
            lblNguoiky.Text = objdata.Nguoiky;
            lblChucvunguoiky.Text = objdata.Chucvunguoiky;
            lblNgayky.Text = objdata.Ngayky.ToString("dd/MM/yyyy");
            lblNgayapdung.Text = "Tháng " + objdata.Ngayapdung.Month.ToString("#,##0") + "/" + objdata.Ngayapdung.Year.ToString();
        }

        //Gan du lieu cho khung nhap lieu
        private void loadNewData()
        {
            DateTime now = DateTime.Now;
            lblBatdauapdung.Text = "Tháng " + now.Month.ToString("#,##0") + "/" + now.Year.ToString();
            txtNgayky.Value = now.ToString("dd/MM/yyyy");
        }

        //Them nhan vien vao dropdownlist
        private void loadNhanvien(int macv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var chucvunhanvien = (from p in db.View_PB_Nhanvien_Thaydoichucvus
                                  where p.Tinhtrang == 1 && p.Machucvu == macv
                                  orderby p.TenNV ascending
                                  select new
                                  {
                                      p.MaNV,
                                      HoTen = p.HoNV + " " + p.TenNV,
                                      p.Machucvu,
                                      p.Tenchucvu
                                  }).ToList();
            cbNguoiky.Items.Clear();
            cbNguoiky.Items.Add(new ListItem("Vui lòng chọn", "0"));
            cbNguoiky.SelectedIndex = 0;
            foreach (var nv in chucvunhanvien)
            {
                cbNguoiky.Items.Add(new ListItem(nv.HoTen, nv.MaNV));
            }
        }
        #endregion Load du lieu

        #region EventHandler
        //Luu lai thay doi
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Thaydoitongsongaychamcong objdata = new PB_Thaydoitongsongaychamcong();

                    objdata.Masongay = Guid.NewGuid();
                    objdata.Ngayapdung = DateTime.Now;
                    objdata.IsCurrent = true;
                    objdata.Tongsongaychamcong = byte.Parse(txtValue.Text);


                    objdata.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    objdata.Nguoiky = cbNguoiky.SelectedItem.Text;

                    objdata.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    objdata.GhiChu = txtDescription.Text;


                    objdata.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objdata.CreatedByDate = DateTime.Now;

                    db.PB_Thaydoitongsongaychamcongs.InsertOnSubmit(objdata);
                    db.SubmitChanges();

                    DiarySystem(36, 6, objdata.Masongay.ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cài đặt số ngày chấm công thành công'); window.location = 'Quydinhsongaychamcong';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Quydinhsongaychamcong';", true);
                };
            }
        }

        protected void cbChucvunguoiky_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChucvunguoiky.SelectedValue != "0")
            {
                loadNhanvien(int.Parse(cbChucvunguoiky.SelectedValue));
                cbNguoiky.Enabled = true;
            }
            else
            {
                cbNguoiky.Enabled = false;
                regChucvunguoiky.IsValid = false;
            }
        }
        #endregion
    }
}

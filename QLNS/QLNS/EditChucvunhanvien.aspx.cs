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
    /// Chức năng: 9
    /// Hành động: Cập nhật (7)
    /// </summary>
    public partial class EditChucvunhanvien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thay đổi chức vụ nhân viên";
                loadRole();
                checkRequire();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    loadData(Request.QueryString["id"]);
                }
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

        //Kiem tra nhung tham so can thiet cho chuc nang nay
        private void checkRequire()
        {
            //Kiem tra xem da co chuc vu Active hay chua!
            dbLinQDataContext db = new dbLinQDataContext();
            int demcv = (from p in db.DIC_Chucvus
                      where p.IsActive == true
                      select p).ToList().Count();
            if (demcv == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không có chức vụ! Vui lòng thêm chức vụ trước'); window.location ='Chucvu';", true);
            }
        }

        #region Load du lieu
        //Load du lieu
        private void loadData(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            //Kiem tra xem co chuc vu nao dc khoi tao chua?
            bool checkfirst = ((from ttnvien in db.PB_Nhanviens
                                join thaydoicvu in db.PB_Thaydoichucvus
                                on ttnvien.MaNV equals thaydoicvu.MaNV
                                where ttnvien.Tinhtrang == 1
                                select new { ttnvien.MaNV }).ToList().Count > 0) ? false : true;
            if (checkfirst)
            {
                btnSave.Visible = false;
                btnSaveFirst.Visible = true;
                pnlNotFirst.Visible = false;
                regChucvunguoiky.IsValid = true;
                regNguoiky.IsValid = true;
                ltrInformation.Text = @"<div class='notification attention png_bg'>
                                            <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                            <div style='color: #AC2C2C'>
                                                Đây là <b>nhân viên đầu tiên</b> được khởi tạo chức vụ.<br />
                                                Bạn nên chọn người có chức vụ cao nhất để khởi tạo.
                                            </div>
                                        </div>";
            }
            else
            {
                btnSaveFirst.Visible = false;
            }
            PB_Thaydoichucvu cvnhanvien = (from p in db.PB_Thaydoichucvus
                                             where p.IsCurrent == true && p.MaNV == manv
                                             orderby p.Ngayapdung descending
                                             select p).FirstOrDefault();

            if (cvnhanvien != null)
            {
                loadOldData(cvnhanvien);
            }
            loadNewData((cvnhanvien != null) ? cvnhanvien.Machucvu : 0);

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

            var nvien = (from p in db.PB_Nhanviens
                         where p.MaNV == manv
                         select new
                         {
                             HoTen = p.HoNV + " " + p.TenNV
                         }).FirstOrDefault();
            lblHoten.Text = nvien.HoTen;
        }

        //Gan du lieu cho phong ban cu~ cua nhan vien
        private void loadOldData(PB_Thaydoichucvu cvnhanvien)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            lblTenchucvu.Text = db.DIC_Chucvus.Where(p => p.Machucvu == cvnhanvien.Machucvu).FirstOrDefault().Tenchucvu;
            lblNguoiky.Text = cvnhanvien.Nguoiky;
            lblChucvunguoiky.Text = cvnhanvien.Chucvunguoiky;
            lblNgayky.Text = cvnhanvien.Ngayky.ToString("dd/MM/yyyy");
            lblNgayapdung.Text = cvnhanvien.Ngayapdung.ToString("dd/MM/yyyy");

            lblLyDo.Text = Server.HtmlEncode(cvnhanvien.LyDo);

            

        }

        //Gan du lieu cho khung nhap lieu
        private void loadNewData(int macv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var pban = (from p in db.DIC_Chucvus
                        where ((macv != 0) ? p.Machucvu != macv : p.Machucvu != 0)
                        select new
                        {
                            p.Machucvu,
                            p.Tenchucvu
                        }
                       ).ToList();

            cbChucvumoi.Items.Clear();
            cbChucvumoi.Items.Add(new ListItem("Vui lòng chọn", "0"));
            foreach (var p in pban)
            {
                cbChucvumoi.Items.Add(new ListItem(p.Tenchucvu, p.Machucvu.ToString()));
            }


            DateTime now = DateTime.Now;
            lblBatdauapdung.Text = now.ToString("dd/MM/yyyy");

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
                    PB_Thaydoichucvu cvunvien = new PB_Thaydoichucvu();

                    cvunvien.MaNV = Request.QueryString["id"];
                    cvunvien.Machucvu = int.Parse(cbChucvumoi.SelectedValue);
                    cvunvien.Ngayapdung = DateTime.Now;
                    cvunvien.LyDo = txtLyDo.Text;
                    cvunvien.IsCurrent = true;

                    cvunvien.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    cvunvien.Nguoiky = cbNguoiky.SelectedItem.Text;

                    cvunvien.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    cvunvien.GhiChu = txtGhiChu.Text;

                    cvunvien.CreatedByUser = new Guid(Session["UserID"].ToString());
                    cvunvien.CreatedByDate = DateTime.Now;

                    db.PB_Thaydoichucvus.InsertOnSubmit(cvunvien);
                    db.SubmitChanges();

                    DiarySystem(9, 7, cvunvien.MaNV);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thay đổi thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                };
            }
        }
        //Luu lai thay doi
        protected void btnSaveFirst_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Thaydoichucvu cvunvien = new PB_Thaydoichucvu();

                    cvunvien.MaNV = Request.QueryString["id"];
                    cvunvien.Machucvu = int.Parse(cbChucvumoi.SelectedValue);
                    cvunvien.Ngayapdung = DateTime.Now;
                    cvunvien.LyDo = txtLyDo.Text;
                    cvunvien.IsCurrent = true;

                    cvunvien.Chucvunguoiky = "<b>Hệ thống</b>";
                    cvunvien.Nguoiky = "<b>Hệ thống</b>";

                    cvunvien.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    cvunvien.GhiChu = txtGhiChu.Text;

                    cvunvien.CreatedByUser = new Guid(Session["UserID"].ToString());
                    cvunvien.CreatedByDate = DateTime.Now;

                    db.PB_Thaydoichucvus.InsertOnSubmit(cvunvien);
                    db.SubmitChanges();

                    DiarySystem(4, 9, cvunvien.MaNV);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thay đổi thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                }
                catch 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
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

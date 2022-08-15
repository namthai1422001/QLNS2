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
    /// Chức năng: 10
    /// Hành động: Cập nhật (7)
    /// </summary>
    public partial class EditCongviecnhanvien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thay đổi công việc nhân viên";
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
            int demcv = (from p in db.DIC_Congviecs
                         select p).ToList().Count();
            if (demcv == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không có công việc! Vui lòng thêm công việc trước'); window.location ='Congviec';", true);
            }

            //Kiem tra xem co nguoi co tham quyen chuyen phong khong (Nhan vien co chuc vu)
            int demcvnhanvien = (from nvien in db.PB_Nhanviens
                                 join cvunvien in db.PB_Thaydoichucvus
                                 on nvien.MaNV equals cvunvien.MaNV
                                 where nvien.Tinhtrang == 1
                                 select nvien.MaNV).ToList().Count();

            if (demcvnhanvien == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không có nhân viên nào có đủ thẩm quyền! Vui lòng khởi tạo chức vụ cho nhân viên!'); window.location ='DanhsachNhanvien';", true);
            }
        }

        #region Load du lieu
        //Load du lieu
        private void loadData(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Thaydoicongviec cviecnhanvien = (from p in db.PB_Thaydoicongviecs
                                             where p.IsCurrent == true && p.MaNV == manv
                                             orderby p.Ngayapdung descending
                                             select p).FirstOrDefault();

            if (cviecnhanvien != null)
            {
                loadOldData(cviecnhanvien);
            }
            loadNewData((cviecnhanvien != null) ? cviecnhanvien.Macongviec : 0);

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
        }

        //Gan du lieu cho phong ban cu~ cua nhan vien
        private void loadOldData(PB_Thaydoicongviec cviecnhanvien)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            lblTencongviec.Text = db.DIC_Congviecs.Where(p => p.Macongviec == cviecnhanvien.Macongviec).FirstOrDefault().Tencongviec;
            lblNguoiky.Text = cviecnhanvien.Nguoiky;
            lblChucvunguoiky.Text = cviecnhanvien.Chucvunguoiky;
            lblNgayky.Text = cviecnhanvien.Ngayky.ToString("dd/MM/yyyy");
            lblNgayapdung.Text = cviecnhanvien.Ngayapdung.ToString("dd/MM/yyyy");

            lblLyDo.Text = Server.HtmlEncode(cviecnhanvien.LyDo);

            var nvien = (from p in db.PB_Nhanviens
                         where p.MaNV == cviecnhanvien.MaNV
                         select new
                         {
                             HoTen = p.HoNV + " " + p.TenNV
                         }).FirstOrDefault();
            lblHoten.Text = nvien.HoTen;

        }

        //Gan du lieu cho khung nhap lieu
        private void loadNewData(int macongviec)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var cviec = (from p in db.DIC_Congviecs
                        where ((macongviec != 0) ? p.Macongviec != macongviec : p.Macongviec != 0)
                        select new
                        {
                            p.Macongviec,
                            p.Tencongviec
                        }
                       ).ToList();

            cbCongviecmoi.Items.Clear();
            cbCongviecmoi.Items.Add(new ListItem("Vui lòng chọn", "0"));
            foreach (var p in cviec)
            {
                cbCongviecmoi.Items.Add(new ListItem(p.Tencongviec, p.Macongviec.ToString()));
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
                    PB_Thaydoicongviec pbannvien = new PB_Thaydoicongviec();

                    pbannvien.MaNV = Request.QueryString["id"];
                    pbannvien.Macongviec = int.Parse(cbCongviecmoi.SelectedValue);
                    pbannvien.Ngayapdung = DateTime.Now;
                    pbannvien.LyDo = txtLyDo.Text;
                    pbannvien.IsCurrent = true;

                    pbannvien.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    pbannvien.Nguoiky = cbNguoiky.SelectedItem.Text;

                    pbannvien.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    pbannvien.GhiChu = txtGhiChu.Text;

                    pbannvien.CreatedByUser = new Guid(Session["UserID"].ToString());
                    pbannvien.CreatedByDate = DateTime.Now;

                    db.PB_Thaydoicongviecs.InsertOnSubmit(pbannvien);
                    db.SubmitChanges();

                    DiarySystem(10, 7, pbannvien.MaNV);

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

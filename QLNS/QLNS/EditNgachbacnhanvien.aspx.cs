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
    /// Chức năng: 11
    /// Hành động: Cập nhật (7)
    /// </summary>
    public partial class EditNgachbacnhanvien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thay đổi ngạch - bậc lương nhân viên";
                loadRole();
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

        #region Load du lieu
        //Load du lieu
        private void loadData(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Thaydoibacluong bacnhanvien = (from p in db.PB_Thaydoibacluongs
                                             where p.IsCurrent == true && p.MaNV == manv
                                             orderby p.Ngayapdung descending
                                             select p).FirstOrDefault();

            if (bacnhanvien != null)
            {
                loadOldData(bacnhanvien);
            }
            loadNewData((bacnhanvien != null) ? bacnhanvien.MaNgach : 0, (bacnhanvien != null) ? bacnhanvien.BacLuong : 0);

            cbChucvunguoiky.Items.Clear();
            cbChucvunguoiky.Items.Add(new ListItem("Vui lòng chọn", "0"));

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
            cbNguoiky.Enabled = false;

            //Load ngay ap dung va ngay ky mac dinh
            DateTime now = DateTime.Now;
            lblBatdauapdung.Text = "Tháng " + now.Month.ToString("#,##0") + "/" + now.Year.ToString();
            txtNgayky.Value = now.ToString("dd/MM/yyyy");

            var nvien = (from p in db.PB_Nhanviens
                         where p.MaNV == manv
                         select new
                         {
                             HoTen = p.HoNV + " " + p.TenNV
                         }).FirstOrDefault();
            lblHoten.Text = nvien.HoTen;
        }

        //Gan du lieu cho phong ban cu~ cua nhan vien
        private void loadOldData(PB_Thaydoibacluong bacnhanvien)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            lblNgach.Text = db.DIC_NgachLuongs.Where(p => p.MaNgach == bacnhanvien.MaNgach).FirstOrDefault().TenNgach;
            lblBac.Text = db.DIC_BacLuongs.Where(p => p.MaNgach == bacnhanvien.MaNgach && p.Bac == bacnhanvien.BacLuong).FirstOrDefault().Tenbac;
            lblHeso.Text = bacnhanvien.Hesoluong.ToString("#,##0.#0");
            lblNguoiky.Text = bacnhanvien.Nguoiky;
            lblChucvunguoiky.Text = bacnhanvien.Chucvunguoiky;
            lblNgayky.Text = bacnhanvien.Ngayky.ToString("dd/MM/yyyy");
            lblNgayapdung.Text = "Tháng " + bacnhanvien.Ngayapdung.Month.ToString("#,##0") + "/" + bacnhanvien.Ngayapdung.Year.ToString();

            lblLyDo.Text = Server.HtmlEncode(bacnhanvien.LyDo);

        }

        //Gan du lieu cho khung nhap lieu
        private void loadNewData(int mangach,int bac)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            //var ngachluong = (from p in db.DIC_NgachLuongs
            //                  where ((mangach != 0) ? p.MaNgach == mangach : p.MaNgach != 0)
            //                  select new
            //                  {
            //                      p.MaNgach,
            //                      p.TenNgach
            //                  }
            //                  ).ToList();
            var ngachluong = (from p in db.DIC_NgachLuongs
                              select new
                              {
                                  p.MaNgach,
                                  p.TenNgach
                              }
                              ).ToList();

            cbNgach.Items.Clear();
            cbNgach.Items.Add(new ListItem("Vui lòng chọn", "0"));

            int i = 1;
            foreach (var p in ngachluong)
            {
                cbNgach.Items.Add(new ListItem(p.TenNgach, p.MaNgach.ToString()));
                if (p.MaNgach == mangach)
                {
                    cbNgach.SelectedIndex = i;
                }
                i++;
            }
            
            if (cbNgach.SelectedIndex != 0)
            {
                loadBac(int.Parse(cbNgach.SelectedValue));
            }
            else
            {
                cbBac.Items.Clear();
                cbBac.Items.Add(new ListItem("Vui lòng chọn", "0"));
                cbBac.Enabled = false;
            }
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

        //Them bac luong vao dropdownlist cbBac
        private void loadBac(int mangach)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstbacluong = (from p in db.DIC_BacLuongs
                            where p.MaNgach == mangach
                            select
                            new
                            {
                                p.Bac,
                                p.Tenbac
                            }).ToList();
            cbBac.Items.Clear();
            cbBac.Items.Add(new ListItem("Vui lòng chọn", "0"));

            foreach (var p in lstbacluong)
            {
                cbBac.Items.Add(new ListItem(p.Tenbac, p.Bac.ToString()));
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
                    if (cbNgach.SelectedItem.Text == lblNgach.Text && cbBac.SelectedItem.Text == lblBac.Text)
                    {
                        string strInfor = @"<div class='notification error png_bg'>
                                            <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                            <div>
                                                Thay đổi bậc lương thất bại.<br />
                                                <b>Ngạch</b> và <b>bậc</b> lương vừa chọn trùng với giá trị cũ.<br />
                                                Vui lòng chọn giá trị khác.
                                            </div>
                                        </div>";
                        ltrInfor.Text = strInfor;
                        return;
                    }

                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Thaydoibacluong bacluongnvien = new PB_Thaydoibacluong();

                    bacluongnvien.MaNV = Request.QueryString["id"];
                    bacluongnvien.MaNgach = int.Parse(cbNgach.SelectedValue);
                    bacluongnvien.BacLuong = int.Parse(cbBac.SelectedValue);
                    bacluongnvien.Hesoluong = double.Parse(lblHesoMoi.Text);
                    bacluongnvien.Ngayapdung = DateTime.Now;
                    bacluongnvien.LyDo = txtLyDo.Text;
                    bacluongnvien.IsCurrent = true;

                    bacluongnvien.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    bacluongnvien.Nguoiky = cbNguoiky.SelectedItem.Text;

                    bacluongnvien.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    bacluongnvien.GhiChu = txtGhiChu.Text;

                    bacluongnvien.CreatedByUser = new Guid(Session["UserID"].ToString());
                    bacluongnvien.CreatedByDate = DateTime.Now;

                    db.PB_Thaydoibacluongs.InsertOnSubmit(bacluongnvien);
                    db.SubmitChanges();

                    DiarySystem(11, 7, bacluongnvien.MaNV);

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
        protected void cbNgach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNgach.SelectedValue != "0")
            {
                loadBac(int.Parse(cbNgach.SelectedValue));
                cbBac.Enabled = true;
            }
            else
            {
                cbBac.Enabled = false;
                regNgach.IsValid = false;
            }
        }
        protected void cbBac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBac.SelectedValue != "0")
            {
                dbLinQDataContext db = new dbLinQDataContext();
                lblHesoMoi.Text = db.DIC_BacLuongs.Where(p => p.MaNgach == int.Parse(cbNgach.SelectedValue) && p.Bac == int.Parse(cbBac.SelectedValue)).FirstOrDefault().Heso.ToString();
            }
            else
            {
                regBac.IsValid = false;
            }
            
        }
        #endregion
        
    }
}

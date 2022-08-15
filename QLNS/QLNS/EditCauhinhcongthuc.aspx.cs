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
    /// Chức năng: 32
    /// Hành động: Tạo (6)
    /// </summary>
    public partial class EditCauhinhcongthuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Cài đặt công thức tính lương";
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
            DIC_Cauhinhcongthuc congthuc = (from p in db.DIC_Cauhinhcongthucs
                                            where p.IsCurrent == true
                                            orderby p.Ngayapdung descending
                                            select p).FirstOrDefault();

            if (congthuc != null)
            {
                loadOldData(congthuc);
                loadNewData();
            }

            cbChucvunguoiky.Items.Clear();
            cbChucvunguoiky.Items.Add(new ListItem("Vui lòng chọn","0"));
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
        private void loadOldData(DIC_Cauhinhcongthuc cauhinh)
        {
            lblBHXH.Text = cauhinh.BHXH.ToString("#,##0.#0");
            lblBHYT.Text = cauhinh.BHYT.ToString("#,##0.#0");
            lblBHTN.Text = cauhinh.BHTN.ToString("#,##0.#0");
            lblBHXHMax.Text = cauhinh.BHXHMAX.ToString("#,##0");

            lblMinIncomeTax.Text = cauhinh.TinhThueTNCN.ToString("#,##0");
            lblChichonguoiphuthuoc.Text = cauhinh.Chinguoiphuthuoc.ToString("#,##0");

            lblTangcaThuong.Text = cauhinh.Tangcathuong.ToString("#,##0.#0");
            lblTangcaChunhat.Text = cauhinh.Tangchunhat.ToString("#,##0.#0");
            lblTangcanghile.Text = cauhinh.Tangnghile.ToString("#,##0.#0");

            lblPhicongdoan.Text = cauhinh.Phicongdoan.ToString("#,##0.#0");
            lblPhicongdoanMax.Text = cauhinh.PhicongdoanMax.ToString("#,##0");

            lblNguoiky.Text = cauhinh.Nguoiky;
            lblChucvunguoiky.Text = cauhinh.Chucvunguoiky;
            lblNgayky.Text = string.Format("{0:dd/MM/yyyy}", cauhinh.Ngayky);
            lblNgayapdung.Text = "Tháng " + cauhinh.Ngayapdung.Month.ToString("#,##0") + "/" + cauhinh.Ngayapdung.Year.ToString();

            lblMota.Text = Server.HtmlDecode(cauhinh.Mota);
        }

        //Gan du lieu cho khung nhap lieu
        private void loadNewData()
        {
            txtBHXH.Text = lblBHXH.Text;
            txtBHYT.Text = lblBHYT.Text;
            txtBHTN.Text = lblBHTN.Text;
            txtBHXHMax.Text = lblBHXHMax.Text;

            txtMinIncomeTax.Text = lblMinIncomeTax.Text;
            txtChichonguoiphuthuoc.Text = lblChichonguoiphuthuoc.Text;

            txtTangcaThuong.Text = lblTangcaThuong.Text;
            txtTangcaChunhat.Text = lblTangcaChunhat.Text;
            txtTangcaNghile.Text = lblTangcanghile.Text;

            txtPhicongdoan.Text = lblPhicongdoan.Text;
            txtPhicongdoanMax.Text = lblPhicongdoanMax.Text;

            //lblNguoiky.Text
            //lblChucvunguoiky.Text
            DateTime now = DateTime.Now;
            lblBatdauapdung.Text = "Tháng " + now.Month.ToString("#,##0") + "/" + now.Year.ToString();

            txtNgayky.Value = now.ToString("dd/MM/yyyy");

            txtDescription.Text = lblMota.Text;
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
                    DIC_Cauhinhcongthuc congthuc = new DIC_Cauhinhcongthuc();

                    congthuc.Macauhinh = Guid.NewGuid();
                    congthuc.Ngayapdung = DateTime.Now;
                    congthuc.IsCurrent = true;

                    congthuc.BHXH = double.Parse(txtBHXH.Text);
                    congthuc.BHYT = double.Parse(txtBHYT.Text);
                    congthuc.BHTN = double.Parse(txtBHTN.Text);
                    congthuc.BHXHMAX = int.Parse(txtBHXHMax.Text.Replace(",", ""));

                    congthuc.Phicongdoan = double.Parse(txtPhicongdoan.Text);
                    congthuc.PhicongdoanMax = int.Parse(txtPhicongdoanMax.Text.Replace(",", ""));

                    congthuc.TinhThueTNCN = int.Parse(txtMinIncomeTax.Text.Replace(",", ""));
                    congthuc.Chinguoiphuthuoc = int.Parse(txtChichonguoiphuthuoc.Text.Replace(",", ""));

                    congthuc.Tangcathuong = double.Parse(txtTangcaThuong.Text);
                    congthuc.Tangchunhat = double.Parse(txtTangcaChunhat.Text);
                    congthuc.Tangnghile = double.Parse(txtTangcaNghile.Text);

                    congthuc.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    congthuc.Nguoiky = cbNguoiky.SelectedItem.Text;

                    congthuc.Ngayky = DateTime.Parse(txtNgayky.Value, new CultureInfo("vi-vn"));
                    congthuc.Mota = txtDescription.Text;

                    congthuc.CreatedByUser = new Guid(Session["UserID"].ToString());
                    congthuc.CreatedByDate = DateTime.Now;

                    db.DIC_Cauhinhcongthucs.InsertOnSubmit(congthuc);
                    db.SubmitChanges();

                    DiarySystem(32, 6, congthuc.Macauhinh.ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cài đặt thành công'); window.location = 'Congthuctinhluong';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Congthuctinhluong';", true);
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

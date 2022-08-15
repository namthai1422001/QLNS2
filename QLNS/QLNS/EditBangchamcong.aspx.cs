using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 6
    /// Chức năng: 19
    /// Hành động: Tạo (6), Cập nhật (7)
    /// </summary>
    public partial class EditBangchamcong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                loadRole();

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Cập nhật thông tin bảng chấm công";
                    ltrh3.Text = "Cập nhật thông tin bảng chấm công";
                    loadNgay();
                    Guid id;
                    try
                    {
                        id = new Guid(Request.QueryString["id"]);
                        loadEditData(id);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Danhsachbangchamcong';", true);
                    }
                }
                else
                {
                    Page.Title = "Tạo bảng chấm công";
                    ltrh3.Text = "Tạo bảng chấm công";
                    VisibleButton(true, false, false, false);
                    loadAddData();
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
            //Người có quyền hoàn thành bảng chấm công mới có thể sử dụng chức năng này
            if (UserRolls.Where(p => p == 6).Count() == 0)
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
        private void VisibleButton(bool them, bool khoa, bool mokhoa, bool hoanthanh)
        {
            btnAdd.Visible = them;
            btnLock.Visible = khoa;
            btnUnlock.Visible = mokhoa;
            btnFinish.Visible = hoanthanh;
        }
        private void EnableControl(bool nam, bool thang, bool chucvunguoiky, bool nguoiky, bool ngayky)
        {
            cbNam.Enabled = nam;
            cbThang.Enabled = thang;
            cbChucvunguoiky.Enabled = chucvunguoiky;
            cbNguoiky.Enabled = nguoiky;
            txtNgayky.Disabled = ngayky;
        }
        #endregion

        #region Methods
        //Load Ngay thang mac dinh
        private void loadNgay()
        {
            DateTime now = DateTime.Now;
            txtNgayky.Value = now.ToString("dd/MM/yyyy");
        }

        private void loadAddData()
        {
            //Unvisible pnlEdit
            pnlEdit.Visible = false;
            pnlThongtin.Visible = false;

            //Clear Items
            cbNam.Items.Clear();
            cbThang.Items.Clear();

            //Disable
            cbThang.Enabled = false;

            //Bind Item first (Vui long chon)
            cbNam.Items.Add(new ListItem(" Chọn ", "0"));
            cbThang.Items.Add(new ListItem(" Chọn ", "0"));

            int Nam = DateTime.Now.Year;

            //Bind data cbNam
            for (int i = 0; i < 5; i++)
            {
                cbNam.Items.Add(new ListItem((Nam - i).ToString(), (Nam - i).ToString()));
            }
        }

        private void loadEditData(Guid Mabangchamcong)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Danhsachchamcong objData = (from p in db.PB_Danhsachchamcongs
                                           where p.Mabangchamcong == Mabangchamcong
                                           select p).FirstOrDefault();

            if (objData != null)
            {
                //Clear Items
                cbNam.Items.Clear();
                cbThang.Items.Clear();

                //Bind data
                cbNam.Items.Add(new ListItem(objData.Nam.ToString(), objData.Nam.ToString()));
                cbThang.Items.Add(new ListItem(objData.Thang.ToString(), objData.Thang.ToString()));

                if (objData.IsFinish == true)
                {
                    cbChucvunguoiky.Items.Add(new ListItem(objData.Chucvunguoiky, objData.Chucvunguoiky));
                    cbNguoiky.Items.Add(new ListItem(objData.Nguoiky, objData.Nguoiky));
                    txtNgayky.Value = objData.Ngayky.Value.ToString("dd/MM/yyyy");
                    ltrStatus.Text = "Đã hoàn thành";
                    EnableControl(false, false, false, false, true);
                    VisibleButton(false, false, false, false);
                }
                else
                {
                    var lstChucvu = (from p in db.DIC_Chucvus
                                     where p.IsActive == true
                                     select new { p.Machucvu, p.Tenchucvu }).ToList();
                    //Clear Items
                    cbChucvunguoiky.Items.Clear();
                    cbNguoiky.Items.Clear();

                    //Bind Item first (Vui long chon)
                    cbChucvunguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
                    cbNguoiky.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));

                    //Bind data and cbChucvunguoiky
                    foreach (var p in lstChucvu)
                    {
                        cbChucvunguoiky.Items.Add(new ListItem(p.Tenchucvu, p.Machucvu.ToString()));
                    }
                    if (objData.IsLock == true)
                    {
                        ltrStatus.Text = "Đã bị khóa";
                        EnableControl(false, false, true, false, false);
                        VisibleButton(false, false, true, true);
                    }
                    else
                    {
                        ltrStatus.Text = "Chưa khóa";
                        EnableControl(false, false, true, false, false);
                        VisibleButton(false, true, false, true);
                    }
                }

                //Load thong tin ve bang cham cong hien tai
                var objThongtin = (from chcongngay in db.PB_ChamcongNhanviens
                                   join chcongtangca in db.PB_Chamtangcas
                                    on chcongngay.Mabangchamcong equals chcongtangca.Mabangchamcong
                                   where chcongngay.Mabangchamcong == Mabangchamcong
                                        && chcongngay.MaNV == chcongtangca.MaNV
                                   group new
                                   {
                                       chcongngay.Sogiocong,
                                       chcongngay.Sogionghiphep,
                                       chcongtangca.TCthuong,
                                       chcongtangca.TCchunhat,
                                       chcongtangca.TCnghile
                                   } by chcongngay.Mabangchamcong into g
                                   select new
                                   {
                                       g.Key,
                                       Tonggiocong = g.Sum(p => p.Sogiocong),
                                       Tonggionghiphep = g.Sum(p => p.Sogionghiphep),
                                       Tonggiotangcathuong = g.Sum(p => p.TCthuong),
                                       Tonggiotangcachunhat = g.Sum(p => p.TCchunhat),
                                       Tonggiotangcanghile = g.Sum(p => p.TCnghile),
                                   }).FirstOrDefault();

                ltrTonggiocong.Text = objThongtin.Tonggiocong.ToString();
                ltrTonggionghi.Text = objThongtin.Tonggionghiphep.ToString();

                ltrTongtangcathuong.Text = objThongtin.Tonggiotangcathuong.ToString();
                ltrTongtangcachunhat.Text = objThongtin.Tonggiotangcachunhat.ToString();
                ltrTongTangcanghile.Text = objThongtin.Tonggiotangcanghile.ToString();

                ltrNguoicapnhatsaucung.Text = db.SYS_Nguoidungs.Where(p => p.ID == objData.CreatedByUser).FirstOrDefault().Username;
                ltrNgaycapnhat.Text = string.Format("{0:HH:mm:ss dd/MM/yyyy}", objData.CreatedByDate);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Danhsachbangchamcong';", true);
            }

        }

        //Load cbNam
        private void loadThangtheoNam(int Nam)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            //Nhung thang da co bang cham cong
            List<int> lstDaco = (from p in db.PB_Danhsachchamcongs
                                 where p.Nam == Nam
                                 select p.Thang).ToList();
            //Thoi gian hien tai
            DateTime now = DateTime.Now;

            //Cac thang trong nam
            List<int> lstThang = new List<int>();

            int i;
            if (Nam < now.Year)
            {
                for (i = 1; i <= 12; i++)
                {
                    lstThang.Add(i);
                }
            }
            else
            {
                for (i = 1; i <= now.Month; i++)
                {
                    lstThang.Add(i);
                }
            }

            //Cac thang chua co bang cham cong
            List<int> lstThangConlai = (from p in lstThang
                                        where !lstDaco.Contains(p)
                                        orderby p descending
                                        select p
                                        ).ToList();


            //Clear Items
            cbThang.Items.Clear();

            //Bind Items
            cbThang.Items.Add(new ListItem(" Chọn ", "0"));

            foreach (var p in lstThangConlai)
            {
                cbThang.Items.Add(new ListItem(p.ToString(), p.ToString()));
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
        protected void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNam.SelectedValue != "0")
            {
                loadThangtheoNam(int.Parse(cbNam.SelectedValue));
                cbThang.Enabled = true;
            }
            else
            {
                cbThang.Items.Clear();
                cbThang.Items.Add(new ListItem(" Chọn ", "0"));
                cbThang.Enabled = false;
                regNam.IsValid = false;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Danhsachchamcong objDataSearch = (from p in db.PB_Danhsachchamcongs
                                                         where p.Nam == int.Parse(cbNam.SelectedValue)
                                                                && p.Thang == int.Parse(cbThang.SelectedValue)
                                                         select p).FirstOrDefault();
                    if (objDataSearch == null)
                    {
                        PB_Danhsachchamcong objData = new PB_Danhsachchamcong();

                        objData.Mabangchamcong = Guid.NewGuid();
                        objData.Nam = int.Parse(cbNam.SelectedValue);
                        objData.Thang = int.Parse(cbThang.SelectedValue);

                        objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                        objData.CreatedByDate = DateTime.Now;
                        objData.IsLock = false;
                        objData.IsFinish = false;

                        db.PB_Danhsachchamcongs.InsertOnSubmit(objData);
                        db.SubmitChanges();

                        DiarySystem(19, 6, "Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tạo bảng chấm công thành công'); window.location = 'Danhsachbangchamcong';", true);
                    }
                    else
                    {
                        //Da ton tai bang cham cong
                        //throw new Exception();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue + " đã được tạo bởi người khác. Không thể tạo lại'); window.location = 'Danhsachbangchamcong';", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangchamcong';", true);
                };
            }
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachchamcong objData = (from p in db.PB_Danhsachchamcongs
                                               where p.Mabangchamcong == new Guid(Request.QueryString["id"])
                                               select p
                                                   ).FirstOrDefault();
                objData.IsLock = true;

                db.SubmitChanges();
                DiarySystem(19, 7, "Khóa Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã khóa'); window.location = 'Danhsachbangchamcong';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangchamcong';", true);
            };
        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachchamcong objData = (from p in db.PB_Danhsachchamcongs
                                               where p.Mabangchamcong == new Guid(Request.QueryString["id"])
                                               select p
                                                   ).FirstOrDefault();
                if (!objData.IsFinish)
                {
                    objData.IsLock = false;

                    db.SubmitChanges();

                    DiarySystem(19, 7, "Mở khóa Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã được mở khóa'); window.location = 'Danhsachbangchamcong';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã hoàn thành. Không thể chỉnh sửa'); window.location = 'Danhsachbangchamcong';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangchamcong';", true);
            };
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachchamcong objData = (from p in db.PB_Danhsachchamcongs
                                               where p.Mabangchamcong == new Guid(Request.QueryString["id"])
                                               select p
                                                   ).FirstOrDefault();
                if (!objData.IsFinish)
                {
                    objData.Chucvunguoiky = cbChucvunguoiky.SelectedItem.Text;
                    objData.Nguoiky = cbNguoiky.SelectedItem.Text;
                    objData.Ngayky = DateTime.Parse(txtNgayky.Value, new System.Globalization.CultureInfo("vi-vn"));
                    objData.IsLock = true;
                    objData.IsFinish = true;

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.SubmitChanges();
                    DiarySystem(19, 7, "Hoàn thành Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã hoàn thành'); window.location = 'Danhsachbangchamcong';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng chấm công tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã được hoàn thành bởi người khác. Không thể chỉnh sửa'); window.location = 'Danhsachbangchamcong';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangchamcong';", true);
            };
        }
        #endregion
    }
}

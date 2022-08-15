using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 8
    /// Chức năng: 23
    /// Hành động: Tạo (6), Cập nhật (7)
    /// </summary>
    public partial class EditBangluong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                loadRole();

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Cập nhật thông tin bảng lương";
                    ltrh3.Text = "Cập nhật thông tin bảng lương";
                    loadNgay();
                    Guid id;
                    try
                    {
                        id = new Guid(Request.QueryString["id"]);
                        loadEditData(id);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Danhsachbangluong';", true);
                    }
                }
                else
                {
                    Page.Title = "Tạo bảng lương";
                    ltrh3.Text = "Tạo bảng lương";
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
            //Người có quyền hoàn thành bảng lương mới có thể sử dụng chức năng này
            if (UserRolls.Where(p => p == 8).Count() == 0)
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

            dbLinQDataContext db = new dbLinQDataContext();

            bool objCauhinhcongthuc = (from p in db.DIC_Cauhinhcongthucs
                                       where p.IsCurrent == true
                                       select p.IsCurrent).FirstOrDefault();
            if (!objCauhinhcongthuc)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Công thức tính lương chưa được cấu hình. Vui lòng cấu hình công thức lương trước khi tạo bảng lương'); window.location = 'Danhsachbangluong';", true);
                return;
            }
            bool objLuongtoithieu = (from p in db.PB_Luongtoithieus
                                     where p.IsCurrent == true
                                     select p.IsCurrent).FirstOrDefault();
            if (!objLuongtoithieu)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mức lương tối thiểu chưa được cấu hình. Vui lòng cấu hình mức lương tối thiểu trước khi tạo bảng lương'); window.location = 'Danhsachbangluong';", true);
                return;
            }
            bool objSongaychamcongtrongthang = (from p in db.PB_Thaydoitongsongaychamcongs
                                                where p.IsCurrent == true
                                                select p.IsCurrent).FirstOrDefault();
            if (!objSongaychamcongtrongthang)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Quy định số ngày chấm công trong tháng chưa được cấu hình. Vui lòng cấu hình quy định số ngày chấm công trước khi tạo bảng lương'); window.location = 'Danhsachbangluong';", true);
                return;
            }


            //Nhung bang cham cong da hoan thanh
            var lstDachamcong = (from dschamcong in db.PB_Danhsachchamcongs
                                 where dschamcong.IsFinish == true
                                 select new
                                 {
                                     dschamcong.Nam,
                                     dschamcong.Thang
                                 }).ToList();
            //Nhung bang luong da duoc tao
            var lstDacobangluong = (from dsbangluong in db.PB_Danhsachbangluongs
                                    select new { dsbangluong.Nam, dsbangluong.Thang }
                                    ).ToList();

            //List cac bang luong co the tao
            var lstBangluongcothetao = (from p in lstDachamcong
                                        where !lstDacobangluong.Contains(p)
                                        select p).ToList();

            //List Nam co the tao bang luong
            List<int> lstNamcothetao = (from p in lstBangluongcothetao
                                        orderby p.Nam descending
                                        select p.Nam).Distinct().ToList();
            if (lstNamcothetao.Count() > 0)
            {

                //Clear Items
                cbNam.Items.Clear();
                cbThang.Items.Clear();

                //Disable
                cbThang.Enabled = false;

                //Bind Item first (Vui long chon)
                cbNam.Items.Add(new ListItem(" Chọn ", "0"));
                cbThang.Items.Add(new ListItem(" Chọn ", "0"));

                //Bind data cbNam
                foreach (var p in lstNamcothetao)
                {
                    cbNam.Items.Add(new ListItem(p.ToString(), p.ToString()));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Phải tồn tại bảng lương có trạng thái là chưa hoàn thành mới có thể tạo bảng lương'); window.location = 'Danhsachbangluong';", true);
            }
        }

        private void loadEditData(Guid Mabangluong)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Danhsachbangluong objData = (from p in db.PB_Danhsachbangluongs
                                           where p.Mabangluong == Mabangluong
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

                //Load thong tin bang luong
                var objThongtin = (from bangluong in db.sp_PB_SoLuong_Select_All(Mabangluong)
                                   join bangluongtangca in  db.sp_PB_Luongtangca_Select_All(Mabangluong)
                                   on bangluong.Mabangluong equals bangluongtangca.Mabangluong
                                   where bangluong.MaNV == bangluongtangca.MaNV
                                   group new
                                   {
                                       bangluong.TienBHXH,
                                       bangluong.TienBHYT,
                                       bangluong.TienBHTN,
                                       bangluong.TienPhicongdoan,
                                       bangluong.Tienluong,
                                       bangluong.TienThueTNCN,
                                       bangluong.Tongtamung,
                                       bangluong.Tienlamthem,
                                       bangluong.Tongkhautru,
                                       bangluong.Tongthuong,
                                       bangluong.Thuclanh,
                                       bangluongtangca.Tientangcathuong,
                                       bangluongtangca.Tientangcachunhat,
                                       bangluongtangca.Tientangcanghile,
                                       bangluongtangca.Tongluongtangca,
                                   } by bangluong.Mabangluong into gg
                                   select new
                                   {
                                       gg.Key,
                                       TienBHXH = gg.Sum(p => p.TienBHXH),
                                       TienBHYT = gg.Sum(p => p.TienBHYT),
                                       TienBHTN = gg.Sum(p => p.TienBHTN),
                                       TienPhicongdoan = gg.Sum(p => p.TienPhicongdoan),
                                       Tienluong = gg.Sum(p => p.Tienluong),
                                       TienThueTNCN = gg.Sum(p => p.TienThueTNCN),
                                       Tongtamung = gg.Sum(p => p.Tongtamung),
                                       Tienlamthem = gg.Sum(p => p.Tienlamthem),
                                       Tongkhautru = gg.Sum(p => p.Tongkhautru),
                                       Tongthuong = gg.Sum(p => p.Tongthuong),
                                       Thuclanh = gg.Sum(p => p.Thuclanh),
                                       Tientangcathuong = gg.Sum(p => p.Tientangcathuong),
                                       Tientangcachunhat = gg.Sum(p => p.Tientangcachunhat),
                                       Tientangcanghile = gg.Sum(p => p.Tientangcanghile),
                                       Tongluongtangca = gg.Sum(p => p.Tongluongtangca)
                                   }).FirstOrDefault();

                ltrBHXH.Text = objThongtin.TienBHXH.Value.ToString("#,##0");
                ltrBHYT.Text = objThongtin.TienBHYT.Value.ToString("#,##0");
                ltrBHTN.Text = objThongtin.TienBHTN.Value.ToString("#,##0");
                ltrPhicongdoan.Text = objThongtin.TienPhicongdoan.Value.ToString("#,##0");

                ltrTienluongngay.Text = objThongtin.Tienluong.Value.ToString("#,##0");
                ltrThueTNCN.Text = objThongtin.TienThueTNCN.Value.ToString("#,##0");

                ltrTamung.Text = objThongtin.Tongtamung.Value.ToString("#,##0");
                ltrLamthem.Text = objThongtin.Tienlamthem.Value.ToString("#,##0");
                ltrKhautru.Text = objThongtin.Tongkhautru.Value.ToString("#,##0");
                ltrThuong.Text = objThongtin.Tongthuong.Value.ToString("#,##0");
                ltrThuclanh.Text = objThongtin.Thuclanh.Value.ToString("#,##0");

                ltrTangcathuong.Text = objThongtin.Tientangcathuong.Value.ToString("#,##0");
                ltrTangcachunhat.Text = objThongtin.Tientangcachunhat.Value.ToString("#,##0");
                ltrTangcanghile.Text = objThongtin.Tientangcanghile.Value.ToString("#,##0");
                ltrTongTangca.Text = objThongtin.Tongluongtangca.Value.ToString("#,##0");

                ltrNguoicapnhat.Text = db.SYS_Nguoidungs.Where(p => p.ID == objData.CreatedByUser).FirstOrDefault().Username;
                ltrNgaycapnhat.Text = string.Format("{0:HH:mm:ss dd/MM/yyyy}", objData.CreatedByDate);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Danhsachbangluong';", true);
            }

        }

        //Load cbThang theo nam
        private void loadThangtheoNam(int Nam)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            //Nhung thang da co bang luong
            List<int> lstDaco = (from p in db.PB_Danhsachbangluongs
                                 where p.Nam == Nam
                                 select p.Thang).ToList();
            //Thoi gian hien tai
            DateTime now = DateTime.Now;

            //Cac thang trong nam da co bang cham cong hoan thanh nhung chua co bang luong
            List<int> lstThangConlai = (from p in db.PB_Danhsachchamcongs
                                        where p.IsFinish == true && p.Nam == Nam
                                              && !lstDaco.Contains(p.Thang)
                                        select p.Thang).ToList();

            //Clear Items
            cbThang.Items.Clear();

            if (lstThangConlai.Count() > 0)
            {

                //Bind Items
                cbThang.Items.Add(new ListItem(" Chọn ", "0"));

                foreach (var p in lstThangConlai)
                {
                    cbThang.Items.Add(new ListItem(p.ToString(), p.ToString()));
                }
            }
            else
            {
                //Khong co bang cham cong da hoan thanh de tao bang luong
                //Bind Items
                cbThang.Items.Add(new ListItem("-- Không tồn tại bảng chấm công đã hoàn thành để tính lương --", "0"));
                cbThang.Width = 420;

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

        //Tao danh sach bang luong
        private void CreateSalaryList()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            //Mot so thong tin can cho viec tao bang luong
            int _luongtoithieu = (from p in db.PB_Luongtoithieus
                                  where p.IsCurrent == true
                                  select p.Sotien).FirstOrDefault();
            int _songaychamcong = (from p in db.PB_Thaydoitongsongaychamcongs
                                   where p.IsCurrent == true
                                   select p.Tongsongaychamcong).FirstOrDefault();
            var objCongthuc = (from p in db.DIC_Cauhinhcongthucs
                               where p.IsCurrent == true
                               select
                                   new
                                   {
                                       p.BHTN,
                                       p.BHXH,
                                       p.BHXHMAX,
                                       p.BHYT,
                                       p.Chinguoiphuthuoc,
                                       p.Phicongdoan,
                                       p.PhicongdoanMax,
                                       p.Tangcathuong,
                                       p.Tangchunhat,
                                       p.Tangnghile,
                                       p.TinhThueTNCN
                                   }).FirstOrDefault();

            PB_Danhsachbangluong objData = new PB_Danhsachbangluong();

            objData.Mabangluong = Guid.NewGuid();
            objData.Nam = int.Parse(cbNam.SelectedValue);
            objData.Thang = int.Parse(cbThang.SelectedValue);

            objData.Luongtoithieu = _luongtoithieu;
            objData.Tongsogioquydinh = (_songaychamcong * 8);

            objData.Tienbatdautinhthue = objCongthuc.TinhThueTNCN;
            objData.Tienmoinguoiphuthuoc = objCongthuc.Chinguoiphuthuoc;

            objData.Hesothuong = objCongthuc.Tangcathuong;
            objData.Hesochunhat = objCongthuc.Tangchunhat;
            objData.Hesonghile = objCongthuc.Tangnghile;

            objData.BHTN = objCongthuc.BHTN;
            objData.BHXH = objCongthuc.BHXH;
            objData.BHYT = objCongthuc.BHYT;
            objData.BHXHMAX = objCongthuc.BHXHMAX;

            objData.Phicongdoan = objCongthuc.Phicongdoan;
            objData.PhicongdoanMax = objCongthuc.PhicongdoanMax;

            objData.CreatedByUser = new Guid(Session["UserID"].ToString());
            objData.CreatedByDate = DateTime.Now;
            objData.IsLock = false;
            objData.IsFinish = false;

            db.PB_Danhsachbangluongs.InsertOnSubmit(objData);
            db.SubmitChanges();

            DiarySystem(23, 6, "Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tạo bảng lương thành công'); window.location = 'Danhsachbangluong';", true);
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
                    //Kiem tra su ton tai cua bang luong
                    PB_Danhsachbangluong objDataSearch = (from p in db.PB_Danhsachbangluongs
                                                         where p.Nam == int.Parse(cbNam.SelectedValue)
                                                                && p.Thang == int.Parse(cbThang.SelectedValue)
                                                         select p).FirstOrDefault();
                    if (objDataSearch == null)
                    {
                        //Danh sach nhan vien trong bang cham cong
                        var lstNhanvien = (from p in db.PB_ChamcongNhanviens
                                           where p.Mabangchamcong == (
                                                                        from q in db.PB_Danhsachchamcongs
                                                                        where q.Nam == int.Parse(cbNam.SelectedValue)
                                                                            && q.Thang == int.Parse(cbThang.SelectedValue)
                                                                        select q.Mabangchamcong
                                                                     ).FirstOrDefault()
                                           select p.MaNV).ToList();

                        //Nhan vien trong bang cham cong da co he so luong
                        var lstHesoNhanvien = (from p in db.PB_Thaydoibacluongs
                                               where lstNhanvien.Contains(p.MaNV)
                                                && p.IsCurrent == true
                                               select p.MaNV).ToList();
                        if (lstNhanvien.Count() == lstHesoNhanvien.Count())
                        {
                            CreateSalaryList();
                        }
                        else
                        {
                            List<string> lstNhanvienchuacoheso = (from p in lstNhanvien
                                                                  where !lstHesoNhanvien.Contains(p)
                                                                  select p).ToList();
                            string strListNhanvien = "";
                            foreach (string p in lstNhanvienchuacoheso)
                            {
                                strListNhanvien += " - "+ p + "<br />";
                            }
                            string strInfor = @"<div class='notification error png_bg'>
                                                <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                                <div>
                                                    Không thể tạo bảng lương.<br />
                                                    Các nhân viên:<br /><b>"
                                                    + strListNhanvien +
                                                    @"Đã chấm công</b> nhưng <b>không tồn tại hệ số lương</b>.<br />
                                                      Phải <b>cài đặt hệ số lương</b> cho các <b>nhân viên</b> này trước khi <b>tạo bảng lương</b>.
                                                </div>
                                            </div>";
                            ltrInfor.Text = strInfor;
                        }
                    }
                    else
                    {
                        //Da ton tai bang luong
                        //throw new Exception();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue + " đã được tạo bởi người khác. Không thể tạo lại'); window.location = 'Danhsachbangluong';", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangluong';", true);
                };
            }
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachbangluong objData = (from p in db.PB_Danhsachbangluongs
                                               where p.Mabangluong == new Guid(Request.QueryString["id"])
                                               select p
                                                   ).FirstOrDefault();
                objData.IsLock = true;

                db.SubmitChanges();

                DiarySystem(23, 7, "Khóa Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã khóa'); window.location = 'Danhsachbangluong';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangluong';", true);
            };
        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachbangluong objData = (from p in db.PB_Danhsachbangluongs
                                               where p.Mabangluong == new Guid(Request.QueryString["id"])
                                               select p
                                                   ).FirstOrDefault();
                if (!objData.IsFinish)
                {
                    objData.IsLock = false;

                    db.SubmitChanges();

                    DiarySystem(23, 7, "Mở khóa Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã được mở khóa'); window.location = 'Danhsachbangluong';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã hoàn thành. Không thể chỉnh sửa'); window.location = 'Danhsachbangluong';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangluong';", true);
            };
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Danhsachbangluong objData = (from p in db.PB_Danhsachbangluongs
                                               where p.Mabangluong == new Guid(Request.QueryString["id"])
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

                    DiarySystem(23, 7, "Hoàn thành Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã hoàn thành'); window.location = 'Danhsachbangluong';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bảng lương tháng " + objData.Thang.ToString() + "/" + objData.Nam.ToString() + " đã được hoàn thành bởi người khác. Không thể chỉnh sửa tiếp'); window.location = 'Danhsachbangluong';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Danhsachbangluong';", true);
            };
        }
        #endregion
    }
}

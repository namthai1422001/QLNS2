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
    /// Chức năng: 6
    /// Hành động: Xem (5), Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditThongtincanhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadRole();
                loaddropdownlist();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Cập nhật thông tin nhân viên";
                    imgNhanvien.ImageUrl = "~/QLNS/Employee-images/chuaco.png";
                    loaddulieu(Request.QueryString["id"].ToString());
                    btnAdd.Visible = false;
                    btnDelete.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    Page.Title = "Thêm nhân viên";
                    imgNhanvien.ImageUrl = "~/QLNS/Employee-images/chuaco.png";
                    btnAdd.Visible = true;
                    btnDelete.Visible = false;
                    btnUpdate.Visible = false;
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

        #region Methods

        private void loaddulieu(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Nhanvien nvien = db.PB_Nhanviens.Where(p => p.MaNV == manv).FirstOrDefault();
            if (nvien != null)
            {
                loadthongtincanhan(nvien);

                DiarySystem(6, 5, nvien.MaNV);
            }
            else
            {
                Response.Redirect("DanhsachNhanvien");
            }
        }

        //Gan du lieu cho cac Dropdownlist
        private void loaddropdownlist()
        {
            dbLinQDataContext db = new dbLinQDataContext();

            var lstquoctich = (from p in db.DIC_Quoctiches
                               where p.IsActive == true
                               select new
                               {
                                   Ma = p.Maquoctich,
                                   Ten = p.Tenquoctich
                               }).ToList();
            var lstdantoc = (from p in db.DIC_Dantocs
                             where p.IsActive == true
                             select new
                             {
                                 Ma = p.Madantoc,
                                 Ten = p.Tendantoc
                             }).ToList();
            var lsttongiao = (from p in db.DIC_Tongiaos
                              where p.IsActive == true
                              select new
                              {
                                  Ma = p.MaTG,
                                  Ten = p.TenTG
                              }).ToList();
            var lstbangcap = (from p in db.DIC_Bangcaps
                              where p.IsActive == true
                              select new
                              {
                                  Ma = p.Mabang,
                                  Ten = p.Tenbang
                              }).ToList();
            var lstngoaingu = (from p in db.DIC_Ngonngus
                               where p.IsActive == true
                               select new
                               {
                                   Ma = p.Mangonngu,
                                   Ten = p.Tenngonngu
                               }).ToList();
            var lstchuyenmon = (from p in db.DIC_Chuyenmons
                                where p.IsActive == true
                                select new
                                {
                                    Ma = p.Machuyenmon,
                                    Ten = p.Tenchuyenmon
                                }).ToList();
            var lsttinhoc = (from p in db.DIC_Tinhocs
                             where p.IsActive == true
                             select new
                             {
                                 Ma = p.MaTH,
                                 Ten = p.TenTH
                             }).ToList();

            //Begin Bind data
            cbQuoctich.Items.Add(new ListItem("--- Vui lòng chọn ---", "0"));
            foreach (var p in lstquoctich)
            {
                cbQuoctich.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }
            cbTongiao.Items.Add(new ListItem("--- Vui lòng chọn ---", "0"));
            foreach (var p in lsttongiao)
            {
                cbTongiao.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }
            cbDantoc.Items.Add(new ListItem("--- Vui lòng chọn ---", "0"));
            foreach (var p in lstdantoc)
            {
                cbDantoc.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }
            cbBangcap.Items.Add(new ListItem("--- Vui lòng chọn ---", "0"));
            foreach (var p in lstbangcap)
            {
                cbBangcap.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }

            cbNgoaingu.Items.Add(new ListItem("--- Chọn ---", "0"));
            foreach (var p in lstngoaingu)
            {
                cbNgoaingu.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }
            cbChuyenmon.Items.Add(new ListItem("--- Chọn ---", "0"));
            foreach (var p in lstchuyenmon)
            {
                cbChuyenmon.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }
            cbTinhoc.Items.Add(new ListItem("--- Chọn ---", "0"));
            foreach (var p in lsttinhoc)
            {
                cbTinhoc.Items.Add(new ListItem(p.Ten, p.Ma.ToString()));
            }


            //End Bind data
        }

        private void loadthongtincanhan(PB_Nhanvien nvien)
        {
            if (!string.IsNullOrEmpty(nvien.Hinhanh))
                imgNhanvien.ImageUrl = "~/QLNS/Employee-images/" + nvien.Hinhanh;
            txtMaNV.Text = nvien.MaNV;
            txtHoNV.Text = nvien.HoNV;
            txtTenNV.Text = nvien.TenNV;
            txtTenthuonggoi.Text = (!string.IsNullOrEmpty(nvien.Bidanh)) ? nvien.Bidanh : "";
            chkGender.Checked = nvien.Nu;
            chkKethon.Checked = nvien.Honnhan;
            txtNgaysinh.Value = nvien.Ngaysinh.ToString("dd/MM/yyyy");
            txtNoisinh.Text = nvien.Noisinh;
            txtDiachi.Text = nvien.Diachi;
            txtTamtru.Text = nvien.Tamtru;
            txtDienthoaididong.Text = nvien.Dienthoaididong;
            txtDienthoainha.Text = nvien.Dienthoainha;
            txtEmail.Text = nvien.Email;

            txtSoCMNN.Text = nvien.SoCMNN;
            txtNgaycap.Value = nvien.Ngaycap.ToString("dd/MM/yyyy");
            txtNoicap.Text = nvien.Noicap;

            txtTinhtrangsuckhoe.Text = nvien.Suckhoe;

            txtDescription.Text = nvien.GhiChu;

            int i;
            int dem = cbQuoctich.Items.Count;
            for (i = 1; i < dem; i++)
            {
                if (int.Parse(cbQuoctich.Items[i].Value) == nvien.Maquoctich)
                {
                    cbQuoctich.SelectedIndex = i;
                    break;
                }
            }

            dem = cbDantoc.Items.Count;
            for (i = 1; i < dem; i++)
            {
                if (int.Parse(cbDantoc.Items[i].Value) == nvien.Madantoc)
                {
                    cbDantoc.SelectedIndex = i;
                    break;
                }
            }

            dem = cbTongiao.Items.Count;
            for (i = 1; i < dem; i++)
            {
                if (int.Parse(cbTongiao.Items[i].Value) == nvien.Matongiao)
                {
                    cbTongiao.SelectedIndex = i;
                    break;
                }
            }

            dem = cbBangcap.Items.Count;
            for (i = 1; i < dem; i++)
            {
                if (int.Parse(cbBangcap.Items[i].Value) == nvien.Mabangcap)
                {
                    cbBangcap.SelectedIndex = i;
                    break;
                }
            }

            if (nvien.Mangonngu != null)
            {
                dem = cbNgoaingu.Items.Count;
                for (i = 1; i < dem; i++)
                {
                    if (int.Parse(cbNgoaingu.Items[i].Value) == nvien.Mangonngu)
                    {
                        cbNgoaingu.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (nvien.Machuyenmon != null)
            {
                dem = cbChuyenmon.Items.Count;
                for (i = 1; i < dem; i++)
                {
                    if (int.Parse(cbChuyenmon.Items[i].Value) == nvien.Machuyenmon)
                    {
                        cbChuyenmon.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (nvien.Matinhoc != null)
            {
                dem = cbTinhoc.Items.Count;
                for (i = 1; i < dem; i++)
                {
                    if (int.Parse(cbTinhoc.Items[i].Value) == nvien.Matinhoc)
                    {
                        cbTinhoc.SelectedIndex = i;
                        break;
                    }
                }
            }

            cbChieucao.SelectedIndex = nvien.Chieucao - 140;
            cbCannang.SelectedIndex = nvien.Cannang - 35;

            cbTrangthainhanvien.SelectedIndex = nvien.Tinhtrang - 1;
        }

        #endregion

        #region Thongtincanhan EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();

                    //Begin lay MaNV
                    var maxnv = (from nv in db.PB_Nhanviens
                                 orderby nv.MaNV descending
                                 select new
                                 {
                                     nv.MaNV
                                 }).FirstOrDefault();
                    string manv = "NV";
                    manv += (maxnv != null) ? (int.Parse(maxnv.MaNV.Substring(2)) + 1).ToString("00000000") : "00000001";
                    //End lay MaNV

                    //Luu hinh nhan vien
                    string tenhinh = null;
                    if (uploadHinhnhanvien.FileName != "")
                    {
                        if (uploadHinhnhanvien.HasFile)
                        {
                            tenhinh = manv;
                            tenhinh += "." + uploadHinhnhanvien.FileName.Split('.').Skip(1).Take(1).FirstOrDefault().ToString();

                            string dirSave = Server.MapPath("~/QLNS/Employee-images/");
                            uploadHinhnhanvien.PostedFile.SaveAs(dirSave + "/" + tenhinh);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng chọn đúng file hình')", true);
                            return;
                        }
                    }


                    PB_Nhanvien nvienmoi = new PB_Nhanvien();
                    nvienmoi.MaNV = manv;
                    nvienmoi.HoNV = txtHoNV.Text.Trim();
                    nvienmoi.TenNV = txtTenNV.Text.Trim();
                    nvienmoi.Bidanh = txtTenthuonggoi.Text.Trim();
                    nvienmoi.Nu = chkGender.Checked;
                    nvienmoi.Ngaysinh = DateTime.Parse(txtNgaysinh.Value, new CultureInfo("vi-vn"));
                    nvienmoi.Noisinh = txtNoisinh.Text.Trim();
                    nvienmoi.Honnhan = chkKethon.Checked;
                    nvienmoi.Diachi = txtDiachi.Text.Trim();
                    nvienmoi.Tamtru = txtTamtru.Text.Trim();
                    nvienmoi.Dienthoaididong = txtDienthoaididong.Text.Trim();
                    nvienmoi.Dienthoainha = txtDienthoainha.Text.Trim();
                    nvienmoi.Email = txtEmail.Text.Trim();

                    nvienmoi.SoCMNN = txtSoCMNN.Text.Trim();
                    nvienmoi.Ngaycap = DateTime.Parse(txtNgaycap.Value, new CultureInfo("vi-vn"));
                    nvienmoi.Noicap = txtNoicap.Text.Trim();

                    nvienmoi.Suckhoe = txtTinhtrangsuckhoe.Text.Trim();
                    nvienmoi.Chieucao = byte.Parse(cbChieucao.SelectedValue);
                    nvienmoi.Cannang = byte.Parse(cbCannang.SelectedValue);

                    nvienmoi.Tinhtrang = byte.Parse(cbTrangthainhanvien.SelectedValue);

                    nvienmoi.Maquoctich = int.Parse(cbQuoctich.SelectedValue);
                    nvienmoi.Matongiao = int.Parse(cbTongiao.SelectedValue);
                    nvienmoi.Madantoc = int.Parse(cbDantoc.SelectedValue);
                    nvienmoi.Mabangcap = int.Parse(cbBangcap.SelectedValue);

                    if (cbNgoaingu.SelectedValue != "0")
                        nvienmoi.Mangonngu = int.Parse(cbNgoaingu.SelectedValue);
                    if (cbChuyenmon.SelectedValue != "0")
                        nvienmoi.Machuyenmon = int.Parse(cbChuyenmon.SelectedValue);
                    if (cbTinhoc.SelectedValue != "0")
                        nvienmoi.Matinhoc = int.Parse(cbTinhoc.SelectedValue);

                    nvienmoi.GhiChu = txtDescription.Text.Trim();
                    nvienmoi.Ngayvaolam = DateTime.Now;

                    if (!string.IsNullOrEmpty(tenhinh)) nvienmoi.Hinhanh = tenhinh;

                    nvienmoi.CreatedByUser = new Guid(Session["UserID"].ToString());
                    nvienmoi.CreatedByDate = DateTime.Now;

                    nvienmoi.BHXH = true;
                    nvienmoi.BHYT = true;
                    nvienmoi.BHTN = true;
                    nvienmoi.Phicongdoan = false;

                    db.PB_Nhanviens.InsertOnSubmit(nvienmoi);
                    db.SubmitChanges();

                    DiarySystem(6, 6, manv);

                    Response.Redirect("DanhsachNhanvien");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Lỗi " + ex.ToString(), true);
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();

                    PB_Nhanvien nvien = db.PB_Nhanviens.Where(p => p.MaNV == Request.QueryString["id"].ToString()).FirstOrDefault();


                    //Luu hinh nhan vien
                    if (uploadHinhnhanvien.FileName != "")
                    {
                        if (uploadHinhnhanvien.HasFile)
                        {
                            if (!string.IsNullOrEmpty(nvien.Hinhanh))
                            {
                                //Da co hinh roi => Chi update lai hinh khac
                                string dirSave = Server.MapPath("~/QLNS/Employee-images/");
                                uploadHinhnhanvien.PostedFile.SaveAs(dirSave + "/" + nvien.Hinhanh);
                            }
                            else
                            {
                                //Chua co hinh thuc hien upload hinh va luu vao CSDL
                                string tenhinh = nvien.MaNV;
                                tenhinh += "." + uploadHinhnhanvien.FileName.Split('.').Skip(1).Take(1).FirstOrDefault().ToString();

                                string dirSave = Server.MapPath("~/QLNS/Employee-images/");
                                uploadHinhnhanvien.PostedFile.SaveAs(dirSave + "/" + tenhinh);

                                nvien.Hinhanh = tenhinh;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng chọn đúng file hình')", true);
                            return;
                        }
                    }

                    nvien.HoNV = txtHoNV.Text.Trim();
                    nvien.TenNV = txtTenNV.Text.Trim();
                    nvien.Bidanh = txtTenthuonggoi.Text.Trim();
                    nvien.Nu = chkGender.Checked;
                    nvien.Ngaysinh = DateTime.Parse(txtNgaysinh.Value, new CultureInfo("vi-vn"));
                    nvien.Noisinh = txtNoisinh.Text.Trim();
                    nvien.Honnhan = chkKethon.Checked;
                    nvien.Diachi = txtDiachi.Text.Trim();
                    nvien.Tamtru = txtTamtru.Text.Trim();
                    nvien.Dienthoaididong = txtDienthoaididong.Text.Trim();
                    nvien.Dienthoainha = txtDienthoainha.Text.Trim();
                    nvien.Email = txtEmail.Text.Trim();

                    nvien.SoCMNN = txtSoCMNN.Text.Trim();
                    nvien.Ngaycap = DateTime.Parse(txtNgaycap.Value, new CultureInfo("vi-vn"));
                    nvien.Noicap = txtNoicap.Text.Trim();

                    nvien.Suckhoe = txtTinhtrangsuckhoe.Text.Trim();
                    nvien.Chieucao = byte.Parse(cbChieucao.SelectedValue);
                    nvien.Cannang = byte.Parse(cbCannang.SelectedValue);

                    nvien.Tinhtrang = byte.Parse(cbTrangthainhanvien.SelectedValue);

                    nvien.Maquoctich = int.Parse(cbQuoctich.SelectedValue);
                    nvien.Matongiao = int.Parse(cbTongiao.SelectedValue);
                    nvien.Madantoc = int.Parse(cbDantoc.SelectedValue);
                    nvien.Mabangcap = int.Parse(cbBangcap.SelectedValue);

                    if (cbNgoaingu.SelectedValue != "0")
                        nvien.Mangonngu = int.Parse(cbNgoaingu.SelectedValue);
                    if (cbChuyenmon.SelectedValue != "0")
                        nvien.Machuyenmon = int.Parse(cbChuyenmon.SelectedValue);
                    if (cbTinhoc.SelectedValue != "0")
                        nvien.Matinhoc = int.Parse(cbTinhoc.SelectedValue);

                    nvien.GhiChu = txtDescription.Text.Trim();
                    nvien.CreatedByUser = new Guid(Session["UserID"].ToString());
                    nvien.CreatedByDate = DateTime.Now;

                    DiarySystem(6, 7, nvien.MaNV);

                    db.SubmitChanges();
                    Response.Redirect("DanhsachNhanvien");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Lỗi " + ex.ToString(), true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            string idnvien = Request.QueryString["id"].ToString();
            PB_Nhanvien nvien = (from p in db.PB_Nhanviens
                                 where p.MaNV == idnvien
                                 select p).FirstOrDefault();
            try
            {
                //Kiem tra su ton tai cua nhan vien trong cac bang:
                //  ChamcongNhanvien, Chamcongtangca, KhautruNhanvien, Soluong, Luongtangca, Luonglamthem
                if ((from p in db.PB_ChamcongNhanviens
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                if ((from p in db.PB_Chamtangcas
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                if ((from p in db.PB_KhautruNhanviens
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                if ((from p in db.PB_SoLuongs
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                if ((from p in db.PB_Luongtangcas
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                if ((from p in db.PB_Luonglamthems
                     where p.MaNV == idnvien
                     select new { p.MaNV }).FirstOrDefault() != null)
                {
                    throw new Exception();
                }
                db.PB_Nhanviens.DeleteOnSubmit(nvien);

                DiarySystem(6, 8, nvien.MaNV);

                Response.Redirect("Danhsachnhanvien");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không thể xóa nhân viên này. Vì nhân viên này đã có dữ liệu.')", true);
            }


        }

        #endregion
    }
}

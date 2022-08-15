using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 4
    /// Chức năng: 7
    /// Hành động: Xem (5), Cập nhật (7)
    /// </summary>
    public partial class DetailNhanvien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin chi tiết";
                loadRole();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    loaddulieu(Request.QueryString["id"]);
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
                    Response.Redirect("Login");
                }
            }
            catch
            {
                Response.Redirect("Login");
            };
        }
        #endregion

        private void loaddulieu(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            View_PB_Nhanvien_Thongtincanhan nvien = db.View_PB_Nhanvien_Thongtincanhans.Where(p => p.MaNV == manv).FirstOrDefault();
            if (nvien != null)
            {
                loadthongtincanhan(nvien);
                loadCongviec(nvien.MaNV);
                loadPhucapnhanvien(nvien.MaNV);
                loadNguoithan(nvien.MaNV);

                DiarySystem(7, 5, nvien.MaNV);
            }
            else
            {
                Response.Redirect("DanhsachNhanvien");
            }
        }


        #region Thongtincanhan EventHandler

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditThongtincanhan@" + Request.QueryString["id"]);
        }
        private void loadthongtincanhan(View_PB_Nhanvien_Thongtincanhan nvien)
        {
            if (!string.IsNullOrEmpty(nvien.Hinhanh))
                imgNhanvien.ImageUrl = "~/QLNS/Employee-images/" + nvien.Hinhanh;
            lblMaNV.Text = nvien.MaNV;
            lblHoTen.Text = nvien.HoNV + " " + nvien.TenNV;
            lblTenthuonggoi.Text = (!string.IsNullOrEmpty(nvien.Bidanh)) ? nvien.Bidanh : "";
            lblHoTenH2.Text = nvien.HoNV + " " + nvien.TenNV;

            if (nvien.Nu == true)
            {
                imgGender.AlternateText = "Nữ";
                imgGender.ImageUrl = "~/images/icons/female32x32.png";
            }
            else
            {
                imgGender.AlternateText = "Nam";
                imgGender.ImageUrl = "~/images/icons/male32x32.png";
            }
            chkKethon.Checked = nvien.Honnhan;
            lblNgaysinh.Text = nvien.Ngaysinh.ToString("dd/MM/yyyy");
            lblNoisinh.Text = nvien.Noisinh;
            lblDiachi.Text = nvien.Diachi;
            lblTamtru.Text = nvien.Tamtru;
            lblDienthoaididong.Text = nvien.Dienthoaididong;
            lblDienthoainha.Text = nvien.Dienthoainha;
            lblEmail.Text = nvien.Email;

            lblSoCMNN.Text = nvien.SoCMNN;
            lblNgaycap.Text = nvien.Ngaycap.ToString("dd/MM/yyyy");
            lblNoicap.Text = nvien.Noicap;

            lblTinhtrangsuckhoe.Text = nvien.Suckhoe;

            txtDescription.Text = nvien.GhiChu;

            lblQuoctich.Text = nvien.Tenquoctich;
            lblDantoc.Text = nvien.Tendantoc;
            lblTongiao.Text = nvien.TenTG;
            lblHocvan.Text = nvien.Tenbang;

            lblNgoaingu.Text = (!string.IsNullOrEmpty(nvien.Tenngonngu) ? nvien.Tenngonngu : "");
            lblTinhoc.Text = (!string.IsNullOrEmpty(nvien.TenTH) ? nvien.TenTH : "");
            lblChuyenmon.Text = (!string.IsNullOrEmpty(nvien.Tenchuyenmon) ? nvien.Tenchuyenmon : "");

            lblChieucao.Text = nvien.Chieucao.ToString();
            lblCannang.Text = nvien.Cannang.ToString();

            cbTrangthainhanvien.SelectedIndex = nvien.Tinhtrang - 1;
        }
        #endregion

        #region Congviec
        //Begin EventHandler
        protected void btnThaydoiphongban_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPhongbannhanvien@" + Request.QueryString["id"]);
        }
        protected void btnThaydoichucvu_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditChucvunhanvien@" + Request.QueryString["id"]);
        }
        protected void btnThaydoibacngach_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditNgachbacnhanvien@" + Request.QueryString["id"]);
        }
        protected void btnThaydoicongviec_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCongviecnhanvien@" + Request.QueryString["id"]);
        }
        protected void btnUpdateCongviec_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Nhanvien nvien = (from p in db.PB_Nhanviens
                                     where p.MaNV == Request.QueryString["id"]
                                     select p).FirstOrDefault();
                nvien.BHXH = chkBHXH.Checked;
                nvien.BHYT = chkBHYT.Checked;
                nvien.BHTN = chkBHTN.Checked;
                nvien.Phicongdoan = chkPhicongdoan.Checked;
                if (cbGroup.SelectedValue != "0")
                {
                    nvien.MaToNhom = int.Parse(cbGroup.SelectedValue);
                }
                else
                {
                    nvien.MaToNhom = null;
                }
                db.SubmitChanges();

                DiarySystem(7, 7, nvien.MaNV);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cập nhật thành công'); window.location = 'DanhsachNhanvien", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'DanhsachNhanvien", true);
            };
            
        }
        //End EventHandler

        //Begin Methods
        private void loadCongviec(string manv)
        {
            hplQuatrinhlamviec.NavigateUrl = "Quatrinhlamviec@" + manv;
            dbLinQDataContext db = new dbLinQDataContext();
            var thongtincongviec = (from nvien in db.View_PB_Nhanvien_Thongtincongviecs
                                    where nvien.MaNV == manv
                                    select nvien).FirstOrDefault();

            lblPhong.Text = (thongtincongviec.Tenphong != null) ? thongtincongviec.Tenphong : "<b>Chưa được cài đặt</b>";
            lblChucvu.Text = (thongtincongviec.Tenchucvu != null) ? thongtincongviec.Tenchucvu : "<b>Chưa được cài đặt</b>";
            lblCongviec.Text = (thongtincongviec.Tencongviec != null) ? thongtincongviec.Tencongviec : "<b>Chưa được cài đặt</b>";
            lblNgach.Text = (thongtincongviec.TenNgach != null) ? thongtincongviec.TenNgach : "<b>Chưa cài đặt</b>";
            lblBacluong.Text = (thongtincongviec.Tenbac != null) ? thongtincongviec.Tenbac : "<b>Chưa cài đặt</b>";
            lblHesoluong.Text = (thongtincongviec.Hesoluong != null) ? thongtincongviec.Hesoluong.ToString() : "<b>Chưa cài đặt</b>";
            lblNgayapdung.Text = (thongtincongviec.Ngayapdung != null) ? thongtincongviec.Ngayapdung.Value.Month.ToString("#,##0") + "/" + thongtincongviec.Ngayapdung.Value.Year.ToString() : "<b>Chưa cài đặt</b>";
            chkBHXH.Checked = thongtincongviec.BHXH;
            chkBHYT.Checked = thongtincongviec.BHYT;
            chkBHTN.Checked = thongtincongviec.BHTN;
            chkPhicongdoan.Checked = thongtincongviec.Phicongdoan;
            var objluongtoithieu = (from p in db.View_PB_Luongtoithieu_HTs
                                 orderby p.Ngayapdung descending
                                 select new 
                                 {
                                     p.Sotien 
                                 }).FirstOrDefault();
            int luongtoithieu = (objluongtoithieu != null) ? objluongtoithieu.Sotien : 0;

            if (luongtoithieu != 0)
            {
                if (thongtincongviec.Hesoluong != null)
                {
                    double luongcoban = luongtoithieu * thongtincongviec.Hesoluong.Value;
                    lblLuongcoban.Text = luongcoban.ToString("#,##0");
                }
                lblLuongtoithieu.Text = luongtoithieu.ToString("#,##0");
            }
            else
            {
                lblLuongcoban.Text = "Chưa được cài đặt";
                lblLuongtoithieu.Text = "Chưa được cài đặt";
            }

            //Load ToNhom
            if (thongtincongviec.Maphong != null)
            {
                //Lay tat ca ca to trong phong
                var tonhom = (from p in db.PB_ToNhoms
                              where p.Maphong == thongtincongviec.Maphong
                              select new
                              {
                                  p.MaToNhom,
                                  p.TenToNhom
                              }).ToList();
                cbGroup.Items.Clear();

                //Neu co To thuoc Phong
                if (tonhom.Count != 0)
                {
                    //Nhan vien hien dang nam trong 1 to
                    if (thongtincongviec.MaToNhom != null)
                    {
                        cbGroup.Items.Add(new ListItem("-- Ra khỏi tổ --", "0"));
                        int i = 1;
                        foreach (var p in tonhom)
                        {
                            cbGroup.Items.Add(new ListItem(p.TenToNhom, p.MaToNhom.ToString()));
                            if (p.MaToNhom == thongtincongviec.MaToNhom)
                            {
                                cbGroup.SelectedIndex = i;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        cbGroup.Items.Add(new ListItem("-- Chọn tổ --", "0"));
                        foreach (var p in tonhom)
                        {
                            cbGroup.Items.Add(new ListItem(p.TenToNhom, p.MaToNhom.ToString()));
                        }
                    }
                }
                else
                {
                    cbGroup.Items.Add(new ListItem("-- Không có tổ thuộc phòng này --", "0"));
                }

            }
            else
            {
                cbGroup.Items.Clear();
                cbGroup.Items.Add(new ListItem("-- Nhân viên chưa có phòng --", "0"));
            }
        }
        //End Methods
        #endregion

        #region Phucapnhanvien
        //Begin EventHandler
        protected void btnPhucapAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPhucapnhanvien@add@" + Request.QueryString["id"]);
        }
        protected void rpDataPhucap_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int mapc = int.Parse(e.Item.DataItem.ToString().Replace("{", "").Split(',').First().Substring(6));
                HyperLink hplEditPhucap = (HyperLink)e.Item.FindControl("hplEditPhucap");
                hplEditPhucap.NavigateUrl = "EditPhucapnhanvien@edit@" + Request.QueryString["id"] + "@" + mapc.ToString();
            }
        }
        //End EventHandler

        //Begin Methods
        private void loadPhucapnhanvien(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstPhucapnvien = (from pcapnhanvien in db.PB_PhucapNhanviens
                           join pcap in db.DIC_Phucaps
                           on pcapnhanvien.Maphucap equals pcap.Maphucap
                           where pcapnhanvien.MaNV == manv
                           select
                           new
                           {
                               pcapnhanvien.Maphucap,
                               pcap.Tenphucap,
                               pcapnhanvien.Sotien
                           }).ToList();

            int stt = 1;
            var lstData = (from p in lstPhucapnvien
                           select
                           new
                           {
                               STT = stt++,
                               p.Maphucap,
                               p.Tenphucap,
                               p.Sotien
                           }).ToList();
            rpDataPhucap.DataSource = lstData;
            rpDataPhucap.DataBind();
        }
        //End Methods
        #endregion

        #region Nguoithan
        //Begin EventHandler
        protected void btnNguoithanAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditNguoithan@add@" + Request.QueryString["id"]);
        }
        protected void rpDataNguoithan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string mant = e.Item.DataItem.ToString().Replace("{", "").Split(',').Skip(1).First().Substring(15);
                HyperLink hplEditNguoithan = (HyperLink)e.Item.FindControl("hplEditNguoithan");
                hplEditNguoithan.NavigateUrl = "EditNguoithan@edit@" + mant;
            }
        }
        //End EventHandler

        //Begin Methods
        private void loadNguoithan(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstNguoithan = (from nguoithannhanvien in db.PB_Nguoithannhanviens
                                join quanhe in db.DIC_Quanhegiadinhs
                                on nguoithannhanvien.Quanhe equals quanhe.Maquanhe
                                where nguoithannhanvien.MaNV == manv
                                select
                                new
                                {
                                    nguoithannhanvien.Manguoithan,
                                    nguoithannhanvien.Hotennguoithan,
                                    nguoithannhanvien.Diachi,
                                    nguoithannhanvien.Dienthoai,
                                    nguoithannhanvien.Email,
                                    nguoithannhanvien.Phuthuoc,
                                    quanhe.Tenquanhe
                                }).ToList();

            int stt = 1;
            var lstData = (from p in lstNguoithan
                           select
                           new
                           {
                               STT = stt++,
                               p.Manguoithan,
                               p.Hotennguoithan,
                               p.Diachi,
                               p.Dienthoai,
                               p.Email,
                               p.Phuthuoc,
                               p.Tenquanhe
                           }).ToList();
            rpDataNguoithan.DataSource = lstData;
            rpDataNguoithan.DataBind();
        }
        //End Methods
        #endregion

    }
}

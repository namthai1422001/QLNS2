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
    /// Chức năng: 12
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditPhucapnhanvien : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadRole();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["mapc"]))
                    {
                        Page.Title = "Cập nhật phụ cấp nhân viên";
                        setVisibleButton(false, true, true);
                        loadEditData(Request.QueryString["id"], int.Parse(Request.QueryString["mapc"]));
                    }
                    else
                    {
                        Page.Title = "Thêm phụ cấp nhân viên";
                        setVisibleButton(true, false, false);
                        loadAddData(Request.QueryString["id"]);
                        panelAudit.Visible = false;
                    }
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
        private void setVisibleButton(bool add, bool update, bool delete)
        {
            btnAdd.Visible = add;
            btnUpdate.Visible = update;
            btnDelete.Visible = delete;
        }
        #endregion

        #region Methods
        //Load du lieu Edit
        private void loadEditData(string manv, int maphucap)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            string hoten = (from nvien in db.PB_Nhanviens
                            where nvien.MaNV == manv
                            select
                                new
                                {
                                    Hoten = nvien.HoNV + " " + nvien.TenNV
                                }
                ).FirstOrDefault().Hoten;
            ltrh3.Text = "<h3>Sửa phụ cấp của nhân viên " + hoten + "</h3>";

            var objData = (from p in db.PB_PhucapNhanviens
                           join pc in db.DIC_Phucaps
                           on p.Maphucap equals pc.Maphucap
                           where p.MaNV == manv && p.Maphucap == maphucap
                           select
                             new
                             {
                                 p.Maphucap,
                                 pc.Tenphucap,
                                 pc.Tienlonnhat,
                                 p.Sotien,
                                 p.CreatedByUser,
                                 p.CreatedByDate
                             }).FirstOrDefault();
            cbPhucap.Items.Clear();
            cbPhucap.Items.Add(new ListItem(objData.Tenphucap, objData.Maphucap.ToString()));

            if (objData != null)
            {

                lblSotenMax.Text = objData.Tienlonnhat.ToString("#,##0");
                txtValue.Text = objData.Sotien.ToString("#,##0");
                lblCreatedByUser.Text = db.SYS_Nguoidungs.Where(p => p.ID == objData.CreatedByUser).FirstOrDefault().Fullname;

                GetTime gettime = new GetTime();
                lblCreatedByDate.Text = gettime.GetDatetime(objData.CreatedByDate);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'DanhsachNhanvien';", true);
            }
        }

        //Load du lieu Add
        private void loadAddData(string manv)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            string hoten = (from nvien in db.PB_Nhanviens
                            where nvien.MaNV == manv
                            select
                                new
                                {
                                    Hoten = nvien.HoNV + " " + nvien.TenNV
                                }
                            ).FirstOrDefault().Hoten;
            ltrh3.Text = "<h3>Thêm phụ cấp cho nhân viên " + hoten + "</h3>";

            //Lay danh sach cac loai phu cap
            List<int> lstPhucap = (from pc in db.DIC_Phucaps
                                   select new { pc.Maphucap }).Select(p => p.Maphucap).ToList();
            if (lstPhucap.Count == 0)
            {
                //Khong co phu cap nao trong bang tham so
                cbPhucap.Items.Clear();
                cbPhucap.Items.Add(new ListItem("-- Chưa có các khoản phụ cấp --", "0"));
                //Phai tao them cac khoan phu cap de co the su dung chuc nang nay
                return;
            }

            //Lay danh sach cac phu cap cua nhan vien
            List<int> lstPhucapnvien = (from pcnvien in db.PB_PhucapNhanviens
                                        where pcnvien.MaNV == manv
                                        select
                                             new
                                             {
                                                 pcnvien.Maphucap
                                             }).Select(p => p.Maphucap).ToList();

            //Lay danh sach cac phu cap ma nhan vien chua duoc lanh
            List<int> lstPhucapnvienchuaduoclanh = (from pcnvien in lstPhucap
                                                    where !lstPhucapnvien.Contains(pcnvien)
                                                    select pcnvien).ToList();

            if (lstPhucapnvienchuaduoclanh.Count == 0)
            {
                //Nhan vien nay da huong day du phu cap => Khong the them phu cap cho nhan vien
                cbPhucap.Items.Clear();
                cbPhucap.Items.Add(new ListItem("-- Đã hưởng đầy đủ phụ cấp --", "0"));
                return;
            }


            //Lay danh sach cac phu cap ma nhan vien chua co de gan du lieu cho cbPhucap
            var lstDataPhucap = (from pc in db.DIC_Phucaps
                                 where lstPhucapnvienchuaduoclanh.Contains(pc.Maphucap)
                                 select
                                 new
                                 {
                                     pc.Maphucap,
                                     pc.Tenphucap
                                 }).ToList();
            cbPhucap.Items.Clear();
            cbPhucap.Items.Add(new ListItem("-- Vui lòng chọn --", "0"));
            foreach(var obj in lstDataPhucap)
            {
                cbPhucap.Items.Add(new ListItem(obj.Tenphucap,obj.Maphucap.ToString()));
            }
        }

        //Kiem tra du lieu nhap vao
        private bool Checkinput()
        {
            if (int.Parse(txtValue.Text.Replace(",", "").Replace(".", "")) > int.Parse(lblSotenMax.Text.Replace(",", "")))
            {
                ltrInfor.Text = @"<div class='notification error png_bg'>
                                    <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Đóng thông báo lỗi' alt='close' /></a>
                                    <div>
                                        <b>Số tiền được nhận</b> không được vượt quá <b>số tiền tối đa</b>
                                    </div>
                                </div>";
                return false;
            }
            ltrInfor.Text = "";
            return true;
        }
        #endregion

        #region EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (Checkinput())
                {
                    try
                    {
                        dbLinQDataContext db = new dbLinQDataContext();
                        PB_PhucapNhanvien objData = new PB_PhucapNhanvien();

                        objData.MaNV = Request.QueryString["id"];
                        objData.Maphucap = int.Parse(cbPhucap.SelectedValue);
                        objData.Sotien = int.Parse(txtValue.Text.Replace(",", "").Replace(".", ""));

                        objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                        objData.CreatedByDate = DateTime.Now;

                        db.PB_PhucapNhanviens.InsertOnSubmit(objData);
                        db.SubmitChanges();

                        DiarySystem(12, 6, objData.MaNV + "|" + objData.Maphucap + "|" + objData.Sotien.ToString());

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thêm phụ cấp cho nhân viên thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                    };
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (Checkinput())
                {
                    try
                    {
                        dbLinQDataContext db = new dbLinQDataContext();
                        PB_PhucapNhanvien objData = (from p in db.PB_PhucapNhanviens
                                                     where p.MaNV == Request.QueryString["id"] && p.Maphucap == int.Parse(Request.QueryString["mapc"])
                                                     select p).FirstOrDefault();

                        objData.MaNV = Request.QueryString["id"];
                        objData.Maphucap = int.Parse(cbPhucap.SelectedValue);
                        objData.Sotien = int.Parse(txtValue.Text.Replace(",", "").Replace(".", ""));
                        objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                        objData.CreatedByDate = DateTime.Now;

                        db.SubmitChanges();

                        DiarySystem(12, 7, objData.MaNV + "|" + objData.Maphucap + "|" + objData.Sotien.ToString());

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cập nhật phụ cấp cho nhân viên thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                    };
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_PhucapNhanvien objData = (from p in db.PB_PhucapNhanviens
                                             where p.MaNV == Request.QueryString["id"] && p.Maphucap == int.Parse(Request.QueryString["mapc"])
                                             select p).FirstOrDefault();
                db.PB_PhucapNhanviens.DeleteOnSubmit(objData);
                db.SubmitChanges();

                DiarySystem(12, 8, objData.MaNV + "|" + objData.Maphucap + "|" + objData.Sotien.ToString());

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Xóa phụ cấp thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
            };
        }
        protected void cbPhucap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhucap.SelectedValue != "0")
            {
                dbLinQDataContext db = new dbLinQDataContext();
                lblSotenMax.Text = db.DIC_Phucaps.Where(p => p.Maphucap == int.Parse(cbPhucap.SelectedValue)).FirstOrDefault().Tienlonnhat.ToString("#,##0");
                txtValue.Text = lblSotenMax.Text;
            }
            else
            {
                lblSotenMax.Text = "0";
                txtValue.Text = "0";
                regPhucap.IsValid = false;
            }
        }
        #endregion
    }
}

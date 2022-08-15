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
    /// Chức năng: 13
    /// Hành động: Cập nhật (7)
    /// </summary>
    public partial class EditNguoithan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadRole();
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Thêm người thân nhân viên";
                    setVisibleButton(true, false, false);
                    loadAddData(Request.QueryString["id"]);
                    panelAudit.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["mant"]))
                    {
                        Page.Title = "Cập nhật thông tin người thân nhân viên";
                        setVisibleButton(false, true, true);
                        loadEditData(Request.QueryString["mant"]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'DanhsachNhanvien';", true);
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

        #region EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Nguoithannhanvien objData = new PB_Nguoithannhanvien();

                    objData.Manguoithan = Guid.NewGuid();
                    objData.MaNV = Request.QueryString["id"];

                    objData.Hotennguoithan = txtHoTenNT.Text;
                    objData.Diachi = txtDiachi.Text;
                    objData.Dienthoai = txtDienthoai.Text;
                    objData.Email = txtEmail.Text;
                    objData.Quanhe = int.Parse(cbQuanhe.SelectedValue);
                    objData.Phuthuoc = chkIsPhuthuoc.Checked;

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.PB_Nguoithannhanviens.InsertOnSubmit(objData);
                    db.SubmitChanges();

                    DiarySystem(13, 6, objData.MaNV + "|" + objData.Manguoithan.ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thêm người thân nhân viên thành công'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'ChitietNhanvien@" + Request.QueryString["id"] + "';", true);
                };
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_Nguoithannhanvien objData = db.PB_Nguoithannhanviens.Where(p => p.Manguoithan == new Guid(Request.QueryString["mant"])).FirstOrDefault();

                    objData.Hotennguoithan = txtHoTenNT.Text;
                    objData.Diachi = txtDiachi.Text;
                    objData.Dienthoai = txtDienthoai.Text;
                    objData.Email = txtEmail.Text;
                    objData.Quanhe = int.Parse(cbQuanhe.SelectedValue);
                    objData.Phuthuoc = chkIsPhuthuoc.Checked;

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.SubmitChanges();

                    DiarySystem(13, 7, objData.MaNV + "|" + objData.Manguoithan.ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cập nhật thông tin người thân nhân viên thành công'); window.location = 'ChitietNhanvien@" + objData.MaNV + "';", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'DanhsachNhanvien';", true);
                };
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_Nguoithannhanvien objData = (from p in db.PB_Nguoithannhanviens
                                                where p.Manguoithan == new Guid(Request.QueryString["mant"])
                                                select p).FirstOrDefault();
                db.PB_Nguoithannhanviens.DeleteOnSubmit(objData);
                db.SubmitChanges();

                DiarySystem(13, 8, objData.MaNV + "|" + objData.Manguoithan.ToString());

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Xóa người thân nhân viên thành công'); window.location = 'ChitietNhanvien@" + objData.MaNV + "';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'DanhsachNhanvien';", true);
            };
        }
        #endregion

        #region Methods
        //Load du lieu Edit
        private void loadEditData(string manguoithan)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            //Thong tin nguoi than
            var objData = (from nvien in db.PB_Nhanviens
                           join nguoithannvien in db.PB_Nguoithannhanviens
                               on nvien.MaNV equals nguoithannvien.MaNV
                           join quanhe in db.DIC_Quanhegiadinhs
                           on nguoithannvien.Quanhe equals quanhe.Maquanhe
                           where nguoithannvien.Manguoithan == new Guid(manguoithan)
                           select
                           new
                           {
                               nguoithannvien.MaNV,
                               HoTenNV = nvien.HoNV + nvien.TenNV,
                               nguoithannvien.Hotennguoithan,
                               Maquanhe = nguoithannvien.Quanhe,
                               quanhe.Tenquanhe,
                               nguoithannvien.Diachi,
                               nguoithannvien.Dienthoai,
                               nguoithannvien.Email,
                               nguoithannvien.Phuthuoc,
                               nguoithannvien.CreatedByUser,
                               nguoithannvien.CreatedByDate
                           }).FirstOrDefault();
            ltrh3.Text = "<h3>Sửa thông tin người thân nhân viên " + objData.HoTenNV + "</h3>";

            //Load cbQuanhe
            var lstQuanhe = (from p in db.DIC_Quanhegiadinhs
                             where p.IsActive == true
                             select
                             new
                             {
                                 p.Maquanhe,
                                 p.Tenquanhe
                             }
                            ).ToList();
            cbQuanhe.Items.Clear();
            cbQuanhe.Items.Add(new ListItem("-- Vui lòng chọn--", "0"));
            if (lstQuanhe.Count > 0)
            {
                int i = 1;
                foreach (var p in lstQuanhe)
                {
                    cbQuanhe.Items.Add(new ListItem(p.Tenquanhe, p.Maquanhe.ToString()));
                    if (p.Maquanhe == objData.Maquanhe)
                    {
                        cbQuanhe.SelectedIndex = i;
                    }
                    i++;
                }
            }

            if (objData != null)
            {

                lblMaNV.Text = objData.MaNV;
                lblHoTenNV.Text = objData.HoTenNV;
                txtDiachi.Text = objData.Diachi;
                txtDienthoai.Text = objData.Dienthoai;
                txtEmail.Text = objData.Email;
                txtHoTenNT.Text = objData.Hotennguoithan;
                chkIsPhuthuoc.Checked = objData.Phuthuoc;
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

            //Thong tin nhanvien
            var objData = (from nvien in db.PB_Nhanviens
                           where nvien.MaNV == manv
                           select
                           new
                           {
                               nvien.MaNV,
                               HoTenNV = nvien.HoNV + nvien.TenNV,
                           }).FirstOrDefault();
            ltrh3.Text = "<h3>Thêm người thân nhân viên " + objData.HoTenNV + "</h3>";

            //Load cbQuanhe
            var lstQuanhe = (from p in db.DIC_Quanhegiadinhs
                             where p.IsActive == true
                             select
                             new
                             {
                                 p.Maquanhe,
                                 p.Tenquanhe
                             }
                            ).ToList();
            cbQuanhe.Items.Clear();
            cbQuanhe.Items.Add(new ListItem("-- Vui lòng chọn--", "0"));
            if (lstQuanhe.Count > 0)
            {
                foreach (var p in lstQuanhe)
                {
                    cbQuanhe.Items.Add(new ListItem(p.Tenquanhe, p.Maquanhe.ToString()));
                }
            }

            if (objData != null)
            {

                lblMaNV.Text = objData.MaNV;
                lblHoTenNV.Text = objData.HoTenNV;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'DanhsachNhanvien';", true);
            }
        }
        #endregion
    }
}

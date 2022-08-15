using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 9
    /// Chức năng: 29
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditTonhom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = -1;
                int PhongID = -1;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = int.Parse(Request.QueryString["id"]);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["phongid"]))
                {
                    PhongID = int.Parse(Request.QueryString["phongid"]);
                }
                if (id > 0)
                {
                    loadRole();
                    loadDataEdit(id);
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    panelAudit.Visible = true;
                }
                else
                {
                    if (PhongID > 0)
                    {
                        loadRole();
                        loadDataAdd(PhongID);
                        btnAdd.Visible = true;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                        panelAudit.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'Tonhom';", true);
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
            if (UserRolls.Where(p => p == 9).Count() == 0)
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
        //Load Add
        private void loadDataAdd(int PhongID)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            lblPhong.Text = db.PB_Phongbans.Where(p => p.Maphong == PhongID).FirstOrDefault().Tenphong;
        }

        //Load Edit
        private void loadDataEdit(int _id)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_ToNhom lst = db.PB_ToNhoms.Where(p => p.MaToNhom == _id).FirstOrDefault();

            lblPhong.Text = db.PB_Phongbans.Where(p => p.Maphong == lst.Maphong).FirstOrDefault().Tenphong;
            txtName.Text = lst.TenToNhom;
            txtDescription.Text = lst.GhiChu;

            lblCreatedByUser.Text = db.SYS_Nguoidungs.Where(p => p.ID == lst.CreatedByUser).FirstOrDefault().Fullname;

            GetTime gettime = new GetTime();
            lblCreatedByDate.Text = gettime.GetDatetime(lst.CreatedByDate);

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
                    PB_ToNhom _data = new PB_ToNhom();
                    int Maphong = int.Parse(Request.QueryString["phongid"]);
                    _data.Maphong = Maphong;
                    _data.TenToNhom = txtName.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.PB_ToNhoms.InsertOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(29, 6, "ID Phòng: " + Maphong.ToString() + "|Tên tổ: " + _data.TenToNhom);

                    Response.Redirect("Tonhom");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Tonhom';", true);
                };
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    dbLinQDataContext db = new dbLinQDataContext();
                    PB_ToNhom _data = (from p in db.PB_ToNhoms
                                          where p.MaToNhom == id
                                          select p).FirstOrDefault();

                    _data.TenToNhom = txtName.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.SubmitChanges();

                    DiarySystem(29, 7, "ID Phòng: " + _data.Maphong.ToString() + "ID tổ: " + _data.MaToNhom.ToString() + "|Tên tổ: " + _data.TenToNhom);

                    Response.Redirect("Tonhom");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Tonhom';", true);
                };
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    dbLinQDataContext db = new dbLinQDataContext();
                    if (db.PB_Nhanviens.Where(p => p.MaToNhom == id).Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tổ này đang có nhân viên. Vui lòng chuyển nhân viên sang tổ khác trước'); window.location = 'Tonhom';", true);
                    }
                    else
                    {
                        PB_ToNhom _data = (from p in db.PB_ToNhoms
                                           where p.MaToNhom == id
                                           select p).FirstOrDefault();
                        db.PB_ToNhoms.DeleteOnSubmit(_data);
                        db.SubmitChanges();

                        DiarySystem(29, 8, "ID Phòng: " + _data.Maphong.ToString() + "ID tổ: " + _data.MaToNhom.ToString() + "|Tên tổ: " + _data.TenToNhom);

                        Response.Redirect("Tonhom");
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Tonhom';", true);
                };
            }
        }
        #endregion
    }
}

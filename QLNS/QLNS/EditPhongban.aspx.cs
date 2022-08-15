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
    /// Chức năng: 28
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditPhongban : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = -1;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = int.Parse(Request.QueryString["id"]);
                }
                if (id > 0)
                {
                    loadRole();
                    loadData(id);
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    panelAudit.Visible = true;
                }
                else
                {
                    loadRole();
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    panelAudit.Visible = false;
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
        private void loadData(int _id)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Phongban lst = db.PB_Phongbans.Where(p => p.Maphong == _id).FirstOrDefault();

            txtName.Text = lst.Tenphong;
            txtDienthoai.Text = string.Format("{0:0,0}", lst.Dienthoai);
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
                    PB_Phongban _data = new PB_Phongban();
                    _data.Tenphong = txtName.Text.Trim();
                    _data.Dienthoai = txtDienthoai.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.PB_Phongbans.InsertOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(28, 6, _data.Tenphong);

                    Response.Redirect("Phongban");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Phongban';", true);
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
                    PB_Phongban _data = db.PB_Phongbans.Where(p => p.Maphong == id).FirstOrDefault();
                    _data.Tenphong = txtName.Text.Trim();
                    _data.Dienthoai = txtDienthoai.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.SubmitChanges();

                    DiarySystem(28, 7, _data.Maphong + "|" + _data.Tenphong);

                    Response.Redirect("Phongban");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Phongban';", true);
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
                    if (db.PB_Thaydoiphongbans.Where(p => p.Maphong == id).Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Phòng này đã được sử dụng. Không được phép xóa'); window.location = 'Phongban';", true);
                    }
                    else
                        if (db.PB_ToNhoms.Where(p => p.Maphong == id).Count() > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Hiện đang có tổ thuộc phòng này. Vui lòng xóa tổ thuộc phòng này trước'); window.location = 'Phongban';", true);
                        }
                        else
                        {
                            PB_Phongban _data = db.PB_Phongbans.Where(p => p.Maphong == id).FirstOrDefault();
                            db.PB_Phongbans.DeleteOnSubmit(_data);
                            db.SubmitChanges();

                            DiarySystem(28, 8, _data.Maphong + "|" + _data.Tenphong);

                            Response.Redirect("Phongban");
                        }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Phongban';", true);
                };
            }
        }
        #endregion
    }
}

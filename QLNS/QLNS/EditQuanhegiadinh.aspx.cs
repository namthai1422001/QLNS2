using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 10
    /// Chức năng: 52
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditQuanhegiadinh : System.Web.UI.Page
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
            if (UserRolls.Where(p => p == 10).Count() == 0)
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
            DIC_Quanhegiadinh lst = db.DIC_Quanhegiadinhs.Where(p => p.Maquanhe == _id).FirstOrDefault();

            txtName.Text = lst.Tenquanhe;
            txtDescription.Text = lst.GhiChu;
            chkActive.Checked = (lst.IsActive == true ? true : false);

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
                    DIC_Quanhegiadinh _data = new DIC_Quanhegiadinh();
                    _data.Tenquanhe = txtName.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    _data.IsActive = chkActive.Checked;
                    db.DIC_Quanhegiadinhs.InsertOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(52, 6, txtName.Text.Trim());

                    Response.Redirect("Quanhegiadinh");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Quanhegiadinh';", true);
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
                    DIC_Quanhegiadinh _data = db.DIC_Quanhegiadinhs.Where(p => p.Maquanhe == id).FirstOrDefault();
                    _data.Tenquanhe = txtName.Text.Trim();
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.IsActive = chkActive.Checked;
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.SubmitChanges();

                    DiarySystem(52, 7, _data.Maquanhe + "|" + txtName.Text.Trim());

                    Response.Redirect("Quanhegiadinh");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Quanhegiadinh';", true);
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
                    DIC_Quanhegiadinh _data = db.DIC_Quanhegiadinhs.Where(p => p.Maquanhe == id).FirstOrDefault();
                    db.DIC_Quanhegiadinhs.DeleteOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(52, 8, _data.Maquanhe + "|" + txtName.Text.Trim());

                    Response.Redirect("Quanhegiadinh");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Quanhegiadinh';", true);
                };
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS
{
    /// <summary>
    /// Quyền: 13
    /// Chức năng: 56
    /// Hành động: Xem (5), Tao (6), Cap nhat (7)
    /// </summary>
    public partial class EditIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            if (UserRolls.Where(p => p == 13).Count() == 0)
            {
                Response.Redirect("DontAllow");
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

        #region Methods

        private void loadData()
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                if (db.SYS_IndexPages.Count() > 0)
                {
                    //Da co du lieu => Cap nhat
                    SYS_IndexPage objData = db.SYS_IndexPages.FirstOrDefault();
                    txtContent.Text = objData.ContentOfIndex;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Index'", true);
            }
        }

        #endregion

        #region EventHandler
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                if (db.SYS_IndexPages.Count() > 0)
                {
                    //Da co du lieu => Cap nhat
                    SYS_IndexPage objData = db.SYS_IndexPages.FirstOrDefault();
                    objData.ContentOfIndex = txtContent.Text.Trim();

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.SubmitChanges();

                    DiarySystem(56, 6, "");

                    Response.Redirect("Index");
                }
                else
                {
                    //Chua co du lieu => Them
                    SYS_IndexPage objData = new SYS_IndexPage();

                    objData.ID = 1;
                    objData.ContentOfIndex = txtContent.Text.Trim();

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.SYS_IndexPages.InsertOnSubmit(objData);
                    db.SubmitChanges();

                    DiarySystem(56, 7, "");

                    Response.Redirect("Index");
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Index'", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index");
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Profile
{
    /// <summary>
    /// Quyền: không cần
    /// Chức năng: 1
    /// Hành động: Thay đổi mật khẩu (3)
    /// </summary>
    public partial class ChangePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Methods
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

        #region EventHandler
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.ID == new Guid(Session["UserID"].ToString())).FirstOrDefault();

                    // sp_SYS_ChangePassResult resultChangePass = db.sp_SYS_ChangePass("nguyenphuongbac","admin", txtNewPassword.Text.Trim()).FirstOrDefault();

                    sp_SYS_ChangePassResult resultChangePass = db.sp_SYS_ChangePass(objData.Username, txtOldPassword.Text.Trim(), txtNewPassword.Text.Trim()).FirstOrDefault();

                    if (resultChangePass.err == 0)
                    {
                        DiarySystem(1, 3, "");
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thay đổi mật khẩu thành công! Quay lại trang chủ'); window.location = '../Index';", true);
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mật khẩu cũ không hợp lệ');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối! Vui lòng thử lại sau'); window.location = '../Index';", true);
                }
            }
        }
        #endregion
    }
}

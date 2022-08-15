using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS
{
    public partial class RenewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region EventHandler
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();

                    SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == txtReNewUsername.Text.Trim()
                                                                    && p.CodeResetPassForget.ToString() == txtReNewVerify.Text.Trim()).FirstOrDefault();

                    if (objData != null)
                    {
                        //Tao ra code moi
                        objData.CodeResetPassForget = Guid.NewGuid();
                        db.SubmitChanges();
                        
                        //Thuc hien doi mat khau moi cho tai khoan
                        db.sp_SYS_ReNewPass(txtReNewUsername.Text.Trim(), txtReNewPassword.Text.Trim());

                        //Thuc hien dang nhap cho Tai khoan
                        sp_SYS_CheckLoginResult resultCheck = db.sp_SYS_CheckLogin(txtReNewUsername.Text.Trim(), txtReNewPassword.Text.Trim()).First();
                        Session["UserID"] = resultCheck.ID;
                        List<int> UserRolls = (from p in db.SYS_PhanQuyens
                                               where p.UserID == new Guid(Session["UserID"].ToString())
                                               select
                                               p.RollID).ToList();

                        Session["UserRolls"] = UserRolls;

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thay đổi mật khẩu thành công! Đã đăng nhập'); window.location = 'Index';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tên đăng nhập hoặc mã xác nhận không hợp lệ! Vui lòng kiểm tra lại email');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Không thể kết nối đến máy chủ! Vui lòng thử lại sau'); window.location = 'Login';", true);
                };
            }
        }
        #endregion
    }
}

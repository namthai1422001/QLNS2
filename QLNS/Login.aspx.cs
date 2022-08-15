using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["btnLogin"]))
                {
                    CheckLogin((Request["txtUsername"]).Trim(), (Request["txtPassword"]).Trim());
                }
            }
        }

        private void CheckLogin(string strUsername, string strPassword)
        {
            //try
            //{
                dbLinQDataContext db = new dbLinQDataContext();
                sp_SYS_CheckLoginResult resultCheck = db.sp_SYS_CheckLogin(strUsername, strPassword).FirstOrDefault();

                int errCode = resultCheck.err;

                switch (errCode)
                {
                    case 0:
                        Session["UserID"] = resultCheck.ID;
                        List<int> UserRolls = (from p in db.SYS_PhanQuyens
                                               where p.UserID == new Guid(Session["UserID"].ToString())
                                               select
                                               p.RollID).ToList();

                        Session["UserRolls"] = UserRolls;
                        lblLoginError.Text = @"<div class='notification success png_bg'>
                            <a href='#' class='close'><img src='images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                            <div>
                                Đăng nhập thành công.
                            </div>
                        </div>";
                        Response.Redirect("Index");
                        break;
                    case 1:
                        lblLoginError.Text = @"<div class='notification error png_bg'>
                            <a href='#' class='close'><img src='images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                            <div>
                                Tài khoản không tồn tại.
                            </div>
                        </div>";
                        break;
                    case 2:
                        lblLoginError.Text = @"<div class='notification error png_bg'>
                            <a href='#' class='close'><img src='images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                            <div>
                                Sai mật khẩu.
                            </div>
                        </div>";
                        break;
                    case 3:
                        lblLoginError.Text = @"<div class='notification error png_bg'>
                            <a href='#' class='close'><img src='images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                            <div>
                                Tài khoản đã bị khóa.<br />Liên hệ với nhà quản trị để được mở khóa.
                            </div>
                        </div>";
                        break;
                }
            //}
            //catch (Exception ex)
            //{
//                lblLoginError.Text = @"<div class='notification error png_bg'>
//                            <a href='#' class='close'><img src='images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
//                            <div>
//                                Xảy ra lỗi nghiêm trọng trên máy chủ.<br />Hãy thử đăng nhập sau.
//                            </div>
//                        </div>";
            //    lblLoginError.Text = ex.ToString();
            //}
        }
    }
}

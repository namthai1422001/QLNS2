using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Mail;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Management;
using System.Net.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace QLNS
{
    public partial class ForgetPassword : System.Web.UI.Page
    {

        #region Methods

        /// <summary>
        /// Summary description for SendEmail
        /// </summary>
        public bool SendMail(string email, string mailbody)
        {
            //duc.vuanh1987@gmail.com tshvfpsqxstfdmwm
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential("nguyenphuongbacmanagement@gmail.com", "superadmintoi");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.Timeout = 80000;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            try
            {
                //foreach (Attachment item in objmail.Attachments)
                //{
                //    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(item.FilePath);
                //    mail.Attachments.Add(attach);
                //}
                mail.From = new System.Net.Mail.MailAddress("nguyenphuongbacmanagement@gmail.com"
                    , "Mã xác nhận để Reset mật khẩu", System.Text.Encoding.UTF8);
                mail.To.Add(email);
                mail.Subject = "Mã xác nhận để Reset mật khẩu";
                mail.Body = mailbody;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #endregion

        #region EventHandler
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if ((Session["Code"] != null) ? ((Session["Code"].ToString().ToLower() == txtCaptchar.Text.Trim().ToLower()) ? true : false) : false)
                {
                    ltrValueCaptchar.Text = "";
                    try
                    {
                        dbLinQDataContext db = new dbLinQDataContext();

                        SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == txtReUsername.Text.Trim() && p.IsDelete == false && p.IsLock == false && p.Email == txtReEmail.Text.Trim()).FirstOrDefault();

                        if (objData != null)
                        {
                            //Gui ma reset mat khau den Email cua nguoi dung.
                            //nguyenphuongbacmanagement@gmail.com superadmintoi
                            string mailBody = @"<div style='margin-top: 20px; margin-bottom: 20px;'><h3>PHẦN MỀM QUẢN LÝ NHÂN SỰ</h3>
                                                <hr />
                                                <p>Bạn đã gửi yêu cầu reset mật khẩu đến hệ thống quản lý nhân sự</p>
                                                <p>Vui lòng sao chép lại mã xác nhận bên dưới để được reset mật khẩu mới</p>
                                                <p><b>Mã xác nhận: </b><b style='color: #AC2C2C'>" + objData.CodeResetPassForget.ToString() + @"</b></p>
                                                <hr />
                                                Cảm ơn bạn đã sử dụng phần mềm của chúng tôi!<br />
                                                Chào bạn!<br /><br />
                                                Tổng giám đốc<br /><br />
                                                <b>Nguyễn Phương Bắc</b></div>";
                            if (SendMail(txtReEmail.Text.Trim(), mailBody) == true)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mã xác nhận để Reset mật khẩu đã được gửi vào Email của bạn! Vui lòng kiểm tra Email!'); window.location = 'Login';", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Đã xảy ra lỗi trong quá trình gửi mã xác nhận! Vui lòng thử lại chức năng này sau!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Gửi mã xác nhận thất bại! Bạn hãy kiểm tra lại tên đăng nhập, địa chỉ email có chính xác hay chưa? Nếu đã đúng thì nghĩa là tài khoản của bạn đã bị khóa');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối! Vui lòng thử lại sau'); window.location = 'Login';", true);
                    };
                }
                else
                {
                    ltrValueCaptchar.Text = "Mã xác nhận không hợp lệ";
                }
            }
        }
        #endregion
    }
}

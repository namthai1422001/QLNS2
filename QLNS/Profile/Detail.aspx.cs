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
    /// Chức năng: 3
    /// Hành động: Xem (5), Cập nhật (7)
    /// </summary>
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin cá nhân";
                loadData();
            }
        }

        #region Methods
        private void loadData()
        {
            Guid UserID;
            try
            {
                UserID = new Guid(Session["UserID"].ToString());
                List<int> UserRoll = Session["UserRolls"] as List<int>;
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.ID == UserID).FirstOrDefault();
                    ltrUsername.Text = objData.Username;
                    ltrNumberOfLogin.Text = objData.NumberOfLogin.ToString();
                    ltrLaterLogin.Text = objData.LaterLogin.ToString("dd/MM/yyyy HH:mm:ss");
                    txtEmail.Text = objData.Email;
                    txtFullname.Text = objData.Fullname;
                    if (objData.CreatedByUser == "-1")
                    {
                        txtEmail.Enabled = false;
                    }

                    //Lay day lieu cho Quuyen tren he thong
                    var lstQuyen = (from p in db.SYS_Quyens
                                    where UserRoll.Contains(p.ID)
                                    select new
                                    {
                                        p.Rollname,
                                        p.Description
                                    }).ToList();
                    int stt = 1;
                    var lstData = (from p in lstQuyen
                                   select new
                                   {
                                       STT = stt++,
                                       p.Rollname,
                                       p.Description
                                   }).ToList();

                    rpData.DataSource = lstData;
                    rpData.DataBind();

                    if (lstData.Count == 0)
                    {
                        ltrInfor.Text = @"<div class='notification information png_bg'>
                                            <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                            <div>
                                                Bạn chưa có quyền trên hệ thống.
                                            </div>
                                        </div>";
                    }

                    DiarySystem(3, 5, "");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối! Vui lòng thử lại sau'); window.location = '../Index';", true);
                };

            }
            catch
            {
                Response.Redirect("../Login");
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

        #region EventHandler

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == ltrUsername.Text).FirstOrDefault();

                    objData.Email = txtEmail.Text.Trim();
                    objData.Fullname = txtFullname.Text.Trim();

                    db.SubmitChanges();
                    DiarySystem(3, 7, "");
                    Response.Redirect("../Index");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
                }
            }
        }
        #endregion EventHandler
    }
}

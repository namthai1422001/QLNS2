using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Admin
{
    /// <summary>
    /// Quyền: 2
    /// Chức năng: 3
    /// Hành động: Xem (5), Tạo (6), Cập nhật (7), Xóa (8)
    /// 
    /// Quyền: 12
    /// Chức năng: 3
    /// Hành động: Reset mật khẩu
    /// </summary>
    public partial class EditTaikhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Cập nhật thông tin người dùng";
                    loadRole();
                    loadData(Request.QueryString["id"]);
                }
                else
                {
                    loadRole();
                    DiarySystem(3, 5, "");
                    ltrh3.Text = "Tạo tài khoản mới";
                    ltrAttention.Text = @"<div class='notification attention png_bg'>
                                                <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                                <div>
                                                    Thông tin về tài khoản chỉ được tạo 1 lần và sẽ không được phép chỉnh sửa<br />
                                                    Chỉ được phép cập nhật Ghi chú, Khóa, Mở khóa và Xóa tài khoản.
                                                </div>
                                            </div>";
                    Page.Title = "Tạo tài khoản mới";
                    VisibleControls(true, false, false);
                    panelAudit.Visible = false;
                    btnLock.Visible = false;
                    btnResetPass.Visible = false;
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
            if (UserRolls.Count == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
            bool QuyenQuantrihethong = (UserRolls.Where(p => p == 2).Count() == 1) ? true : false;
            bool QuyenResetmatkhau = (UserRolls.Where(p => p == 12).Count() == 1) ? true : false;
            if (QuyenQuantrihethong || QuyenResetmatkhau)
            {
                btnResetPass.Visible = QuyenResetmatkhau;
                btnAdd.Visible = QuyenQuantrihethong;
                btnDelete.Visible = QuyenQuantrihethong;
                btnUpdate.Visible = QuyenQuantrihethong;
                btnLock.Visible = QuyenQuantrihethong;
            }
            else
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
        

        #region Method

        //Visible controls
        private void VisibleControls(bool them, bool capnhat, bool xoa)
        {
            if (btnAdd.Visible) btnAdd.Visible = them;
            if (btnUpdate.Visible) btnUpdate.Visible = capnhat;
            if (btnDelete.Visible) btnDelete.Visible = xoa;
        }

        //Load Edit
        private void loadData(string username)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == username && p.IsDelete == false).FirstOrDefault();

                if (objData != null)
                {
                    ltrh3.Text = "Cập nhật thông tin của tài khoản " + objData.Username;

                    txtUsername.Text = objData.Username;
                    txtEmail.Text = objData.Email;
                    txtFullname.Text = objData.Fullname;
                    txtPassword.Text = "nguyenphuongbac";
                    txtPasswordRe.Text = "nguyenphuongbac";
                    chkSuper.Checked = objData.IsSuper;

                    txtUsername.Enabled = false;
                    txtEmail.Enabled = false;
                    txtFullname.Enabled = false;
                    txtPassword.Enabled = false;
                    txtPasswordRe.Enabled = false;
                    chkSuper.Enabled = false;

                    txtDescription.Text = objData.GhiChu;
                    if (objData.IsLock == true)
                    {
                        btnLock.Text = "Mở khóa";
                    }
                    else
                    {
                        btnLock.Text = "Khóa";
                    }
                    VisibleControls(false, true, true);

                    lblCreatedByUser.Text = db.SYS_Nguoidungs.Where(p => p.ID == new Guid(objData.CreatedByUser)).FirstOrDefault().Fullname;

                    GetTime gettime = new GetTime();
                    lblCreatedByDate.Text = gettime.GetDatetime(objData.CreatedByDate);

                    DiarySystem(3, 5, objData.Username);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = '../Index';", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
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
                    sp_SYS_Nguoidung_InsertResult objData = db.sp_SYS_Nguoidung_Insert(txtUsername.Text.Trim(), txtPassword.Text.Trim(),
                                                                txtEmail.Text.Trim(), txtFullname.Text.Trim(),
                                                                chkSuper.Checked, Session["UserID"].ToString()).FirstOrDefault();
                    if (objData.err == 0)
                    {
                        DiarySystem(3, 6, txtUsername.Text.Trim());
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tạo tài khoản thành công'); window.location = 'Quanlytaikhoan';", true);
                    }
                    else
                    {
                        string strInfor;
                        if (objData.err == 1)
                        {
                            strInfor = @"<div class='notification error png_bg'>
                                            <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                            <div>
                                                Không thể tạo tài khoản.<br />
                                                Tên đăng nhập đã được sử dụng.<br />
                                                Vui lòng nhập tên đăng nhập khác.
                                            </div>
                                        </div>";
                        }
                        else
                        {
                            strInfor = @"<div class='notification error png_bg'>
                                            <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                            <div>
                                                Không thể tạo tài khoản.<br />
                                                Email đã được sử dụng.<br />
                                                Vui lòng nhập Email khác.
                                            </div>
                                        </div>";
                        }
                        ltrInfor.Text = strInfor;
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
                };
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == Request.QueryString["id"]).FirstOrDefault();

                objData.GhiChu = txtDescription.Text.Trim();
                db.SubmitChanges();
                DiarySystem(3, 7, objData.Username);
                Response.Redirect("Quanlytaikhoan");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == Request.QueryString["id"]).FirstOrDefault();

                if (btnLock.Text == "Khóa")
                {
                    objData.IsLock = true;
                    DiarySystem(3, 7, "Khóa tài khoản " + objData.Username);
                }
                else
                {
                    objData.IsLock = false;
                    DiarySystem(3, 7, "Mở khóa tài khoản " + objData.Username);
                }

                db.SubmitChanges();
                Response.Redirect("Quanlytaikhoan");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
        }

        protected void btnResetPass_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                db.sp_SYS_ResetPass(Request.QueryString["id"]);

                DiarySystem(3, 4, Request.QueryString["id"]);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reset mật khẩu cho tài khoản " + Request.QueryString["id"] + " thành công!'); window.location = 'Quanlytaikhoan';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == Request.QueryString["id"]).FirstOrDefault();

                objData.IsDelete = true;

                db.SubmitChanges();
                DiarySystem(3, 8, objData.Username);
                Response.Redirect("Quanlytaikhoan");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
        }
        #endregion

    }
}

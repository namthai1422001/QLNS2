using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 3
    /// Chức năng: 31
    /// Hành động: Xem (5 - Everyone), Tao (6)
    /// </summary>
    public partial class Songaychamcong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quy định số ngày chấm công";
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
            if (UserRolls.Where(p => p == 3).Count() == 1)
            {
                btnReInstall.Visible = true;
            }
            else
            {
                btnReInstall.Visible = false;
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
        //Load du lieu
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            PB_Thaydoitongsongaychamcong objdata = (from p in db.PB_Thaydoitongsongaychamcongs
                                        where p.IsCurrent == true
                                        orderby p.Ngayapdung descending
                                        select p
                                        ).FirstOrDefault();
            if (objdata != null)
            {
                lblValue.Text = objdata.Tongsongaychamcong.ToString();
                lblNguoiky.Text = objdata.Nguoiky;
                lblNgayky.Text = objdata.Ngayky.ToString("dd/MM/yyyy");
                lblChucvunguoiky.Text = objdata.Chucvunguoiky;
                lblNgayapdung.Text = "Tháng " + objdata.Ngayapdung.Month.ToString() + "/" + objdata.Ngayapdung.Year.ToString();

                DiarySystem(35, 5, "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lương tối thiểu chưa được cài đặt. Chuyển sang cài đặt lương tối thiểu'); window.location = 'CaidatLuongtoithieu';", true);
            }
        }
        #endregion

        #region EventHandler
        protected void btnReInstall_Click(object sender, EventArgs e)
        {
            Response.Redirect("CaidatSongaychamcong");
        }
        #endregion
    }
}

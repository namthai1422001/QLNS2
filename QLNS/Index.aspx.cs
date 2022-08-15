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
    /// Hành động: Xem (5), Cap nhat (7)
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    loadData();
            //}
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
                hplEdit.Visible = false;
            }
            else
            {
                hplEdit.Visible = true;
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
                    ltrContentOfIndex.Text = Server.HtmlDecode(objData.ContentOfIndex);
                }
                else
                {
                    ltrContentOfIndex.Text = "<span style='color: Red; font-size: 20px;'>Chưa có dữ liệu của trang chủ</span>";
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Index'", true);
            }
        }
        #endregion
    }
}

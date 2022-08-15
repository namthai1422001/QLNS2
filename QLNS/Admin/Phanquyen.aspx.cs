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
    /// Chức năng: 41
    /// Hành động: Xem (5), Cập nhật (7)
    /// </summary>
    public partial class Phanquyen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Phân quyền cho người dùng";
                    loadRole();
                    loadData(Request.QueryString["id"]);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = '../Index';", true);
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
            if (UserRolls.Where(p => p == 2).Count() == 0)
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

        //Load Edit
        private void loadData(string username)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == username && p.IsDelete == false).FirstOrDefault();

                if (objData != null)
                {
                    if (objData.IsSuper == true)
                    {
                        ltrInfor.Visible = true;
                        btnUpdate.Visible = false;
                    }
                    ltrh3.Text = "Phân quyền cho tài khoản " + Request.QueryString["id"];
                    var lstQuyen = (from p in db.SYS_Quyens
                                    where p.ID != 1
                                    select new { p.ID, p.Rollname }).ToList();
                    var lstQuyenNhanvien = (from p in db.SYS_PhanQuyens
                                            where p.UserID == objData.ID
                                            select new { p.RollID }).ToList();

                    var lstQuyenHienThi = (from quyen in lstQuyen
                                           join quyennhanvien in lstQuyenNhanvien
                                           on quyen.ID equals quyennhanvien.RollID into ps
                                           from quyennhanvien in ps.DefaultIfEmpty()
                                           select new
                                           {
                                               RollID = quyen.ID,
                                               quyen.Rollname,
                                               IsRoll = (quyennhanvien != null) ? true : false
                                           }).ToList();
                    int stt=1;
                    var lstData = (from p in lstQuyenHienThi
                                   select new
                                   {
                                       STT = stt++,
                                       p.RollID,
                                       p.Rollname,
                                       p.IsRoll
                                   }).ToList();

                    rpData.DataSource = lstData;
                    rpData.DataBind();

                    DiarySystem(41, 5, objData.Username);
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

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string RollID = e.Item.DataItem.ToString().Split(',').ElementAt(1).Split('=').ElementAt(1).Replace(" ", "");

                bool IsRoll = bool.Parse(e.Item.DataItem.ToString().Replace("}", "").Split(',').Skip(3).FirstOrDefault().Split('=').Skip(1).FirstOrDefault());
                Literal ltrRoll = (Literal)e.Item.FindControl("ltrRoll");
                string strCheckbox = @"<input type='checkbox' name='chkRollID'";
                if (IsRoll)
                {
                    strCheckbox += @" checked='checked'";
                }
                if (ltrInfor.Visible == true)
                {
                    strCheckbox += @" disabled='disabled'";
                }
                strCheckbox +=  " value='" + RollID +  "'" +" />";

                ltrRoll.Text = strCheckbox;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                SYS_Nguoidung objData = db.SYS_Nguoidungs.Where(p => p.Username == Request.QueryString["id"]).FirstOrDefault();

                if (objData.IsSuper == true)
                {
                    throw new Exception();
                }

                //Lay lstRollID is Checked
                List<int> lstRollID = Request["chkRollID"].Split(',').Select(p => int.Parse(p.ToString())).ToList();
                //Bo het quyen hien tai cua Tai khoan
                db.sp_SYS_UnInstallRollForUser(objData.Username);

                //Cai dat lai Roll
                foreach (int p in lstRollID)
                {
                    SYS_PhanQuyen objPhanQuyen = new SYS_PhanQuyen();
                    objPhanQuyen.RollID = p;
                    objPhanQuyen.UserID = objData.ID;

                    objPhanQuyen.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objPhanQuyen.CreatedByDate = DateTime.Now;

                    db.SYS_PhanQuyens.InsertOnSubmit(objPhanQuyen);
                    db.SubmitChanges();
                }
                DiarySystem(41, 7, objData.Username);
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

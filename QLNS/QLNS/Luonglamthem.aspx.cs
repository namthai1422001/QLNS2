using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 7,8
    /// Chức năng: 24
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Luonglamthem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["y"])
                    && !string.IsNullOrEmpty(Request.QueryString["m"])
                    && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    loadRole();
                    DateTime now = DateTime.Now;
                    int Nam = now.Year;
                    int Thang = now.Month;
                    try
                    {
                        Nam = int.Parse(Request.QueryString["y"]);
                        Thang = int.Parse(Request.QueryString["m"]);
                        if (Nam > now.Year || (Nam == now.Year && Thang > now.Month))
                            throw new Exception();
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'Luongngay.aspx';", true);
                        return;
                    };
                    loadData(Nam, Thang, Request.QueryString["id"]);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'Luongngay.aspx';", true);
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
            if (UserRolls.Where(p => p == 7 || p == 8).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
            else
            {
                if (UserRolls.Where(p => p == 7).Count() == 1)
                {
                    hplAdd.Visible = true;
                }
                else
                {
                    hplAdd.Visible = false;
                }
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

        #region Methods
        //Load du lieu cho Repeater
        private void loadData(int Nam, int Thang, string MaNV)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objBangluong = (from dsbangluong in db.PB_Danhsachbangluongs
                                where dsbangluong.Nam == Nam && dsbangluong.Thang == Thang
                                select
                                new
                                {
                                    dsbangluong.Mabangluong,
                                    dsbangluong.IsLock,
                                    dsbangluong.IsFinish
                                }).FirstOrDefault();
            var objNhanvien = (from p in db.PB_Nhanviens
                               where p.MaNV == MaNV
                               select new { HoTen = p.HoNV + " " + p.TenNV }).FirstOrDefault();
            if (objBangluong == null || objNhanvien == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'Luongngay.aspx';", true);
                return;
            }
            else
            {
                if (objBangluong.IsFinish)
                {
                    ltrStatus.Text = "Đã hoàn thành";
                    imgStatus.ImageUrl = "~/images/icons/finished.png";
                    imgStatus.AlternateText = "Đã hoàn thành";
                    hplAdd.Visible = false;
                }
                else
                    if (objBangluong.IsLock)
                    {
                        ltrStatus.Text = "Đã khóa";
                        imgStatus.ImageUrl = "~/images/icons/lock.png";
                        imgStatus.AlternateText = "Đã khóa";
                        hplAdd.Visible = false;
                    }
                    else
                    {
                        ltrStatus.Text = "Không khóa";
                        imgStatus.ImageUrl = "~/images/icons/unlock.png";
                        imgStatus.AlternateText = "Không khóa";
                    }
                ltrh3.Text = "Chi tiết lương làm thêm tháng " + Request.QueryString["m"] + "/" + Request.QueryString["y"]
                                + " của nhân viên "
                                + objNhanvien.HoTen.ToString();
                var lst = (from p in db.PB_Luonglamthems
                           where p.Mabangluong == objBangluong.Mabangluong && p.MaNV == MaNV
                           select
                            new
                            {
                                p.Tenluonglamthem,
                                p.Sotien
                            }).ToList();
                int stt = 1;
                var lstData = (from p in lst
                               select
                               new
                               {
                                   STT = stt++,
                                   p.Tenluonglamthem,
                                   p.Sotien
                               }).ToList();

                //Bind Data
                rpData.DataSource = lstData;
                rpData.DataBind();

                DiarySystem(24, 5, MaNV + " tháng " + Thang.ToString() + "/" + Nam.ToString());

                hplAdd.NavigateUrl = string.Format("EditLuonglamthem.aspx?y={0}&m={1}&id={2}&height=310", Nam, Thang, MaNV);
            }
        }
        #endregion
    }
}

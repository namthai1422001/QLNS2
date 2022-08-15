using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 7
    /// Chức năng: 25
    /// Hành động: Tạo (6)
    /// </summary>
    public partial class EditLuonglamthem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["y"]) && !string.IsNullOrEmpty(Request.QueryString["m"]) && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Page.Title = "Nhập lương làm thêm nhân viên";
                    loadRole();
                    int Nam = 0;
                    int Thang = 0;
                    DateTime now = DateTime.Now;
                    try
                    {
                        Nam = int.Parse(Request.QueryString["y"]);
                        Thang = int.Parse(Request.QueryString["m"]);
                        if (Nam > now.Year || (Nam == now.Year && Thang > now.Month))
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = '../Index';", true);
                    };
                    loadData(Nam, Thang, Request.QueryString["id"]);
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
            if (UserRolls.Where(p => p == 7).Count() == 0)
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

        #region Methods

        private void loadData(int Nam, int Thang, string MaNV)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objNhanvien = (from p in db.PB_Nhanviens
                               where p.MaNV == MaNV
                               select new
                               {
                                   HoTen = p.HoNV + " " + p.TenNV
                               }).FirstOrDefault();
            if (objNhanvien == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = '../Index';", true);
                return;
            }
            ltrMaNV.Text = MaNV;
            ltrHoTen.Text = objNhanvien.HoTen;
            ltrBangluong.Text = Thang.ToString() + "/" + Nam.ToString();
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
                    int Nam = int.Parse(Request.QueryString["y"]);
                    int Thang = int.Parse(Request.QueryString["m"]);
                    string MaNV = Request.QueryString["id"];
                    var objSalary = (from p in db.PB_Danhsachbangluongs
                                     where p.Nam == Nam
                                         && p.Thang == Thang
                                     select
                                         new
                                         {
                                             p.Mabangluong,
                                             p.IsLock,
                                             p.IsFinish
                                         }).FirstOrDefault();
                    if (objSalary != null)
                    {
                        //Bang luong trong thang da duoc tao.
                        //Kiem tra trang thai cua bang luong do da hoan thanh hay bi khoa hay ko
                        if (objSalary.IsFinish)
                        {
                            string strMsg = "Bảng lương tháng " + Thang + "/" + Nam + " đã hoàn thành. Không thể thêm lương làm thêm trong tháng này";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + strMsg + "'); window.location = '../Index';", true);
                            return;
                        }
                        else
                        {
                            if (objSalary.IsLock)
                            {
                                string strMsg = "Bảng lương tháng " + Thang + "/" + Nam + " đã bị khóa. Hãy mở khóa bảng lương tháng này trước khi thêm";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + strMsg + "'); window.location = '../Index';", true);
                                return;
                            }
                        }
                    }
                    //Bang luong trong thang cho phep them.

                    PB_Luonglamthem objData = new PB_Luonglamthem();

                    objData.Mabangluong = objSalary.Mabangluong;
                    objData.MaNV = MaNV;
                    objData.Tenluonglamthem = txtTenluonglamthem.Text;
                    objData.Sotien = int.Parse(txtTienluonglamthem.Text.Replace(",", ""));

                    objData.CreatedByUser = new Guid(Session["UserID"].ToString());
                    objData.CreatedByDate = DateTime.Now;

                    db.PB_Luonglamthems.InsertOnSubmit(objData);
                    db.SubmitChanges();

                    DiarySystem(25, 6, MaNV + " tháng " + Thang.ToString() + "/" + Nam.ToString());

                    Response.Redirect("Luonglamthem@" + Nam + "@" + Thang + "@" + MaNV);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
                };
            }
        }
        #endregion
    }
}

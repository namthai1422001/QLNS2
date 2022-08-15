using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 4
    /// Chức năng: 15
    /// Hành động: Xem (5)
    /// </summary>
    public partial class DetailKhenthuong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin chi tiết về việc khen thưởng của nhân viên";
                loadRole();
                Guid id;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    try
                    {
                        id = new Guid(Request.QueryString["id"]);
                        loadData(id);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Khenthuong';", true);
                    };
                }
            }
        }

        #region Security
        //Phan quyen tren trang dua vao day!
        private void loadRole()
        {
            //if (Session["UserRolls"] == null)
            //{
            //    Response.Redirect(ResolveUrl("~/Login"));
            //}
            //List<int> UserRolls = Session["UserRolls"] as List<int>;
            //if (UserRolls.Where(p => p == 4).Count() == 0)
            //{
            //    Response.Redirect(ResolveUrl("~/DontAllow"));
            //}
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
        private void loadData(Guid makhenthuong)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objData = (from nvien in db.PB_Nhanviens
                           join khenthuong in db.PB_KhenthuongNhanviens
                           on nvien.MaNV equals khenthuong.MaNV
                           where khenthuong.Makhenthuong == makhenthuong
                           select
                     new
                     {
                         nvien.MaNV,
                         HoTen = nvien.HoNV + " " + nvien.TenNV,
                         khenthuong.Makhenthuong,
                         khenthuong.Tenkhenthuong,
                         khenthuong.Hinhthuckhenthuong,
                         khenthuong.LyDo,
                         khenthuong.Ngaykhenthuong,
                         khenthuong.Sotien,
                         khenthuong.Nguoiky,
                         khenthuong.Chucvunguoiky,
                         khenthuong.Ngayky
                     }).FirstOrDefault();
            if (objData != null)
            {
                ltrh3.Text = "Thông tin chi tiết về khen thưởng của " + objData.HoTen;
                ltrMaNV.Text = objData.MaNV;
                ltrHoTen.Text = objData.HoTen;
                ltrMakhenthuong.Text = objData.Makhenthuong.ToString();
                ltrTenkhenthuong.Text = objData.Tenkhenthuong;
                ltrHinhthuckhenthuong.Text = objData.Hinhthuckhenthuong;
                ltrLydo.Text = objData.LyDo;
                ltrNgaykhenthuong.Text = objData.Ngaykhenthuong.ToString("dd/MM/yyyy");
                ltrHoTenNguoiky.Text = objData.Nguoiky;
                ltrChucvunguoiky.Text = objData.Chucvunguoiky;
                ltrNgayky.Text = objData.Ngayky.ToString("dd/MM/yyyy");
                ltrTienkhenthuong.Text = objData.Sotien.ToString("#,##0");

                DiarySystem(15, 5, objData.Makhenthuong.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = '../Index';", true);
            }
        }
        #endregion

        #region EventHandler

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    public partial class DetailTamung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin chi tiết về việc tạm ứng của nhân viên";
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Tamung';", true);
                    };
                }
            }
        }

        #region Security
        //Phan quyen tren trang dua vao day!
        private void loadRole()
        {

        }
        #endregion

        #region Methods
        private void loadData(Guid matamungnvien)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objData = (from nvien in db.PB_Nhanviens
                           join tamungnvien in db.PB_TamungNhanviens
                           on nvien.MaNV equals tamungnvien.MaNV
                           where tamungnvien.Matamung == matamungnvien
                           select
                     new
                     {
                         nvien.MaNV,
                         HoTen = nvien.HoNV + " " + nvien.TenNV,
                         tamungnvien.Matamung,
                         tamungnvien.LyDo,
                         tamungnvien.Ngaytamung,
                         tamungnvien.Sotien,
                         tamungnvien.Nguoiky,
                         tamungnvien.Chucvunguoiky,
                         tamungnvien.Ngayky
                     }).FirstOrDefault();
            if (objData != null)
            {
                ltrh3.Text = "Thông tin chi tiết về tạm ứng của " + objData.HoTen;
                ltrMaNV.Text = objData.MaNV;
                ltrHoTen.Text = objData.HoTen;
                ltrMatamung.Text = objData.Matamung.ToString();
                ltrLydo.Text = objData.LyDo;
                ltrNgaytamung.Text = objData.Ngaytamung.ToString("dd/MM/yyyy");
                ltrHoTenNguoiky.Text = objData.Nguoiky;
                ltrChucvunguoiky.Text = objData.Chucvunguoiky;
                ltrNgayky.Text = objData.Ngayky.ToString("dd/MM/yyyy");
                ltrTientamung.Text = objData.Sotien.ToString("#,##0");
            }
        }
        #endregion

        #region EventHandler

        #endregion
    }
}

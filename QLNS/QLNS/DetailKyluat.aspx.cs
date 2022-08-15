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
    /// Chức năng: 16
    /// Hành động: Xem (5), Xóa (8)
    /// </summary>
    public partial class DetailKyluat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin chi tiết về việc kỷ luật của nhân viên";
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = 'Kyluat';", true);
                    };
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
            if (UserRolls.Where(p => p == 4).Count() == 0)
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
        private void loadData(Guid makyluat)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objData = (from nvien in db.PB_Nhanviens
                           join kyluat in db.PB_KyluatNhanviens
                           on nvien.MaNV equals kyluat.MaNV
                           where kyluat.Makyluat == makyluat
                           select
                     new
                     {
                         nvien.MaNV,
                         HoTen = nvien.HoNV + " " + nvien.TenNV,
                         kyluat.Makyluat,
                         kyluat.Tenkyluat,
                         kyluat.Hinhthuckyluat,
                         kyluat.LyDo,
                         kyluat.Ngaykyluat,
                         kyluat.Nguoiky,
                         kyluat.Chucvunguoiky,
                         kyluat.Ngayky,
                         kyluat.Motasuviec,
                         kyluat.Ngayxayra,
                         kyluat.Nguoibikyluatgiaithich,
                         kyluat.Nguoichungkien,
                         kyluat.Diadiem
                     }).FirstOrDefault();
            if (objData != null)
            {
                ltrh3.Text = "Thông tin chi tiết về kỷ luật của " + objData.HoTen;
                ltrMaNV.Text = objData.MaNV;
                ltrHoTen.Text = objData.HoTen;
                ltrMakyluat.Text = objData.Makyluat.ToString();
                ltrTenkyluat.Text = objData.Tenkyluat;
                ltrHinhthuckyluat.Text = objData.Hinhthuckyluat;
                ltrLydo.Text = objData.LyDo;
                ltrHoTenNguoiky.Text = objData.Nguoiky;
                ltrMotasuviec.Text = objData.Motasuviec;
                ltrLydo.Text = objData.LyDo;
                ltrNguoibikyluatgiaithich.Text = Server.HtmlDecode(objData.Nguoibikyluatgiaithich);
                ltrNguoichungkien.Text = objData.Nguoichungkien;
                ltrDiadiem.Text = objData.Diadiem;

                ltrNgaykyluat.Text = objData.Ngaykyluat.ToString("dd/MM/yyyy");
                ltrNgayxayra.Text = objData.Ngayxayra.ToString("dd/MM/yyyy");

                ltrChucvunguoiky.Text = objData.Chucvunguoiky;
                ltrNgayky.Text = objData.Ngayky.ToString("dd/MM/yyyy");

                DiarySystem(16, 5, objData.Makyluat.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập đúng cách'); window.location = '../Index';", true);
            }
        }
        #endregion

        #region EventHandler
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Guid makyluat = new Guid(Request.QueryString["id"]);
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                PB_KyluatNhanvien objData = db.PB_KyluatNhanviens.Where(p => p.Makyluat == makyluat).FirstOrDefault();

                db.PB_KyluatNhanviens.DeleteOnSubmit(objData);

                db.SubmitChanges();
                DiarySystem(16, 8, objData.Makyluat.ToString() + "|" + objData.MaNV);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Xóa kỷ luật thành công'); window.location = 'Kyluat';", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Kyluat';", true);
            };


        }
        #endregion
    }
}

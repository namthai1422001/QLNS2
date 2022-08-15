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
    /// Chức năng: 14
    /// Hành động: Xem (5), Tạo (6)
    /// </summary>
    public partial class DetailDicongtac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Thông tin chi tiết về việc đi công tác của nhân viên";
                loadRole();
                int id = -1;
                if(!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    try
                    {
                        id = int.Parse(Request.QueryString["id"]);
                        if(id <= 0)
                            throw new Exception();
                        loadData(id);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this,GetType(),"alert","alert('Vui lòng truy cập đúng cách'); window.location = 'Dicongtac';",true);
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
        private void loadData(int macongtac)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var objData = (from nvien in db.PB_Nhanviens
                           join congtac in db.PB_Dicongtacs
                           on nvien.MaNV equals congtac.MaNV
                           where congtac.Macongtac == macongtac
                           select
                     new
                     {
                         nvien.MaNV,
                         HoTen = nvien.HoNV + " " + nvien.TenNV,
                         congtac.Macongtac,
                         congtac.Noicongtac,
                         congtac.Veviec,
                         congtac.LyDo,
                         congtac.Tungay,
                         congtac.Denngay,
                         congtac.Tiendicongtac,
                         congtac.Nguoiky,
                         congtac.Chucvunguoiky,
                         congtac.Ngayky
                     }).FirstOrDefault();
            if (objData != null)
            {
                ltrh3.Text = "Thông tin chi tiết về công tác của " + objData.HoTen;
                ltrMacongtac.Text = objData.Macongtac.ToString();
                ltrMaNV.Text = objData.MaNV;
                ltrHoTen.Text  = objData.HoTen;
                ltrVeviec.Text = objData.Veviec;
                ltrLydo.Text = objData.LyDo;
                ltrNoicongtac.Text = objData.Noicongtac;
                ltrNgaydi.Text = objData.Tungay.ToString("dd/MM/yyyy");
                ltrNgayve.Text = objData.Denngay.ToString("dd/MM/yyyy");
                ltrHoTenNguoiky.Text = objData.Nguoiky;
                ltrChucvunguoiky.Text = objData.Chucvunguoiky;
                ltrNgayky.Text = objData.Ngayky.ToString("dd/MM/yyyy");
                ltrTiendicongtac.Text = objData.Tiendicongtac.ToString("#,##0");

                DiarySystem(14, 5, objData.Macongtac.ToString());
            }
        }
        #endregion

        #region EventHandler
        
        #endregion
    }
}

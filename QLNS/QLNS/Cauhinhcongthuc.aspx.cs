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
    public partial class Cauhinhcongthuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Công thức tính lương";
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
                    Response.Redirect("../Login");
                }
            }
            catch
            {
                Response.Redirect("../Login");
            };
        }
        #endregion

        //Load du lieu
        private void loadData()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            DIC_Cauhinhcongthuc congthuc = db.DIC_Cauhinhcongthucs.Where(p => p.IsCurrent == true).FirstOrDefault();

            if (congthuc != null)
            {

                lblNguoiky.Text = congthuc.Nguoiky;
                lblNgayky.Text = string.Format("{0:dd/MM/yyyy}", congthuc.Ngayky);
                lblChucvunguoiky.Text = congthuc.Chucvunguoiky;
                lblNgayapdung.Text = "Tháng " + congthuc.Ngayapdung.Month.ToString() + "/" + congthuc.Ngayapdung.Year.ToString();
                lblMota.Text = Server.HtmlDecode(congthuc.Mota);

                lblBHXH.Text = congthuc.BHXH.ToString("#,###.#0");
                lblBHYT.Text = congthuc.BHYT.ToString("#,###.#0");
                lblBHTN.Text = congthuc.BHTN.ToString("#,###.#0");
                lblBHXHMax.Text = congthuc.BHXHMAX.ToString("#,##0");

                lblPhicongdoan.Text = congthuc.Phicongdoan.ToString("#,###.#0");
                lblPhicongdoanMax.Text = congthuc.PhicongdoanMax.ToString("#,##0");

                lblTangcaThuong.Text = congthuc.Tangcathuong.ToString("#,###.#0");
                lblTangcaChunhat.Text = congthuc.Tangchunhat.ToString("#,###.#0");
                lblTangcanghile.Text = congthuc.Tangnghile.ToString("#,###.#0");

                lblMinIncomeTax.Text = congthuc.TinhThueTNCN.ToString("#,##0");
                lblChichonguoiphuthuoc.Text = congthuc.Chinguoiphuthuoc.ToString("#,##0");

                DiarySystem(31, 5, "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Công thức tính lương chưa được cài đặt. Chuyển sang cài đặt công thức'); window.location = 'CaidatCongthuctinhluong';", true);
            }
        }

        protected void btnReInstall_Click(object sender, EventArgs e)
        {
            Response.Redirect("CaidatCongthuctinhluong");
        }
    }
}

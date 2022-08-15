using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 10
    /// Chức năng: 44
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditBacluong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = -1;
                int NgachID = -1;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = int.Parse(Request.QueryString["id"]);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ngachid"]))
                {
                    NgachID = int.Parse(Request.QueryString["ngachid"]);
                }
                if (NgachID > 0 && id > 0)
                {
                    loadRole();
                    loadData(NgachID, id);
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    panelAudit.Visible = true;
                }
                else
                {
                    if (NgachID > 0)
                    {
                        loadRole();
                        loadData(NgachID);
                        btnAdd.Visible = true;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                        panelAudit.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = 'Bacluong';", true);
                    }
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
            if (UserRolls.Where(p => p == 10).Count() == 0)
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

        //Add
        private void loadData(int NgachID)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            lblNgach.Text = db.DIC_NgachLuongs.Where(p => p.MaNgach == NgachID).FirstOrDefault().TenNgach;
        }

        //Edit
        private void loadData(int NgachID, int _id)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            DIC_BacLuong lst = db.DIC_BacLuongs.Where(p => p.Bac == _id && p.MaNgach == NgachID).FirstOrDefault();

            lblNgach.Text = db.DIC_NgachLuongs.Where(p => p.MaNgach == NgachID).FirstOrDefault().TenNgach;
            txtName.Text = lst.Tenbac;
            txtValue.Text = lst.Heso.ToString("#,###.#0");
            txtDescription.Text = lst.GhiChu;

            lblCreatedByUser.Text = db.SYS_Nguoidungs.Where(p => p.ID == lst.CreatedByUser).FirstOrDefault().Fullname;

            GetTime gettime = new GetTime();
            lblCreatedByDate.Text = gettime.GetDatetime(lst.CreatedByDate);

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    DIC_BacLuong _data = new DIC_BacLuong();
                    int MaNgach = int.Parse(Request.QueryString["ngachid"]);
                    _data.MaNgach = MaNgach;

                    List<int> BacMax = db.DIC_BacLuongs.Where(p => p.MaNgach == MaNgach).Select(p => p.Bac).ToList();

                    _data.Bac = (BacMax.Count() > 0) ? BacMax.Max() + 1 : 1;
                    _data.Tenbac = txtName.Text.Trim();
                    _data.Heso = double.Parse(txtValue.Text);
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.DIC_BacLuongs.InsertOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(44, 6, txtName.Text.Trim());
                    Response.Redirect("Bacluong");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Bacluong';", true);
                };
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    int ngachid = int.Parse(Request.QueryString["ngachid"]);
                    dbLinQDataContext db = new dbLinQDataContext();
                    DIC_BacLuong _data = (from p in db.DIC_BacLuongs
                                          where p.MaNgach == ngachid && p.Bac == id
                                          select p).FirstOrDefault();

                    _data.Tenbac = txtName.Text.Trim();
                    _data.Heso = double.Parse(txtValue.Text);
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    db.SubmitChanges();

                    DiarySystem(44, 7, _data.MaNgach + "|" + txtName.Text.Trim());

                    Response.Redirect("Bacluong");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Bacluong';", true);
                };
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    int ngachid = int.Parse(Request.QueryString["ngachid"]);
                    dbLinQDataContext db = new dbLinQDataContext();
                    DIC_BacLuong _data = (from p in db.DIC_BacLuongs
                                          where p.MaNgach == ngachid && p.Bac == id
                                          select p).FirstOrDefault();
                    db.DIC_BacLuongs.DeleteOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(44, 8, _data.MaNgach + "|" + txtName.Text.Trim());

                    Response.Redirect("Bacluong");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Bacluong';", true);
                };
            }
        }
    }
}

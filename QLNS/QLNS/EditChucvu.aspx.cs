using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 9
    /// Chức năng: 30
    /// Hành động: Tạo (6), Cập nhật (7), Xóa (8)
    /// </summary>
    public partial class EditChucvu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = -1;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = int.Parse(Request.QueryString["id"]);
                }
                if (id > 0)
                {
                    loadRole();
                    loadData(id);
                    btnAdd.Visible = false;

                    txtName.Enabled = false;
                    txtDescription.Enabled = false;
                    cbCaptren.Enabled = false;
                }
                else
                {
                    loadRole();
                    loadChucvu();

                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    panelAudit.Visible = false;
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
            if (UserRolls.Where(p => p == 9).Count() == 0)
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
        private void loadChucvu()
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lst = (from p in db.DIC_Chucvus
                       select new
                       {
                           p.Machucvu,
                           p.Tenchucvu
                       }).ToList();
            cbCaptren.Items.Add(new ListItem("Là cao nhất", "0"));
            foreach (var p in lst)
            {
                cbCaptren.Items.Add(new ListItem(p.Tenchucvu, p.Machucvu.ToString()));
            }
            cbCaptren.SelectedIndex = 0;
            
        }

        private void loadData(int _id)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            DIC_Chucvu lst = db.DIC_Chucvus.Where(p => p.Machucvu == _id).FirstOrDefault();

            txtName.Text = lst.Tenchucvu;
            if (lst.Captren != 0)
            {
                cbCaptren.Items.Add(new ListItem(db.DIC_Chucvus.Where(p => p.Machucvu == lst.Captren).FirstOrDefault().Tenchucvu, lst.Captren.ToString()));
            }
            else
            {
                cbCaptren.Items.Add(new ListItem("Là cao nhất", "0"));
            }
            txtDescription.Text = lst.GhiChu;
            btnUpdate.Text = (lst.IsActive == true ? "Không được sử dụng" : "Được sử dụng");

            lblCreatedByUser.Text = db.SYS_Nguoidungs.Where(p => p.ID == lst.CreatedByUser).FirstOrDefault().Fullname;

            GetTime gettime = new GetTime();
            lblCreatedByDate.Text = gettime.GetDatetime(lst.CreatedByDate);
        }
        #endregion

        #region EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    DIC_Chucvu _data = new DIC_Chucvu();
                    _data.Tenchucvu = txtName.Text.Trim();
                    _data.Captren = int.Parse(cbCaptren.SelectedValue);
                    _data.GhiChu = txtDescription.Text.Trim();
                    _data.CreatedByUser = new Guid(Session["UserID"].ToString());
                    _data.CreatedByDate = DateTime.Now;
                    _data.IsActive = true;
                    db.DIC_Chucvus.InsertOnSubmit(_data);
                    db.SubmitChanges();

                    DiarySystem(30, 6, _data.Tenchucvu);

                    Response.Redirect("Chucvu");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Chucvu';", true);
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
                    dbLinQDataContext db = new dbLinQDataContext();
                    if (db.PB_Thaydoichucvus.Where(p => p.Machucvu == id).Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Chức vụ này đã được sử dụng. Không được phép xóa'); window.location = 'Chucvu';", true);
                    }
                    else
                    {
                        DIC_Chucvu _data = db.DIC_Chucvus.Where(p => p.Machucvu == id).FirstOrDefault();
                        db.DIC_Chucvus.DeleteOnSubmit(_data);
                        db.SubmitChanges();

                        DiarySystem(30, 8, _data.Machucvu + "|" + _data.Tenchucvu);

                        Response.Redirect("Chucvu");
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Chucvu';", true);
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
                    dbLinQDataContext db = new dbLinQDataContext();
                    DIC_Chucvu _data = db.DIC_Chucvus.Where(p => p.Machucvu == id).FirstOrDefault();
                    _data.IsActive = (btnUpdate.Text == "Được sử dụng") ? true : false;
                    db.SubmitChanges();

                    DiarySystem(30, 7, _data.Machucvu + "|" + _data.Tenchucvu);

                    Response.Redirect("Chucvu");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = 'Chucvu';", true);
                };
            }
        }
        #endregion
    }
}

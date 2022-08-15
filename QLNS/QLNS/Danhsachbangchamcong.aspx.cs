using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.QLNS
{
    /// <summary>
    /// Quyền: 5,6
    /// Chức năng: 18
    /// Hành động: Xem (5), Tao (6 - Hoan thanh tinh luong (quyen 6))
    /// </summary>
    public partial class Danhsachbangchamcong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Danh sách bảng chấm công";
                loadRole();
                loadDropdownlist();
                loadData(int.Parse(cbNam.SelectedValue));
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
            //Nguoi co quyen hoàn thành bảng lương mới được sử dụng chức năng này
            if (UserRolls.Where(p => p == 5 || p == 6).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
            else
            {
                if (UserRolls.Where(p => p == 6).Count() == 1)
                {
                    hdIsRoll.Value = "True";
                    btnAdd.Visible = true;
                }
                else
                {
                    hdIsRoll.Value = "False";
                    btnAdd.Visible = false;
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

        //Load du lieu cho cbNam
        private void loadDropdownlist()
        {
            cbNam.Items.Clear();

            dbLinQDataContext db = new dbLinQDataContext();
            List<int> lstYear = (from p in db.PB_Danhsachchamcongs
                                 orderby p.Nam
                                 select p.Nam).Distinct().OrderByDescending(p => p).ToList();

            int CurrentYear = DateTime.Now.Year;
            if (lstYear.Count != 0)
            {
                foreach (int p in lstYear)
                {
                    cbNam.Items.Add(new ListItem(p.ToString(), p.ToString()));
                }
            }
            else
            {
                cbNam.Items.Add(new ListItem(CurrentYear.ToString(), CurrentYear.ToString()));
            }

        }

        //Load du lieu cho Repeater
        private void loadData(int nam)
        {
            dbLinQDataContext db = new dbLinQDataContext();
            var lstBangchamcong = (from p in db.PB_Danhsachchamcongs
                                   where p.Nam == nam
                                   select
                                     new
                                     {
                                         p.Mabangchamcong,
                                         p.Nam,
                                         p.Thang,
                                         p.IsLock,
                                         p.IsFinish
                                     }).ToList();
            int stt = 1;
            var lstData = (from p in lstBangchamcong
                           orderby p.Thang descending
                           select
                           new
                           {
                               STT = stt++,
                               p.Mabangchamcong,
                               p.Nam,
                               p.Thang,
                               p.IsLock,
                               p.IsFinish
                           }).ToList();
            rpData.DataSource = lstData;
            rpData.DataBind();
            DiarySystem(18, 5, "Năm " + nam.ToString());
        }
        #endregion

        #region EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditBangchamcong");
        }
        protected void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(int.Parse(cbNam.SelectedValue));
        }
        #endregion

        
    }
}

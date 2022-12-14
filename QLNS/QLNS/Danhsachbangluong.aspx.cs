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
    /// Chức năng: 22
    /// Hành động: Xem (5), Tao (6 - Hoan thanh tinh luong (quyen 8))
    /// </summary>
    public partial class Danhsachbangluong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Danh sách bảng lương";
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
            if (UserRolls.Where(p => p == 7 || p == 8).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
            else
            {
                if (UserRolls.Where(p => p == 8).Count() == 1)
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
            List<int> lstYear = (from p in db.PB_Danhsachbangluongs
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
            var lstBangluong = (from p in db.PB_Danhsachbangluongs
                                   where p.Nam == nam
                                   select
                                     new
                                     {
                                         p.Mabangluong,
                                         p.Nam,
                                         p.Thang,
                                         p.IsLock,
                                         p.IsFinish
                                     }).ToList();
            int stt = 1;
            var lstData = (from p in lstBangluong
                           orderby p.Thang descending
                           select
                           new
                           {
                               STT = stt++,
                               p.Mabangluong,
                               p.Nam,
                               p.Thang,
                               p.IsLock,
                               p.IsFinish
                           }).ToList();
            rpData.DataSource = lstData;
            rpData.DataBind();
            DiarySystem(22, 5, "Năm " + nam);
        }
        #endregion

        #region EventHandler
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            dbLinQDataContext db = new dbLinQDataContext();

            //Nhung bang cham cong da hoan thanh
            var lstDachamcong = (from dschamcong in db.PB_Danhsachchamcongs
                                 where dschamcong.IsFinish == true
                                 select new
                                 {
                                     dschamcong.Nam,
                                     dschamcong.Thang
                                 }).ToList();
            //Nhung bang luong da duoc tao
            var lstDacobangluong = (from dsbangluong in db.PB_Danhsachbangluongs
                                    select new { dsbangluong.Nam, dsbangluong.Thang }
                                    ).ToList();

            //List cac bang luong co the tao
            var lstBangluongcothetao = (from p in lstDachamcong
                                        where !lstDacobangluong.Contains(p)
                                        select p).ToList();

            //List Nam co the tao bang luong
            List<int> lstNamcothetao = (from p in lstBangluongcothetao
                                        select p.Nam).Distinct().ToList();

            //List cac bang luong co the tao > 0 thi co the tao bang luong
            if (lstNamcothetao.Count() > 0)
            {
                Response.Redirect("EditBangluong");
            }
            else
            {
                ltrInfor.Text = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Phải tồn tại bảng chấm công có trạng thái đã hoàn thành và chưa được tạo bảng lương thì mới có thể tạo bảng lương.
                                        </div>
                                    </div>";
            }
        }
        protected void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(int.Parse(cbNam.SelectedValue));
        }
        #endregion
    }
}

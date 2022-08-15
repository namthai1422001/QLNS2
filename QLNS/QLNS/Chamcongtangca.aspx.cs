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
    /// Chức năng: 21
    /// Hành động: Cập nhật (7)
    /// </summary>
    public partial class Chamcongtangca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Chấm công tăng ca";
                loadRole();
                loadcbNam();
                loadcbThang(int.Parse(cbNam.SelectedValue));
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
            if (UserRolls.Where(p => p == 5 || p == 6).Count() == 0)
            {
                Response.Redirect(ResolveUrl("~/DontAllow"));
            }
            else
            {
                if (UserRolls.Where(p => p == 5).Count() == 1)
                {
                    //Co quyen cham cong
                    btnSave.Visible = true;
                    pnlChamcong.Visible = true;
                }
                else
                {
                    //Khong co quyen cham cong;
                    btnSave.Visible = false;
                    pnlChamcong.Visible = false;
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

        //Load du lieu cho Dropdownlist cbNam
        private void loadcbNam()
        {
            cbNam.Items.Clear();

            dbLinQDataContext db = new dbLinQDataContext();
            List<int> lstYear = (from p in db.PB_Danhsachchamcongs
                                 orderby p.Nam
                                 select new { p.Nam }).Distinct().OrderByDescending(p => p.Nam).Select(p => p.Nam).ToList();


            int CurrentYear = DateTime.Now.Year;

            int YearRequest;
            try
            {
                YearRequest = int.Parse(Request.QueryString["y"]);
                if (YearRequest > CurrentYear)
                    YearRequest = CurrentYear;
            }
            catch
            {
                YearRequest = CurrentYear;
            };

            if (lstYear.Count != 0)
            {
                int i = 0;
                foreach (int p in lstYear)
                {
                    cbNam.Items.Add(new ListItem(p.ToString(), p.ToString()));
                    if (p == YearRequest)
                        cbNam.SelectedIndex = i;
                    i++;
                }
            }
            else
            {
                cbNam.Items.Add(new ListItem(CurrentYear.ToString(), CurrentYear.ToString()));
            }
        }

        //Load du lieu cho Dropdownlist cbThang
        private void loadcbThang(int Nam)
        {
            cbThang.Items.Clear();

            dbLinQDataContext db = new dbLinQDataContext();
            List<int> lstMonth = (from p in db.PB_Danhsachchamcongs
                                  where p.Nam == Nam
                                  orderby p.Thang descending
                                  select new { p.Thang }).Select(p => p.Thang).ToList();

            int CurrentMonth = DateTime.Now.Month;

            int MonthRequest;
            try
            {
                MonthRequest = int.Parse(Request.QueryString["m"]);
                if (MonthRequest < 0 || MonthRequest > 12)
                    MonthRequest = CurrentMonth;
            }
            catch
            {
                MonthRequest = CurrentMonth;
            };

            if (lstMonth.Count != 0)
            {
                int i = 0;
                foreach (int p in lstMonth)
                {
                    cbThang.Items.Add(new ListItem(p.ToString(), p.ToString()));
                    if (p == MonthRequest)
                        cbThang.SelectedIndex = i;
                    i++;
                }
            }
            else
            {
                cbThang.Items.Add(new ListItem(CurrentMonth.ToString(), CurrentMonth.ToString()));
            }
        }

        //Load du lieu cho Repeater
        private void loadData()
        {
            //PageSize: So ban ghi tren moi trang; TotalRow: Tong so ban ghi
            int PageSize = 30;
            int TotalRow = 0;
            int CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                if (int.Parse(Request.QueryString["p"]) > 0)
                    CurrentPage = int.Parse(Request.QueryString["p"]);
            }
            dbLinQDataContext db = new dbLinQDataContext();
            var lstNhanvien = (from nvien in db.PB_Nhanviens
                               join cctangca in db.PB_Chamtangcas
                               on nvien.MaNV equals cctangca.MaNV
                               join dschamcong in db.PB_Danhsachchamcongs
                               on cctangca.Mabangchamcong equals dschamcong.Mabangchamcong
                               where dschamcong.Nam == int.Parse(cbNam.SelectedValue) && dschamcong.Thang == int.Parse(cbThang.SelectedValue)
                               select new
                               {
                                   nvien.MaNV,
                                   nvien.HoNV,
                                   nvien.TenNV,
                                   cctangca.Mabangchamcong,
                                   cctangca.TCthuong,
                                   cctangca.TCchunhat,
                                   cctangca.TCnghile
                               }).ToList();
            if (lstNhanvien.Count == 0)
            {
                hplFirstPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
                hplNextPage.NavigateUrl = hplFirstPage.NavigateUrl;
                hplPreviousPage.NavigateUrl = hplFirstPage.NavigateUrl;
                hplLastPage.NavigateUrl = hplFirstPage.NavigateUrl;
                return;
            }
            int stt = 1;
            var lstData = (from p in lstNhanvien
                           orderby p.TenNV
                           select
                           new
                           {
                               STT = stt++,
                               p.MaNV,
                               p.HoNV,
                               p.TenNV,
                               p.Mabangchamcong,
                               p.TCthuong,
                               p.TCchunhat,
                               p.TCnghile
                           }).ToList();
            TotalRow = lstData.Count();
            lblTotalRowCount.Text = TotalRow.ToString();

            //Goi ham gan du lieu cho div Pagination
            GetPagination(CurrentPage, PageSize, TotalRow);


            //Lay trang hien tai de hien thi len danh sach
            var lstPaging = lstData.Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
            lblCurrent.Text = lstPaging.Count.ToString();

            //Gan URL cho FirtPage va LastPage
            hplFirstPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
            hplLastPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, (TotalRow / PageSize + ((TotalRow % PageSize == 0) ? 0 : 1)));

            //Gan du lieu cho Repeater
            rpData.DataSource = lstPaging;
            rpData.DataBind();

            //Trang thai bang cham cong
            var objBangchamcong = (from p in db.PB_Danhsachchamcongs
                                   where p.Nam == int.Parse(cbNam.SelectedValue) && p.Thang == int.Parse(cbThang.SelectedValue)
                                   select new { p.IsLock, p.IsFinish }).FirstOrDefault();

            if (objBangchamcong != null)
            {
                if (objBangchamcong.IsFinish)
                {
                    ltrStatus.Text = "Đã hoàn thành";
                    imgStatus.ImageUrl = "~/images/icons/finished.png";
                    imgStatus.AlternateText = "Đã hoàn thành";
                    btnSave.Visible = false;

                    txtThuong.Enabled = false;
                    txtNghile.Enabled = false;
                    txtChunhat.Enabled = false;
                }
                else
                    if (objBangchamcong.IsLock)
                    {
                        ltrStatus.Text = "Đã khóa";
                        imgStatus.ImageUrl = "~/images/icons/lock.png";
                        imgStatus.AlternateText = "Đã khóa";
                        btnSave.Visible = false;

                        txtThuong.Enabled = false;
                        txtNghile.Enabled = false;
                        txtChunhat.Enabled = false;
                    }
                    else
                    {
                        ltrStatus.Text = "Không khóa";
                        imgStatus.ImageUrl = "~/images/icons/unlock.png";
                        imgStatus.AlternateText = "Không khóa";

                        btnSave.Visible = true;

                        txtThuong.Enabled = true;
                        txtNghile.Enabled = true;
                        txtChunhat.Enabled = true;
                    }

                //Load thong tin ve bang cham cong hien tai
                var objThongtin = (from chcongtangca in db.PB_Chamtangcas
                                   where chcongtangca.Mabangchamcong == db.PB_Danhsachchamcongs.Where(p => p.Nam == int.Parse(cbNam.SelectedValue) && p.Thang == int.Parse(cbThang.SelectedValue)).First().Mabangchamcong
                                   group new
                                   {
                                       chcongtangca.TCthuong,
                                       chcongtangca.TCchunhat,
                                       chcongtangca.TCnghile
                                   } by chcongtangca.Mabangchamcong into g
                                   select new
                                   {
                                       g.Key,
                                       Tonggiotangcathuong = g.Sum(p => p.TCthuong),
                                       Tonggiotangcachunhat = g.Sum(p => p.TCchunhat),
                                       Tonggiotangcanghile = g.Sum(p => p.TCnghile)
                                   }).FirstOrDefault();

                ltrTonggiotangcathuong.Text = objThongtin.Tonggiotangcathuong.ToString();
                ltrTonggiotangcachunhat.Text = objThongtin.Tonggiotangcachunhat.ToString();
                ltrTonggiotangcanghile.Text = objThongtin.Tonggiotangcanghile.ToString();
            }
        }

        /// <summary>
        /// Lay danh sach trang
        /// </summary>
        /// <param name="CurrentPage">Trang hien tai</param>
        /// <param name="PageSize">So ban ghi tren moi trang</param>
        /// <param name="TotalRowCount">Tong so ban ghi</param>
        private void GetPagination(int CurrentPage, int PageSize, int TotalRowCount)
        {
            //So trang can hien thi tren danh sach
            int NumberOf = 5;
            int Between = NumberOf / 2;
            List<int> listPage = new List<int>();
            int TotalOfPage = ((TotalRowCount % PageSize) == 0) ? TotalRowCount / PageSize : (TotalRowCount / PageSize + 1);

            if (TotalOfPage >= NumberOf)
            {
                int i;
                for (i = 1; (i < TotalOfPage - Between - 1) && i != CurrentPage; i++) ;

                for (int j = i - 1; (j >= CurrentPage - Between) && (j > 0); j--)
                {
                    listPage.Add(j);
                }
                listPage.Add(i);
                for (int j = i + 1; (listPage.Count < NumberOf) && (j <= TotalOfPage); j++)
                {
                    listPage.Add(j);
                }
                if (listPage.Count < NumberOf)
                {
                    int k = listPage.Min();
                    for (int j = k - 1; listPage.Count < NumberOf; j--)
                        listPage.Add(j);
                }
            }
            else
            {
                for (int i = 1; i <= TotalOfPage; i++)
                {
                    listPage.Add(i);
                }
            }

            //Gan du lieu cho Repeater Pagination
            rpPagination.DataSource = listPage.OrderBy(p => p);
            rpPagination.DataBind();
        }

        protected void rpPagination_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //PageSize: So ban ghi tren moi trang; strValue: Item hien tai;
                int PageSize = 30;
                string strValue = e.Item.DataItem.ToString();
                int CurrentPage;
                try
                {
                    CurrentPage = int.Parse(Request.QueryString["p"]);
                    if (CurrentPage <= 0)
                        CurrentPage = 1;
                }
                catch
                {
                    CurrentPage = 1;
                }

                HyperLink hplPage = (HyperLink)e.Item.FindControl("hplPage");
                hplPage.Text = strValue;
                hplPage.ToolTip = strValue;
                hplPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, strValue);


                if (strValue == CurrentPage.ToString())
                {
                    hplPage.Attributes.Add("class", "number current");
                    hplPage.Attributes.Add("onclick", "return false");
                    if (CurrentPage > 1)
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, CurrentPage - 1);
                    }
                    else
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
                        hplPreviousPage.Attributes.Add("onclick", "return false");
                    }
                    if (CurrentPage < (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)))
                    {
                        hplNextPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, CurrentPage + 1);
                    }
                    else
                    {
                        hplNextPage.NavigateUrl = string.Format("Chamcongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)));
                        hplNextPage.Attributes.Add("onclick", "return false");
                    }
                }
                else
                {
                    hplPage.Attributes.Add("class", "number");
                }
            }
        }
        #endregion

        #region EventHandler
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string strInfor;
                if (!string.IsNullOrEmpty(Request["chkMaNV"]))
                {
                    dbLinQDataContext db = new dbLinQDataContext();
                    var objBangchamcong = (from p in db.PB_Danhsachchamcongs
                                           where p.Nam == int.Parse(cbNam.SelectedValue) && p.Thang == int.Parse(cbThang.SelectedValue)
                                           select new { p.Mabangchamcong, p.IsLock, p.IsFinish, p.Nam, p.Thang }).FirstOrDefault();
                    if (objBangchamcong != null)
                    {
                        if (objBangchamcong.IsFinish)
                        {
                            strInfor = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Bảng chấm công này đã hoàn thành trước đó.<br />
                                            Không thể chỉnh sửa.<br />
                                            Vui lòng nhấn phím F5 và thử lại sau.
                                        </div>
                                    </div>";
                        }
                        else
                            if (objBangchamcong.IsLock)
                            {
                                strInfor = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Bảng chấm công này đã bị khóa trước đó.<br />
                                            Không thể chỉnh sửa.
                                            Vui lòng nhấn phím F5 và thử lại sau.
                                        </div>
                                    </div>";
                            }
                            else
                            {
                                //Thuc hien update Chamcongtangca cho nhan vien
                                Guid machamcong = objBangchamcong.Mabangchamcong;
                                int Sogiotangcathuong = int.Parse(txtThuong.Text);
                                int Sogiotangcachunhat = int.Parse(txtChunhat.Text);
                                int Sogiotangcanghile = int.Parse(txtNghile.Text);
                                List<string> lstMaNV = Request["chkMaNV"].ToString().Split(',').ToList();
                                Guid CreatedByUser = new Guid(Session["UserID"].ToString());
                                DateTime CreatedByDate = DateTime.Now;

                                try
                                {
                                    foreach (string p in lstMaNV)
                                    {
                                        PB_Chamtangca objData = (from cc in db.PB_Chamtangcas
                                                                       where cc.Mabangchamcong == machamcong
                                                                       && cc.MaNV == p.ToString()
                                                                       select cc).FirstOrDefault();
                                        objData.TCthuong = Sogiotangcathuong;
                                        objData.TCchunhat = Sogiotangcachunhat;
                                        objData.TCnghile = Sogiotangcanghile;
                                        objData.CreatedByUser = CreatedByUser;
                                        objData.CreatedByDate = CreatedByDate;

                                        db.SubmitChanges();
                                    }
                                    strInfor = @"<div class='notification success png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Lưu thành công.
                                        </div>
                                    </div>";
                                    DiarySystem(21, 7, objBangchamcong.Thang + "/" + objBangchamcong.Nam);
                                    loadData();
                                }
                                catch
                                {
                                    strInfor = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Lỗi kết nối.
                                        </div>
                                    </div>";
                                };
                            }
                    }
                    else
                    {
                        strInfor = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Bảng chấm công này không tồn tại.<br />
                                            Vui lòng nhấn phím F5 và thử lại sau.
                                        </div>
                                    </div>";
                    }

                }
                else
                {
                    strInfor = @"<div class='notification error png_bg'>
                                        <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                        <div>
                                            Vui lòng chọn nhân viên trước khi nhấn lưu.
                                        </div>
                                    </div>";
                }
                ltrInfor.Text = strInfor;
            }
        }

        protected void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcbThang(int.Parse(cbNam.SelectedValue));
            loadData();
        }
        protected void cbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }
        #endregion
    }
}

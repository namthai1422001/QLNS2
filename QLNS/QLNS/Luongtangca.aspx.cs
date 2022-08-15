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
    /// Chức năng: 43
    /// Hành động: Xem (5)
    /// </summary>
    public partial class Luongtangca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Lương tăng ca";
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
            if (UserRolls.Where(p => p == 7 || p == 8).Count() == 0)
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

        //Load du lieu cho Dropdownlist cbNam
        private void loadcbNam()
        {
            cbNam.Items.Clear();

            dbLinQDataContext db = new dbLinQDataContext();
            List<int> lstYear = (from p in db.PB_Danhsachbangluongs
                                 orderby p.Nam descending
                                 select new { p.Nam }).Distinct().Select(p => p.Nam).ToList();


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
            List<int> lstMonth = (from p in db.PB_Danhsachbangluongs
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
            Guid Mabangluong = (from p in db.PB_Danhsachbangluongs
                                where p.Nam == int.Parse(cbNam.SelectedValue)
                                    && p.Thang == int.Parse(cbThang.SelectedValue)
                                select p.Mabangluong).FirstOrDefault();

            var lstNhanvien = (from bangluong in db.sp_PB_Luongtangca_Select_All(Mabangluong)
                               join nvien in db.PB_Nhanviens
                               on bangluong.MaNV equals nvien.MaNV
                               select new
                               {
                                   bangluong.Mabangluong,
                                   bangluong.MaNV,
                                   nvien.HoNV,
                                   nvien.TenNV,
                                   bangluong.LuongGio,
                                   bangluong.Tientangcathuong,
                                   bangluong.Sotangcathuong,
                                   bangluong.Tientangcachunhat,
                                   bangluong.Sotangcachunhat,
                                   bangluong.Tientangcanghile,
                                   bangluong.Sotangcale,
                                   bangluong.Tongluongtangca
                               }).ToList();
            if (lstNhanvien.Count == 0)
            {
                hplFirstPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
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
                               p.Mabangluong,
                               p.MaNV,
                               p.HoNV,
                               p.TenNV,
                               LuongGio = p.LuongGio.ToString("#,##0"),
                               Tientangcathuong = p.Tientangcathuong.Value.ToString("#,##0"),
                               p.Sotangcathuong,
                               Tientangcachunhat = p.Tientangcachunhat.Value.ToString("#,##0"),
                               p.Sotangcachunhat,
                               Tientangcanghile = p.Tientangcanghile.Value.ToString("#,##0"),
                               p.Sotangcale,
                               Tongluongtangca = p.Tongluongtangca.Value.ToString("#,##0")
                           }).ToList();
            TotalRow = lstData.Count();
            lblTotalRowCount.Text = TotalRow.ToString();

            //Goi ham gan du lieu cho div Pagination
            GetPagination(CurrentPage, PageSize, TotalRow);


            //Lay trang hien tai de hien thi len danh sach
            var lstPaging = lstData.Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
            lblCurrent.Text = lstPaging.Count.ToString();

            //Gan URL cho FirtPage va LastPage
            hplFirstPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
            hplLastPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, (TotalRow / PageSize + ((TotalRow % PageSize == 0) ? 0 : 1)));

            //Gan du lieu cho Repeater
            rpData.DataSource = lstPaging;
            rpData.DataBind();

            DiarySystem(43, 5, "Tháng " + cbThang.SelectedValue + "/" + cbNam.SelectedValue);

            //Trang thai bang luong
            var objBangluongtangca = (from p in db.PB_Danhsachbangluongs
                                    where p.Nam == int.Parse(cbNam.SelectedValue) && p.Thang == int.Parse(cbThang.SelectedValue)
                                    select new { p.IsLock, p.IsFinish }).FirstOrDefault();

            if (objBangluongtangca != null)
            {
                if (objBangluongtangca.IsFinish)
                {
                    ltrStatus.Text = "Đã hoàn thành";
                    imgStatus.ImageUrl = "~/images/icons/finished.png";
                    imgStatus.AlternateText = "Đã hoàn thành";
                    btnTinhluong.Visible = false;
                }
                else
                    if (objBangluongtangca.IsLock)
                    {
                        ltrStatus.Text = "Đã khóa";
                        imgStatus.ImageUrl = "~/images/icons/lock.png";
                        imgStatus.AlternateText = "Đã khóa";
                        btnTinhluong.Visible = false;
                    }
                    else
                    {
                        ltrStatus.Text = "Không khóa";
                        imgStatus.ImageUrl = "~/images/icons/unlock.png";
                        imgStatus.AlternateText = "Không khóa";
                    }
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
                hplPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, strValue);


                if (strValue == CurrentPage.ToString())
                {
                    hplPage.Attributes.Add("class", "number current");
                    hplPage.Attributes.Add("onclick", "return false");
                    if (CurrentPage > 1)
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, CurrentPage - 1);
                    }
                    else
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
                        hplPreviousPage.Attributes.Add("onclick", "return false");
                    }
                    if (CurrentPage < (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)))
                    {
                        hplNextPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, CurrentPage + 1);
                    }
                    else
                    {
                        hplNextPage.NavigateUrl = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)));
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
        protected void btnTinhluong_Click(object sender, EventArgs e)
        {
            string _query = string.Format("Luongtangca.aspx?y={0}&m={1}&p={2}", cbNam.SelectedValue, cbThang.SelectedValue, 1);
            Response.Redirect(_query);
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

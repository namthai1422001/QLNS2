using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace QLNS.Admin
{
    /// <summary>
    /// Quyền: 1
    /// Chức năng: 38
    /// Hành động: Xem (5), Xóa (8)
    /// </summary>
    public partial class Nhatkyhethong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Nhật ký hệ thống";
                loadRole();
                loadNgay();
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
            if (UserRolls.Where(p => p == 1).Count() == 0)
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

        //Load Ngay thang mac dinh
        private void loadNgay()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fd"]) && !string.IsNullOrEmpty(Request.QueryString["fm"])
                    && !string.IsNullOrEmpty(Request.QueryString["fy"]) && !string.IsNullOrEmpty(Request.QueryString["td"])
                    && !string.IsNullOrEmpty(Request.QueryString["tm"])
                    && !string.IsNullOrEmpty(Request.QueryString["ty"]))
            {
                try
                {
                    DateTime fromD = DateTime.Parse(Request.QueryString["fd"] + "/" + Request.QueryString["fm"] + "/" + Request.QueryString["fy"], new CultureInfo("vi-vn"));
                    DateTime ToD = DateTime.Parse(Request.QueryString["td"] + "/" + Request.QueryString["tm"] + "/" + Request.QueryString["ty"], new CultureInfo("vi-vn"));
                    if (ToD >= fromD)
                    {
                        txtFromDate.Value = fromD.ToString("dd/MM/yyyy");
                        txtToDate.Value = ToD.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Vui lòng truy cập trang đúng cách'); window.location = '../Index';", true);
                }
            }
            else
            {
                DateTime ToD = DateTime.Now;
                DateTime fromD = ToD.AddDays(-30);
                Response.Redirect(string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, 1));
            }
        }

        //Load du lieu cho Repeater
        private void loadData()
        {
            //PageSize: So ban ghi tren moi trang; TotalRow: Tong so ban ghi
            int PageSize = 50;
            int TotalRow = 0;
            int CurrentPage = 1;
            try
            {
                if (int.Parse(Request.QueryString["p"]) > 0)
                    CurrentPage = int.Parse(Request.QueryString["p"]);
            }
            catch
            {
                CurrentPage = 1;
            }

            DateTime fromD = DateTime.Parse(Request.QueryString["fd"] + "/" + Request.QueryString["fm"] + "/" + Request.QueryString["fy"], new CultureInfo("vi-vn"));
            DateTime ToD = DateTime.Parse(Request.QueryString["td"] + "/" + Request.QueryString["tm"] + "/" + Request.QueryString["ty"], new CultureInfo("vi-vn")).AddDays(1);

            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                var lstNhatky = (from nky in db.SYS_Nhatkyhethongs
                                 join cnang in db.SYS_Chucnangs
                                 on nky.Chucnang equals cnang.FunctionID
                                 join hdong in db.SYS_Hanhdongs
                                 on nky.Hanhdong equals hdong.ActivityID
                                 where nky.Thoigian >= fromD && nky.Thoigian <= ToD
                                 select new
                                 {
                                     nky.Username,
                                     nky.Thoigian,
                                     Tenchucnang = cnang.Name,
                                     Tenhanhdong = hdong.ActivityName,
                                     nky.Doituong
                                 }).ToList();
                if (lstNhatky.Count == 0)
                {
                    hplFirstPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, 1);
                    hplNextPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    hplPreviousPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    hplLastPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    return;
                }

                int stt = 1;
                var lstData = (from p in lstNhatky
                               orderby p.Thoigian descending
                               select
                               new
                               {
                                   STT = stt++,
                                   p.Username,
                                   p.Thoigian,
                                   p.Tenchucnang,
                                   p.Tenhanhdong,
                                   p.Doituong
                               }).ToList();
                TotalRow = lstData.Count();
                lblTotalRowCount.Text = TotalRow.ToString();

                //Goi ham gan du lieu cho div Pagination
                GetPagination(CurrentPage, PageSize, TotalRow);


                //Lay trang hien tai de hien thi len danh sach
                var lstPaging = lstData.Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
                lblCurrent.Text = lstPaging.Count.ToString();

                //Gan URL cho FirtPage va LastPage
                hplFirstPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, 1);
                hplLastPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, (TotalRow / PageSize + ((TotalRow % PageSize == 0) ? 0 : 1)));

                //Gan du lieu cho Repeater
                rpData.DataSource = lstPaging;
                rpData.DataBind();

                DiarySystem(38, 5, "");
            }
            catch
            {
                Response.Redirect("../Login");
            };
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
                int PageSize = 50;
                string strValue = e.Item.DataItem.ToString();
                int CurrentPage = 1;
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
                DateTime fromD = DateTime.Parse(Request.QueryString["fd"] + "/" + Request.QueryString["fm"] + "/" + Request.QueryString["fy"], new CultureInfo("vi-vn"));
                DateTime ToD = DateTime.Parse(Request.QueryString["td"] + "/" + Request.QueryString["tm"] + "/" + Request.QueryString["ty"], new CultureInfo("vi-vn"));

                HyperLink hplPage = (HyperLink)e.Item.FindControl("hplPage");
                hplPage.Text = strValue;
                hplPage.ToolTip = strValue;
                hplPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, strValue);


                if (strValue == CurrentPage.ToString())
                {
                    hplPage.Attributes.Add("class", "number current");
                    hplPage.Attributes.Add("onclick", "return false");
                    if (CurrentPage > 1)
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, CurrentPage - 1);
                    }
                    else
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, 1);
                        hplPreviousPage.Attributes.Add("onclick", "return false");
                    }
                    if (CurrentPage < (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)))
                    {
                        hplNextPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, CurrentPage + 1);
                    }
                    else
                    {
                        hplNextPage.NavigateUrl = string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)));
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

        protected void btnXem_Click(object sender, EventArgs e)
        {
            DateTime fromD = DateTime.Parse(txtFromDate.Value,new CultureInfo("vi-vn"));
            DateTime ToD = DateTime.Parse(txtToDate.Value,new CultureInfo("vi-vn"));
            Response.Redirect(string.Format("Nhatkyhethong@{0}@{1}@{2}@{3}@{4}@{5}@{6}", fromD.Day, fromD.Month, fromD.Year, ToD.Day, ToD.Month, ToD.Year, 1));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime fromD = DateTime.Parse(txtFromDate.Value, new CultureInfo("vi-vn"));
            DateTime ToD = DateTime.Parse(txtToDate.Value, new CultureInfo("vi-vn")).AddDays(1);

            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                string Username = db.SYS_Nguoidungs.Where(p => p.ID == new Guid(Session["UserID"].ToString())).FirstOrDefault().Username;

                //Thuc thi proc Xoa nhat ky
                db.sp_SYS_Nhatkyhethong_Delete(Username, fromD, ToD);
                ltrInfo.Text = @"<div class='notification information png_bg'>
                                    <a href='#' class='close'><img src='../images/icons/cross_grey_small.png' title='Close this notification' alt='close' /></a>
                                    <div>
                                        Xóa nhật ký hệ thống thành công.
                                    </div>
                                </div>";
                loadData();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Lỗi kết nối'); window.location = '../Index';", true);
            };
        }
        #endregion
    }
}

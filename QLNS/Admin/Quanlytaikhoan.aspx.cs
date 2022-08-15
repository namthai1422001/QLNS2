using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Admin
{
    /// <summary>
    /// Quyền: 2
    /// Chức năng: 2
    /// Hành động: Reset mật khẩu (4), Xem (5)
    /// </summary>
    public partial class Quanlytaikhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Quản lý người dùng";
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
            if (UserRolls.Where(p => p == 2).Count() == 0)
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
        
        //Load du lieu cho Repeater
        private void loadData()
        {
            //PageSize: So ban ghi tren moi trang; TotalRow: Tong so ban ghi
            int PageSize = 10;
            int TotalRow = 0;
            int CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                if (int.Parse(Request.QueryString["p"]) > 0)
                    CurrentPage = int.Parse(Request.QueryString["p"]);
            }
            try
            {
                dbLinQDataContext db = new dbLinQDataContext();
                var lstNguoidung = (from ngdung in db.SYS_Nguoidungs
                                    where ngdung.IsDelete == false
                                         && ngdung.ID != new Guid(Session["UserID"].ToString())
                                         && ngdung.CreatedByUser != "-1"
                                    select new
                                    {
                                        ngdung.Username,
                                        ngdung.Fullname,
                                        ngdung.Email,
                                        ngdung.NumberOfLogin,
                                        ngdung.LaterLogin,
                                        ngdung.IsLock,
                                        ngdung.IsSuper,
                                        RollAvailable = ((db.SYS_PhanQuyens.Where(p => p.UserID == ngdung.ID).Count() > 0) ? true : false),
                                        ngdung.GhiChu
                                    }).ToList();
                if (lstNguoidung.Count == 0)
                {
                    hplFirstPage.NavigateUrl = string.Format("Quanlytaikhoan@{0}", 1);
                    hplNextPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    hplPreviousPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    hplLastPage.NavigateUrl = hplFirstPage.NavigateUrl;
                    return;
                }
                int stt = 1;
                var lstData = (from p in lstNguoidung
                               orderby p.Username ascending
                               select
                               new
                               {
                                   STT = stt++,
                                   p.Username,
                                   p.Fullname,
                                   p.Email,
                                   p.NumberOfLogin,
                                   p.LaterLogin,
                                   p.IsLock,
                                   p.IsSuper,
                                   p.RollAvailable,
                                   p.GhiChu
                               }).ToList();
                TotalRow = lstData.Count();
                lblTotalRowCount.Text = TotalRow.ToString();

                //Goi ham gan du lieu cho div Pagination
                GetPagination(CurrentPage, PageSize, TotalRow);


                //Lay trang hien tai de hien thi len danh sach
                var lstPaging = lstData.Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
                lblCurrent.Text = lstPaging.Count.ToString();

                //Gan URL cho FirtPage va LastPage
                hplFirstPage.NavigateUrl = string.Format("Quanlytaikhoan@{0}", 1);
                hplLastPage.NavigateUrl = string.Format("Quanlytaikhoan@{0}", (TotalRow / PageSize + ((TotalRow % PageSize == 0) ? 0 : 1)));

                //Gan du lieu cho Repeater
                rpData.DataSource = lstPaging;
                rpData.DataBind();

                DiarySystem(2, 5, "");
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
                int PageSize = 10;
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
                hplPage.NavigateUrl = string.Format("Quanlynguoidung@{0}", strValue);


                if (strValue == CurrentPage.ToString())
                {
                    hplPage.Attributes.Add("class", "number current");
                    hplPage.Attributes.Add("onclick", "return false");
                    if (CurrentPage > 1)
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Quanlynguoidung@{0}", CurrentPage - 1);
                    }
                    else
                    {
                        hplPreviousPage.NavigateUrl = string.Format("Quanlynguoidung@{0}", 1);
                        hplPreviousPage.Attributes.Add("onclick", "return false");
                    }
                    if (CurrentPage < (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)))
                    {
                        hplNextPage.NavigateUrl = string.Format("Quanlynguoidung@{0}", CurrentPage + 1);
                    }
                    else
                    {
                        hplNextPage.NavigateUrl = string.Format("Quanlynguoidung@{0}", (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)));
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

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //{ STT = 1, Username = nguyenhoainam, Fullname = Nguyễn Hoài Nam, Email = nguyenhoainam@gmail.com,
                // NumberOfLogin = 6, LaterLogin = 5/14/2012 11:48:01 AM, IsLock = False, IsSuper = False,
                // RollAvailable = True, GhiChu =  }


                bool IsSuper = bool.Parse(e.Item.DataItem.ToString().Split(',').Skip(7).Take(1).FirstOrDefault().Split('=').Skip(1).FirstOrDefault());
                if (IsSuper)
                {
                    Literal ltrSuper = (Literal)e.Item.FindControl("ltrSuper");
                    ltrSuper.Text = @"<img alt='Là Supper'
                                            title='Là Supper'
                                            src='../images/icons/super.png' />";
                }

                bool RollAvailable = bool.Parse(e.Item.DataItem.ToString().Split(',').Skip(8).Take(1).FirstOrDefault().Split('=').Skip(1).FirstOrDefault());
                if (RollAvailable)
                {
                    Literal ltrRoll = (Literal)e.Item.FindControl("ltrRoll");
                    ltrRoll.Text = @"<img alt='Là Supper'
                                        title='Là Supper'
                                        src='../images/icons/tick_circle.png' />";
                }
            }
        }

        #endregion
    }
}

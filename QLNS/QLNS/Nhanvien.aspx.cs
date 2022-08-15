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
    /// Chức năng: 5
    /// Hành động: Xem
    /// </summary>
    public partial class Nhanvien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Danh sách nhân viên";
                loadRole();
                loaddropdown();
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
        //Load du lieu cho cac Dropdownlist
        private void loaddropdown()
        {
            //Load du lieu cho Dropdownlist cbPhong
            dbLinQDataContext db = new dbLinQDataContext();

            var lstPhong = (from p in db.PB_Phongbans
                            select new
                            {
                                p.Maphong,
                                p.Tenphong
                            }).ToList();

            cbPhong.Items.Clear();
            cbPhong.Items.Add(new ListItem("-- Tất cả --", "0"));
            foreach (var p in lstPhong)
            {
                cbPhong.Items.Add(new ListItem(p.Tenphong, p.Maphong.ToString()));
            }
            int i;
            int dem = cbPhong.Items.Count;
            int phongid;
            try
            {
                phongid = int.Parse(Request.QueryString["d"]);
                if (phongid < 0)
                    phongid = 0;
            }
            catch
            {
                phongid = 0;
            }
            for (i = 0; i < dem; i++)
            {
                if (cbPhong.Items[i].Value.ToString() == phongid.ToString())
                {
                    cbPhong.SelectedIndex = i;
                    break;
                }
            }

            ////Ma phong khong hop le
            //if (i == dem)
            //    cbPhong.SelectedIndex = 0;

            //cbTrangthainhanvien
            cbTrangthainhanvien.Items.Clear();
            cbTrangthainhanvien.Items.Add(new ListItem("Đang làm việc", "1"));
            cbTrangthainhanvien.Items.Add(new ListItem("Đang thử việc", "2"));
            cbTrangthainhanvien.Items.Add(new ListItem("Tạm ngưng việc", "3"));
            cbTrangthainhanvien.Items.Add(new ListItem("Đã nghỉ việc", "4"));

            int trangthaiid;
            try
            {
                trangthaiid = int.Parse(Request.QueryString["s"]);
                if (trangthaiid < 1)
                    trangthaiid = 1;
            }
            catch
            {
                trangthaiid = 1;
            }
            dem = cbTrangthainhanvien.Items.Count;
            for (i = 0; i < dem; i++)
            {
                if (cbTrangthainhanvien.Items[i].Value.ToString() == trangthaiid.ToString())
                {
                    cbTrangthainhanvien.SelectedIndex = i;
                    break;
                }
            }

            ////Ma phong khong hop le
            //if (i == dem)
            //    cbTrangthainhanvien.SelectedIndex = 0;
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
            var lstNhanvien = (from nvien in db.View_PB_Nhanvien_Thongtincobans
                               where nvien.Tinhtrang == byte.Parse(cbTrangthainhanvien.SelectedValue)
                                    && ((cbPhong.SelectedValue != "0") ? nvien.Maphong == int.Parse(cbPhong.SelectedValue) : (nvien.Maphong != 0 || nvien.Maphong == null))
                               select new
                               {
                                   nvien.MaNV,
                                   nvien.HoNV,
                                   nvien.TenNV,
                                   nvien.Nu,
                                   nvien.Ngaysinh,
                                   nvien.Tenphong,
                                   nvien.Tenchucvu,
                                   nvien.TenToNhom,
                                   nvien.Tencongviec
                               }).ToList();
            if (lstNhanvien.Count == 0)
            {
                hplFirstPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, 1);
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
                               p.Nu,
                               p.Ngaysinh,
                               p.Tenphong,
                               p.Tenchucvu,
                               p.TenToNhom,
                               p.Tencongviec
                           }).ToList();
            TotalRow = lstData.Count();
            lblTotalRowCount.Text = TotalRow.ToString();

            //Goi ham gan du lieu cho div Pagination
            GetPagination(CurrentPage, PageSize, TotalRow);


            //Lay trang hien tai de hien thi len danh sach
            var lstPaging = lstData.Skip(PageSize * (CurrentPage - 1)).Take(PageSize).ToList();
            lblCurrent.Text = lstPaging.Count.ToString();

            //Gan URL cho FirtPage va LastPage
            hplFirstPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, 1);
            hplLastPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, (TotalRow / PageSize + ((TotalRow % PageSize == 0) ? 0 : 1)));

            //Gan du lieu cho Repeater
            rpData.DataSource = lstPaging;
            rpData.DataBind();

            DiarySystem(5, 5, "");
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
                hplPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, strValue);


                if (strValue == CurrentPage.ToString())
                {
                    hplPage.Attributes.Add("class", "number current");
                    hplPage.Attributes.Add("onclick", "return false");
                    if (CurrentPage > 1)
                    {
                        hplPreviousPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, CurrentPage - 1);
                    }
                    else
                    {
                        hplPreviousPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, 1);
                        hplPreviousPage.Attributes.Add("onclick", "return false");
                    }
                    if (CurrentPage < (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)))
                    {
                        hplNextPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, CurrentPage + 1);
                    }
                    else
                    {
                        hplNextPage.NavigateUrl = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, (int.Parse(lblTotalRowCount.Text) / PageSize + ((int.Parse(lblTotalRowCount.Text) % PageSize == 0) ? 0 : 1)));
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
        protected void cbTrangthainhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _query = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, 1);
            Response.Redirect(_query);
            //loadData();
        }

        protected void cbPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _query = string.Format("DanhsachNhanvien@s={0}@d={1}@p={2}", cbTrangthainhanvien.SelectedValue, cbPhong.SelectedValue, 1);
            Response.Redirect(_query);
        }
        #endregion

    }
}

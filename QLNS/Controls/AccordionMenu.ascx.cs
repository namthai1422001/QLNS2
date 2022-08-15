using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Controls
{
    public partial class AccordionMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadMenu();
            }
        }

        /*
         * 1. Nhật ký hệ thống
         * 2. Quản trị hệ thống
         * 3. Cài đặt
         * 4. Quản lý nhân sự
         * 5. Chấm công
         * 6. Hoàn thành chấm công
         * 7. Tính lương
         * 8. Hoàn thành tính lương
         * 9. Cơ cấu - tổ chức
         * 10. Bảng tham số
         * 11. Giải trí
         * 12. Reset mật khẩu
         * */
        private void loadMenu()
        {
            if (Session["UserRolls"] == null)
            {
                Response.Redirect(ResolveUrl("~/Login"));
            }

            List<int> UserRolls = Session["UserRolls"] as List<int>;

            bool quyennhansu = (UserRolls.Where(p => p == 4).Count() == 1) ? true : false;
            bool quyenchamcong = (UserRolls.Where(p => p == 5 || p == 6).Count() >= 1) ? true : false;
            bool quyentinhluong = (UserRolls.Where(p => p == 7 || p == 8).Count() >= 1) ? true : false;
            bool quyenquantrihethong = (UserRolls.Where(p => p == 2).Count() == 1) ? true : false;
            bool quyennhatkyhethong = (UserRolls.Where(p => p == 1).Count() == 1) ? true : false;
            //bool quyencaidat = (UserRolls.Where(p => p == 3).Count() == 1) ? true : false;
            bool quyengiaitri = (UserRolls.Where(p => p == 11).Count() == 1) ? true : false;


            string menu_nav = @"<ul id='main-nav'>  <!-- Accordion Menu -->
                                        <li id='index-nav'>
	                                        <a href='" + ResolveUrl("~/Index") + @"' class='nav-top-item no-submenu'>
		                                        Trang chủ
	                                        </a>       
                                        </li>";

            if (quyennhansu)
                menu_nav += @"<li id='nhansu-nav'> 
                                    <a href='#' class='nav-top-item'>
                                        Nhân viên
                                    </a>
                                    <ul>
                                        <li><a id='DanhsachNhanvien-nav' href='" + ResolveUrl("~/QLNS/DanhsachNhanvien") + @"'>Danh sách nhân viên</a></li>
                                        <li><a id='Dicongtac-nav' href='" + ResolveUrl("~/QLNS/Dicongtac") + @"'>Danh sách đi công tác</a></li>
                                        <li><a id='Khenthuong-nav' href='" + ResolveUrl("~/QLNS/Khenthuong") + @"'>Danh sách khen thưởng</a></li>
                                        <li><a id='Kyluat-nav' href='" + ResolveUrl("~/QLNS/Kyluat") + @"'>Danh sách kỷ luật</a></li>
                                        <li><a id='Tamung-nav' href='" + ResolveUrl("~/QLNS/Tamung") + @"'>Danh sách tạm ứng</a></li>
                                    </ul>
                                </li>";
            if (quyenchamcong)
                menu_nav += @"<li id='chamcong-nav'> 
                                    <a href='#' class='nav-top-item'>
                                        Chấm công
                                    </a>
                                    <ul>
                                        <li><a id='Danhsachbangchamcong-nav' href='" + ResolveUrl("~/QLNS/Danhsachbangchamcong") + @"'>Danh sách bảng chấm công</a></li>
                                        <li><a id='Chamcongngay-nav' href='" + ResolveUrl("~/QLNS/Chamcongngay.aspx") + @"'>Chấm công ngày</a></li>
                                        <li><a id='Chamcongtangca-nav' href='" + ResolveUrl("~/QLNS/Chamcongtangca.aspx") + @"'>Chấm công tăng ca</a></li>
                                    </ul>
                                </li>";
            if (quyentinhluong)
                menu_nav += @"<li id='luong-nav'> 
                                    <a href='#' class='nav-top-item'>
                                        Lương
                                    </a>
                                    <ul>
                                        <li><a id='Danhsachbangluong-nav' href='" + ResolveUrl("~/QLNS/Danhsachbangluong") + @"'>Danh sách bảng lương</a></li>
                                        <li><a id='Luongngay-nav' href='" + ResolveUrl("~/QLNS/Luongngay.aspx") + @"'>Lương ngày</a></li>
                                        <li><a id='Luongtangca-nav' href='" + ResolveUrl("~/QLNS/Luongtangca.aspx") + @"'>Lương tăng ca</a></li>
                                    </ul>
                                </li>";
            menu_nav += @"<li id='cocautochuc-nav'>
                                <a href='#' class='nav-top-item'>
                                    Cơ cấu - tổ chức
                                </a>
                                <ul>
                                    <li><a id='Phongban-nav' href='" + ResolveUrl("~/QLNS/Phongban") + @"'>Phòng ban</a></li>
                                    <li><a id='Tonhom-nav' href='" + ResolveUrl("~/QLNS/Tonhom") + @"'>Tổ - nhóm</a></li>
                                    <li><a id='Chucvu-nav' href='" + ResolveUrl("~/QLNS/Chucvu") + @"'>Chức vụ</a></li>
                                </ul>
                            </li>
                            <li id='caidat-nav'>
                                <a href='#' class='nav-top-item'>
                                    Cài đặt
                                </a>
                                <ul>
                                    <li><a id='Congthuctinhluong-nav' href='" + ResolveUrl("~/QLNS/Congthuctinhluong") + @"'>Công thức tính lương</a></li>
                                    <li><a id='Luongtoithieu-nav' href='" + ResolveUrl("~/QLNS/Luongtoithieu") + @"'>Lương tối thiểu</a></li>
                                    <li><a id='Quydinhsongaychamcong-nav' href='" + ResolveUrl("~/QLNS/Quydinhsongaychamcong") + @"'>QĐ số ngày chấm công</a></li>
                                </ul>
                            </li>";
            if (quyenquantrihethong || quyennhatkyhethong)
            {
                menu_nav += @"<li id='quantrihethong-nav'> 
                                    <a href='#' class='nav-top-item'>
                                        Quản trị hệ thống
                                    </a>
                                    <ul>";
                if (quyenquantrihethong)
                    menu_nav += @"<li><a id='Quanlytaikhoan-nav' href='" + ResolveUrl("~/Admin/Quanlytaikhoan") + @"'>Quản lý người dùng</a></li>";
                if (quyennhatkyhethong)
                {
                    menu_nav += @"<li><a id='Nhatkyhethong-nav' href='" + ResolveUrl("~/Admin/Nhatkyhethong") + @"'>Nhật ký hệ thống</a></li>";
                }
                menu_nav += @"</ul>
                                </li>";
            }
            menu_nav += @"<li id='khac-nav'>
                                <a href='#' class='nav-top-item'>
                                    Khác
                                </a>
                                <ul>
                                    <li><a id='Bangcap-nav' href='" + ResolveUrl("~/QLNS/Bangcap") + @"'>Bằng cấp</a></li> <!-- Add class 'current' to sub menu items also -->
                                    <li><a id='Bacluong-nav' href='" + ResolveUrl("~/QLNS/Bacluong") + @"'>Bậc lương</a></li>
                                    <li><a id='Chuyenmon-nav' href='" + ResolveUrl("~/QLNS/Chuyenmon") + @"'>Chuyên môn</a></li>
                                    <li><a id='Congviec-nav' href='" + ResolveUrl("~/QLNS/Congviec") + @"'>Công việc</a></li>
                                    <li><a id='Dantoc-nav' href='" + ResolveUrl("~/QLNS/Dantoc") + @"'>Dân tộc</a></li>
                                    <li><a id='Ngachluong-nav' href='" + ResolveUrl("~/QLNS/Ngachluong") + @"'>Ngạch lương</a></li>
                                    <li><a id='Ngonngu-nav' href='" + ResolveUrl("~/QLNS/Ngonngu") + @"'>Ngôn ngữ</a></li>
                                    <li><a id='Phucap-nav' href='" + ResolveUrl("~/QLNS/Phucap") + @"'>Phụ cấp</a></li>
                                    <li><a id='Quanhegiadinh-nav' href='" + ResolveUrl("~/QLNS/Quanhegiadinh") + @"'>Quan hệ gia đình</a></li>
                                    <li><a id='Quoctich-nav' href='" + ResolveUrl("~/QLNS/Quoctich") + @"'>Quốc tịch</a></li>
                                    <li><a id='Tinhoc-nav' href='" + ResolveUrl("~/QLNS/Tinhoc") + @"'>Tin học</a></li>
                                    <li><a id='Tongiao-nav' href='" + ResolveUrl("~/QLNS/Tongiao") + @"'>Tôn giáo</a></li>
                                </ul>
                            </li>";
            if (quyengiaitri)
                menu_nav += @"<li id='giaitri-nav'>
                                    <a href='" + ResolveUrl("~/Relax") + @"' class='nav-top-item no-submenu'>
                                        Giải trí
                                    </a>
                                </li>";
            menu_nav += "</ul>";
            ltr_main_nav.Text = menu_nav;
        }
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="QLNS.QLNS.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="<%= ResolveUrl("~/css/Search.css") %>" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#index-nav a").addClass("current");
            $("#<%= txtHoten.ClientID %>").focus();
            $("#SearchResult .item").click(function() {
                var ckitem = $(this).hasClass("selected");
                if (!ckitem)
                    $(this).addClass("selected");
                else
                    $(this).removeClass("selected");
            });
        });
    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
    <div class="content-box" style="min-width: 700px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Kết quả tìm kiếm</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <div id="BoxSeachPage">
                <asp:TextBox ID="txtHoten" runat="server" 
                    ontextchanged="txtHoten_TextChanged" />
                <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" 
                    onclick="btnSearch_Click" />
            </div>
            
            <div class="notification success png_bg search-information">
                <div>
                    Tìm thấy <b><asp:Literal ID="ltrInfor" runat="server" Text="0" /></b> kết quả đối với từ khóa <b><asp:Literal ID="ltrKey" runat="server" /></b>
                </div>
            </div>
            
    <asp:Repeater ID="rpData" runat="server">
        <HeaderTemplate>
            <div id="SearchResult">
        </HeaderTemplate>
        <ItemTemplate>
                <div class="item">
                    <h5># <%# Eval("MaNV") %> <span><%# Eval("STT").ToString() %>/<%# ltrInfor.Text %> #</span></h5>
                    <div class="content">
                        <div class="imgNhanvien">
                            <img src='<%# string.Format("Employee-images/{0}", Eval("Hinhanh")) %>' alt="Hình đã bị xóa" />
                        </div>
                        <div class="main-content">
                            <div class="left-content">
                                <p><b>Họ tên:</b><span><%# Eval("Hoten") %></span></p>
                                <p><b>Giới tính:</b><span><%# Eval("Gioitinh") %></span></p>
                                <p><b>Ngày sinh:</b><span><%# string.Format("{0:dd/MM/yyyy}",Eval("Ngaysinh")) %></span></p>
                                <p><b>Nơi sinh:</b><span><%# Eval("Noisinh") %></span></p>
                            </div>
                            <div class="right-content">
                                <p><b>Đang làm việc tại phòng:</b><span><%# Eval("Tenphong") %></span></p>
                                <p><b>Hiện giữ chức vụ:</b><span><%# Eval("Tenchucvu") %></span></p>
                                <p><b>Công việc:</b><span><%# Eval("Tencongviec") %></span></p>
                                <p><b>Trạng thái:</b><span><%# Eval("Trangthai") %></span></p>
                            </div>
                            <div class="clear"></div>
                            <p><b>Điện thoại nhà:</b><span><%# Eval("Dienthoainha")%></span></p>
                            <p><b>Di động:</b><span><%# Eval("Dienthoaididong")%></span></p>
                            <p><b>Email:</b><span><%# Eval("Email")%></span></p>
                            <p><b>Địa chỉ:</b><span><%# Eval("Diachi")%></span>></p>
                            <p><b>Tạm trú</b><span><%# Eval("Tamtru")%></span></p>
                            
                            
                            <p><a href='<%# ResolveUrl("~/QLNS/ChitietNhanvien") + "@" + Eval("MaNV") %>'>Chi tiết nhân viên</a> |
                             <a href='<%# ResolveUrl("~/QLNS/Quatrinhlamviec") + "@" + Eval("MaNV")  %>'>Xem quá trình làm việc</a>
                            </p>
                        </div>
                    </div>
                </div> <!--End .item-->
        </ItemTemplate>
        <FooterTemplate>
            </div> <!-- End #SearchResult -->
        </FooterTemplate>
    </asp:Repeater>
            
                
            
            
            <div class="clear"></div>
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

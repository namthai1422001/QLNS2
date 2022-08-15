﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Luongngay.aspx.cs" Inherits="QLNS.QLNS.Luongngay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="../scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tablesorter.widgets.js" type="text/javascript"></script>

    <link href="../css/blue/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("#luong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Luongngay-nav").addClass("current");
            $("#listEmployee").tablesorter({
                // initialize zebra striping and filter widgets 
                widgets: ["zebra", "filter"],

                headers: { 0: { sorter: false, filter: false} },

                widgetOptions: {

                    // css class applied to the table row containing the filters & the inputs within that row 
                    filter_cssFilter: 'tablesorter-filter',

                    // filter widget: If there are child rows in the table (rows with class name from "cssChildRow" option) 
                    // and this option is true and a match is found anywhere in the child row, then it will make that row 
                    // visible; default is false 
                    filter_childRows: false,

                    // Set this option to true to use the filter to find text from the start of the column 
                    // So typing in "a" will find "albert" but not "frank", both have a's; default is false 
                    filter_startsWith: false
                }
            });
        });
    </script>
    <style type="text/css">
        .spancol1{display: inline-block; width: 40px; font-weight: bold; margin-left: 10px;}
        .spancol2{display: inline-block; width: 50px; font-weight: bold; margin-left: 10px;}
        .spancol3{display: inline-block; width: 80px; font-weight: bold; margin-left: 10px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 1150px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Lương ngày</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrInfor" runat="server" />
            <p style="margin-left: 5px;">
                <span class="spancol1">Năm: </span><asp:DropDownList ID="cbNam" runat="server" Width="67px"
                    AutoPostBack="true" onselectedindexchanged="cbNam_SelectedIndexChanged" />
                <span class="spancol2">Tháng: </span><asp:DropDownList ID="cbThang" runat="server" Width="55px"
                    AutoPostBack="true" onselectedindexchanged="cbThang_SelectedIndexChanged" />
                <b class="spancol3">Trạng thái: </b>
                <b style="color: #AC2C2C;"><asp:Literal ID="ltrStatus" runat="server" /></b>
                <asp:Image ID="imgStatus" runat="server" style="width: 24px; height: 24px;" />
                <asp:Button ID="btnTinhluong" runat="server" CssClass="button"
                    Text="Tính lương" onclick="btnTinhluong_Click" />
            </p>
            
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table id="listEmployee" class="tablesorter" style="min-width: 1150px;">
                    <thead>
                        <th style="font-size: 11px; font-weight: bold; width: 40px;">STT</th>
                        <th style="font-size: 11px; font-weight: bold; width: 80px;">Mã NV </th>
                        <th style="font-size: 11px; font-weight: bold;">Họ đệm</th>
                        <th style="font-size: 11px; font-weight: bold;">Tên</th>
                        <th style="font-size: 11px; font-weight: bold;">LCB</th>
                        <th style="font-size: 11px; font-weight: bold;">BHXH</th>
                        <th style="font-size: 11px; font-weight: bold;">BHYT</th>
                        <th style="font-size: 11px; font-weight: bold;">BHTN</th>
                        <th style="font-size: 11px; font-weight: bold;">Phí công đoàn</th>
                        <th style="font-size: 11px; font-weight: bold;">Tiền lương</th>
                        <th style="font-size: 11px; font-weight: bold;">Phụ cấp</th>
                        <th style="font-size: 11px; font-weight: bold;">Thuế TNCN</th>
                        <th style="font-size: 11px; font-weight: bold;">Tạm ứng</th>
                        <th style="font-size: 11px; font-weight: bold;">Làm thêm</th>
                        <th style="font-size: 11px; font-weight: bold;">Khấu trừ</th>
                        <th style="font-size: 11px; font-weight: bold;">Thưởng</th>
                        <th style="font-size: 11px; font-weight: bold;">Công tác</th>
                        <th style="font-size: 11px; font-weight: bold;">Thực lãnh</th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("MaNV") %>
                        </td>
                        <td> 
                            <%# Eval("HoNV") %>
                        </td>
                        <td> 
                            <%# Eval("TenNV") %>
                        </td>
                        <td> 
                            <%# Eval("Luongcoban") %>
                        </td>
                        <td> 
                            <%# Eval("TienBHXH") %>
                        </td>
                        <td> 
                            <%# Eval("TienBHYT") %>
                        </td>
                        <td> 
                            <%# Eval("TienBHTN") %>
                        </td>
                        <td> 
                            <%# Eval("TienPhicongdoan") %>
                        </td>
                        <td> 
                            <%# Eval("Tienluong") %>
                        </td>
                        <td> 
                            <%# Eval("Tongphucap") %>
                        </td>
                        <td> 
                            <%# Eval("TienThueTNCN") %>
                        </td>
                        <td> 
                            <%# Eval("Tongtamung") %>
                        </td>
                        <td>
                            <a href='<%# String.Format("Luonglamthem@{0}@{1}@{2}",Eval("Nam"),Eval("Thang"),Eval("MaNV")) %>' title="Chi tiết lương làm thêm">
                                <%# Eval("Tienlamthem")%>
                            </a>
                        </td>
                        <td> 
                            <a href='<%# String.Format("Khautru@{0}@{1}@{2}",Eval("Nam"),Eval("Thang"),Eval("MaNV")) %>' title="Chi tiết khấu trừ">
                                <%# Eval("Tongkhautru")%>
                            </a>
                        </td>
                        <td> 
                            <%# Eval("Tongthuong") %>
                        </td>
                        <td> 
                            <%# Eval("Phicongtac") %>
                        </td>
                        <td> 
                            <%# Eval("Thuclanh") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
    
            <div class="bulk-actions align-left" style="margin: 10px auto 5px 5px;">
                <asp:Label ID="lblCurrent" runat="server" Text="0" />/
                <asp:Label ID="lblTotalRowCount" runat="server" Text="0" />
            </div>
            <div class="pagination">
                <asp:HyperLink ID="hplFirstPage" runat="server" NavigateUrl="#" ToolTip="Trang đầu">&laquo; Đầu</asp:HyperLink>
                <asp:HyperLink ID="hplPreviousPage" runat="server" NavigateUrl="#" ToolTip="Trang trước">&laquo; Trước</asp:HyperLink>
                <asp:Repeater ID="rpPagination" runat="server" 
                    onitemdatabound="rpPagination_ItemDataBound">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplPage" runat="server" NavigateUrl="#">1</asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:HyperLink ID="hplNextPage" runat="server" NavigateUrl="#" ToolTip="Trang sau">Sau &raquo;</asp:HyperLink>
                <asp:HyperLink ID="hplLastPage" runat="server" NavigateUrl="#" ToolTip="Trang cuối">Cuối &raquo;</asp:HyperLink>
            </div> <!-- End .pagination -->
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

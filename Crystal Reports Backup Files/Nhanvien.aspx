<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Nhanvien.aspx.cs" Inherits="QLNS.QLNS.Nhanvien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tablesorter.widgets.js" type="text/javascript"></script>

    <link href="../css/blue/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
            $("#listEmployee").tablesorter({
                // initialize zebra striping and filter widgets 
                widgets: ["zebra", "filter"],

                headers: { 9: { sorter: false, filter: false} },

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Danh sách nhân viên</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="padding-left: 10px;">
                <asp:HyperLink ID="hplAdd" runat="server" CssClass="button"
                    NavigateUrl="EditThongtincanhan" style="margin-left: 5px;"
                    title="Thêm nhân viên">Thêm nhân viên</asp:HyperLink>
            </p>
            <p style="text-align: right; padding-right: 20px;">
                Nhân viên: 
                <asp:DropDownList ID="cbTrangthainhanvien" runat="server" Width="200px"
                    AutoPostBack="true" 
                    onselectedindexchanged="cbTrangthainhanvien_SelectedIndexChanged">
			        <asp:ListItem Value="1" Text="Đang làm việc"></asp:ListItem>
			        <asp:ListItem Value="2" Text="Đang thử việc"></asp:ListItem>
			        <asp:ListItem Value="3" Text="Tạm ngưng việc"></asp:ListItem>
			        <asp:ListItem Value="4" Text="Đã nghỉ việc"></asp:ListItem>
                </asp:DropDownList>
                Phòng: 
                <asp:DropDownList ID="cbPhong" runat="server" Width="200px"
                    AutoPostBack="true" onselectedindexchanged="cbPhong_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table id="listEmployee" class="tablesorter">
                    <thead>
                        <th style="font-size: 11px; font-weight: bold;">STT</th>
                        <th style="font-size: 11px; font-weight: bold;">Mã NV </th>
                        <th style="font-size: 11px; font-weight: bold;">Họ đệm</th>
                        <th style="font-size: 11px; font-weight: bold;">Tên</th>
                        <th style="font-size: 11px; font-weight: bold;">GT</th>
                        <th style="font-size: 11px; font-weight: bold;">Ngày sinh</th>
                        <th style="font-size: 11px; font-weight: bold;">Chức vụ</th>
                        <th style="font-size: 11px; font-weight: bold;">Tổ</th>
                        <th style="font-size: 11px; font-weight: bold;">Công việc</th>
                        <th></th>
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
                            <img alt='<%# (Eval("Nu").ToString() == "True") ? "Nữ" : "Nam" %>'
                                title='<%# (Eval("Nu").ToString() == "True") ? "Nữ" : "Nam" %>'
                                src='<%# String.Format("../images/icons/{0}32x32.png", (Eval("Nu").ToString() == "True") ? "female" : "male") %>' />
                        </td>
                        <td> 
                            <%# string.Format("{0:dd/MM/yyyy}", Eval("Ngaysinh")) %>
                        </td>
                        <td> 
                            <%# Eval("Tenchucvu") %>
                        </td>
                        <td> 
                            <%# Eval("TenToNhom") %>
                        </td>
                        <td> 
                            <%# Eval("Tencongviec") %>
                        </td>
                        <td style="width: 15px;"> 
                            <a href='<%# String.Format("ChitietNhanvien@{0}",Eval("MaNV")) %>' title="Xem chi tiết">
                                <img src="../images/icons/information.png" alt="Chi tiết" />
                            </a>
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

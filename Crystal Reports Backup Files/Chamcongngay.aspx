<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Chamcongngay.aspx.cs" Inherits="QLNS.QLNS.Chamcongngay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="../scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tablesorter.widgets.js" type="text/javascript"></script>

    <link href="../css/blue/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("#chamcong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Chamcongngay-nav").addClass("current");
            $("#<%= btnSave.ClientID %>").hide();
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
            $(".check-all-input").click(function() {
                var chkMaNV = $(this).parent().parent().parent().parent().parent().find("input[class!=check-all-input]:visible");
                chkMaNV.attr('checked', $(this).is(':checked'));
                if (chkMaNV.parent().find("input:checked").length > 0) {
                    $("#<%= btnSave.ClientID %>").slideDown(200);
                } else {
                    $("#<%= btnSave.ClientID %>").slideUp(80);
                }
            });
            $("#listEmployee tbody tr td input[type=checkbox]").click(function() {
                if ($(this).parent().parent().parent().find("input:checked").length == $(this).parent().parent().parent().find("input").length) {
                    $(".check-all-input").attr('checked', 'checked');
                } else {
                    $(".check-all-input").removeAttr('checked');
                }
                if ($(this).parent().parent().parent().find("input:checked").length > 0) {
                    $("#<%= btnSave.ClientID %>").slideDown(200);
                } else {
                    $("#<%= btnSave.ClientID %>").slideUp(80);
                }
            });
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .spancol1{display: inline-block; width: 85px; font-weight: bold; margin-left: 10px;}
        .spancol2{display: inline-block; width: 115px; font-weight: bold; margin-left: 10px;}
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 750px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Chấm công ngày</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrInfor" runat="server" />
            <asp:RequiredFieldValidator ID="reqSogiocongAll" runat="server"
                ControlToValidate="txtSogiocongAll"
                ErrorMessage="<div class='notification error png_bg'><div>Vui lòng nhập số giờ công</div></div>"
                Display="Dynamic" />
            <asp:RequiredFieldValidator ID="reqSogionghiphepAll" runat="server"
                ControlToValidate="txtSogionghiphepAll"
                ErrorMessage="<div class='notification error png_bg'><div>Vui lòng nhập số giờ nghỉ phép</div></div>"
                Display="Dynamic" />
            <asp:RegularExpressionValidator ID="regSogiocongAll" runat="server"
                ValidationExpression="[0-9]+"
                ControlToValidate="txtSogiocongAll"
                ErrorMessage="<div class='notification error png_bg'><div>Số giờ công nhập sai định dạng</div></div>"
                Display="Dynamic" />
            <asp:RegularExpressionValidator ID="regSogionghiphepAll" runat="server"
                ValidationExpression="[0-9]+"
                ControlToValidate="txtSogionghiphepAll"
                ErrorMessage="<div class='notification error png_bg'><div>Số giờ nghỉ phép nhập sai định dạng</div></div>"
                Display="Dynamic" />
            <p style="margin-left: 5px;">
                <span class="spancol1">Năm: </span><asp:DropDownList ID="cbNam" runat="server" Width="67px"
                    AutoPostBack="true" onselectedindexchanged="cbNam_SelectedIndexChanged" />
                <span class="spancol2">Tháng: </span><asp:DropDownList ID="cbThang" runat="server" Width="55px"
                    AutoPostBack="true" onselectedindexchanged="cbThang_SelectedIndexChanged" />
                <b class="spancol1">Trạng thái: </b>
                <b style="color: #AC2C2C;"><asp:Literal ID="ltrStatus" runat="server" /></b>
                <asp:Image ID="imgStatus" runat="server" style="width: 24px; height: 24px;" />
            </p>
        <asp:Panel ID="pnlChamcong" runat="server">
            <p style="margin-left: 5px;">
                <span class="spancol1">Số giờ công: </span>
                    <asp:TextBox ID="txtSogiocongAll" runat="server" CssClass="text-input" style="width: 50px;" MaxLength="3" onkeypress="return onlyNumbers(event)" Text="0" />
                <span class="spancol2">Số giờ nghỉ phép: </span>
                    <asp:TextBox ID="txtSogionghiphepAll" runat="server" CssClass="text-input" style="width: 50px;" MaxLength="3" onkeypress="return onlyNumbers(event)" Text="0" />
                <asp:Button ID="btnSave" runat="server" CssClass="button"
                    Text="Lưu" onclick="btnSave_Click" />
            </p>
        </asp:Panel>
            <p style="margin-left: 5px; color: #AC2C2C;">
                Tổng giờ công: <b style="display: inline-block; width: 35px;"><asp:Literal ID="ltrTonggiocong" runat="server" Text="0" /></b>
                Tổng giờ nghỉ phép: <b style="display: inline-block; width: 35px;"><asp:Literal ID="ltrTonggionghi" runat="server" Text="0" /></b>
            </p>
                
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table id="listEmployee" class="tablesorter">
                    <thead>
                        <th style="width: 30px;"><input type="checkbox" class="check-all-input" /> </th>
                        <th style="font-size: 11px; font-weight: bold; width: 40px;">STT</th>
                        <th style="font-size: 11px; font-weight: bold; width: 80px;">Mã NV </th>
                        <th style="font-size: 11px; font-weight: bold;">Họ đệm</th>
                        <th style="font-size: 11px; font-weight: bold;">Tên</th>
                        <th style="font-size: 11px; font-weight: bold;">Giờ công</th>
                        <th style="font-size: 11px; font-weight: bold;">Giờ nghỉ phép</th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="checkbox" name="chkMaNV" value='<%# Eval("MaNV").ToString() %>' />
                        </td>
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
                            <%# Eval("Sogiocong").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Sogionghiphep").ToString() %>
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
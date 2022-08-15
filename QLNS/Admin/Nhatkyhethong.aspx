<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Nhatkyhethong.aspx.cs" Inherits="QLNS.Admin.Nhatkyhethong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tablesorter.widgets.js" type="text/javascript"></script>

    <link href="../css/blue/style.css" rel="stylesheet" type="text/css" />

    <link href="../le-frog_Green/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.button.css" rel="stylesheet" type="text/css" />

<%--    <script src="../scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../scripts/jquery.ui.widget.js" type="text/javascript"></script>--%>
    <script src="../scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../scripts/jquery.ui.datepicker-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            var dates = $("#<%= txtFromDate.ClientID %>, #<%= txtToDate.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                changeMonth: true,
                changeYear: true,
                maxDate: "0",
                onSelect: function(selectedDate) {
                    var option = this.id == "<%= txtFromDate.ClientID %>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
            $("#listDiary").tablesorter({
                // initialize zebra striping and filter widgets 
                widgets: ["zebra", "filter"],

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

    <script type="text/javascript">
        $(document).ready(function() {
            $("#quantrihethong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Nhatkyhethong-nav").addClass("current");
            $("#SearchBox").hide();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
    <div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Nhật ký hệ thống</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="text-align: center;">
                Từ ngày: <input id="txtFromDate" type="text" runat="server" style="width: 80px;" class="text-input" />
                Đến ngày: <input id="txtToDate" type="text" runat="server" style="width: 80px;" class="text-input" />
                <asp:Button ID="btnXem" runat="server" CssClass="button" OnClick="btnXem_Click" Text="Xem" ToolTip="Xem" /> | 
                <asp:Button ID="btnDelete" runat="server" CssClass="button" OnClick="btnDelete_Click" Text="Xóa nhật ký" ToolTip="Xóa nhật ký"
                    OnClientClick="return confirm('Bạn có thực sự muốn xóa nhật ký hệ thống! Một khi xóa thì không thể khôi phục lại! Hãy cân nhắc kỹ!');" />
            </p>
			<asp:Literal ID="ltrInfo" runat="server" />
			<asp:Repeater ID="rpData" runat="server" >
                <HeaderTemplate>
                <table id="listDiary">
                    <thead>
                        <th style="font-size: 11px; font-weight: bold; width: 55px;">STT</th>
                        <th style="font-size: 11px; font-weight: bold;">Tên tài khoản</th>
                        <th style="font-size: 11px; font-weight: bold;">Thời gian</th>
                        <th style="font-size: 11px; font-weight: bold;">Chức năng</th>
                        <th style="font-size: 11px; font-weight: bold;">Hành động</th>
                        <th style="font-size: 11px; font-weight: bold;">Đối tượng</th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Username") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy HH:mm:ss}",Eval("Thoigian")) %>
                        </td>
                        <td> 
                            <%# Eval("Tenchucnang") %>
                        </td>
                        <td> 
                            <%# Eval("Tenhanhdong") %>
                        </td>
                        <td> 
                            <%# Eval("Doituong") %>
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

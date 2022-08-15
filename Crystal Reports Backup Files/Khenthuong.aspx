<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Khenthuong.aspx.cs" Inherits="QLNS.QLNS.Khenthuong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        });
	</script>
	
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Khenthuong-nav").addClass("current");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Các quyết định khen thưởng nhân viên</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="margin-left: 5px;">
                <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Thêm" ToolTip="Thêm"
                    OnClick="btnAdd_Click" />
            </p>
            <p style="text-align: center;">
                Từ ngày: <input id="txtFromDate" type="text" runat="server" style="width: 80px;" class="text-input" />
                Đến ngày: <input id="txtToDate" type="text" runat="server" style="width: 80px;" class="text-input" />
                <asp:Button ID="btnXem" runat="server" CssClass="button" OnClick="btnXem_Click" Text="Xem" ToolTip="Xem" />
            </p>
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Mã NV</b> </th>
                        <th> <b>Họ tên</b> </th>
                        <th> <b>Tên khen thưởng</b> </th>
                        <th> <b>Hình thức khen thưởng</b> </th>
                        <th> <b>Ngày</b> </th>
                        <th> <b>Tiền (VNĐ)</b> </th>
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
                            <%# Eval("HoTen") %>
                        </td>
                        <td> 
                            <%# Eval("Tenkhenthuong") %>
                        </td>
                        <td> 
                            <%# Eval("Hinhthuckhenthuong") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngaykhenthuong")) %>
                        </td>
                        <td> 
                            <%# String.Format("{0:#,##0}", Eval("Sotien")) %>
                        </td>
                        <td style="width: 15px;"> 
                            <a href='<%# String.Format("ViewKhenthuong@{0}",Eval("Makhenthuong")) %>' title="Chi tiết khen thưởng">
                                <img src="../images/icons/information.png" alt="Chi tiết" />
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditDicongtac.aspx.cs" Inherits="QLNS.QLNS.EditDicongtac" %>
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
            var dates = $("#<%= txtNgaydi.ClientID %>, #<%= txtNgayve.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                changeMonth: true,
                changeYear: true,
                onSelect: function(selectedDate) {
                    var option = this.id == "<%= txtNgaydi.ClientID %>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
            $("#<%= txtNgayky.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-28d",
                maxDate: "0"
            }); ;
        });
	</script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Dicongtac-nav").addClass("current");
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 145px;}
        .fontbolder{font-weight: bold;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Nhập nhân viên đi công tác</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
			<div class="notification attention png_bg">
                <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
                <div>
                    Một khi đã lưu thì không được phép sửa
                </div>
            </div>
			<!-- Begin Đi công tác -->
			<div style="margin-left: 10px;">
			    <p>
			        <span class="spantitle">Phòng<span class="isrequire">(*)</span>: </span>
			        <asp:DropDownList ID="cbPhong" runat="server" Width="180px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="cbPhong_SelectedIndexChanged" />
                    <span>
                        <asp:RegularExpressionValidator ID="regPhong"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ControlToValidate="cbPhong"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Nhân viên<span class="isrequire">(*)</span>: </span>
			        <asp:DropDownList ID="cbNhanviencongtac" runat="server" Width="180px" />
                    <span>
                        <asp:RegularExpressionValidator ID="regNhanviencongtac"
                            runat="server"
                            ValidationExpression="[^0]([0-9NV]*)?"
                            ControlToValidate="cbNhanviencongtac"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày đi<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgaydi" runat="server" type="text" class="text-input fontbolder" style="width: 80px;" />
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày về<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgayve" runat="server" class="text-input fontbolder" type="text" style="width: 80px;" />
			    </p>
    			
    			
			    <p>
			        <span class="spantitle">Nơi công tác<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtNoicongtac" runat="server" class="text-input" Width="200px" MaxLength="99" />
                    <span>
                        <asp:RequiredFieldValidator ID="reqNoicongtac"
                            runat="server"
                            ControlToValidate="txtNoicongtac"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span>Về việc<span class="isrequire">(*)</span>: </span><br />
                    <asp:TextBox ID="txtVeviec" runat="server" CssClass="text-input" Width="90%" MaxLength="99" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqVeviec"
                            runat="server"
                            ControlToValidate="txtVeviec"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span>Lý do<span class="isrequire">(*)</span>: </span><br />
                    <asp:TextBox ID="txtLyDo" runat="server" CssClass="text-input" Width="90%" MaxLength="255" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server"
                            ControlToValidate="txtVeviec"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Tiền đi công tác (VNĐ): </span>
                    <asp:TextBox ID="txtTiendicongtac" runat="server" class="text-input fontbolder" Width="100px" MaxLength="12" Text="0"
                        onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
                    <span>
                        <asp:RegularExpressionValidator ID="regTiendicongtac"
                            runat="server"
                            ValidationExpression="[0-9,]+"
                            ControlToValidate="txtTiendicongtac"
                            CssClass="input-notification error errorimg"
                            ErrorMessage="Sai định dạng" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Chức vụ người ký<span class="isrequire">(*)</span>: </span>
			        <asp:DropDownList ID="cbChucvunguoiky" runat="server" Width="180px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="cbChucvunguoiky_SelectedIndexChanged" />
                    <span>
                        <asp:RegularExpressionValidator ID="regChucvunguoiky"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ControlToValidate="cbChucvunguoiky"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Người ký<span class="isrequire">(*)</span>: </span>
			        <asp:DropDownList ID="cbNguoiky" runat="server" Width="180px" />
                    <span>
                        <asp:RegularExpressionValidator ID="regNguoiky"
                            runat="server"
                            ValidationExpression="[^0]([0-9NV]*)?"
                            ControlToValidate="cbNguoiky"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày ký<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgayky" runat="server" class="text-input fontbolder" type="text" style="width: 80px;" />
			    </p>
			</div>
			<!-- End Đi công tác -->
			
            <div class="clear"></div>
            
            <p style="text-align: left; margin-left: 10px;">
                <a href="javascript:history.go(-1)">Trở lại</a>
                |
                <asp:Button ID="btnSave" runat="server"
                    Text="Lưu" ToolTip="Lưu" OnClientClick="confirm('Bạn có thực sự muốn lưu?')"
                    CssClass="button" onclick="btnSave_Click" />
            </p>
            
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditKyluat.aspx.cs" Inherits="QLNS.QLNS.EditKyluat" %>
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
            var dates = $("#<%= txtNgayky.ClientID %>, #<%= txtNgaykyluat.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                changeMonth: true,
                changeYear: true,
                maxDate: "7",
                onSelect: function(selectedDate) {
                    var option = this.id == "<%= txtNgayky.ClientID %>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
            $("#<%= txtNgayxayra.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-60d",
                maxDate: "0"
            });
        });
	</script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Kyluat-nav").addClass("current");
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 155px;}
        .fontbolder{font-weight: bold;}
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Quyết định kỷ luật nhân viên</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
			
			<!-- Begin Kỷ luật nhân viên -->
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
			        <asp:DropDownList ID="cbNhanvienbikyluat" runat="server" Width="180px" />
                    <span>
                        <asp:RegularExpressionValidator ID="regNhanvienduocthuong"
                            runat="server"
                            ValidationExpression="[^0]([0-9NV]*)?"
                            ControlToValidate="cbNhanvienbikyluat"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
			    </p>
    			
    			<p>
			        <span class="spantitle">Ngày xảy ra<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgayxayra" runat="server" type="text" class="text-input fontbolder" style="width: 80px;" />
			    </p>
			    
			    <p>
			        <span class="spantitle">Tên kỷ luật<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtTenkyluat" runat="server" class="text-input" Width="90%" MaxLength="99" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqTenkyluat"
                            runat="server"
                            ControlToValidate="txtTenkyluat"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày kỷ luật<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgaykyluat" runat="server" type="text" class="text-input fontbolder" style="width: 80px;" />
			    </p>
			    
			    <p>
			        <span class="spantitle">Địa điểm<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtDiadiem" runat="server" class="text-input" Width="90%" MaxLength="99" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqDiadiem"
                            runat="server"
                            ControlToValidate="txtDiadiem"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
			    
			    <p>
			        <span class="spantitle">Người chứng kiến<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtNguoichungkien" runat="server" class="text-input" Width="90%" MaxLength="99" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqNguoichungkien"
                            runat="server"
                            ControlToValidate="txtNguoichungkien"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
			    
			    <p>
			        <span class="spantitle">Mô tả sự việc<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtMotasuviec" runat="server" class="text-input" Rows="6" TextMode="MultiLine" MaxLength="499" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqMotasuviec"
                            runat="server"
                            ControlToValidate="txtMotasuviec"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span>Người bị kỷ luật giải thích<span class="isrequire">(*)</span>: </span>
                    <asp:TextBox ID="txtNguoibikyluatgiaithich" runat="server" class="text-input" Rows="6" TextMode="MultiLine" MaxLength="499" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqNguoibikyluatgiaithich"
                            runat="server"
                            ControlToValidate="txtNguoibikyluatgiaithich"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
    			
			    <p>
			        <span>Lý do kỷ luật<span class="isrequire">(*)</span>: </span><br />
                    <asp:TextBox ID="txtLyDo" runat="server" CssClass="text-input" Width="90%" MaxLength="255" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqLyDo"
                            runat="server"
                            ControlToValidate="txtLyDo"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
			    </p>
			    
			    <p>
			        <span>Hình thức kỷ luật<span class="isrequire">(*)</span>: </span><br />
                    <asp:TextBox ID="txtHinhthuckyluat" runat="server" CssClass="text-input" Width="90%" MaxLength="99" /><br />
                    <span>
                        <asp:RequiredFieldValidator ID="reqHinhthuckyluat"
                            runat="server"
                            ControlToValidate="txtHinhthuckyluat"
                            CssClass="input-notification error errortop"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
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
			<!-- End Kỷ luật nhân viên -->
			
            <div class="clear"></div>
            
            <p style="text-align: left; margin-left: 10px;">
                <a href="Kyluat">Trở lại</a>
                |
                <asp:Button ID="btnSave" runat="server"
                    Text="Lưu" ToolTip="Lưu" OnClientClick="confirm('Bạn có thực sự muốn lưu?')"
                    CssClass="button" onclick="btnSave_Click" />
            </p>
            
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

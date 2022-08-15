<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditNguoithan.aspx.cs" Inherits="QLNS.QLNS.EditNguoithan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
        
        <asp:Literal ID="ltrh3" runat="server" />
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Mã NV:</span>
                <asp:Label ID="lblMaNV" runat="server" style="float: left; margin-top: 4px; font-weight: bold;" />
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Nhân viên:</span>
                <asp:Label ID="lblHoTenNV" runat="server" style="float: left; margin-top: 4px; font-weight: bold;" />
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Họ tên người thân<span class="isrequire">(*)</span>:</span>
                <asp:TextBox ID="txtHoTenNT" runat="server" CssClass="text-input" MaxLength="39" 
                    Width="150px" style="float: left;" />
                <span style="margin-top: 4px; float: left;">
                    <asp:RequiredFieldValidator ID="reqHoTenNT"
                        runat="server"
                        ControlToValidate="txtHoTenNT"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Không được để trống" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Quan hệ<span class="isrequire">(*)</span>:</span>
                <asp:DropDownList ID="cbQuanhe" runat="server"
                    style="float: left; width: 200px;">
                </asp:DropDownList>
                <span style="margin-top: 4px; float: left;">
                    <asp:RegularExpressionValidator ID="regQuanhe"
                        runat="server"
                        ValidationExpression="[^0]([0-9]*)?"
                        ControlToValidate="cbQuanhe"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Địa chỉ<span class="isrequire">(*)</span>:</span>
                <asp:TextBox ID="txtDiachi" runat="server" CssClass="text-input" MaxLength="99" 
                    Width="350px" style="float: left;" />
                <span style="margin-top: 4px; float: left;">
                    <asp:RequiredFieldValidator ID="reqDiachi"
                        runat="server"
                        ControlToValidate="txtDiachi"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Không được để trống" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Email:</span>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="text-input" MaxLength="49" 
                    Width="350px" style="float: left;" />
                <span style="margin-top: 4px; float: left;">
                    <asp:RegularExpressionValidator ID="regEmail"
                        runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="txtEmail"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Sai định dạng" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Điện thoại:</span>
                <asp:TextBox ID="txtDienthoai" runat="server" CssClass="text-input" MaxLength="20" 
                    Width="100px" style="float: left;" onkeypress="return onlyNumbers(event)" />
                <span style="margin-top: 4px; float: left;">
                    <asp:RegularExpressionValidator ID="regDienthoai"
                        runat="server"
                        ValidationExpression="[^0]([0-9]*)?"
                        ControlToValidate="txtDienthoai"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Sai định dạng" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Phụ thuộc<span class="isrequire">(*)</span>:</span>
                <span style="margin-top: 4px; float: left;">
                    <asp:CheckBox ID="chkIsPhuthuoc" runat="server" />
                </span>
            </p>
            
    		
            <p style="margin-left: 10px;">
                <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="button"
                    onclick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="button" 
                    onclick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="button"
                    onclick="btnDelete_Click" 
                    OnClientClick="return confirm('Bạn có thực sự muốn xóa?');" />
                <a href="javascript:history.go(-1)">Trở lại</a>
            </p>
            
            <asp:Panel ID="panelAudit" runat="server">
	            <div class="audit">
                    <asp:Label ID="lblCreatedByUser" runat="server" Font-Bold="true" />
                     vào 
                    <asp:Label ID="lblCreatedByDate" runat="server" Font-Bold="true" />
                </div>
            </asp:Panel>
            
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditPhucapnhanvien.aspx.cs" Inherits="QLNS.QLNS.EditPhucapnhanvien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>

    <style type="text/css">
        .button
        {
            height: 26px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
        
        <asp:Literal ID="ltrh3" runat="server" />
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrInfor" runat="server" />
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Phụ cấp<span class="isrequire">(*)</span>:</span>
                <asp:DropDownList ID="cbPhucap" runat="server" AutoPostBack="true" 
                    style="float: left; width: 200px;" onselectedindexchanged="cbPhucap_SelectedIndexChanged">
                </asp:DropDownList>
                <span style="margin-top: 4px; float: left;">
                    <asp:RegularExpressionValidator ID="regPhucap"
                        runat="server"
                        ValidationExpression="[^0]([0-9]*)?"
                        ControlToValidate="cbPhucap"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                </span>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Số tiền tối đa(VNĐ):</span>
                <asp:Label ID="lblSotenMax" runat="server" style="float: left; margin-top: 4px; font-weight: bold;">0</asp:Label>
            </p>
            
            <p style="margin-left: 10px; height: 25px;">
                <span style="display: block; width: 170px; float: left; margin-top: 4px;">Số tiền được nhận<span class="isrequire">(*)</span>(VNĐ):</span>
                <asp:TextBox ID="txtValue" runat="server" CssClass="text-input" MaxLength="12" 
                    Width="100px" style="float: left;"
                     onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" >0</asp:TextBox>
                <span style="margin-top: 4px; float: left;">
                    <asp:RequiredFieldValidator ID="reqValue"
                        runat="server"
                        ControlToValidate="txtValue"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Không được để trống" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regValue"
                        runat="server"
                        ValidationExpression="[0-9,]+"
                        ControlToValidate="txtValue"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Không đúng định dạng" Display="Dynamic" />
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

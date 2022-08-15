<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditTaikhoan.aspx.cs" Inherits="QLNS.Admin.EditTaikhoan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#quantrihethong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Quanlytaikhoan-nav").addClass("current");
            $("#SearchBox").hide();
        });
    </script>
    
    <style type="text/css">
        #divEditTaikhoan{margin-left: 10px; margin-top: 10px; font-size: large;}
        #divEditTaikhoan .title-account{display: inline-block; width: 190px;}
        #divEditTaikhoan .text-input{width: 400px; font-size: large; font-family: Arial,Helvetica,sans-serif; }
        #divEditTaikhoan .error-span{font-size: small; font-style: italic;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 1000px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3><asp:Literal ID="ltrh3" runat="server" /></h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrAttention" runat="server" />
            <asp:Literal ID="ltrInfor" runat="server" />
            <div id="divEditTaikhoan">
                <p>
                    <span class="title-account">Tên đăng nhập <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtUsername" runat="server" MaxLength="30" CssClass="text-input" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqUsername"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtUsername"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regRangeUsername"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^┼]{6,30}"
                            ControlToValidate="txtUsername"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Phải từ 6 ký tự trở lên" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regUsername"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[A-Za-z0-9_]+"
                            ControlToValidate="txtUsername"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Chỉ được phép nhập chữ cái (a-z), số (0-9) và dấu gạch dưới (_)" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Mật khẩu <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regRangePassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^┼]{6,50}"
                            ControlToValidate="txtPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Phải từ 6 ký tự trở lên" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]+"
                            ControlToValidate="txtPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Không được phép nhập tiếng việt" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Xác nhận mật khẩu <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtPasswordRe" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqPasswordRe"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtPasswordRe"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:CompareValidator ID="comparePass"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtPasswordRe"
                            ControlToCompare="txtPassword"
                            Operator="Equal"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Xác nhận mật khẩu không trùng" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Email <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="text-input" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqEmail"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtEmail"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regEmail"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Sai định dạng" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Họ tên <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtFullname" runat="server" MaxLength="50" CssClass="text-input" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqFullname"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtFullname"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regFullname"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^┼]{4,50}"
                            ControlToValidate="txtFullname"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Phải từ 4 ký tự trở lên" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Tài khoản Super: </span>
                    <asp:CheckBox ID="chkSuper" runat="server" />
                </p>
                <p>
                    <span class="title-account">Ghi chú: </span>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" CssClass="text-input" Width="80%" />
                </p>
                <p>
                    <a href="Quanlytaikhoan" style="font-size: medium;">&laquo; Trở về</a>
                    <asp:Button ID="btnAdd" runat="server" Text="Tạo" onclick="btnAdd_Click" ValidationGroup="AddAccount" CssClass="button" Width="120px" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" 
                        onclick="btnUpdate_Click" CssClass="button" Width="120px" />
                    <asp:Button ID="btnLock" runat="server" Text="Khóa" onclick="btnLock_Click" CssClass="button" Width="120px" />
                    <asp:Button ID="btnResetPass" runat="server" Text="Reset mật khẩu" Visible="false"
                        CssClass="button" Width="120px" onclick="btnResetPass_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Xóa" OnClientClick="return confirm('Bạn có thực sự muốn xóa tài khoản này?');"
                        onclick="btnDelete_Click" CssClass="button" Width="120px" />
                </p>
            </div>
            <div class="clear"></div>
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

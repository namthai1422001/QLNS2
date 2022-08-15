<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="QLNS.Profile.ChangePass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#index-nav a").addClass("current");
            $("#SearchBox").hide();
        });
    </script>
    <style type="text/css">
        #change-password{margin-left: 10px; margin-top: 10px; font-size: large;}
        #change-password .title-account{display: inline-block; width: 190px;}
        #change-password .text-input{width: 400px; font-size: large; font-family: Arial,Helvetica,sans-serif; }
        #change-password .error-span{font-size: small; font-style: italic;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Thay đổi mật khẩu</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <div id="change-password">
                <p>
                    <span class="title-account">Mật khẩu<span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqOldPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtOldPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Mật khẩu mới<span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqNewPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtNewPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regRangeNewPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^┼]{6,50}"
                            ControlToValidate="txtNewPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Phải từ 6 ký tự trở lên" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regNewPassword"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ValidationExpression="[^ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]+"
                            ControlToValidate="txtNewPassword"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Không được phép nhập tiếng việt" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-account">Xác nhận mật khẩu <span class="isrequire">(*)</span>:</span>
                    <asp:TextBox ID="txtNewPasswordRe" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqNewPasswordRe"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtNewPasswordRe"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:CompareValidator ID="comparePass"
                            runat="server"
                            ValidationGroup="AddAccount"
                            ControlToValidate="txtNewPasswordRe"
                            ControlToCompare="txtNewPassword"
                            Operator="Equal"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Xác nhận mật khẩu không trùng" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Lưu" 
                        onclick="btnSave_Click" />
                </p>

            </div>
            
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

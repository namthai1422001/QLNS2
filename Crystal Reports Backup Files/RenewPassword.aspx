<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewPassword.aspx.cs" Inherits="QLNS.RenewPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Nhập mã xác nhận để lấy lại mật khẩu</title>
    <link href="css/styleByMe.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/icons/icon.png" type="image/png" />
    
    <style type="text/css">
        #forget-password .title-forget{width: 180px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="forget-password">
            <h2>Thay đổi mật khẩu</h2>
            <hr />
            <p>
                <span class="title-forget">Tên đăng nhập <span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtReNewUsername" runat="server" CssClass="text-input" MaxLength="30" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqReNewUsername"
                        runat="server"
                        ControlToValidate="txtReNewUsername"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                </span>
            </p>
            <p>
                <span class="title-forget">Mã xác nhận <span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtReNewVerify" runat="server" MaxLength="255" CssClass="text-input" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqReNewVerify"
                        runat="server"
                        ControlToValidate="txtReNewVerify"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                </span>
            </p>
            <p>
                <span class="title-forget">Mật khẩu mới <span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtReNewPassword" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqReNewPassword"
                        runat="server"
                        ControlToValidate="txtReNewPassword"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regRangeReNewPassword"
                        runat="server"
                        ValidationExpression="[^┼]{6,50}"
                        ControlToValidate="txtReNewPassword"
                        ErrorMessage="Phải từ 6 ký tự trở lên" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regReNewPassword"
                        runat="server"
                        ValidationExpression="[^ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]+"
                        ControlToValidate="txtReNewPassword"
                        ErrorMessage="Không được phép nhập tiếng việt" Display="Dynamic" />
                </span>
            </p>
            <p>
                <span class="title-forget">Xác nhận mật khẩu <span class="isrequire">(*)</span>:</span>
                <asp:TextBox ID="txtPasswordRe" runat="server" MaxLength="50" CssClass="text-input" TextMode="Password" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqPasswordRe"
                        runat="server"
                        ControlToValidate="txtPasswordRe"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:CompareValidator ID="comparePass"
                        runat="server"
                        ControlToValidate="txtPasswordRe"
                        ControlToCompare="txtReNewPassword"
                        Operator="Equal"
                        ErrorMessage="Xác nhận mật khẩu không trùng" Display="Dynamic" />
                </span>
            </p>
            
            <hr />
            
            <p>
                <asp:Button ID="btnSubmit" runat="server" Text="Thay đổi" CssClass="button"
                    onclick="btnSubmit_Click" /> | 
                <a href="Login" >Đăng nhập</a> | 
                <a href="StepBeforeForget" >Gửi lại yêu cầu Reset mật khẩu</a>
            </p>
        </div>
    </form>
</body>
</html>

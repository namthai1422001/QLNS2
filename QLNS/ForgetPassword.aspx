<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="QLNS.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Quên mật khẩu</title>
    <link href="css/styleByMe.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/icons/icon.png" type="image/png" />
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="forget-password">
            <h2>Quên mật khẩu</h2>
            <hr />
            <p>
                <span class="title-forget">Tên đăng nhập <span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtReUsername" runat="server" CssClass="text-input" MaxLength="30" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqReUsername"
                        runat="server"
                        ControlToValidate="txtReUsername"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                </span>
            </p>
            <p>
                <span class="title-forget">Email <span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtReEmail" runat="server" MaxLength="50" CssClass="text-input" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqReEmail"
                        runat="server"
                        ControlToValidate="txtReEmail"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regReEmail"
                        runat="server"
                        ControlToValidate="txtReEmail"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ErrorMessage="Sai định dạng" Display="Dynamic" />
                </span>
            </p>
            <p>
                <span class="title-forget">Mã xác nhận <span class="isrequire">(*)</span>: </span>
                <img src="CaptchaText.aspx" alt="Not found Image" title="Mã kiểm tra" class="captcha" />
                <asp:TextBox ID="txtCaptchar" runat="server" MaxLength="50" CssClass="text-input" Width="100px" />
                <span class="error-span">
                    <asp:RequiredFieldValidator ID="reqCaptchar"
                        runat="server"
                        ControlToValidate="txtCaptchar"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:Literal ID="ltrValueCaptchar" runat="server" />
                </span>
            </p>
            
            <hr />
            
            <p>
                <asp:Button ID="btnSubmit" runat="server" Text="Gửi yêu cầu" CssClass="button"
                    onclick="btnSubmit_Click" /> | 
                <a href="Login" >Đăng nhập</a> | 
                <a href="StepBeforeForget" >Thay đổi hình thức gửi yêu cầu</a>
            </p>
        </div>
    </form>
</body>
</html>

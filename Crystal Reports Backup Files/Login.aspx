<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QLNS.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Đăng nhập hệ thống quản lý nhân sự</title>
    <link rel="stylesheet" href="css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/style.css" type="text/css" media="screen" />
    <link rel="icon" href="images/icons/icon.png" type="image/png" />
    
    <script type="text/javascript" src="scripts/jquery-1.7.2.min.js"></script>
    <script src="scripts/login.js" type="text/javascript"></script>
    
</head>
<body id="login">
    <form id="form1" runat="server">
        <div id="login-wrapper" class="png_bg">
			<div id="login-top">
				<h1>Simpla Admin</h1>
				<!-- Logo (221px width) -->
				<img id="logo" src="images/logo.png" alt="Simpla Admin logo" />
			</div> <!-- End #logn-top -->
			
			<div id="login-content">
					<p>
						<label>Tên đăng nhập:</label>
						<input class="text-input" type="text" id="txtUsername" name="txtUsername" title="Tên đăng nhập" maxlength="29" />
					</p>
					<label id="Username-error" class="input-empty">
					   
					</label>
					<div class="clear"></div>
					<p>
						<label>Mật khẩu:</label>
						<input class="text-input" type="password" id="txtPassword" name="txtPassword" title="Mật khẩu" maxlength="49" />
					</p>
					<label id="Password-error" class="input-empty">
					</label>
					<div class="clear"></div>
					<p id="remember-password">
						<input type="checkbox" name="chkRemember" /><span style="float: right; padding-top: 5px;">Nhớ mật khẩu</span>
					</p>
					<div class="clear"></div>
					<p style="text-align: right;">
					    <input class="button" type="button" value="Quên mật khẩu" onclick="window.location = 'StepBeforeForget';" />
						<span> | </span>
						<input class="button" type="submit" value="Đăng nhập" id="btnLogin" name="btnLogin" onclick="return kt_nhap();" />
					</p>
					<div class="clear"></div>
                    <asp:Label ID="lblLoginError" runat="server">
                    </asp:Label>
			</div> <!-- End #login-content -->
		</div>
    </form>
</body>
</html>

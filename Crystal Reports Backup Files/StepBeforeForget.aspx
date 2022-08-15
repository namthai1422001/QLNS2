<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StepBeforeForget.aspx.cs" Inherits="QLNS.StepBeforeForget" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hình thức lấy lại mật khẩu</title>
    <link href="css/styleByMe.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/icons/icon.png" type="image/png" />
    
    <script type="text/javascript">
        function GetStepForget() {
            if (document.getElementById("selectBy").value == "0") {
                window.location = "ForgetPassword@Email";
            }
            else {
                window.location = "ForgetPassword@Reset";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="forget-password">
            <h2>Hình thức lấy lại mật khẩu</h2>
            <hr />
            <p style="height: 200px;">
                <span class="title-forget">Hình thức <span class="isrequire">(*)</span>: </span>
                <select name="selectBy" id="selectBy">
                    <option value="0">Thông qua email</option>
                    <!--<option value="1">Gửi yêu cầu Reset mật khẩu mặc định lên Quản trị</option>-->
                </select>
            </p>
            <hr />
            <p>
                <input type="button" class="button" value="Bước tiếp theo" onclick="GetStepForget();" /> | 
                <a href="RenewPassword">Nhập mã Reset mật khẩu</a> | 
                <a href="Login" >Đăng nhập</a>
            </p>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DontAllow.aspx.cs" Inherits="QLNS.DontAllow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Truy cập bất hợp pháp</title>
    <link rel="icon" href="images/icons/icon.png" type="image/png" />
    <style type="text/css">
        .DontAllow{width: 576px; height: 407px; margin: auto;}
        .TitleDontAllow{font-size: 80px; color: White; text-align: center;}
        a, a:visited{color: White; font-size: 25px;}
    </style>
</head>
<body style="background-color: Black;">
    <form id="form1" runat="server">
        <div class="DontAllow">
            <img src="images/3dSkull.jpg" alt="Truy cập bất hợp pháp" />
        </div>
        <div class="TitleDontAllow">
            TRUY CẬP BẤT HỢP PHÁP
        </div>
        <div><a href="Index">Trang chủ</a></div>
    </form>
</body>
</html>

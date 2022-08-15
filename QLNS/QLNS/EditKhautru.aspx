<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditKhautru.aspx.cs" Inherits="QLNS.QLNS.EditKhautru" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Thêm khấu trừ lương nhân viên</title>
    <%--<link href="css/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 155px;}
        .fontbolder{font-weight: bold;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="notification attention png_bg">
            <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
            <div>
                Một khi đã lưu thì không được phép sửa
            </div>
        </div>
		<!-- Begin Khấu trừ lương nhân viên -->
		<div style="margin-left: 10px;">
			
		    <p>
		        <span class="spantitle">Mã NV: </span>
                <span class="fontbolder"><asp:Literal ID="ltrMaNV" runat="server" /></span>
		    </p>
		    
		    <p>
		        <span class="spantitle">Họ tên nhân viên: </span>
                <span class="fontbolder"><asp:Literal ID="ltrHoTen" runat="server" /></span>
		    </p>
		    
		    <p>
		        <span class="fontbolder">Khấu trừ lương tháng  <span style="color: #AC2C2C;"><asp:Literal ID="ltrBangluong" runat="server" /></span></span>
		    </p>
			
		    <p>
		        <span class="spantitle">Tên khấu trừ<span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtTenkhautru" runat="server" CssClass="text-input" Width="200px" MaxLength="49" />
                <span>
                    <asp:RequiredFieldValidator ID="reqTenkhautru"
                        runat="server"
                        ControlToValidate="txtTenkhautru"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                </span>
		    </p>
			
		    <p>
		        <span class="spantitle">Tiền khấu trừ (VNĐ)<span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtTienkhautru" runat="server" class="text-input fontbolder" Width="100px" MaxLength="12" Text="0"
                    onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
                <span>
                    <asp:RequiredFieldValidator ID="reqTienkhautru"
                        runat="server"
                        ControlToValidate="txtTienkhautru"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regTienkhautru"
                        runat="server"
                        ValidationExpression="[0-9,]+"
                        ControlToValidate="txtTienkhautru"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Sai định dạng" Display="Dynamic" />
                </span>
		    </p>
		</div>
		<!-- End Khấu trừ lương nhân viên -->
		
        <div class="clear"></div>
        
        <p style="text-align: left; margin-left: 10px;">
            <a href="Tamung">&laquo; Trở lại</a>
            |
            <asp:Button ID="btnSave" runat="server"
                Text="Lưu" ToolTip="Lưu" OnClientClick="confirm('Bạn có thực sự muốn lưu?')"
                CssClass="button" onclick="btnSave_Click" />
        </p>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLuonglamthem.aspx.cs" Inherits="QLNS.QLNS.EditLuonglamthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Thêm lương làm thêm nhân viên</title>
    <%--<link href="css/style.css" rel="stylesheet" type="text/css" />--%>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 190px;}
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
		<!-- Begin Lương làm thêm nhân viên -->
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
		        <span class="fontbolder">Lương làm thêm tháng  <span style="color: #AC2C2C;"><asp:Literal ID="ltrBangluong" runat="server" /></span></span>
		    </p>
			
		    <p>
		        <span class="spantitle">Tên lương làm thêm<span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtTenluonglamthem" runat="server" CssClass="text-input" Width="500px" MaxLength="249" /><br />
                <span>
                    <asp:RequiredFieldValidator ID="reqTenluonglamthem"
                        runat="server"
                        ControlToValidate="txtTenluonglamthem"
                        CssClass="input-notification error errortop"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                </span>
		    </p>
			
		    <p>
		        <span class="spantitle">Tiền lương làm thêm (VNĐ)<span class="isrequire">(*)</span>: </span>
                <asp:TextBox ID="txtTienluonglamthem" runat="server" class="text-input fontbolder" Width="100px" MaxLength="12" Text="0"
                    onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
                <span>
                    <asp:RequiredFieldValidator ID="reqTienluonglamthem"
                        runat="server"
                        ControlToValidate="txtTienluonglamthem"
                        CssClass="input-notification error errorleft"
                        ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regTienluonglamthem"
                        runat="server"
                        ValidationExpression="[0-9,]+"
                        ControlToValidate="txtTienluonglamthem"
                        CssClass="input-notification error errorimg"
                        ErrorMessage="Sai định dạng" Display="Dynamic" />
                </span>
		    </p>
		</div>
		<!-- End Lương làm thêm nhân viên -->
		
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

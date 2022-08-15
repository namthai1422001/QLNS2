<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPhongban.aspx.cs" Inherits="QLNS.QLNS.EditPhongban" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cập nhật phòng ban</title>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset style="margin: 10px auto 20px auto; width: 80%;"> 
            <p>
                <label>Tên<span class="isrequire">(*)</span></label>
                <asp:TextBox ID="txtName" runat="server" CssClass="text-input medium-input" MaxLength="49"/>
                <asp:RequiredFieldValidator ID="reqName"
                    runat="server"
                    ControlToValidate="txtName"
                    CssClass="input-notification error errorimg"
                    ErrorMessage="Không được để trống" Display="Dynamic" />
                
            </p>
            
            <p>
                <label>Số điện thoại</label>
                <asp:TextBox ID="txtDienthoai" runat="server" CssClass="text-input small-input" MaxLength="20"
                     onkeypress="return onlyNumbers(event)" />
                <asp:RegularExpressionValidator ID="regDienthoai"
                    runat="server"
                    ValidationExpression="[0-9]+"
                    ControlToValidate="txtDienthoai"
                    CssClass="input-notification error errorimg"
                    ErrorMessage="Không đúng định dạng" Display="Dynamic" />
                
            </p>
    		
		    <p>
                <label>Ghi chú</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="text-input large-input" MaxLength="99" />
            </p>
    		
            <p>
                <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="button"
                    onclick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="button" 
                    onclick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="button"
                    onclick="btnDelete_Click" 
                    OnClientClick="return confirm('Bạn có thực sự muốn xóa?');" />
                <a href="#" onclick="javascript:window.parent.tb_remove()" >Hủy</a>
            </p>
    		
        </fieldset>
        <div class="clear"></div>
        <asp:Panel ID="panelAudit" runat="server">
	        <div class="audit">
                <asp:Label ID="lblCreatedByUser" runat="server" Font-Bold="true" />
                 vào 
                <asp:Label ID="lblCreatedByDate" runat="server" Font-Bold="true" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="QLNS.Profile.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#index-nav a").addClass("current");
            $("#SearchBox").hide();
        });
    </script>
    <style type="text/css">
        #detail-profile{margin-left: 10px; margin-top: 10px; font-size: 14px;}
        #detail-profile .title-detail{display: inline-block; width: 140px;}
        #detail-profile .text-input{width: 300px; font-family: Arial,Helvetica,sans-serif; }
        #detail-profile .error-span{font-size: small; font-style: italic;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Thông tin tài khoản</h3>
		<ul class="content-box-tabs">
			<li><a href="#detail" class="default-tab">Thông tin cá nhân</a></li> <!-- href must be unique and match the id of target div -->
			<li><a href="#Roll">Quyền hạn trên hệ thống</a></li>
		</ul>
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="detail"> <!-- This is the target div. id must match the href of this div's tab -->
            <div id="detail-profile">
                <p>
                    <span class="title-detail">Tên đăng nhập: </span>
                    <b><asp:Literal ID="ltrUsername" runat="server" /></b>
                </p>
                <p>
                    <span class="title-detail">Mật khẩu: </span>
                    <span style="color: Red;">******</span>
                    <a href="ChangePass">Thay đổi</a>
                </p>
                <p>
                    <span class="title-detail">Email: </span>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="text-input" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="input-notification error errorleft"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Sai định dạng" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-detail">Họ tên: </span>
                    <asp:TextBox ID="txtFullname" runat="server" MaxLength="30" CssClass="text-input" />
                    <span class="error-span">
                        <asp:RequiredFieldValidator ID="reqFullname"
                            runat="server"
                            ControlToValidate="txtFullname"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                    </span>
                </p>
                <p>
                    <span class="title-detail">Số lần đăng nhập: </span>
                    <b><asp:Literal ID="ltrNumberOfLogin" runat="server" /></b>
                </p>
                <p>
                    <span class="title-detail">Đăng nhập vào: </span>
                    <b><asp:Literal ID="ltrLaterLogin" runat="server" /></b>
                </p>
                <p>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Lưu thay đổi" 
                        onclick="btnSave_Click" />
                </p>
            </div>
            
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->
        
        <div class="tab-content" id="Roll"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrInfor" runat="server" />
        <asp:Repeater ID="rpData" runat="server">
            <HeaderTemplate>
            <table>
                <thead>
                    <th> <b>STT</b> </th>
                    <th> <b>Tên</b> </th>
                    <th> <b>Ghi chú</b> </th>
                </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td> 
                        <%# Eval("STT").ToString() %>
                    </td>
                    <td> 
                        <%# Eval("Rollname") %>
                    </td>
                    <td> 
                        <%# Eval("Description")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </table>
            </FooterTemplate>
        </asp:Repeater>
            
            <div class="clear"></div>
            
        </div> <!-- End #Roll -->  
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

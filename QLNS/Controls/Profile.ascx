<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="QLNS.Controls.Profile" %>
<!-- Sidebar Profile links -->
<div id="profile-links">
    Chào, 
        <asp:HyperLink ID="hplEditProfile" runat="server" ToolTip="Thông tin tài khoản" NavigateUrl="~/Profile/Detail" />
       <!-- , bạn có 
        <a href="#messages" rel="modal" title="3 Messages">3 tin nhắn</a><br /
    <br />
    <a href="#" title="View the Site">View the Site</a>--> | 
    <a href="<%= ResolveUrl("~/Logout")%>" title="Thoát" onclick="return confirm('Bạn có thực sự muốn thoát?')">Thoát</a>
</div>
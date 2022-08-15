<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quanlytaikhoan.aspx.cs" Inherits="QLNS.Admin.Quanlytaikhoan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#quantrihethong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Quanlytaikhoan-nav").addClass("current");
            $("#SearchBox").hide();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Quản lý người dùng</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="padding-left: 10px;">
                <asp:HyperLink ID="hplAdd" runat="server" CssClass="button"
                    NavigateUrl="EditTaikhoan" style="margin-left: 5px;"
                    title="Thêm người dùng">Thêm người dùng</asp:HyperLink>
            </p>
			<asp:Repeater ID="rpData" runat="server" 
                onitemdatabound="rpData_ItemDataBound">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th style="font-size: 11px; font-weight: bold;">STT</th>
                        <th style="font-size: 11px; font-weight: bold;">Tên tài khoản</th>
                        <th style="font-size: 11px; font-weight: bold;">Họ tên</th>
                        <th style="font-size: 11px; font-weight: bold;">Email</th>
                        <th style="font-size: 11px; font-weight: bold;">Ghi chú</th>
                        <th style="font-size: 11px; font-weight: bold;">Khóa</th>
                        <th style="font-size: 11px; font-weight: bold;">Super</th>
                        <th style="font-size: 11px; font-weight: bold;">Quyền</th>
                        <th style="font-size: 11px; font-weight: bold;">Sửa</th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Username") %>
                        </td>
                        <td> 
                            <%# Eval("Fullname") %>
                        </td>
                        <td> 
                            <%# Eval("Email") %>
                        </td>
                        <td> 
                            <%# Eval("GhiChu") %>
                        </td>
                        <td>
                            <img alt='<%# (Eval("IsLock").ToString() == "True") ? "Khóa" : "Không khóa" %>'
                                title='<%# (Eval("IsLock").ToString() == "True") ? "Khóa" : "Không khóa" %>'
                                src='<%# String.Format("../images/icons/{0}.png", (Eval("IsLock").ToString() == "True") ? "lock" : "unlock") %>' />
                        </td>
                        <td>
                            <asp:Literal ID="ltrSuper" runat="server" />
                        </td>
                        <td>
                            <a href='<%# String.Format("Phanquyen@{0}",Eval("Username")) %>' title="Phân quyền">
                                <img src="../images/icons/pencil.png" alt="Phân quyền" />
                            </a>
                            <asp:Literal ID="ltrRoll" runat="server" />
                        </td>
                        <td style="width: 15px;"> 
                            <a href='<%# String.Format("EditTaikhoan@{0}",Eval("Username")) %>' title="Sửa thông tin">
                                <img src="../images/icons/pencil.png" alt="Sửa" />
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
    
            <div class="bulk-actions align-left" style="margin: 10px auto 5px 5px;">
                <asp:Label ID="lblCurrent" runat="server" Text="0" />/
                <asp:Label ID="lblTotalRowCount" runat="server" Text="0" />
            </div>
            <div class="pagination">
                <asp:HyperLink ID="hplFirstPage" runat="server" NavigateUrl="#" ToolTip="Trang đầu">&laquo; Đầu</asp:HyperLink>
                <asp:HyperLink ID="hplPreviousPage" runat="server" NavigateUrl="#" ToolTip="Trang trước">&laquo; Trước</asp:HyperLink>
                <asp:Repeater ID="rpPagination" runat="server" 
                    onitemdatabound="rpPagination_ItemDataBound">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplPage" runat="server" NavigateUrl="#">1</asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:HyperLink ID="hplNextPage" runat="server" NavigateUrl="#" ToolTip="Trang sau">Sau &raquo;</asp:HyperLink>
                <asp:HyperLink ID="hplLastPage" runat="server" NavigateUrl="#" ToolTip="Trang cuối">Cuối &raquo;</asp:HyperLink>
            </div> <!-- End .pagination -->
            
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

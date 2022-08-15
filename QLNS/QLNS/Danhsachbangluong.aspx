<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Danhsachbangluong.aspx.cs" Inherits="QLNS.QLNS.Danhsachbangluong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#luong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Danhsachbangluong-nav").addClass("current");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Danh sách bảng lương</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <asp:Literal ID="ltrInfor" runat="server" />
            <p style="margin-left: 5px;">
                <asp:Button ID="btnAdd" runat="server" CssClass="button"
                    Text="Tạo bảng lương" onclick="btnAdd_Click" />
            </p>
            
            <p style="margin-left: 5px;">
                Năm: <asp:DropDownList ID="cbNam" runat="server" Width="80px"
                    AutoPostBack="true" onselectedindexchanged="cbNam_SelectedIndexChanged" />
            </p>
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên bảng lương</b> </th>
                        <th> <b>Khóa</b> </th>
                        <th> <b>Hoàn thành</b> </th>
                        <%if (hdIsRoll.Value == "True")
                          { %>
                        <th></th>
                        <%} %>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# "Tháng " + Eval("Thang").ToString() + "/" + Eval("Nam").ToString() %>
                        </td>
                        <td> 
                            <img alt='<%# (Eval("IsLock").ToString() == "True") ? "Đã khóa" : "Mở khóa" %>'
                                title='<%# (Eval("IsLock").ToString() == "True") ? "Đã khóa" : "Mở khóa" %>'
                                src='<%# String.Format("../images/icons/{0}.png", (Eval("IsLock").ToString() == "True") ? "lock" : "unlock") %>' />
                        </td>
                        <td> 
                            <img alt='<%# (Eval("IsFinish").ToString() == "True") ? "Đã hoàn thành" : "Chưa hoàn thành" %>'
                                title='<%# (Eval("IsFinish").ToString() == "True") ? "Đã hoàn thành" : "Chưa hoàn thành" %>'
                                src='<%# String.Format("../images/icons/{0}.png", (Eval("IsFinish").ToString() == "True") ? "finished" : "unfinished") %>' />
                        </td>
                        <% if (hdIsRoll.Value == "True")
                           { %>
                        <td style="width: 15px;"> 
                            <a href='<%# String.Format("EditBangluong@{0}",Eval("Mabangluong")) %>' title="Sửa thông tin bảng bảng lương">
                                <img src="../images/icons/pencil.png" alt="Sửa" />
                            </a>
                        </td>
                        <%} %>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	<asp:HiddenField ID="hdIsRoll" runat="server" />
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Ngachluong.aspx.cs" Inherits="QLNS.QLNS.Ngachluong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/thickbox.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/thickbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#khac-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Ngachluong-nav").addClass("current");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Ngạch lương</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p> 
                <asp:HyperLink ID="hplAdd" runat="server"
                    NavigateUrl="EditNgachluong/add?height=230" style="margin-left: 5px;"
                    class="thickbox"
                    title="Thêm ngạch lương" >Thêm</asp:HyperLink>
            </p>
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên</b> </th>
                        <th> <b>Ghi chú</b> </th>
                        <% if (hdIsRoll.Value == "True")
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
                            <%# Eval("TenNgach") %>
                        </td>
                        <td> 
                            <%# Eval("Ghichu") %>
                        </td>
                        <% if (hdIsRoll.Value == "True")
                           { %>
                        <td style="width: 15px;"> 
                            <a href='<%# String.Format("EditNgachluong/edit/{0}?height=250",Eval("MaNgach")) %>' class="thickbox" title="Cập nhật ngạch lương">
                                <img src="../images/icons/pencil.png" alt="Edit" />
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

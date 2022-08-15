<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Khautru.aspx.cs" Inherits="QLNS.QLNS.Khautru" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../css/thickbox.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/thickbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#luong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Luongngay-nav").addClass("current");
        });
    </script>
    <style type="text/css">
        .spancol1{display: inline-block; width: 160px; font-weight: bold; margin-left: 10px;}
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3><asp:Literal ID="ltrh3" runat="server" /></h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p>
                <asp:HyperLink ID="hplAdd" runat="server"
                    style="margin-left: 5px;"
                    class="thickbox"
                    title="Thêm khấu trừ" >Thêm</asp:HyperLink>
                |
                <a href="Luongngay.aspx" >&laquo; Quay lại</a> |
                <b class="spancol1">Trạng thái bảng lương: </b>
                <b style="color: #AC2C2C;"><asp:Literal ID="ltrStatus" runat="server" /></b>
                <asp:Image ID="imgStatus" runat="server" style="width: 24px; height: 24px;" />
            </p>
			
			<asp:Repeater ID="rpData" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên khấu trừ</b> </th>
                        <th> <b>Số tiền (VNĐ)</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Tenkhautru") %>
                        </td>
                        <td> 
                            <%# Eval("Sotien") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
    
</div>
</asp:Content>

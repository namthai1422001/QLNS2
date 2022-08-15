<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Phanquyen.aspx.cs" Inherits="QLNS.Admin.Phanquyen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#quantrihethong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Quanlytaikhoan-nav").addClass("current");
            $("#SearchBox").hide();
        });
    </script>
    <style type="text/css">
        #divEditPhanquyen{margin-left: 20px; margin-top: 10px; font-size: large;}
        #divEditPhanquyen .title-roll{display: inline-block; font-size: large;}
        #divEditPhanquyen input{}
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
            <asp:Literal ID="ltrInfor" runat="server" Visible="false">
            <b style="margin-left: 20px;">Super User: </b>
                <img alt='Là Supper'
                    title='Là Supper'
                    src='../images/icons/super.png' />
            </asp:Literal>
             <div id="divEditPhanquyen">
        <asp:Repeater ID="rpData" runat="server" onitemdatabound="rpData_ItemDataBound">
            <ItemTemplate>
                <p>
                    <asp:Literal ID="ltrRoll" runat="server" />
                    <span class="title-roll"><%# Eval("STT").ToString() %> <%# Eval("Rollname") %></span>
                </p>
            </ItemTemplate>
        </asp:Repeater>
                <p>
                    <a href="Quanlytaikhoan" style="font-size: medium;">&laquo; Trở về</a>
                    <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" 
                        onclick="btnUpdate_Click" CssClass="button" Width="100px" />
                </p>
            </div>
            <div class="clear"></div>
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

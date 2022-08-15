<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="QLNS.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#index-nav a").addClass("current");
            $("#SearchBox").hide();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Trang chủ</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="text-align: right;">
                <asp:HyperLink ID="hplEdit" runat="server" title="Sửa trang chủ" NavigateUrl="EditIndex">
                    <img src="images/icons/paper_content_pencil_48.png" alt="Edit" height="32px" width="32px" />
                </asp:HyperLink>
            </p>
            <div style="margin-left: 5px; margin-right: 5px;">
                <asp:Literal ID="ltrContentOfIndex" runat="server" />
            </div>
            
            <div class="clear"></div>
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

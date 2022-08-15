<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetailDicongtac.aspx.cs" Inherits="QLNS.QLNS.DetailDicongtac" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Dicongtac-nav").addClass("current");
        });
    </script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 150px;}
        .fontbolder{font-weight: bold;}
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
            
            <!-- Begin Đi công tác -->
			<div style="margin-left: 10px;">
			
			    <p>
			        <span class="spantitle">Mã NV: </span>
			        <b><asp:Literal ID="ltrMaNV" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Họ tên: </span>
			        <b><asp:Literal ID="ltrHoTen" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Mã công tác: </span>
			        <b><asp:Literal ID="ltrMacongtac" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Về việc: </span>
                    <b><asp:Literal ID="ltrVeviec" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Lý do: </span>
                    <asp:Literal ID="ltrLydo" runat="server" />
			    </p>
			    
			    <p>
			        <span class="spantitle">Nơi công tác: </span>
                    <b><asp:Literal ID="ltrNoicongtac" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày đi: </span>
                    <b><asp:Literal ID="ltrNgaydi" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày về: </span>
                    <b><asp:Literal ID="ltrNgayve" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Tiền đi công tác (VNĐ): </span>
                    <b><asp:Literal ID="ltrTiendicongtac" runat="server" /></b>
			    </p>
    			
    			<p>
			        <span class="spantitle">Người ký: </span>
                    <asp:Literal ID="ltrHoTenNguoiky" runat="server" />
			    </p>
    			
			    <p>
			        <span class="spantitle">Chức vụ người ký: </span>
                    <asp:Literal ID="ltrChucvunguoiky" runat="server" />
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày ký: </span>
			        <b><asp:Literal ID="ltrNgayky" runat="server" /></b>
			    </p>
			    
			    <p>
                    <a href="Dicongtac">&laquo; Trở về</a>
			    </p>
			</div>
			<!-- End Đi công tác -->
			
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

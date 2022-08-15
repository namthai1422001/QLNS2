<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetailTamung.aspx.cs" Inherits="QLNS.QLNS.DetailTamung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Tamung-nav").addClass("current");
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
            
            <!-- Begin thông tin khen thưởng nhân viên -->
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
			        <span class="spantitle">Mã tạm ứng: </span>
			        <b><asp:Literal ID="ltrMatamung" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Ngày tạm ứng: </span>
                    <b><asp:Literal ID="ltrNgaytamung" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Tiền tạm ứng (VNĐ): </span>
                    <b><asp:Literal ID="ltrTientamung" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Lý do: </span>
                    <asp:Literal ID="ltrLydo" runat="server" />
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
                    <a href="Tamung">&laquo; Trở về</a>
			    </p>
			</div>
			<!-- End thông tin khen thưởng nhân viên -->
			
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

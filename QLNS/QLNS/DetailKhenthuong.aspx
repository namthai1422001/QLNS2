<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetailKhenthuong.aspx.cs" Inherits="QLNS.QLNS.DetailKhenthuong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Khenthuong-nav").addClass("current");
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
			        <span class="spantitle">Mã khen thưởng: </span>
			        <b><asp:Literal ID="ltrMakhenthuong" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Tên khen thưởng: </span>
                    <b><asp:Literal ID="ltrTenkhenthuong" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Hình thức khen thưởng: </span>
                    <b><asp:Literal ID="ltrHinhthuckhenthuong" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Lý do: </span>
                    <asp:Literal ID="ltrLydo" runat="server" />
			    </p>
			    
			    <p>
			        <span class="spantitle">Tiền khen thưởng (VNĐ): </span>
                    <b><asp:Literal ID="ltrTienkhenthuong" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày khen thưởng: </span>
                    <b><asp:Literal ID="ltrNgaykhenthuong" runat="server" /></b>
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
                    <a href="Khenthuong">&laquo; Trở về</a>
			    </p>
			</div>
			<!-- End thông tin khen thưởng nhân viên -->
			
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

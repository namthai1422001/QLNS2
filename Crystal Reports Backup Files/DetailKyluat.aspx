<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetailKyluat.aspx.cs" Inherits="QLNS.QLNS.DetailKyluat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Kyluat-nav").addClass("current");
        });
    </script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 180px;}
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
            
            <!-- Begin thông tin kỷ luật nhân viên -->
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
			        <span class="spantitle">Mã kỷ luật: </span>
			        <b><asp:Literal ID="ltrMakyluat" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Tên kỷ luật: </span>
                    <b><asp:Literal ID="ltrTenkyluat" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Hình thức kỷ luật: </span>
                    <b><asp:Literal ID="ltrHinhthuckyluat" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Ngày xảy ra sự việc: </span>
                    <b><asp:Literal ID="ltrNgayxayra" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Địa điểm xảy ra: </span>
                    <b><asp:Literal ID="ltrDiadiem" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Người chứng kiến: </span>
                    <b><asp:Literal ID="ltrNguoichungkien" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Mô tả sự việc: </span>
                    <b><asp:Literal ID="ltrMotasuviec" runat="server" /></b>
			    </p>
			    
			    <p>
			        <span class="spantitle">Nhân viên bị kỷ luật giải thích: </span>
                    <b><asp:Literal ID="ltrNguoibikyluatgiaithich" runat="server" /></b>
			    </p>
    			
			    <p>
			        <span class="spantitle">Lý do: </span>
                    <asp:Literal ID="ltrLydo" runat="server" />
			    </p>
    			
			    <p>
			        <span class="spantitle">Ngày kỷ luật: </span>
                    <b><asp:Literal ID="ltrNgaykyluat" runat="server" /></b>
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
                    <a href="Kyluat">&laquo; Trở về</a>
                    |
                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Xóa kỷ luật" 
                        OnClientClick="confirm('Bạn có thực sự muốn xóa?')" onclick="btnDelete_Click" />
			    </p>
			</div>
			<!-- End thông tin kỷ luật nhân viên -->
			
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

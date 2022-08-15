<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditBangchamcong.aspx.cs" Inherits="QLNS.QLNS.EditBangchamcong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../le-frog_Green/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../le-frog_Green/jquery.ui.button.css" rel="stylesheet" type="text/css" />

<%--    <script src="../scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../scripts/jquery.ui.widget.js" type="text/javascript"></script>--%>
    <script src="../scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../scripts/jquery.ui.datepicker-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#chamcong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Danhsachbangchamcong-nav").addClass("current");
            $("#<%= txtNgayky.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-28d",
                maxDate: "0"
            }); ;
        });
    </script>
    <style type="text/css">
        .spantitle{display: inline-block; width: 125px; margin-left: 5px;}
        .fontbolder{font-weight: bold;}
        .Title-Thongtin{display: inline-block; width: 220px; font-weight: bold;}
        #divThongtin {margin-left: 5px;}
        #divThongtin b{color: #AC2C2C;}
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
            <!-- Begin EditBangchamcong -->
            <div style="margin-left: 5px;">
                <p>
                    <span class="spantitle">Năm<span class="isrequire">(*)</span>: </span>
                    <asp:DropDownList ID="cbNam" runat="server" Width="80px"
                        AutoPostBack="true" OnSelectedIndexChanged="cbNam_SelectedIndexChanged" />
                    <span>
                        <asp:RegularExpressionValidator ID="regNam"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="validateAdd"
                            ControlToValidate="cbNam"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
                </p>
                
                <p>
                    <span class="spantitle">Tháng<span class="isrequire">(*)</span>: </span>
                    <asp:DropDownList ID="cbThang" runat="server" Width="80px" />
                    <span>
                        <asp:RegularExpressionValidator ID="regThang"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="validateAdd"
                            ControlToValidate="cbThang"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
                </p>
                
                <asp:Panel ID="pnlEdit" runat="server">
        		
	            <p>
	                <span class="spantitle">Chức vụ người ký<span class="isrequire">(*)</span>: </span>
	                <asp:DropDownList ID="cbChucvunguoiky" runat="server" Width="180px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="cbChucvunguoiky_SelectedIndexChanged" />
                    <span>
                        <asp:RegularExpressionValidator ID="regChucvunguoiky"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="validateFinished"
                            ControlToValidate="cbChucvunguoiky"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
	            </p>
        		
	            <p>
	                <span class="spantitle">Người ký<span class="isrequire">(*)</span>: </span>
	                <asp:DropDownList ID="cbNguoiky" runat="server" Width="180px" />
                    <span>
                        <asp:RegularExpressionValidator ID="regNguoiky"
                            runat="server"
                            ValidationExpression="[^0]([0-9NV]*)?"
                            ValidationGroup="validateFinished"
                            ControlToValidate="cbNguoiky"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                    </span>
	            </p>
        		
	            <p>
	                <span class="spantitle">Ngày ký<span class="isrequire">(*)</span>: </span>
                    <input id="txtNgayky" runat="server" class="text-input fontbolder" type="text" style="width: 80px;" />
	            </p>
        	    
	            <p>
	                <span class="spantitle">Trạng thái<span class="isrequire">(*)</span>: </span>
                    <b><asp:Literal ID="ltrStatus" runat="server" /></b>
	            </p>
        		
		        </asp:Panel>
        		
        	<asp:Panel ID="pnlThongtin" runat="server">
                <div id="divThongtin">
                    <p>
                        <span class="Title-Thongtin">Tổng giờ công :</span>
                        <b><asp:Literal ID="ltrTonggiocong" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng giờ nghỉ :</span>
                        <b><asp:Literal ID="ltrTonggionghi" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng giờ tăng ca thường :</span>
                        <b><asp:Literal ID="ltrTongtangcathuong" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng giờ tăng ca Chủ Nhật :</span>
                        <b><asp:Literal ID="ltrTongtangcachunhat" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng giờ tăng ca ngày nghỉ - lễ :</span>
                        <b><asp:Literal ID="ltrTongTangcanghile" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Người cập nhật sau cùng :</span>
                        <b><asp:Literal ID="ltrNguoicapnhatsaucung" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Ngày cập nhật :</span>
                        <b><asp:Literal ID="ltrNgaycapnhat" runat="server" /></b>
                    </p>
                </div>
            </asp:Panel>
        		
                <p style="margin-left: 5px;">
                    <asp:Button ID="btnAdd" runat="server" Text="Tạo" CssClass="button"
                        ValidationGroup="validateAdd"
                        OnClientClick="confirm('Bạn có thực sự muốn tạo bảng chấm công này ?');"
                        onclick="btnAdd_Click" />
                    <asp:Button ID="btnLock" runat="server" Text="Khóa" CssClass="button" 
                        onclick="btnLock_Click" />
                    <asp:Button ID="btnUnlock" runat="server" Text="Mở khóa" CssClass="button" 
                        onclick="btnUnlock_Click" />
                    <asp:Button ID="btnFinish" runat="server" Text="Hoàn thành" CssClass="button"
                        ValidationGroup="validateFinished"
                        onclick="btnFinish_Click" 
                        OnClientClick="confirm('Khi hoàn thành bảng chấm công này sẽ không được phép chỉnh sửa ?');" />
                    <a href="Danhsachbangchamcong" >Quay lại</a>
                </p>
                
            
            </div><!-- End EditBangchamcong -->
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditBangluong.aspx.cs" Inherits="QLNS.QLNS.EditBangluong" %>
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
            $("#luong-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Danhsachbangluong-nav").addClass("current");
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
        .spantitle{display: inline-block; width: 125px;}
        .fontbolder{font-weight: bold;}
        #divThongtin {margin-left: 5px;}
        #divThongtin .Title-Thongtin{display: inline-block; width: 160px;}
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
            <asp:Literal ID="ltrInfor" runat="server" />
        
            <!-- Begin EditBangluong -->
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
                        <span class="Title-Thongtin">Tổng tiền BHXH:</span>
                        <b><asp:Literal ID="ltrBHXH" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền BHYT:</span>
                        <b><asp:Literal ID="ltrBHYT" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền BHTN :</span>
                        <b><asp:Literal ID="ltrBHTN" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền phí công đoàn:</span>
                        <b><asp:Literal ID="ltrPhicongdoan" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền lương ngày:</span>
                        <b><asp:Literal ID="ltrTienluongngay" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng thuế TNCN:</span>
                        <b><asp:Literal ID="ltrThueTNCN" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền tạm ứng:</span>
                        <b><asp:Literal ID="ltrTamung" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền làm thêm:</span>
                        <b><asp:Literal ID="ltrLamthem" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền khấu trừ:</span>
                        <b><asp:Literal ID="ltrKhautru" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền thưởng:</span>
                        <b><asp:Literal ID="ltrThuong" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền thực lãnh:</span>
                        <b><asp:Literal ID="ltrThuclanh" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền tăng ca thường:</span>
                        <b><asp:Literal ID="ltrTangcathuong" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền tăng ca chủ nhật:</span>
                        <b><asp:Literal ID="ltrTangcachunhat" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền tăng ca nghỉ - lễ:</span>
                        <b><asp:Literal ID="ltrTangcanghile" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Tổng tiền tăng ca:</span>
                        <b><asp:Literal ID="ltrTongTangca" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Người cập nhật sau cùng:</span>
                        <b><asp:Literal ID="ltrNguoicapnhat" runat="server" /></b>
                    </p>
                    <p>
                        <span class="Title-Thongtin">Ngày cập nhật:</span>
                        <b><asp:Literal ID="ltrNgaycapnhat" runat="server" /></b>
                    </p>
                </div>
            </asp:Panel>
        		
                <p>
                    <asp:Button ID="btnAdd" runat="server" Text="Tạo" CssClass="button"
                        ValidationGroup="validateAdd"
                        OnClientClick="confirm('Bạn có thực sự muốn tạo bảng lương này ?');"
                        onclick="btnAdd_Click" />
                    <asp:Button ID="btnLock" runat="server" Text="Khóa" CssClass="button" 
                        onclick="btnLock_Click" />
                    <asp:Button ID="btnUnlock" runat="server" Text="Mở khóa" CssClass="button" 
                        onclick="btnUnlock_Click" />
                    <asp:Button ID="btnFinish" runat="server" Text="Hoàn thành" CssClass="button"
                        ValidationGroup="validateFinished"
                        onclick="btnFinish_Click" 
                        OnClientClick="confirm('Khi hoàn thành bảng lương này sẽ không được phép chỉnh sửa ?');" />
                    <a href="Danhsachbangluong" >Quay lại</a>
                </p>
            </div><!-- End EditBangluong -->
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>
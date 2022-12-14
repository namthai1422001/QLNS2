<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditNgachbacnhanvien.aspx.cs" Inherits="QLNS.QLNS.EditNgachbacnhanvien" %>
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
            $("#<%= txtNgayky.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-28d",
                maxDate: "0"
            }); ;
        });
	</script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
        });
    </script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    
    <style type="text/css">
        .leftcol
        {
            float: left;
            position: relative;
            width: 35%;
            min-width: 320px;
            border: solid 1px #AC2C2C;
            padding: 20px 4px 10px;
            border-radius: 5px;
            moz-border-radius: 5px;
            margin: 10px auto 4px 10px;
        }
        .leftcol h5
        {
            position: absolute;
            top: -8px;
            left: 20px;
            background-color: #FFF;
            padding-left: 2px;
            padding-right: 2px;
        }
        
        .rightcol
        {
            float: right;
            position: relative;
            width: 55%;
            min-width: 350px;
            border: solid 1px #4A9600;
            padding: 20px 4px 10px;
            border-radius: 5px;
            moz-border-radius: 5px;
            margin: 10px 10px 4px auto;
        }
        .rightcol h5
        {
            position: absolute;
            top: -8px;
            right: 20px;
            background-color: #FFF;
            padding-left: 2px;
            padding-right: 2px;
        }
        
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 800px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Thay đổi Ngạch - Bậc lương cho nhân viên
            <asp:Literal ID="lblHoten" runat="server" /></h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
			
			<div class="notification information png_bg">
                <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
                <div>
                    <span style="color: #AC2C2C;">Lưu ý:</span><br />
                    <span style="color: #AC2C2C;">- Tính lương tháng <b>trước tháng hiện tại</b> trước khi thực hiện thay đổi.</span><br />
                    <span style="color: #AC2C2C;">- <b>Ngày bắt đầu có hiệu lực</b> sẽ chính là <b>tháng hiện tại</b>.</span><br />
                    <span style="color: #AC2C2C;">- <b>Bậc lương</b> sẽ được <b>thay đổi ngay sau khi nhấn nút lưu</b>.</span>
                    
                </div>
            </div>
            <asp:Literal ID="ltrInfor" runat="server" />
			
			<!-- Begin Thông tin -->
			<div class="leftcol">
                <h5>Thông tin bậc lương hiên tại</h5>
                <table>
                    <tr style="width: 120px;" >
                        <td>
                            Ngạch:</td>
                        <td>
                            <asp:Label ID="lblNgach" runat="server" Font-Bold="True" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bậc: 
                        </td>
                        <td>
                            <asp:Label ID="lblBac" runat="server" Font-Bold="True" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Hệ số lương:</td>
                        <td>
                            <asp:Label ID="lblHeso" runat="server" Font-Bold="True" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Áp dụng từ: 
                        </td>
                        <td>
                            <asp:Label ID="lblNgayapdung" runat="server" ForeColor="#AC2C2C" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Lý do thay đổi: 
                        </td>
                        <td>
                            <asp:Label ID="lblLyDo" runat="server" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Người ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNguoiky" runat="server" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chức vụ người ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblChucvunguoiky" runat="server" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ngày ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNgayky" runat="server" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="rightcol">
                <h5>Thông tin bậc lương mới</h5>
                <table>
                    <tr>
                        <td style="width: 130px;" >
                            Ngạch:</td>
                        <td style="width: 172px;">
                            <asp:DropDownList ID="cbNgach" runat="server" Width="180px" AutoPostBack="True" 
                                onselectedindexchanged="cbNgach_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="regNgach"
                                runat="server"
                                ValidationExpression="[^0]([0-9]*)?"
                                ControlToValidate="cbNgach"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 130px;" >
                            Bậc:</td>
                        <td style="width: 172px;">
                            <asp:DropDownList ID="cbBac" runat="server" Width="180px" AutoPostBack="True" 
                                onselectedindexchanged="cbBac_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="regBac"
                                runat="server"
                                ValidationExpression="[^0]([0-9]*)?"
                                ControlToValidate="cbBac"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Hệ số:</td>
                        <td>
                            <asp:Label ID="lblHesoMoi" runat="server" Font-Bold="True" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td>
                            Bắt đầu áp dụng từ:
                        </td>
                        <td>
                            <asp:Label ID="lblBatdauapdung" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Lý do thay đổi:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLyDo" runat="server" TextMode="MultiLine" Rows="4" MaxLength="99" Width="180px" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqLyDo"
                                runat="server"
                                ControlToValidate="txtLyDo"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Chức vụ người ký: 
                        </td>
                        <td>
                            <asp:DropDownList ID="cbChucvunguoiky" runat="server" Width="180px" 
                                AutoPostBack="true" 
                                onselectedindexchanged="cbChucvunguoiky_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="regChucvunguoiky"
                                runat="server"
                                ValidationExpression="[^0]([0-9]*)?"
                                ControlToValidate="cbChucvunguoiky"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Người ký: 
                        </td>
                        <td>            
                            <asp:DropDownList ID="cbNguoiky" runat="server" Width="180px" />
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="regNguoiky"
                                runat="server"
                                ValidationExpression="[^0]([0-9NV]*)?"
                                ControlToValidate="cbNguoiky"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng chọn" Display="Dynamic" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Ngày ký: 
                        </td>
                        <td>
                            <input type="text" id="txtNgayky" runat="server" class="text-input" readonly="readonly" style="width: 96px;" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ghi chú: 
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" Rows="4" MaxLength="99" />
                        </td>
                    </tr>
                    
                </table>
            </div>
			<!-- End Thông tin -->
			
            <div class="clear"></div>
            
            <p style="text-align: right; margin-right: 10px;">
                <a href="javascript:history.go(-1)">Trở lại</a>
                |
                <asp:Button ID="btnSave" runat="server"
                    Text="Lưu" ToolTip="Lưu"
                    CssClass="button" onclick="btnSave_Click" />
            </p>
            
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

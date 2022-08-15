<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditCauhinhcongthuc.aspx.cs" Inherits="QLNS.QLNS.EditCauhinhcongthuc" %>
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
            $("#caidat-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Congthuctinhluong-nav").addClass("current");
            $("#<%= txtBHXH.ClientID %>").focus();
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
		
        <h3>Cài đặt công thức tính lương</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            
            <div class="notification attention png_bg">
                <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
                <div>
                    Tất cả dữ liệu là bắt buộc nhập<br />
                    Dữ liệu là (%) thì không được nhập lớn hơn 99<br />
                    Phần thập phân chỉ được nhập tối đa 3 số
                </div>
            </div>
            
            <div class="notification information png_bg">
                <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
                <div>
                    Chỉ được cài đặt công thức khi đã có đầy đủ quyết định thi hành, có thông tin người ký, ngày ký.<br />
                    <span style="color: #AC2C2C;">Lưu ý:</span><br />
                    <span style="color: #AC2C2C;">- Tính lương tháng <b>trước tháng hiện tại</b> trước khi thực hiện thay đổi.</span><br />
                    <span style="color: #AC2C2C;">- <b>Ngày bắt đầu có hiệu lực</b> sẽ chính là <b>tháng hiện tại</b>.</span><br />
                    <span style="color: #AC2C2C;">- <b>Công thức tính lương</b> sẽ được <b>thay đổi ngay sau khi nhấn nút lưu cài đặt</b>.</span>
                    
                </div>
            </div>
            
            <!-- Begin Bảo hiểm -->
			<div class="leftcol">
			    <h5>Bảo hiểm</h5>
			    <table>
			        <tr>
			            <td style="width: 145px;">
			                Xã hội: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHXH" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Y tế: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHYT" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Thất nghiệp: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHTN" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng BHXH tối đa: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHXHMax" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Bảo hiểm</h5>
			    <table>
			        <tr>
			            <td style="width: 230px;">
			                Xã hội (%)<span class="isrequire">(*)</span>:
			            </td>
			            <td style="width: 172px;">
                            <asp:TextBox ID="txtBHXH" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqBHXH"
                                runat="server"
                                ControlToValidate="txtBHXH"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regBHXH"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtBHXH"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Y tế (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtBHYT" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqBHYT"
                                runat="server"
                                ControlToValidate="txtBHYT"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regBHYT"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtBHYT"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Thất nghiệp (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtBHTN" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqBHTN"
                                runat="server"
                                ControlToValidate="txtBHTN"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regBHTN"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtBHTN"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng BHXH tối đa (VNĐ)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtBHXHMax" runat="server" CssClass="text-input" style="width: 170px;" MaxLength="13"
                                onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqBHXHMax"
                                runat="server"
                                ControlToValidate="txtBHXHMax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regBHXHMax"
                                runat="server"
                                ValidationExpression="[0-9,]+"
                                ControlToValidate="txtBHXHMax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			    </table>
			</div>
			<!-- End Bảo hiểm -->
			
			<div class="clear"></div>
			
			<!-- Begin Thuế TNCN -->
			<div class="leftcol">
			    <h5>Thuế thu nhập cá nhân</h5>
			    <table>
			        <tr>
			            <td style="width: 200px;">
			                Mức chịu thuế thu nhập: 
			            </td>
			            <td>
                            <asp:Label ID="lblMinIncomeTax" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;VNĐ
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chi cho người phụ thuộc: 
			            </td>
			            <td>
                            <asp:Label ID="lblChichonguoiphuthuoc" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Thuế thu nhập cá nhân</h5>
			    <table>
			        <tr>
			            <td style="width: 230px;">
			                Mức chịu thuế thu nhập (VNĐ)<span class="isrequire">(*)</span>: 
			            </td>
			            <td style="width: 172px;">
                            <asp:TextBox ID="txtMinIncomeTax" runat="server" CssClass="text-input" style="width: 170px;" MaxLength="13"
                                onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqMinIncomeTax"
                                runat="server"
                                ControlToValidate="txtMinIncomeTax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regMinIncomeTax"
                                runat="server"
                                ValidationExpression="[0-9,]+"
                                ControlToValidate="txtMinIncomeTax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chi cho người phụ thuộc (VNĐ)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtChichonguoiphuthuoc" runat="server" CssClass="text-input" style="width: 170px;" MaxLength="13"
                                onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqChichonguoiphuthuoc"
                                runat="server"
                                ControlToValidate="txtChichonguoiphuthuoc"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regChichonguoiphuthuoc"
                                runat="server"
                                ValidationExpression="[0-9,]+"
                                ControlToValidate="txtChichonguoiphuthuoc"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			    </table>
			</div>
			<!-- End Thuế TNCN -->
			
			<div class="clear"></div>
			
			<!-- Begin Tăng ca -->
			<div class="leftcol">
			    <h5>Tăng ca</h5>
			    <table>
			        <tr>
			            <td style="width: 145px;">
			                Thường: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcaThuong" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chủ Nhật: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcaChunhat" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Ngày nghỉ - lễ: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcanghile" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Tăng ca</h5>
			    <table>
			        <tr>
			            <td style="width: 230px;">
			                Thường (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td style="width: 172px;">
                            <asp:TextBox ID="txtTangcaThuong" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqTangcaThuong"
                                runat="server"
                                ControlToValidate="txtTangcaThuong"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regTangcaThuong"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtTangcaThuong"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chủ Nhật (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtTangcaChunhat" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqTangcaChunhat"
                                runat="server"
                                ControlToValidate="txtTangcaChunhat"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regTangcaChunhat"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtTangcaChunhat"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Ngày nghỉ - lễ (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtTangcaNghile" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqTangcaNghile"
                                runat="server"
                                ControlToValidate="txtTangcaNghile"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regTangcaNghile"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtTangcaNghile"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			    </table>
			</div>
			<!-- End Tăng ca -->
			
			<div class="clear"></div>
			
			<!-- Begin Công đoàn -->
			<div class="leftcol">
			    <h5>Công đoàn</h5>
			    <table>
			        <tr>
			            <td style="width: 200px;">
			                Phí công đoàn: 
			            </td>
			            <td>
                            <asp:Label ID="lblPhicongdoan" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng phí công đoàn tối đa: 
			            </td>
			            <td>
                            <asp:Label ID="lblPhicongdoanMax" runat="server" Font-Bold="true" Text="Chưa cài đặt" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Công đoàn</h5>
			    <table>
			        <tr>
			            <td style="width: 230px;">
			                Phí công đoàn (%)<span class="isrequire">(*)</span>: 
			            </td>
			            <td style="width: 172px;">
                            <asp:TextBox ID="txtPhicongdoan" runat="server" CssClass="text-input" style="width: 96px;" MaxLength="5" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqPhicongdoan"
                                runat="server"
                                ControlToValidate="txtPhicongdoan"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regPhicongdoan"
                                runat="server"
                                ValidationExpression="([0-9]?[0-9]?[.][0-9]?[0-9]?[0-9]?)|([0-9]?[0-9])"
                                ControlToValidate="txtPhicongdoan"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng phí công đoàn tối đa (VNĐ)<span class="isrequire">(*)</span>: 
			            </td>
			            <td>
                            <asp:TextBox ID="txtPhicongdoanMax" runat="server" CssClass="text-input" style="width: 170px;" MaxLength="13"
                                onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
			            </td>
			            <td>
			                <asp:RequiredFieldValidator ID="reqPhicongdoanMax"
                                runat="server"
                                ControlToValidate="txtPhicongdoanMax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regPhicongdoanMax"
                                runat="server"
                                ValidationExpression="[0-9,]+"
                                ControlToValidate="txtPhicongdoanMax"
                                CssClass="input-notification error errorimg"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
			            </td>
			        </tr>
			    </table>
			</div>
			<!-- End Công đoàn -->
			
			<div class="clear"></div>
			
			<!-- Begin Thông tin -->
			<div class="leftcol">
                <h5>Thông tin</h5>
                <table>
                    <tr>
                        <td style="width: 120px;" >
                            Người ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNguoiky" runat="server" Font-Bold="true" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chức vụ: 
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
                    <tr>
                        <td>
                            Bắt đầu áp dụng từ:
                        </td>
                        <td>
                            <asp:Label ID="lblNgayapdung" runat="server" Font-Bold="true" ForeColor="#AC2C2C" Text="Chưa cài đặt" />
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="rightcol">
                <h5>Thông tin</h5>
                <table>
                    <tr>
                        <td style="width: 230px;" >
                            Chức vụ người ký<span class="isrequire">(*)</span>: 
                        </td>
                        <td style="width: 172px;">
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
                            Người ký<span class="isrequire">(*)</span>: 
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
                            Ngày ký<span class="isrequire">(*)</span>: 
                        </td>
                        <td>
                            <input type="text" id="txtNgayky" runat="server" class="text-input" readonly="readonly" style="width: 96px;" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bắt đầu áp dụng từ<span class="isrequire">(*)</span>:
                        </td>
                        <td>
                            <asp:Label ID="lblBatdauapdung" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
			<!-- End Thông tin -->
			
			<div class="clear"></div>
			
			<!-- Begin Mô tả -->
			<div class="leftcol">
                <h5>Mô tả về công thức</h5>
                <asp:Label ID="lblMota" runat="server" Text="<b>Chưa cài đặt</b>" />
            </div>
            
            <div class="rightcol" style="height: 370px;">
                <h5>Mô tả về công thức<span class="isrequire">(*)</span></h5>
                <asp:TextBox ID="txtDescription" runat="server"
                    TextMode="MultiLine" Rows="20" />
                <br />
                <asp:RequiredFieldValidator ID="reqDescription"
                    runat="server"
                    ControlToValidate="txtDescription"
                    CssClass="input-notification error errorimg"
                    ErrorMessage="Vui lòng nhập" Display="Dynamic" />
            </div>
			<!-- End Mô tả -->
			
            <div class="clear"></div>
            
            <p style="text-align: right; margin-right: 10px;">
                <a href="javascript:history.go(-1)">Trở lại</a>
                |
                <asp:Button ID="btnSave" runat="server"
                    Text="Lưu cài đặt" ToolTip="Lưu cài đặt"
                    CssClass="button" onclick="btnSave_Click" />
            </p>
            
        </div> <!-- End #tab1 -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

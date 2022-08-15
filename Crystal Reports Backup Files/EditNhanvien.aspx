<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditNhanvien.aspx.cs" Inherits="QLNS.QLNS.EditNhanvien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.theme.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.core.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.datepicker.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.button.css")%>" rel="stylesheet" type="text/css" />

<%--    <script src="<%= ResolveUrl("~/scripts/jquery.ui.core.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/scripts/jquery.ui.widget.js")%>" type="text/javascript"></script>--%>
    <script src="<%= ResolveUrl("~/scripts/jquery.ui.datepicker.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/scripts/jquery.ui.datepicker-vi.js")%>" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= txtNgaysinh.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-80Y",
                maxDate: "-15Y",
                changeMonth: true,
                changeYear: true,
                yearRange: 'c-65'
            });
            $("#<%= txtNgaycap.ClientID %>").datepicker({
                showWeek: true,
                showAnim: "slideDown",
                firstDay: 1,
                minDate: "-60Y",
                maxDate: "0Y",
                changeMonth: true,
                changeYear: true,
                yearRange: 'c-65'
            });
        });
	</script>
    <script src="<%= ResolveUrl("~/scripts/inputnumber.js")%>" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
        });
    </script>
    <style type="text/css">
        .leftcol
        {
            float: left;
            position: relative;
            width: 300px;
            border: solid 1px #AC2C2C;
            padding: 20px 4px 10px;
            border-radius: 5px;
            moz-border-radius: 5px;
            margin: 10px 10px 4px 10px;
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
        .leftcol p span.errortop
        {
        	margin-left: 180px;
        }
        
        .rightcol
        {
            float: left;
            position: relative;
            width: 470px;
            border: solid 1px #AC2C2C;
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
<div class="content-box" style="min-width: 900px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Cập nhật thông tin nhân viên</h3>
		<ul class="content-box-tabs">
			<li><a href="#thongtincanhan" class="default-tab">Thông tin cá nhân</a></li> <!-- href must be unique and match the id of target div -->
			<li><a href="#congviec">Công việc</a></li>
			<li><a href="#phucap">Được hưởng phụ cấp</a></li>
			<li><a href="#nguoithan">Người thân</a></li>
		</ul>
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="thongtincanhan"> <!-- This is the target div. id must match the href of this div's tab -->
            
            <asp:RegularExpressionValidator ID="regHinhnhanvien" runat="server"
                ValidationExpression="^.+(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$"
                ValidationGroup="thongtincanhan"
                ControlToValidate="uploadHinhnhanvien"
                ErrorMessage="<div class='notification error png_bg'><div>Chỉ nhận hình định dạng .png,.jpg,.bmp và .gif</div></div>"
                Display="Dynamic" />
            <div id="hinhnhanvien">
                <asp:Image ID="imgNhanvien" runat="server" AlternateText="Hình đã bị xóa" />
                <asp:FileUpload ID="uploadHinhnhanvien" runat="server" />
            </div>
			<div id="hinhnhanvien-right">
			    <p>
			        <sub class="col-1">Mã nhân viên <span class="isrequire">(*)</span>:</sub>
                    <asp:TextBox ID="txtMaNV" runat="server" Enabled="false" CssClass="text-input col-1" Width="150px" />
			    </p>
			    <p>
			        <sub class="col-1">Họ nhân viên <span class="isrequire">(*)</span>:</sub>
                    <asp:TextBox ID="txtHoNV" runat="server" CssClass="text-input col-1" Width="150px" MaxLength="30" />
                    <asp:RequiredFieldValidator ID="reqHoNV"
                        runat="server"
                        ControlToValidate="txtHoNV"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errorleft col-1"
                        Display="Static" />
                    <sub class="col-2">Tên <span class="isrequire">(*)</span>:</sub>
                    <asp:TextBox ID="txtTenNV" runat="server" CssClass="text-input col-2" Width="150px" MaxLength="10" />
                    <asp:RequiredFieldValidator ID="reqTenNV"
                        runat="server"
                        ControlToValidate="txtTenNV"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errorleft col-2"
                        Display="Static" />
                    <sub class="col-3">Nữ: <asp:CheckBox ID="chkGender" runat="server" /></sub>
			    </p>
			    <p>
			        <sub class="col-1">Tên thường gọi:</sub>
                    <asp:TextBox ID="txtTenthuonggoi" runat="server" CssClass="text-input col-1" Width="150px" MaxLength="40" />
                    <sub class="col-2">Kết hôn: <asp:CheckBox ID="chkKethon" runat="server"/></sub>
			    </p>
			    <p>
			        <sub class="col-1">Ngày sinh <span class="isrequire">(*)</span>:</sub>
			        <input type="text" id="txtNgaysinh" runat="server" class="text-input col-1" readonly="readonly" style="width: 150px;" />
			        <asp:RequiredFieldValidator ID="reqNgaysinh"
                        runat="server"
                        ControlToValidate="txtNgaysinh"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errorleft col-1"
                        Display="Static" />
			        <sub class="col-2">Nơi sinh <span class="isrequire">(*)</span>:</sub>
			        <asp:TextBox ID="txtNoisinh" runat="server" CssClass="text-input col-2" Width="150px" MaxLength="50" />
			        <asp:RequiredFieldValidator ID="reqNoisinh"
                        runat="server"
                        ControlToValidate="txtNoisinh"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errorleft col-2"
                        Display="Static" />
			    </p>
			    <p>
			        <sub class="col-1">Địa chỉ <span class="isrequire">(*)</span>:</sub>
			        <asp:TextBox ID="txtDiachi" runat="server" CssClass="text-input col-1" Width="550px" MaxLength="100" />
			        <asp:RequiredFieldValidator ID="reqDiachi"
                        runat="server"
                        ControlToValidate="txtDiachi"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errorleft col-3"
                        Display="Static" />
			    </p>
			    <p>
			        <sub class="col-1">Tạm trú:</sub>
			        <asp:TextBox ID="txtTamtru" runat="server" CssClass="text-input col-1" Width="550px" MaxLength="100" />
			    </p>
			</div><!--End #hinhnhanvien-right-->
			
			<div class="clear"></div>
			<table>
			    <tr>
			        <td style="width: 130px;">
			            Điện thoại di động: 
			        </td>
			        <td  style="width: 270px;">
			            <asp:TextBox ID="txtDienthoaididong" runat="server" CssClass="text-input" Width="150px" MaxLength="20"
			                onkeypress="return onlyNumbers(event)" />
			        </td>
			        <td>
			        </td>
			    </tr>
			    <tr>
			        <td>
			            Điện thoại nhà: 
			        </td>
			        <td>
			            <asp:TextBox ID="txtDienthoainha" runat="server" CssClass="text-input" Width="150px" MaxLength="20"
			                onkeypress="return onlyNumbers(event)" />
			        </td>
			        <td>
			        </td>
			    </tr>
			    <tr>
			        <td>
			            Email: 
			        </td>
			        <td>
			            <asp:TextBox ID="txtEmail" runat="server" CssClass="text-input" Width="250px" MaxLength="50" />
			        </td>
			        <td>
			            <asp:RegularExpressionValidator ID="regEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ValidationGroup="thongtincanhan"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Sai định dạng"
                            Display="Static" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  />
			        </td>
			    </tr>
			</table>
			<table>
			    <tr>
			        <td style="width: 105px;">
			            Quốc tịch <span class="isrequire">(*)</span>:
			        </td>
			        <td style="width: 155px;">
                        <asp:DropDownList ID="cbQuoctich" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td style="width: 120px;">
			            <asp:RegularExpressionValidator ID="regQuoctich"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="thongtincanhan"
                            ControlToValidate="cbQuoctich"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
			        </td>
			        <td style="width: 130px;">
			            Dân tộc <span class="isrequire">(*)</span>:
			        </td>
			        <td style="width: 155px;">
			            <asp:DropDownList ID="cbDantoc" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td style="width: 120px;">
			            <asp:RegularExpressionValidator ID="regDantoc"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="thongtincanhan"
                            ControlToValidate="cbDantoc"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
			        </td>
			    </tr>
			    <tr>
			        <td>
			            Tôn giáo <span class="isrequire">(*)</span>:
			        </td>
			        <td>
			            <asp:DropDownList ID="cbTongiao" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td>
			            <asp:RegularExpressionValidator ID="regTongiao"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="thongtincanhan"
                            ControlToValidate="cbTongiao"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
			        </td>
			        <td>
			            Học vấn <span class="isrequire">(*)</span>:
			        </td>
			        <td>
			            <asp:DropDownList ID="cbBangcap" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td>
			            <asp:RegularExpressionValidator ID="regBangcap"
                            runat="server"
                            ValidationExpression="[^0]([0-9]*)?"
                            ValidationGroup="thongtincanhan"
                            ControlToValidate="cbBangcap"
                            CssClass="input-notification error errorleft"
                            ErrorMessage="Vui lòng chọn" Display="Dynamic" />
			        </td>
			    </tr>
			    <tr>
			        <td>
			            Ngoại ngữ:
			        </td>
			        <td>
			            <asp:DropDownList ID="cbNgoaingu" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td></td>
			        <td>
			            Chuyên môn:
			        </td>
			        <td>
			            <asp:DropDownList ID="cbChuyenmon" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td></td>
			    </tr>
			    <tr>
			        <td>
			            Tin học:
			        </td>
			        <td>
			            <asp:DropDownList ID="cbTinhoc" runat="server" Width="160px">
                        </asp:DropDownList>
			        </td>
			        <td></td>
			        <td></td>
			        <td></td>
			        <td></td>
			    </tr>
			</table>
			
			<div class="leftcol">
			    <h5>Chứng minh nhân dân</h5>
			    <p>
			        Số CMND <span class="isrequire">(*)</span>:
			        <asp:TextBox ID="txtSoCMNN" runat="server" CssClass="text-input" style="float:right; width: 160px;"
			            MaxLength="9"
			            onkeypress="return onlyNumbers(event)"/>
                    <br />
			        <small>Phải nhập đủ 9 số</small>
			        <asp:RequiredFieldValidator ID="reqSoCMNN"
                        runat="server"
                        ControlToValidate="txtSoCMNN"
                        ValidationGroup="thongtincanhan"
                        ErrorMessage="Vui lòng nhập"
                        CssClass="input-notification error errortop"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regSoCMNN"
                        runat="server"
                        ControlToValidate="txtSoCMNN"
                        ValidationExpression="[0-9]{9}"
                        ErrorMessage="Sai định dạng"
                        ValidationGroup="thongtincanhan"
                        CssClass="input-notification error errortop"
                        Display="Dynamic" />
			    </p>
			    <p>
			        Ngày cấp <span class="isrequire">(*)</span>:
			        <input type="text" id="txtNgaycap" runat="server" class="text-input" readonly="readonly" style="width: 160px; float:right;" />
			        <br />
			        <asp:RequiredFieldValidator ID="reqNgaycap"
                        runat="server"
                        ControlToValidate="txtNgaycap"
                        ValidationGroup="thongtincanhan"
                        ErrorMessage="Vui lòng chọn"
                        CssClass="input-notification error errortop"
                        Display="Dynamic" />
			    </p>
			    <p>
			        Nơi cấp <span class="isrequire">(*)</span>:
			        <asp:TextBox ID="txtNoicap" runat="server" CssClass="text-input" style="float:right; width: 160px;"/>
			        <br />
			        <asp:RequiredFieldValidator ID="reqNoicap"
                        runat="server"
                        ControlToValidate="txtNoicap"
                        ValidationGroup="thongtincanhan"
                        ErrorMessage="Vui lòng nhập"
                        CssClass="input-notification error errortop"
                        Display="Dynamic" />
			    </p>
			</div>
			<div class="rightcol">
			    <h5>Tình trạng sức khỏe</h5>
			    <p>
			        Tình trạng sức khỏe: 
			        <asp:TextBox ID="txtTinhtrangsuckhoe" runat="server" CssClass="text-input" Width="270px"/>
			    </p>
			    <p>
			        Chiều cao: 
                    <asp:DropDownList ID="cbChieucao" runat="server" Width="60px">
                        <asp:ListItem Value="140"></asp:ListItem>
                        <asp:ListItem Value="141"></asp:ListItem>
                        <asp:ListItem Value="142"></asp:ListItem>
                        <asp:ListItem Value="143"></asp:ListItem>
                        <asp:ListItem Value="144"></asp:ListItem>
                        <asp:ListItem Value="145"></asp:ListItem>
                        <asp:ListItem Value="146"></asp:ListItem>
                        <asp:ListItem Value="147"></asp:ListItem>
                        <asp:ListItem Value="148"></asp:ListItem>
                        <asp:ListItem Value="149"></asp:ListItem>
                        <asp:ListItem Value="150"></asp:ListItem>
                        <asp:ListItem Value="151"></asp:ListItem>
                        <asp:ListItem Value="152"></asp:ListItem>
                        <asp:ListItem Value="153"></asp:ListItem>
                        <asp:ListItem Value="154"></asp:ListItem>
                        <asp:ListItem Value="155"></asp:ListItem>
                        <asp:ListItem Value="156"></asp:ListItem>
                        <asp:ListItem Value="157"></asp:ListItem>
                        <asp:ListItem Value="158"></asp:ListItem>
                        <asp:ListItem Value="159"></asp:ListItem>
                        <asp:ListItem Value="160" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="161"></asp:ListItem>
                        <asp:ListItem Value="162"></asp:ListItem>
                        <asp:ListItem Value="163"></asp:ListItem>
                        <asp:ListItem Value="164"></asp:ListItem>
                        <asp:ListItem Value="165"></asp:ListItem>
                        <asp:ListItem Value="166"></asp:ListItem>
                        <asp:ListItem Value="167"></asp:ListItem>
                        <asp:ListItem Value="168"></asp:ListItem>
                        <asp:ListItem Value="169"></asp:ListItem>
                        <asp:ListItem Value="170"></asp:ListItem>
                        <asp:ListItem Value="171"></asp:ListItem>
                        <asp:ListItem Value="172"></asp:ListItem>
                        <asp:ListItem Value="173"></asp:ListItem>
                        <asp:ListItem Value="174"></asp:ListItem>
                        <asp:ListItem Value="175"></asp:ListItem>
                        <asp:ListItem Value="176"></asp:ListItem>
                        <asp:ListItem Value="177"></asp:ListItem>
                        <asp:ListItem Value="178"></asp:ListItem>
                        <asp:ListItem Value="179"></asp:ListItem>
                        <asp:ListItem Value="180"></asp:ListItem>
                        <asp:ListItem Value="181"></asp:ListItem>
                        <asp:ListItem Value="182"></asp:ListItem>
                        <asp:ListItem Value="183"></asp:ListItem>
                        <asp:ListItem Value="184"></asp:ListItem>
                        <asp:ListItem Value="185"></asp:ListItem>
                        <asp:ListItem Value="186"></asp:ListItem>
                        <asp:ListItem Value="187"></asp:ListItem>
                        <asp:ListItem Value="188"></asp:ListItem>
                        <asp:ListItem Value="189"></asp:ListItem>
                        <asp:ListItem Value="190"></asp:ListItem>
                        <asp:ListItem Value="191"></asp:ListItem>
                        <asp:ListItem Value="192"></asp:ListItem>
                        <asp:ListItem Value="193"></asp:ListItem>
                        <asp:ListItem Value="194"></asp:ListItem>
                        <asp:ListItem Value="195"></asp:ListItem>
                        <asp:ListItem Value="196"></asp:ListItem>
                        <asp:ListItem Value="197"></asp:ListItem>
                        <asp:ListItem Value="198"></asp:ListItem>
                        <asp:ListItem Value="199"></asp:ListItem>
                        <asp:ListItem Value="200"></asp:ListItem>
                        <asp:ListItem Value="201"></asp:ListItem>
                        <asp:ListItem Value="202"></asp:ListItem>
                        <asp:ListItem Value="203"></asp:ListItem>
                        <asp:ListItem Value="204"></asp:ListItem>
                        <asp:ListItem Value="205"></asp:ListItem>
                        <asp:ListItem Value="206"></asp:ListItem>
                        <asp:ListItem Value="207"></asp:ListItem>
                        <asp:ListItem Value="208"></asp:ListItem>
                        <asp:ListItem Value="209"></asp:ListItem>
                    </asp:DropDownList> Cm | 
                    Cân nặng: 
			        <asp:DropDownList ID="cbCannang" runat="server" Width="60px">
			            <asp:ListItem Value="35"></asp:ListItem>
			            <asp:ListItem Value="36"></asp:ListItem>
			            <asp:ListItem Value="37"></asp:ListItem>
			            <asp:ListItem Value="38"></asp:ListItem>
			            <asp:ListItem Value="39"></asp:ListItem>
			            <asp:ListItem Value="40"></asp:ListItem>
			            <asp:ListItem Value="41"></asp:ListItem>
			            <asp:ListItem Value="42"></asp:ListItem>
			            <asp:ListItem Value="43"></asp:ListItem>
			            <asp:ListItem Value="44"></asp:ListItem>
			            <asp:ListItem Value="45"></asp:ListItem>
			            <asp:ListItem Value="46"></asp:ListItem>
			            <asp:ListItem Value="47"></asp:ListItem>
			            <asp:ListItem Value="48"></asp:ListItem>
			            <asp:ListItem Value="49"></asp:ListItem>
			            <asp:ListItem Value="50" Selected="True"></asp:ListItem>
			            <asp:ListItem Value="51"></asp:ListItem>
			            <asp:ListItem Value="52"></asp:ListItem>
			            <asp:ListItem Value="53"></asp:ListItem>
			            <asp:ListItem Value="54"></asp:ListItem>
			            <asp:ListItem Value="55"></asp:ListItem>
			            <asp:ListItem Value="56"></asp:ListItem>
			            <asp:ListItem Value="57"></asp:ListItem>
			            <asp:ListItem Value="58"></asp:ListItem>
			            <asp:ListItem Value="59"></asp:ListItem>
			            <asp:ListItem Value="60"></asp:ListItem>
			            <asp:ListItem Value="61"></asp:ListItem>
			            <asp:ListItem Value="62"></asp:ListItem>
			            <asp:ListItem Value="63"></asp:ListItem>
			            <asp:ListItem Value="64"></asp:ListItem>
			            <asp:ListItem Value="65"></asp:ListItem>
			            <asp:ListItem Value="66"></asp:ListItem>
			            <asp:ListItem Value="67"></asp:ListItem>
			            <asp:ListItem Value="68"></asp:ListItem>
			            <asp:ListItem Value="69"></asp:ListItem>
			            <asp:ListItem Value="70"></asp:ListItem>
			            <asp:ListItem Value="71"></asp:ListItem>
			            <asp:ListItem Value="72"></asp:ListItem>
			            <asp:ListItem Value="73"></asp:ListItem>
			            <asp:ListItem Value="74"></asp:ListItem>
			            <asp:ListItem Value="75"></asp:ListItem>
			            <asp:ListItem Value="76"></asp:ListItem>
			            <asp:ListItem Value="77"></asp:ListItem>
			            <asp:ListItem Value="78"></asp:ListItem>
			            <asp:ListItem Value="79"></asp:ListItem>
			            <asp:ListItem Value="80"></asp:ListItem>
			            <asp:ListItem Value="81"></asp:ListItem>
			            <asp:ListItem Value="82"></asp:ListItem>
			            <asp:ListItem Value="83"></asp:ListItem>
			            <asp:ListItem Value="84"></asp:ListItem>
			            <asp:ListItem Value="85"></asp:ListItem>
			            <asp:ListItem Value="86"></asp:ListItem>
			            <asp:ListItem Value="87"></asp:ListItem>
			            <asp:ListItem Value="88"></asp:ListItem>
			            <asp:ListItem Value="89"></asp:ListItem>
			            <asp:ListItem Value="90"></asp:ListItem>
			            <asp:ListItem Value="91"></asp:ListItem>
			            <asp:ListItem Value="92"></asp:ListItem>
			            <asp:ListItem Value="93"></asp:ListItem>
			            <asp:ListItem Value="94"></asp:ListItem>
			            <asp:ListItem Value="95"></asp:ListItem>
			            <asp:ListItem Value="96"></asp:ListItem>
			            <asp:ListItem Value="97"></asp:ListItem>
			            <asp:ListItem Value="98"></asp:ListItem>
			            <asp:ListItem Value="99"></asp:ListItem>
			            <asp:ListItem Value="100"></asp:ListItem>
			            <asp:ListItem Value="150"></asp:ListItem>
                    </asp:DropDownList>Kg
			    </p>
			</div>
			<div class="clear"></div>
			
			<p style="margin-left: 5px;">
			    Ghi chú: 
			    <asp:TextBox ID="txtDescription" runat="server" CssClass="text-input" Width="100%" TextMode="MultiLine"
			        Rows="4" />
			</p>
			
			<p style="margin-left: 5px;">
			    Trạng thái <span class="isrequire">(*)</span>: 
			    <asp:DropDownList ID="cbTrangthainhanvien" runat="server" Width="200px">
			        <asp:ListItem Value="1" Text="Đang làm việc"></asp:ListItem>
			        <asp:ListItem Value="2" Text="Đang thử việc"></asp:ListItem>
			        <asp:ListItem Value="3" Text="Tạm ngưng việc"></asp:ListItem>
			        <asp:ListItem Value="4" Text="Đã nghỉ việc"></asp:ListItem>
                </asp:DropDownList>
			</p>
			
			<p style="margin-left: 5px;">
			    
                <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="button"
                    ValidationGroup="thongtincanhan" onclick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="button"
                    ValidationGroup="thongtincanhan" onclick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa nhân viên" CssClass="button"
                    OnClientClick="return confirm('Bạn có thực sự muốn xóa nhân viên này?');"
                    onclick="btnDelete_Click" />
                <a href="DanhsachNhanvien">Danh sách Nhân viên</a>
			</p>
            <div class="clear"></div>
            
        </div> <!-- End #thongtincanhan -->
        
                <!-- Begin #congviec -->
        <div class="tab-content" id="congviec">
            
            <div class="clear"></div>
            
        </div>  <!-- End #congviec -->
        
                <!-- Begin #phucap -->
        <div class="tab-content" id="phucap">
            
            <div class="clear"></div>
            
        </div>  <!-- End #phucap -->
        
        
                <!-- Begin #nguoithan -->
        <div class="tab-content" id="nguoithan">
            
            <div class="clear"></div>
            
        </div>  <!-- End #nguoithan -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

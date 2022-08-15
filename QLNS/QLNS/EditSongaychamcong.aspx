<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditSongaychamcong.aspx.cs" Inherits="QLNS.QLNS.EditSongaychamcong" %>
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
            $("#caidat-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Quydinhsongaychamcong-nav").addClass("current");
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
		
        <h3>Thay đổi số ngày chấm công</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            
            <div class="notification information png_bg">
                <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="Close this notification" alt="close" /></a>
                <div>
                    Chỉ được cài đặt công thức khi đã có đầy đủ quyết định thi hành, có thông tin người ký, ngày ký.<br />
                    <span style="color: #AC2C2C;">Lưu ý:</span><br />
                    <span style="color: #AC2C2C;">- Tính lương tháng <b>trước tháng hiện tại</b> trước khi thực hiện thay đổi.</span><br />
                    <span style="color: #AC2C2C;">- <b>Ngày bắt đầu có hiệu lực</b> sẽ chính là <b>tháng hiện tại</b>.</span><br />
                    <span style="color: #AC2C2C;">- <b>Tổng số ngày quy định chấm công</b> sẽ được <b>thay đổi ngay sau khi nhấn nút lưu cài đặt</b>.</span>
                    
                </div>
            </div>
			
			<!-- Begin Thông tin -->
			<div class="leftcol">
                <h5>Thông tin cũ</h5>
                <table>
                    <tr>
                        <td style="width: 120px;" >
                            Số ngày:</td>
                        <td>
                            <asp:Label ID="lblValue" runat="server" Font-Bold="True" Text="Chưa cài đặt" />
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
                            Số ngày<span class="isrequire">(*)</span>:
                        </td>
                        <td style="width: 172px;">
                            <asp:TextBox ID="txtValue" runat="server" CssClass="text-input" MaxLength="12" Width="100px"
                                onkeypress="return onlyNumbers(event)" onkeyup="keyuped(this)" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqValue"
                                runat="server"
                                ControlToValidate="txtValue"
                                CssClass="input-notification error errorleft"
                                ErrorMessage="Vui lòng nhập" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="regValue"
                                runat="server"
                                ValidationExpression="[0-9,]+"
                                ControlToValidate="txtValue"
                                CssClass="input-notification error errorleft"
                                ErrorMessage="Sai định dạng" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chức vụ người ký<span class="isrequire">(*)</span>: 
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
                                CssClass="input-notification error errorleft"
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
                                ValidationExpression="[^0]([0-99NV]*)?"
                                ControlToValidate="cbNguoiky"
                                CssClass="input-notification error errorleft"
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
                            Bắt đầu áp dụng từ:
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
            
            <div class="rightcol" style="height: 100px;">
                <h5>Ghi chú</h5>
                <asp:TextBox ID="txtDescription" runat="server"
                    TextMode="MultiLine" Rows="3" />
                <br />
            </div>
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

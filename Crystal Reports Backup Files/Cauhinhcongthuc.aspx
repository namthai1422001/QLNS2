<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cauhinhcongthuc.aspx.cs" Inherits="QLNS.QLNS.Cauhinhcongthuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#caidat-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Congthuctinhluong-nav").addClass("current");
        });
    </script>
    
    <style type="text/css">
        .maininfor
        {
            float: none;
            position: relative;
            min-width: 735px;
            min-width: 350px;
            border: solid 1px #AC2C2C;
            padding: 20px 4px 10px;
            border-radius: 5px;
            moz-border-radius: 5px;
            margin: 10px 10px 4px 10px;
        }
        .maininfor h5
        {
            position: absolute;
            top: -8px;
            left: 20px;
            background-color: #FFF;
            padding-left: 2px;
            padding-right: 2px;
        }
        
        .leftcol
        {
            float: left;
            position: relative;
            width: 45%;
            min-width: 350px;
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
            width: 45%;
            min-width: 350px;
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
            left: 20px;
            background-color: #FFF;
            padding-left: 2px;
            padding-right: 2px;
        }
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 755px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Công thức tính lương</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="padding-left: 10px;"> 
                <asp:Button ID="btnReInstall" runat="server"
                    Text="Cài đặt lại công thức" ToolTip="Cài đặt lại công thức"
                    CssClass="button" onclick="btnReInstall_Click" />
            </p>

			<div class="leftcol">
			    <h5>Bảo hiểm</h5>
			    <table>
			        <tr>
			            <td style="width: 145px;">
			                Xã hội: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHXH" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Y tế: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHYT" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Thất nghiệp: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHTN" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng BHXH tối đa: 
			            </td>
			            <td>
                            <asp:Label ID="lblBHXHMax" runat="server" Font-Bold="true" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Thuế thu nhập cá nhân</h5>
			    <table>
			        <tr>
			            <td style="width: 200px;">
			                Mức chịu thuế thu nhập: 
			            </td>
			            <td>
                            <asp:Label ID="lblMinIncomeTax" runat="server" Font-Bold="true" />&nbsp;VNĐ
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chi cho người phụ thuộc: 
			            </td>
			            <td>
                            <asp:Label ID="lblChichonguoiphuthuoc" runat="server" Font-Bold="true" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="clear"></div>
			
			<div class="leftcol">
			    <h5>Tăng ca</h5>
			    <table>
			        <tr>
			            <td style="width: 145px;">
			                Thường: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcaThuong" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Chủ Nhật: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcaChunhat" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Ngày nghỉ - lễ: 
			            </td>
			            <td>
                            <asp:Label ID="lblTangcanghile" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="rightcol">
			    <h5>Công đoàn</h5>
			    <table>
			        <tr>
			            <td style="width: 200px;">
			                Phí công đoàn: 
			            </td>
			            <td>
                            <asp:Label ID="lblPhicongdoan" runat="server" Font-Bold="true" />&nbsp;%
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Mức đóng phí công đoàn tối đa: 
			            </td>
			            <td>
                            <asp:Label ID="lblPhicongdoanMax" runat="server" Font-Bold="true" />&nbsp;VNĐ
			            </td>
			        </tr>
			    </table>
			</div>
			
			<div class="clear"></div>
			
			<div class="maininfor">
                <h5>Thông tin</h5>
                <table>
                    <tr>
                        <td style="width: 60px;" >
                            Người ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNguoiky" runat="server" Font-Bold="true" />
                        </td>
                        <td style="width: 120px;">
                            Chức vụ: 
                        </td>
                        <td>
                            <asp:Label ID="lblChucvunguoiky" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ngày ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNgayky" runat="server" Font-Bold="true" />
                        </td>
                        <td>
                            Bắt đầu áp dụng từ:
                        </td>
                        <td>
                            <asp:Label ID="lblNgayapdung" runat="server" Font-Bold="true" ForeColor="#AC2C2C" />
                        </td>
                    </tr>
                </table>
            </div>
            
			<div class="maininfor">
                <h5>Mô tả về công thức</h5>
                <asp:Label ID="lblMota" runat="server" />
            </div>
			
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

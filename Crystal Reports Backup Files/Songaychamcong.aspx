<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Songaychamcong.aspx.cs" Inherits="QLNS.QLNS.Songaychamcong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#caidat-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Quydinhsongaychamcong-nav").addClass("current");
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
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 755px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Quy định tổng số ngày chấm công trong tháng</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p style="padding-left: 10px;"> 
                <asp:Button ID="btnReInstall" runat="server"
                    Text="Thay đổi số ngày chấm công" ToolTip="Thay đổi số ngày chấm công"
                    CssClass="button" onclick="btnReInstall_Click" />
            </p>
			
			<div class="maininfor">
                <h5>Thông tin</h5>
                <table>
                    <tr>
                        <td style="width: 120px;" >
                            Số ngày:</td>
                        <td colspan="3">
                            <asp:Label ID="lblValue" runat="server" Font-Bold="True" ForeColor="#AC2C2C" >Chưa được cài đặt</asp:Label>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Người ký: 
                        </td>
                        <td style="width: 200px;">
                            <asp:Label ID="lblNguoiky" runat="server" Font-Bold="True" >Chưa được cài đặt</asp:Label>
                        </td>
                        <td style="width: 120px;">
                            Chức vụ: 
                        </td>
                        <td>
                            <asp:Label ID="lblChucvunguoiky" runat="server" Font-Bold="True" >Chưa được cài đặt</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ngày ký: 
                        </td>
                        <td>
                            <asp:Label ID="lblNgayky" runat="server" Font-Bold="True" >Chưa được cài đặt</asp:Label>
                        </td>
                        <td>
                            Bắt đầu áp dụng từ:
                        </td>
                        <td>
                            <asp:Label ID="lblNgayapdung" runat="server" Font-Bold="True" 
                                ForeColor="#AC2C2C" >Chưa được cài đặt</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

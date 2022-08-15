<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quatrinhlamviec.aspx.cs" Inherits="QLNS.QLNS.Quatrinhlamviec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#nhansu-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#DanhsachNhanvien-nav").addClass("current");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
    <div class="content-box" style="min-width: 850px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3 style="font-size: 13px; width: 200px; overflow: hidden; height: 18px;"><asp:Literal ID="lblHoTenH2" runat="server" /></h3>
		<ul class="content-box-tabs">
			<li><a href="#chuyenphongban" class="default-tab">Chuyển phòng</a></li> <!-- href must be unique and match the id of target div -->
			<li><a href="#thaydoichucvu">Chức vụ</a></li>
			<li><a href="#thaydoicongviec">Công việc</a></li>
			<li><a href="#dienbienluong">Lương</a></li>
			<li><a href="#dicongtac">Công tác</a></li>
			<li><a href="#tamung">Tạm ứng</a></li>
			<li><a href="#khenthuong">Khen thưởng</a></li>
			<li><a href="#kyluat">Kỷ luật</a></li>
		</ul>
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		        <!-- Begin #chuyenphongban -->
        <div class="tab-content default-tab" id="chuyenphongban"> <!-- This is the target div. id must match the href of this div's tab -->
            
            <asp:Repeater ID="rpDataChuyenphongban" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên phòng được chuyển đến</b> </th>
                        <th> <b>Ngày chuyển</b> </th>
                        <th> <b>Lý do</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Tenphong") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngayapdung"))%>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div> <!-- End #chuyenphongban -->
        
                <!-- Begin #thaydoichucvu -->
        <div class="tab-content" id="thaydoichucvu">
            
            <asp:Repeater ID="rpDataThaydoichucvu" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên chức vụ mới</b> </th>
                        <th> <b>Ngày áp dụng</b> </th>
                        <th> <b>Lý do</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngayapdung"))%>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #thaydoichucvu -->
        
                <!-- Begin #thaydoicongviec -->
        <div class="tab-content" id="thaydoicongviec">
            
            <asp:Repeater ID="rpDataThaydoicongviec" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên công việc mới</b> </th>
                        <th> <b>Ngày áp dụng</b> </th>
                        <th> <b>Lý do</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngayapdung"))%>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #thaydoicongviec -->
        
                <!-- Begin #dienbienluong -->
        <div class="tab-content" id="dienbienluong">
            
            <asp:Repeater ID="rpDataDienbienluong" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên ngạch mới</b> </th>
                        <th> <b>Bậc mới</b> </th>
                        <th> <b>Hệ số mới</b> </th>
                        <th> <b>Ngày áp dụng</b> </th>
                        <th> <b>Lý do</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("TenNgach") %>
                        </td>
                        <td> 
                            <%# Eval("TenBac") %>
                        </td>
                        <td> 
                            <%# Eval("Hesoluong").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngayapdung"))%>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #dienbienluong -->
        
            <!-- Begin #dicongtac -->
        <div class="tab-content" id="dicongtac">
            
            <asp:Repeater ID="rpDataDicongtac" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Từ ngày</b> </th>
                        <th> <b>Đến ngày</b> </th>
                        <th> <b>Nơi công tác</b> </th>
                        <th> <b>Về việc</b> </th>
                        <th> <b>Lý do</b> </th>
                        <th> <b>Tiền đi công tác</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Tungay"))%>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Denngay"))%>
                        </td>
                        <td> 
                            <%# Eval("Noicongtac") %>
                        </td>
                        <td> 
                            <%# Eval("Veviec") %>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:#,##0}", Eval("Tiendicongtac")) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #dicongtac -->
        
            <!-- Begin #tamung -->
        <div class="tab-content" id="tamung">
            
            <asp:Repeater ID="rpDataTamung" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Ngày tạm ứng</b> </th>
                        <th> <b>Số tiền</b> </th>
                        <th> <b>Lý do</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngaytamung"))%>
                        </td>
                        <td> 
                            <%# String.Format("{0:#,##0}", Eval("Sotien")) %>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #tamung -->
        
            <!-- Begin #khenthuong -->
        <div class="tab-content" id="khenthuong">
            
            <asp:Repeater ID="rpDataKhenthuong" runat="server">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Ngày KT</b> </th>
                        <th> <b>Tên KT</b> </th>
                        <th> <b>Lý do</b> </th>
                        <th> <b>Hình thức KT</b> </th>
                        <th> <b>Tiền</b> </th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# String.Format("{0:dd/MM/yyyy}", Eval("Ngaykhenthuong"))%>
                        </td>
                        <td> 
                            <%# Eval("Tenkhenthuong") %>
                        </td>
                        <td> 
                            <%# Eval("LyDo") %>
                        </td>
                        <td> 
                            <%# Eval("Hinhthuckhenthuong") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:#,##0}", Eval("Sotien")) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #khenthuong -->
        
            <!-- Begin #kyluat -->
        <div class="tab-content" id="kyluat">
            
            <div class="clear"></div>
            
        </div>  <!-- End #kyluat -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

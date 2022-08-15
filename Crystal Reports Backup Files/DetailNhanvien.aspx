<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetailNhanvien.aspx.cs" Inherits="QLNS.QLNS.DetailNhanvien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
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
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
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
        #chitietcanhan{position: relative;}
        #chitietcanhan .imgNhanvien{position: absolute; display: inline-block; background-image: url('../images/border-img.png'); top: 2px; left: 2px; margin-right: 2px; margin-bottom: 2px; width: 124px; height: 164px;}
        #chitietcanhan .imgNhanvien img{width: 116px; height: 155px; padding: 2px; margin-top: 2px; margin-left: 2px;}
        #chitietcanhan > p{margin-left: 130px;}
        #chitietcanhan .value{font-weight: bold;}
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box" style="min-width: 850px;"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3 style="width: 380px; overflow: hidden; height: 18px;">Thông tin nhân viên <asp:Literal ID="lblHoTenH2" runat="server" /></h3>
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
            
            <div id="chitietcanhan">
                <div class="imgNhanvien">
                    <asp:Image ID="imgNhanvien" runat="server" AlternateText="Hình đã bị xóa"
                        ImageUrl="~/QLNS/Employee-images/chuaco.png" />
                </div>
		        <p>
		            <span>Mã: </span>
                    <asp:Label ID="lblMaNV" runat="server" Text="NV000001" CssClass="value" />
		        </p>
		        <p>
		            <span>Họ tên: </span>
		            <asp:Label ID="lblHoTen" runat="server" Text="Không chưa có" CssClass="value" />
		        </p>
		        <p>
		            <span>Giới tính: </span>
		            <asp:Image ID="imgGender" runat="server" />
		        </p>
		        <p>
		            <span>Tên thường gọi: </span>
		            <asp:Label ID="lblTenthuonggoi" runat="server" Text="Không chưa có" CssClass="value" />
		            <span style="margin-left: 15%;">Kết hôn: </span>
                    <asp:CheckBox ID="chkKethon" runat="server"/>
		        </p>
		        <p>
		            <span>Ngày sinh: </span>
		            <asp:Label ID="lblNgaysinh" runat="server" Text="1991" CssClass="value" />
		            <span style="margin-left: 15%;">Nơi sinh: </span>
		            <asp:Label ID="lblNoisinh" runat="server" Text="Đồng Nai nè" CssClass="value" />
		        </p>
		        <p>
		            <span>Địa chỉ: </span>
		            <asp:Label ID="lblDiachi" runat="server" Text="Đồng Nai luôn" CssClass="value" />
		        </p>
		        <p>
		            <span>Tạm trú:</span>
		            <asp:Label ID="lblTamtru" runat="server" Text="Đồng Nai luôn" CssClass="value" />
		        </p>
    			
			    <table>
			        <tr>
			            <td style="width: 130px;">
			                Điện thoại di động: 
			            </td>
			            <td  style="width: 270px;">
			                <asp:Label ID="lblDienthoaididong" runat="server" Text="0123456789" CssClass="value" />
			            </td>
			            <td>
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Điện thoại nhà: 
			            </td>
			            <td>
			                <asp:Label ID="lblDienthoainha" runat="server" Text="0123456789" CssClass="value" />
			            </td>
			            <td>
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Email: 
			            </td>
			            <td>
			                <asp:Label ID="lblEmail" runat="server" Text="0123456789" CssClass="value" />
			            </td>
			            <td>
			            </td>
			        </tr>
			    </table>
			    <table>
			        <tr>
			            <td style="width: 105px;">
			                Quốc tịch:
			            </td>
			            <td style="width: 155px;">
			                <asp:Label ID="lblQuoctich" runat="server" Text="Việt Nam" CssClass="value" />
			            </td>
			            <td style="width: 120px;">
			            </td>
			            <td style="width: 130px;">
			                Dân tộc:
			            </td>
			            <td style="width: 155px;">
			                <asp:Label ID="lblDantoc" runat="server" Text="Kinh" CssClass="value" />
			            </td>
			            <td style="width: 120px;">
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Tôn giáo:
			            </td>
			            <td>
			                <asp:Label ID="lblTongiao" runat="server" Text="ThienChua" CssClass="value" />
			            </td>
			            <td>
			            </td>
			            <td>
			                Học vấn:
			            </td>
			            <td>
			                <asp:Label ID="lblHocvan" runat="server" Text="THPT" CssClass="value" />
			            </td>
			            <td>
			            </td>
			        </tr>
			        <tr>
			            <td>
			                Ngoại ngữ:
			            </td>
			            <td>
			                <asp:Label ID="lblNgoaingu" runat="server" Text="THPT" CssClass="value" />
			            </td>
			            <td></td>
			            <td>
			                Chuyên môn:
			            </td>
			            <td>
			                <asp:Label ID="lblChuyenmon" runat="server" Text="Công nghệ thông tin" CssClass="value" />
			            </td>
			            <td></td>
			        </tr>
			        <tr>
			            <td>
			                Tin học:
			            </td>
			            <td>
			                <asp:Label ID="lblTinhoc" runat="server" Text="Bằng A" CssClass="value" />
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
			            <span>Số CMND: </span>
			            <asp:Label ID="lblSoCMNN" runat="server" Text="123456789" CssClass="value" />
			        </p>
			        <p>
			            <span>Ngày cấp: </span>
			            <asp:Label ID="lblNgaycap" runat="server" Text="12/12/2012" CssClass="value" />
			        </p>
			        <p>
			            <span>Nơi cấp: </span>
			            <asp:Label ID="lblNoicap" runat="server" Text="Đồng Nai" CssClass="value" />
			        </p>
			    </div>
			    <div class="rightcol">
			        <h5>Tình trạng sức khỏe</h5>
			        <p>
			            <span>Tình trạng sức khỏe: </span>
			            <asp:Label ID="lblTinhtrangsuckhoe" runat="server" Text="Đồng Nai" CssClass="value" />
			        </p>
			        <p>
			            <span>Chiều cao: </span>
			            <asp:Label ID="lblChieucao" runat="server" Text="160" CssClass="value" /> Cm | 
                        <span>Cân nặng: </span>
                        <asp:Label ID="lblCannang" runat="server" Text="160" CssClass="value" /> Kg
			        </p>
			    </div>
			    <div class="clear"></div>
    			
			    <p style="margin-left: 5px;">
			        Ghi chú: 
			        <asp:TextBox ID="txtDescription" runat="server" CssClass="text-input" Width="100%" TextMode="MultiLine"
			            Rows="4" />
			    </p>
    			
			    <p style="margin-left: 5px;">
			        Trạng thái: 
			        <asp:DropDownList ID="cbTrangthainhanvien" runat="server" Width="200px" Enabled="false">
			            <asp:ListItem Value="1" Text="Đang làm việc"></asp:ListItem>
			            <asp:ListItem Value="2" Text="Đang thử việc"></asp:ListItem>
			            <asp:ListItem Value="3" Text="Tạm ngưng việc"></asp:ListItem>
			            <asp:ListItem Value="4" Text="Đã nghỉ việc"></asp:ListItem>
                    </asp:DropDownList>
			    </p>
    			
			    <p style="margin-left: 5px;">
    			    
                    <asp:Button ID="btnUpdate" runat="server" Text="Sửa" CssClass="button"
                        onclick="btnUpdate_Click" />
                    <a href="DanhsachNhanvien">Danh sách Nhân viên</a>
			    </p>
			</div> <!-- End #chitietcanhan -->
            <div class="clear"></div>
            
        </div> <!-- End #thongtincanhan -->
        
                <!-- Begin #congviec -->
        <div class="tab-content" id="congviec">
            <table>
                <tr>
                    <td style="width: 140px;">
                        Phòng :
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="lblPhong" runat="server" Font-Bold="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnThaydoiphongban" CssClass="button" runat="server" 
                            Text="Chuyển phòng" OnClick="btnThaydoiphongban_Click" Width="180px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Tổ :
                    </td>
                    <td>
                        <asp:DropDownList ID="cbGroup" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        Chức vụ :
                    </td>
                    <td>
                        <asp:Label ID="lblChucvu" runat="server" Font-Bold="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnThaydoichucvu" CssClass="button" runat="server" 
                            Text="Thay đổi chức vụ" OnClick="btnThaydoichucvu_Click" Width="180px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Công việc :
                    </td>
                    <td>
                        <asp:Label ID="lblCongviec" runat="server" Font-Bold="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnThaydoicongviec" CssClass="button" runat="server" 
                            Text="Thay đổi công việc" OnClick="btnThaydoicongviec_Click" Width="180px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Ngạch lương:
                    </td>
                    <td>
                        <asp:Label ID="lblNgach" runat="server" Width="35%" Font-Bold="true" />
                        Bậc lương: 
                        <asp:Label ID="lblBacluong" runat="server" Width="35%" Font-Bold="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnThaydoibacngach" CssClass="button" runat="server" Width="180px"
                            Text="Thay đổi bậc - ngạch" onclick="btnThaydoibacngach_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Lương tối thiểu (VNĐ):
                    </td>
                    <td>
                        <asp:Label ID="lblLuongtoithieu" runat="server" Width="35%" Font-Bold="true" />
                        Hệ số lương (%): 
                        <asp:Label ID="lblHesoluong" runat="server" Font-Bold="true" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Lương cơ bản (VNĐ):
                    </td>
                    <td>
                        <asp:Label ID="lblLuongcoban" runat="server" Width="35%" Font-Bold="true" />
                        Từ tháng 
                        <asp:Label ID="lblNgayapdung" runat="server" Width="35%" Font-Bold="true" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Các khoản phải đóng:
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chkBHXH" runat="server" />
                        <span style="margin-right: 30px; font-weight: bold;">BHXH</span>
                        <asp:CheckBox ID="chkBHYT" runat="server" />
                        <span style="margin-right: 30px; font-weight: bold;">BHYT</span>
                        <asp:CheckBox ID="chkBHTN" runat="server" />
                        <span style="margin-right: 30px; font-weight: bold;">BHTN</span>
                        <asp:CheckBox ID="chkPhicongdoan" runat="server" />
                        <span style="margin-right: 30px; font-weight: bold;">Phí công đoàn</span>
                    </td>
                </tr>
            </table>
            <div style="float: left; margin-left: 10px; margin-top: 5px;">
                <asp:Button ID="btnUpdateCongviec" runat="server" Text="Cập nhật" CssClass="button"
                    onclick="btnUpdateCongviec_Click" />
                <asp:HyperLink ID="hplQuatrinhlamviec" runat="server" Text="Quá trình làm việc" />
            </div>
            
            <div class="clear"></div>
            
        </div>  <!-- End #congviec -->
        
                <!-- Begin #phucap -->
        <div class="tab-content" id="phucap">
            <p style="margin-left: 10px;">
                <asp:Button ID="btnPhucapAdd" runat="server" CssClass="button" 
                    Text="Thêm phụ cấp cho nhân viên" onclick="btnPhucapAdd_Click" />
            </p>
            <asp:Repeater ID="rpDataPhucap" runat="server" 
                onitemdatabound="rpDataPhucap_ItemDataBound">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Tên</b> </th>
                        <th> <b>Số tiền được lãnh</b> </th>
                        <th></th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Tenphucap") %>
                        </td>
                        <td> 
                            <%# String.Format("{0:0,0}",Eval("Sotien")) %>
                        </td>
                        <td style="width: 15px;"> 
                            <asp:HyperLink ID="hplEditPhucap" runat="server" ToolTip="Sửa">
                                <img src="../images/icons/pencil.png" alt="Edit" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #phucap -->
        
        
                <!-- Begin #nguoithan -->
        <div class="tab-content" id="nguoithan">
            <p style="margin-left: 10px;">
                <asp:Button ID="btnNguoithanAdd" runat="server" CssClass="button" 
                    Text="Thêm" onclick="btnNguoithanAdd_Click" />
            </p>
            <asp:Repeater ID="rpDataNguoithan" runat="server" 
                onitemdatabound="rpDataNguoithan_ItemDataBound">
                <HeaderTemplate>
                <table>
                    <thead>
                        <th> <b>STT</b> </th>
                        <th> <b>Họ tên</b> </th>
                        <th> <b>Quan hệ</b> </th>
                        <th> <b>Địa chỉ</b> </th>
                        <th> <b>Điện thoại</b> </th>
                        <th> <b>Phụ thuộc</b> </th>
                        <th></th>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> 
                            <%# Eval("STT").ToString() %>
                        </td>
                        <td> 
                            <%# Eval("Hotennguoithan")%>
                        </td>
                        <td> 
                            <%# Eval("Tenquanhe") %>
                        </td>
                        <td> 
                            <%# Eval("Diachi") %>
                        </td>
                        <td> 
                            <%# Eval("Dienthoai") %>
                        </td>
                        <td> 
                            <%# (Eval("Phuthuoc").ToString() == "True") ? "<b>Có</b>" : "<b>Không</b>" %>
                        </td>
                        <td style="width: 15px;"> 
                            <asp:HyperLink ID="hplEditNguoithan" runat="server" ToolTip="Sửa">
                                <img src="../images/icons/pencil.png" alt="Edit" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div>  <!-- End #nguoithan -->
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

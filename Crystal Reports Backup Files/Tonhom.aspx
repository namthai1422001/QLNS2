<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tonhom.aspx.cs" Inherits="QLNS.QLNS.Tonhom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/thickbox.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/thickbox.js" type="text/javascript"></script>
    
    <link href="../css/TreeView.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/TreeView.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#cocautochuc-nav > a").addClass("current").parent().find("ul").css("display", "block").find("#Tonhom-nav").addClass("current");
            $(".chkParent").eq(0).attr("checked", "checked");
            $("#<%= hplAdd.ClientID %>").attr("href", "EditTonhom/add/" + $(".chkParent").eq(0).val() + "?height=340");
            $(".chkParent").click(function() {
                $(".chkParent").removeAttr("checked");
                $(this).attr("checked", "checked");
                $("#<%= hplAdd.ClientID %>").attr("href", "EditTonhom/add/" + $(this).val() + "?height=340");
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Tổ - nhóm</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <p> 
                <asp:HyperLink ID="hplAdd" runat="server"
                    NavigateUrl="EditTonhom/add/?height=340" style="margin-left: 5px;"
                    class="thickbox"
                    title="Thêm tổ" >Thêm</asp:HyperLink>
            </p>
            
            <asp:Repeater ID="rpData" runat="server" onitemdatabound="rpData_ItemDataBound">
                <HeaderTemplate>
                    <div id="TreeView">
                        <div class="Head">
                            <span class="GroupName">Tên tổ</span>
                            <span class="TotalEmployee">Số nhân viên</span>
                            <span class="Description">Ghi chú</span>
                        </div><!--End .Head -->
                </HeaderTemplate>
                <ItemTemplate>
                        <div class="Nodes">
                            <div class="Parent">
                                <img src="../images/icons/Collapse.gif" alt="Đóng" class="StatusImg" title="Đóng" />
                                <input type="checkbox" name="chkParent" class="chkParent" value='<%# Eval("Maphong") %>' />
                                <span>Phòng <%# Eval("Tenphong") %></span>
                            </div><!--End .Parent -->
                    <asp:Repeater ID="rpChild" runat="server">
                        <ItemTemplate>
                            <div class="Childs">
                                <div class="Content">
                                    <span class="GroupName"><%# Eval("TenToNhom") %></span>
                                    <span class="TotalEmployee"><%# Eval("Tongsonhanvien").ToString() %></span>
                                    <span class="Description"><%# Eval("GhiChu") %></span>
                                </div>
                                <asp:HyperLink ID="hplEdit" runat="server" class="thickbox" title="Sửa thông tin tổ">
                                    <img src="../images/icons/pencil.png" alt="Edit" />
                                </asp:HyperLink>
                            </div><!--End .Childs -->
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="Childs" alt="alt">
                                <div class="Content">
                                    <span class="GroupName"><%# Eval("TenToNhom") %></span>
                                    <span class="TotalEmployee"><%# Eval("Tongsonhanvien").ToString() %></span>
                                    <span class="Description"><%# Eval("GhiChu") %></span>
                                </div>
                                <asp:HyperLink ID="hplEdit" runat="server" class="thickbox" title="Sửa thông tin tổ">
                                    <img src="../images/icons/pencil.png" alt="Edit" />
                                </asp:HyperLink>
                            </div><!--End .Childs[alt=alt] -->
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                        </div><!--End .Nodes -->
                </ItemTemplate>
                <FooterTemplate>
                    </div><!--End #TreeView -->
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear"></div>
            
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	<asp:HiddenField ID="hdIsRoll" runat="server" />
</div>
</asp:Content>

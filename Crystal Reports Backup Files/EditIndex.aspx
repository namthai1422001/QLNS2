<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditIndex.aspx.cs" Inherits="QLNS.EditIndex" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#index-nav a").addClass("current");
            $("#SearchBox").hide();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div class="content-box"><!-- Start Content Box -->
                    	
    <div class="content-box-header">
		
        <h3>Chỉnh sửa nội dung trang chủ</h3>
		
        <div class="clear"></div>
		
    </div> <!-- End .content-box-header -->
	
    <div class="content-box-content">
		
        <div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
            <CKEditor:CKEditorControl ID="txtContent" runat="server" Height="700px"></CKEditor:CKEditorControl>
            <p style="margin-left: 10px;">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Font-Size="Large"
                    Width="100px" Height="30px" Text="Lưu" onclick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Font-Size="Large"
                    Width="100px" Height="30px" Text="Hủy" onclick="btnCancel_Click" />
            </p>
            
            <div class="clear"></div>
        </div> <!-- End #tab1 -->     
		
    </div> <!-- End .content-box-content -->
	
</div>
</asp:Content>

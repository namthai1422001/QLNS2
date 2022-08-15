<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="QLNS.Controls.Search" %>

<%--<script src="<%= ResolveUrl("~/scripts/searchbox.js")%>" type="text/javascript"></script>--%>
<script src="<%= ResolveUrl("~/scripts/jquery.ui.position.js")%>" type="text/javascript"></script>    
<script src="<%= ResolveUrl("~/scripts/jquery.ui.autocomplete.js")%>" type="text/javascript"></script>

<link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.theme.css")%>" rel="stylesheet" type="text/css" />
<link href="<%= ResolveUrl("~/le-frog_Green/jquery.ui.autocomplete.css")%>" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function() {
        $(".arrowimg").click(function() {
            if ($(this).attr("title") == "Hiện") {
                $(".SearchIcon").hide();
                $(this).attr("title", "Ẩn");
                $(".bgimg").css("background-position", "0px -395px");
                $(".searchimg").css("display", "none");
                $("#<%= txtSearch.ClientID %>").slideDown(300);
            }
            else {
                $(this).attr("title", "Hiện");
                $(".bgimg").css("background-position", "0px -439px");
                $("#<%= txtSearch.ClientID %>").slideUp(150);
                $(".searchimg").css("display", "block");
                $(".searchimg").css("display", "block");
                $(".SearchIcon").show();
            }
        });
        $("#<%= txtSearch.ClientID %>").focus(function() {
            if ($(this).val() == "Tìm kiếm") {
                $(this).val("").css({ "color": "black" });
            }
        });
        $("#<%= txtSearch.ClientID %>").blur(function() {
            if ($(this).val() == "") {
                $(this).val("Tìm kiếm").css({ "color": "#C3C3C3" });
            }
            else {
                $(this).css({ "color": "black" });
            }
            $(".arrowimg").attr("title", "Hiện");
            $(".bgimg").css("background-position", "0px -439px");
            $(".searchimg").css("display", "block");
            $(".searchimg").css("display", "block");
            $(".SearchIcon").show();
            $(this).slideUp(150);
        });

        $("#<%= txtSearch.ClientID %>").autocomplete({
            minLength: 2,
            source:
                   function(request, response) {
                       $.ajax({
                           type: "POST",
                           url: "SearchKey.ashx?keyword=" + request.term,
                           contentType: "application/json; charset=utf-8",
                           dataType: "json",
                           success: function(data) {
                               response($.map(data, function(item) {
                                   return {
                                       value: item
                                   }
                               }))
                           },
                           error: function() {
                               alert("An unexpected error has occurred during processing.");
                           }
                       });
                   }
        });
    });
</script>

<div id="SearchBox">
    
    <asp:TextBox ID="txtSearch" runat="server" Text="Tìm kiếm"
        CssClass="text-input" ontextchanged="txtSearch_TextChanged" />
    
    <div class="arrowimg" title="Hiện">
        <img class="SearchIcon" src="<%= ResolveUrl("~/images/icons/search_icon.png") %>" alt="Tìm kiếm" title="Tìm kiếm" />
        <div class="bgimg">
        </div>
    </div>
</div>

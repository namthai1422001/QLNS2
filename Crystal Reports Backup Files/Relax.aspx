<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Relax.aspx.cs" Inherits="QLNS.Relax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        #gamemenu{display: block;}
        #gamemenu ul{list-style-type: none; float: left; width: 150px; margin-left: 5px;}
        #gamemenu ul li{cursor: pointer; background-color: #5FA600 !important; color: #fff; margin-bottom: 2px;}
        #gamemenu ul li:hover{background-color: Black !important;}
        #gamemenu ul li.current{background-color: Black !important;}
        #player{float: none; margin-left: auto; margin-right: auto; width: 768px;}
    </style>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#giaitri-nav a").addClass("current");
            $("#SearchBox").hide();

            $("#gamemenu ul:eq(0) li:eq(0)").addClass("current");

            $("#gamemenu ul li").click(function() {
                var file = $(this).attr("src");
                $(this).parent().parent().find("ul li").removeClass("current");
                $(this).addClass("current");

                var objplayer = $("#player object");
                $(objplayer).find("param[name=movie]").attr("value", file);
                $(objplayer).find("embed").attr("src", file);
                //alert("Duong dan la: " + $(objplayer).html() );
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
<div id="gamemenu">
    <ul>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Reach_for_the_sky.swf">
           Chạm tới trời cao 
        </li>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Ban_bong_mau.swf" >
           Bắn bóng màu
        </li>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Kiss.swf">
           Thi hôn
        </li>
    </ul>
    <ul>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Sudoku.swf">
           Sudoku
        </li>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Penalty_Shootout.swf">
           Panalty
        </li>
        <li src="http://phuongbacmanagement.herobo.com/Flash/Mariok_art_racing.swf">
           Mariok art racing
        </li>
    </ul>
       
</div>
<div id="player">
    <object width="768" height="480">
        <param value="http://phuongbacmanagement.herobo.com/Flash/Reach_for_the_sky.swf" name="movie" />
        <param value="true" name="allowFullScreen" />
        <param value="always" name="allowscriptaccess" />
        <embed width="768" height="480" allowfullscreen="true"
            allowscriptaccess="always"
            type="application/x-shockwave-flash"
            src="http://phuongbacmanagement.herobo.com/Flash/Reach_for_the_sky.swf" >
    </object>
</div>
<div class="clear"></div>
</asp:Content>

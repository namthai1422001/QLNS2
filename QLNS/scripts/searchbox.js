/*SearchBox*/
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
});
/*Xử lý trang đăng nhập*/

$(document).ready(function() {
    $("input").focus(function() {
        $(this).css("background-color", "#FFFBCC");
        $("#lblLoginError").html("");
        //$("#<%= lblLoginError.ClientID %>").html("");
    });
    $("#txtUsername").blur(function() {
        check_null("Username");
    });
    $("#txtPassword").blur(function() {
        check_null("Password");
    });
    //$("#txtUsername").focus();
});
function check_null(doituong) {
    if ($("#txt" + doituong.toString()).val() == "") {
        $("#" + doituong.toString() + "-error").html(document.getElementById("txt" + doituong.toString()).title + " không được để trống");
        $("#txt" + doituong.toString()).css("background-color", "#FFCECE");
        //$("#txt" + doituong.toString()).focus();
        return false;
    }
    else {
        $("#" + doituong.toString() + "-error").html("");
        $("#txt" + doituong.toString()).css("background-color", "#FFFFFF");
        return true;
    }
}
function kt_nhap() {
    var hople = 0;
    if (check_null("Password") == true) {
        hople++;
    }
    if (check_null("Username") == true) {
        hople++;
    }
    if (hople == 2) {
        return true;
    }
    return false;
}
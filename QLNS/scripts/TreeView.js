$(document).ready(function() {
    $(".StatusImg").click(function() {
        var objNode = $(this).parent().parent();
        var objChilds = $(objNode).find(".Childs");
        if ($(this).attr("alt") == "Đóng") {
            $(objChilds).css("display", "none");
            $(this).attr({ src: "../images/icons/Expand.gif", alt: "Mở", title: "Mở" });
        }
        else {
            $(objChilds).css("display", "block");
            $(this).attr({ src: "../images/icons/Collapse.gif", alt: "Đóng", title: "Đóng" });
        }
    });
});
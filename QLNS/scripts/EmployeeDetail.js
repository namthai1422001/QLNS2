/*Xem chi tiet nhan vien*/

$(document).ready(function() {
    //setup Detail dialog
    $("#Detail").dialog({
        autoOpen: false,
        modal: true,
        width: "730px",
        show: "explode",
        hide: "explode",
        resizable: false,
        draggable: true,
        title: "Chi tiết nhân viên",
        open: function(type, data) {
            $(this).parent().appendTo("form");
        }
    });
});

function showDialog(iddialog, id) {
    $.ajax({
        type: "POST",
        url: "Employee.aspx" + "/GetDetail",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            var objEmployee = eval("(" + data.d + ")");
            $("#lblFullname").html(objEmployee.HoTen);
            $("#lblSex").html((objEmployee.Nu == "True") ? "Nữ" : "Nam");
            $("#lblBirthday").html(objEmployee.Ngaysinh);
            $("#lblPhongBan").html(objEmployee.Tenphong);
            $("#lblPosition").html(objEmployee.TenCV);
            $("#lblAddress").html(objEmployee.Diachi);
            $("#lblPhoneNumber").html(objEmployee.Dienthoai);
            $("#lblNguoiThan").html(objEmployee.NguoiThan);
            $("#lblPhoneNumberNguoiThan").html(objEmployee.DienthoaiNT);
            $("#lblNgayVaoLam").html(objEmployee.Ngayvaolam);
            $("#lblDescription").html(objEmployee.GhiChu);
        },
        error: function() {
            alert("An unexpected error has occurred during processing.");
        }
    });
    $('#' + iddialog).dialog("open");
}

function closeDialog(iddialog) {
    $('#' + iddialog).dialog("close");
}
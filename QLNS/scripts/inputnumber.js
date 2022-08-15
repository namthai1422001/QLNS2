/*

    =========================== NGUYEN PHUONG BAC ===========================

*/


//Chi cho phep nhap so
//Chen vao input: onkeypress="return onlyNumbers(event)"

function onlyNumbers(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;

}

function onlyFloats(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57 || charCode == 46))
        return false;

    return true;

}

/*
    Chuyen so vua nhap thanh chuoi co phan cach bang dau phay (,)
    Them thuoc tinh nay vao input: onkeyup="keyuped(this)"
*/
function keyuped(obj) {
    var chuoiso = obj.value.toString();

    //Bo het ky tu phan cach
    chuoiso = chuoiso.replace(/,/g, '');
    var daochuoi = "";
    var i = chuoiso.length - 1;

    //Dao chuoi so truyen vao
    while (i >= 0) {
        daochuoi += chuoiso[i];
        i--;
    }
    i = 0;

    //Duyet chuoi vua duoc dao de them dau phan cach
    chuoiso = "";
    while (i + 3 < daochuoi.length) {
        chuoiso += daochuoi.substring(i, i + 3) + ",";
        i += 3;
    }
    chuoiso += daochuoi.substring(i, daochuoi.length + 1);
    i = chuoiso.length - 1;
    daochuoi = "";

    //Duyet nguoc chuoi de tra ve
    while (i >= 0) {
        daochuoi += chuoiso[i];
        i--;
    }
    obj.value = daochuoi;
}
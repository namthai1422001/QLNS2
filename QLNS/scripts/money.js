
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

//var old = '';

//function keypressed(id) {
//    old = $("#ins").val();
//}

//function keyupeds(id) {
//  var x = $("#" + id).val();
//   var arr = x.split('');
//    x = '';  // bỏ dau chấm.
//    for (var q = 0; q < arr.length; q++) {
//        if (arr[q] != ',')
//            x = x + arr[q];
//    }

//    // check xem co phai la so hay khong.
//    if (isNaN(x)) {
//        arr = old.split('');
//        x = '';  // bỏ dau chấm.
//        for (var q = 0; q < arr.length; q++) {
//            if (arr[q] == ',' || !isNaN(x))
//                x = x + arr[q];
//        }

//        $("#" + id).val(x);
//        return;
//    }

//    // dao chuoi them dau . vao sau 3 ki tu tiep theo
//    arr = x.split('');

//    var b = '';
//    var  z = 0;
//    for (var i = arr.length - 1; i > -1; i--) {
//        z++;
//        if (z % 3 == 0 && z != arr.length)
//            b += arr[i] + ",";
//        else
//            b += arr[i];
//    }

//    // dao nguoc chuoi hoan tat
//    arr = b.split('');
//    x = '';
//    for (i = arr.length - 1; i > -1; i--) {
//        x += arr[i];
//    }

//    $("#" + id).val(x);

//}
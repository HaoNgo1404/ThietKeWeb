﻿function getValue(id) {
    return document.getElementById(id).value.trim();
}

// Hiển thị lỗi
function showError(key, mess) {
    document.getElementById(key + '_error').innerHTML = mess;
}
function ValiDate() {
    var check = true;
    var email = getValue('email');
    if (email == ' ' || !/^[a-zA-Z0-9]+$/.test(email)) {
        check = false;
        showError('email', 'Bạn nhập sai email, mời nhập lại');
    }
    var password = getValue('password');
    if (password == '' || password.length < 1) {
        check = false;
        showError('password', 'Bạn nhập sai mật khẩu, mời nhập lại');
    }
    var repassword = getValue('repassword');
    if (repassword == '' || password != repassword) {
        check = false;
        showError('repassword', 'Bạn nhập sai xác nhận mật khẩu, mời nhập lại');
    }
    return check;
}
﻿var createController = {
    init: function () {
        createController.registerEvent();
    },
    registerEvent: function () {

        //$('.ddlQuocGia').off('change').on('change', function () {
        //    var optionValue = $('.ddlQuocGia').val();
        //    createController.loadDdlThanhPhoByQuocGia(optionValue);
        //});

    }
    //loadDdlThanhPhoByQuocGia: function (optionValue) {
    //    $('.ddlThanhPho').html('');
    //    var option = '';

    //    $.ajax({
    //        url: '/KhachHangs/GetThanhPhosByQuocGia',
    //        type: 'GET',
    //        data: {
    //            idQuocGia: optionValue
    //        },
    //        dataType: 'json',
    //        success: function (response) {
    //            var data = JSON.parse(response.data);
    //            console.log(data);
    //            $.each(data, function (i, item) {
    //                option = option + '<option value="' + item.Id + '">' + item.TenThanhPho + '</option>';

    //            });
    //            $('.ddlThanhPho').html(option);
    //        }
    //    });
    //}
};
createController.init();
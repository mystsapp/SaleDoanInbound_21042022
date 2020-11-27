var editController = {
    init: function () {
        editController.registerEvent();
    },
    registerEvent: function () {

        $('#btnTaoKhachHang').off('click').on('click', function () {
            if ($('#txtEmail').val() === '') {
                bootbox.alert({
                    size: "small",
                    title: "Infomation!",
                    message: "Vui lòng cập nhật email, sau đó hãy tạo khách hàng trên VNPTTour này đã có invoices!"
                });
                return;
            }
            //else {
            //    $('#frmTaoKhachHang').submit();
            //}

        })

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
editController.init();
var editController = {
    init: function () {
        editController.registerEvent();
    },
    registerEvent: function () {

        $('#btnTaoKhachHang').click(function (e) {

            if ($('#txtEmail').val() === '') {
                bootbox.alert({
                    size: "small",
                    title: "Infomation!",
                    message: "Vui lòng cập nhật email, sau đó hãy tạo khách hàng trên VNPT!"
                });
                return false;
            }
            else {
                return confirm("are your ok?");
                //bootbox.confirm({
                //    message: "Bạn muốn tạo / cập nhật thông tin khách hàng trên hoá đơn điện tử VNPT?",
                //    buttons: {
                //        confirm: {
                //            label: 'Yes',
                //            className: 'btn-success'
                //        },
                //        cancel: {
                //            label: 'No',
                //            className: 'btn-danger'
                //        }
                //    },
                //    callback: function (result) {
                //        if (!result) {
                //            alert('false');
                //        }
                //        else {
                //            alert('true');
                //        }
                //        //console.log('This was logged in the callback: ' + result);
                //    }
                //});

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
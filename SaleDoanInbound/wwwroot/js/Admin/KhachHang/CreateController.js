var CreateController = {
    init: function () {
        CreateController.registerEvent();
    },
    registerEvent: function () {

        $('.ddlQuocGia').off('change').on('change', function () {
            var optionValue = $('.ddlQuocGia').val();
            CreateController.loadDdlThanhPhoByQuocGia(optionValue);
        });

    },
    loadDdlThanhPhoByQuocGia: function (optionValue) {
        $('.ddlThanhPho').html('');
        var option = '';

        $.ajax({
            url: '/KhachHangs/GetThanhPhosByQuocGia',
            type: 'GET',
            data: {
                idQuocGia: optionValue
            },
            dataType: 'json',
            success: function (response) {
                var data = JSON.parse(response.data);
                console.log(data);
                $.each(data, function (i, item) {
                    option = option + '<option value="' + item.Id + '">' + item.TenThanhPho + '</option>';

                });
                $('.ddlThanhPho').html(option);
            }
        });
    }
};
CreateController.init();
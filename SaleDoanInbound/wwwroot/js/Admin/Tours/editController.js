function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}


var CreateController = {
    init: function () {
        CreateController.registerEvent();
    },
    registerEvent: function () {

        // format .numbers
        $('input.numbers').keyup(function (event) {

            // Chỉ cho nhập số
            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return addCommas(value);
            });
        });

        // btnSubmit 
        $('#btnSubmit').off('click').on('click', function () {
            var fileExtension = ['xls', 'xlsx'];
            var filename = $('#fUpload').val();
            if (filename.length !== 0) {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) === -1) {
                    bootbox.alert({
                        size: "small",
                        title: "Infomation",
                        message: "Chọn chưa đúng định dạng <b> Excel! </b>"
                    });

                    return false;
                }
            }

        });
        // btnSubmit

        // for tuyentq edit
        var selectedValues = new Array();
        //selectedValues[0] = "BAL";
        //selectedValues[1] = "BAN";

        //$('#ddlTuyenTQ').val(selectedValues);
        var tuyentq = $('#hidDdlTuyenTQ').val();
        selectedValues = tuyentq.split(',');
        $('#ddlTuyenTQ').val(selectedValues);
        console.log(selectedValues);
        // for tuyentq edit

        $('#ddlMaKh').off('change').on('change', function () {
            var optionValue = $(this).val();
            CreateController.GetKHByMaKH(optionValue);
        });

        // gan'  qua hidden field
        $('#ddlTuyenTQ').off('change').on('change', function () {
            tuyenTQ = $(this).val();

            $('#hidDdlTuyenTQ').val(tuyenTQ);
            
        });

    },
    GetKHByMaKH: function (optionValue) {

        $.ajax({
            url: '/Tours/GetKHByMaKH',
            type: 'GET',
            data: {
                maKH: optionValue
            },
            dataType: 'json',
            success: function (response) {
                var khachHang = JSON.parse(response.khachHang)
                //console.log(khachHang.Address);
                //console.log(khachHang);
                $('#txtTenKH').val(khachHang.Name)
                $('#txtDienThoai').val(khachHang.Tel)
                $('#txtFax').val(khachHang.Fax)
                $('#txtDiaChi').val(khachHang.Address)
            }
        });
    }
};
CreateController.init();
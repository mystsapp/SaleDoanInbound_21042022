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

        //var selectedValues = new Array();
        //selectedValues[0] = "BAL";
        //selectedValues[1] = "BAN";

        //$('#ddlTuyenTQ').val(selectedValues);
        //selectedValues = $('#hidTuyenTQ').val().split(',');
        //$('#ddlTuyenTQ').val(selectedValues);
        //console.log(selectedValues);

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
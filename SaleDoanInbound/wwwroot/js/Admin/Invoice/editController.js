function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var editController = {
    init: function () {
        editController.loaiIVChange();
        editController.registerEvent();
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

        // loaiiv : R - E
        $('#ddlLoaiIV').off('change').on('change', function () {
            editController.loaiIVChange();
        });
        // loaiiv : R - E

    },
    loaiIVChange: function () {
        loaiIV = $('#ddlLoaiIV').val();
        if (loaiIV === 'R') {
            $('#txtTenKhach').prop('disabled', true);
            $('#txtGhiChu').prop('disabled', true);
        }
        else {
            $('#txtTenKhach').prop('disabled', false);
            $('#txtGhiChu').prop('disabled', false);
        }
    }
    
};
editController.init();
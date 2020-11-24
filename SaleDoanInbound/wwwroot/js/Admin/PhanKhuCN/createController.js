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

        // gan'  qua hidden field
        $('#ddlChiNhanhs').off('change').on('change', function () {
            chiNhanh = $(this).val();

            $('#hidDdlChiNhanhs').val(chiNhanh);

        });
        
    }
    
};
CreateController.init();
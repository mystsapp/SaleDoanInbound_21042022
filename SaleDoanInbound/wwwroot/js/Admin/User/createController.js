function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}


var CreateController = {
    init: function () {
        optionValue = $('#ddlRoles').val();
        CreateController.DdlRolesChange(optionValue);

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


        $('#ddlRoles').on('change', function () {

            optionValue = $(this).val();

            CreateController.DdlRolesChange(optionValue);

        });

    },
    DdlRolesChange: function (optionValue) {

        if (optionValue === '1' || optionValue === '2') {// Admins, Users
            $('#ddlPhongBans').prop('disabled', true);
        }
        else {
            $('#ddlPhongBans').prop('disabled', false);
        }

    }
};
CreateController.init();
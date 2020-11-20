function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var editController = {
    init: function () {
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

        $('#txtQuantity').off('blur').on('blur', function () {
            editController.amountFunction();
        });

        $('#txtUnitPrice').off('blur').on('blur', function () {
            editController.amountFunction();
        });

        $('#ckDS').change(function () {
            $('#ckDLHH').prop('checked', false);


        });

        $('#ckDLHH').change(function () {
            $('#ckDS').prop('checked', false);

        });

    },
    amountFunction: function () {
        quantity = $('#txtQuantity').val();
        unitPrice = $('#txtUnitPrice').val();

        $.ajax({
            url: '/CTVATs/GetAount',
            data: {
                quantity: quantity,
                unitPrice: unitPrice
            },
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                // console.log(response);
                if (response.status) {
                    amount = numeral(response.data).format('0,0');
                    $('#txtAmount').val(amount);
                }

            }
        });

    }

};
editController.init();
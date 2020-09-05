var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
        $.each($('.tdVal'), function (i, item) {

            var id = $(item).data('id')
            var invoicesCount = indexController.checkInvoices(id);
            if (invoicesCount > 0) {
                $('#btnHuy').prop('disabled', true);
            }
        });

        $('.tdVal').click(function () {
            id = $(this).data('id');
            $('#hidId').val(id);
            //var page = $('.active .page-link').text();
            var page = $('.active span').text();
            $('#hidPage').val(page);

            //$.ajax({
            //    url: '/CapThes/Index',
            //    data: {
            //        maCT: id
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {

            //    }
            //});

            $('#btnSubmit').click();
        });

    },
    checkInvoices: function (tourId) {

            $.ajax({
                url: '/Tours/CheckInvoices',
                data: {
                    tourId: tourId
                },
                dataType: 'json',
                type: 'GET',
                success: function (response) {
                    return response.count;
                }
            });

    }
};
indexController.init();
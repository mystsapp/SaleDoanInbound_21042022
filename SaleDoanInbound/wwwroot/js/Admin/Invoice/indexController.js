var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
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

        $('.tdValInvoice').click(function () {
            var invoiceId = $(this).data('id');
            $('#hidInvoiceId').val(invoiceId);
            var tourIBId = $(this).data('touribid');
            $('#hidId').val(tourIBId);
            // page
            var page = $('.active .page-link').text();
            $('#hidPage').val(page);

            $('#btnSubmit').click();
        });
    }
};
indexController.init();
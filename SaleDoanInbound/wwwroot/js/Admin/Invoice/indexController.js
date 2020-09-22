var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
        $.each($('.cursor-pointer'), function (i, item) {

            var huy = $(item).data('huy');
            //console.log(huy);
            if (huy === 'True') {
                $(this).addClass('bg-secondary');
            }

        });

        $('.tdVal').click(function () {
            id = $(this).data('id');
            $('#hidId').val(id);
            //var page = $('.active .page-link').text();
            //var page = $('.active span').text();
            //$('#hidPage').val(page);
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

        $('#btnNewCTVAT').off('click').on('click', function (e) {
            invoiceId = $(this).data('id');
            if (invoiceId === '') {
                e.preventDefault();
                bootbox.alert({
                    title: "Information",
                    size: "small",
                    message: "Bạn chưa chọn invoice nào!"
                });
            }
        });
        $('#btnNewCTInvoice').off('click').on('click', function (e) {
            invoiceId = $(this).data('id');
            if (invoiceId === '') {
                e.preventDefault();
                bootbox.alert({
                    title: "Information",
                    size: "small",
                    message: "Bạn chưa chọn invoice nào!"
                });
            }
        });


        $('.btnHuyInvoice').off('click').on('click', function () {
            id = $(this).data('id');
            strUrl = $(this).data('url');

            $.get('/Invoices/HuyInvoicePartial', { id: id, strUrl: strUrl }, function (response) {

                console.log(response);
                $('#huyInvoiceModal').modal('show');
                $('.huyInvoicePartial').html(response);
                $('#huyInvoiceModal').draggable();
            });
        });


    }
};
indexController.init();
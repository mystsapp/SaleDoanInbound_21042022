var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
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
    }
};
indexController.init();
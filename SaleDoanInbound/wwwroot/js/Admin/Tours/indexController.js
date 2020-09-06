var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
        $.each($('.tdVal'), function (i, item) {

            var id = $(item).data('id');
            $.when(indexController.checkInvoices(id)).done(function (response) {
                if (response.status) { // co invoice roi
                    $('.btnHuy').addClass('disabled');
                }
            })
            //$.when(indexController.checkHuy(id)).done(function (response) {
            //    if (response.status === true) { // check huy
            //        console.log(response);
            //        $('.cursor-pointer').addClass('bg-secondary');
            //    }
            //})
            //var invoicesCount = indexController.checkInvoices(id);
            //if (invoicesCount > 0) {
            //    $('#btnHuy').prop('disabled', true);
            //}
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

        return $.ajax({
            url: '/Tours/CheckInvoices',
            data: {
                tourId: tourId
            },
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                //console.log(response.status);
                //if (response.status) {
                //    console.log(response.toursCount);
                //    return response.toursCount;
                //}
                    
                //else
                //    return 10;
            }
        });

    },
    checkHuy: function (tourId) {

        return $.ajax({
            url: '/Tours/CheckHuy',
            data: {
                tourId: tourId
            },
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                //console.log(response.status);
                //if (response.status) {
                //    console.log(response.toursCount);
                //    return response.toursCount;
                //}
                    
                //else
                //    return 10;
            }
        });

    }
};
indexController.init();
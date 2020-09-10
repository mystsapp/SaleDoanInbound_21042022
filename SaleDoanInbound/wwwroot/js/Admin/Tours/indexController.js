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
        //});

        $('.btnKhoiPhucTour').off('click').on('click', function () {
            //return $.ajax({
            //    url: '/Tours/HuyTourPartial',
            //    data: {
            //        id: $(this).data('id')
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {
            //        console.log(response);
            //        //if (response.status) {
            //        //    console.log(response.toursCount);
            //        //    return response.toursCount;
            //        //}

            //        //else
            //        //    return 10;
            //    }
            //});
            id = $(this).data('id');
            bootbox.confirm({
                title: "Restore Confirm?",
                message: "Bạn có muốn <b> khôi phục </b> User này không?",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Confirm'
                    }

                },
                callback: function (result) {
                    if (result) {
                        $('#hidTourId').val(id);
                        $('#frmKhoiPhucTour').submit();
                    }
                }

            });
        });

        $('.btnHuyTour').off('click').on('click', function () {
            //return $.ajax({
            //    url: '/Tours/HuyTourPartial',
            //    data: {
            //        id: $(this).data('id')
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {
            //        console.log(response);
            //        //if (response.status) {
            //        //    console.log(response.toursCount);
            //        //    return response.toursCount;
            //        //}

            //        //else
            //        //    return 10;
            //    }
            //});
            strUrl = $('.btnHuyTour').data('url');
            $.get('/Tours/HuyTourPartial', { id: $(this).data('id'), strUrl: strUrl }, function (data) {

                $('#huyTourModal').modal('show');
                $('.huyTourPartial').html(data);
                $('#huyTourModal').draggable();
            });
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

    }
    //checkInvoices: function (tourId) {

    //    return $.ajax({
    //        url: '/Tours/CheckInvoices',
    //        data: {
    //            tourId: tourId
    //        },
    //        dataType: 'json',
    //        type: 'GET',
    //        success: function (response) {
    //            //console.log(response.status);
    //            //if (response.status) {
    //            //    console.log(response.toursCount);
    //            //    return response.toursCount;
    //            //}

    //            //else
    //            //    return 10;
    //        }
    //    });

    //}
    //checkHuy: function (tourId) {

    //    return $.ajax({
    //        url: '/Tours/CheckHuy',
    //        data: {
    //            tourId: tourId
    //        },
    //        dataType: 'json',
    //        type: 'GET',
    //        success: function (response) {
    //            //console.log(response.status);
    //            //if (response.status) {
    //            //    console.log(response.toursCount);
    //            //    return response.toursCount;
    //            //}

    //            //else
    //            //    return 10;
    //        }
    //    });

    //}
};
indexController.init();
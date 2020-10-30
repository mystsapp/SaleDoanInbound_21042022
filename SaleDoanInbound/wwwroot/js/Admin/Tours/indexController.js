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
                message: "Bạn có muốn <b> khôi phục </b> Tour này không?",
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

        // check invoices
        $('.btnHuy').off('click').on('click', function () {
            invoices = $(this).data('invoices');
            if (parseInt(invoices) > 0) {
                bootbox.alert({
                    size: "small",
                    title: "Infomation!",
                    message: "Tour này đã có invoices!"
                });
                $('#confirmDeleteSpan_' + $(this).data('id')).hide();
                $('#deleteSpan_' + $(this).data('id')).show();
            }
        });
        // check invoices

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

        // tour click --> load tourpro in qltour
        $('tr .tdVal').click(function () {

            tourId = $(this).data('id');
            
            var url = '/Tours/KeToan_TourInfoByTourPartial';
            $.get(url, { tourId: tourId }, function (response) {

                $('#tabs_KeToan_TourInfo').html(response);

            });

            //$('#hidId').val(tourId);
            ////var page = $('.active .page-link').text();
            //var page = $('.active span').text();
            //$('#hidPage').val(page);

            ////$.ajax({
            ////    url: '/CapThes/Index',
            ////    data: {
            ////        maCT: id
            ////    },
            ////    dataType: 'json',
            ////    type: 'GET',
            ////    success: function (response) {

            ////    }
            ////});

            //$('#btnSubmit').click();

        });
        // tour click --> load tourpro in qltour


        // hrefToTabInvoices click
        //$('#hrefToTabInvoices').click(function () {

        //    var tourId = $(this).data('tourid');
        //    indexController.loadInvoices(tourId);
        //});
        // hrefToTabInvoices click


        // giu trang thai tour click
        $('#tourTbl .cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai tour click

        // invoice click --> CTInvoices & CTVAT
        $('.tdInvoiceVal').click(function () {

            invoiceId = $(this).data('id');
            var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
            $.get(url, { invoiceId: invoiceId }, function (response) {

                $('.cTInVoiceCTVAT').html(response);

            });
        });
        // invoice click --> CTInvoices & CTVAT

    }

    //loadBienNhans: function (tourid) {

    //    var url = '/BienNhans/BienNhansByTourPartial';
    //    $.get(url, { tourId: tourid }, function (response) {
            
    //            $('#tab_biennhans').html(response);

    //        });
    //},

    //loadInvoices: function (tourid) {

    //    var url = '/Invoices/IncoicesByTourPartial';
    //    $.get(url, { tourId: tourid }, function (response) {
            
    //            $('#tab_invoices').html(response);

    //        });
    //},

    //load_KeToan_TourInfo_QLTour: function (tourId) {

    //    var url = '/Tours/KeToan_TourInfoByTourPartial';
    //    $.get(url, { tourId: tourId }, function (response) {
            
    //            $('#tabs_TourInfo').html(response);

    //        });
    //}
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
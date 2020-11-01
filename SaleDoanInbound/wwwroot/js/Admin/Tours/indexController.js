
var indexController = {
    init: function () {

        toastr.options = { // toastr options
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "2000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

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

            $('#createInvoicePartial').hide();
            $('#createCTInvoicePartial').hide();            

            tourId = $(this).data('id');
            var url = '/Tours/KeToan_TourInfoByTourPartial';
            $.get(url, { tourId: tourId }, function (response) {

                $('#tabs_KeToan_TourInfo').html(response);
                $('#tabs_KeToan_TourInfo').show();

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
            indexController.Load_CTInvoice_CTVAT_Partial(invoiceId);

            //var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
            //$.get(url, { invoiceId: invoiceId }, function (response) {

            //    $('.cTInVoiceCTVAT').html(response);

            //});
        });
        // invoice click --> CTInvoices & CTVAT

        //////////////////////////////////////////////////////////////////////////////// CreateInvoicePartial finish post

        // create new invoice
        $('#btnNewInvoice').off('click').on('click', function () {

            tourid = $(this).data('tourid');

            $('#tabs_KeToan_TourInfo').hide();

            var url = '/Invoices/CreateInvoicePartial';
            $.get(url, { tourid: tourid }, function (response) {

                $('#createInvoicePartial').show();

                $('#createInvoicePartial').html(response);

            });
        });
        // create new invoice

        $('#frmInvoiceCreatePartial').validate({
            //rules: {
            //    email: {
            //        required: true,
            //        email: true,
            //    },
            //    NgayDen: {
            //        required: true
            //    },
            //    chuDetour1: {
            //        required: true
            //    },
            //},
            //messages: {
            //    email: {
            //        required: "Please enter a email address",
            //        email: "Please enter a vaild email address"
            //    },
            //    NgayDen: {
            //        required: "Please provide a date"
            //    },
            //    chuDetour1: "Please enter value"
            //},
            errorElement: 'span',
            errorPlacement: function (error, element) {
                // add error text

                //error.addClass('invalid-feedback').removeClass('error');
                //element.closest('.chuDeTourGroup').append(error);
            },
            highlight: function (element, errorClass, validClass) {
                $(element).addClass('is-invalid');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });

        $('#ddlLoaiTien').rules('add', {
            required: true,
            messages: {
                // required: "Ngày đến không được để trống"
            }
        });

        $('#txtTyGia').rules('add', {
            required: true,
            messages: {
                // required: "Ngày đến không được để trống"
            }
        });

        $('#btnCreateInvoicePartial').off('click').on('click', function () {

            // if frm valid
            if ($('#frmInvoiceCreatePartial').valid()) {
                var invoice = $('#frmInvoiceCreatePartial').serialize();
                $.ajax({
                    type: "POST",
                    url: "/Invoices/CreateInvoicePartial",
                    data: invoice,
                    dataType: "json",
                    success: function (response) {
                        if (response.status) {

                            toastr.success('Thêm mới invoice thành công!');

                            $('#createInvoicePartial').hide();

                            $('#tabs_KeToan_TourInfo').show();
                            tourId = tourIdInCreateInvoicePartial; // receive it from CreateInvoicePartial
                            indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                        }
                        else {
                            toastr.error('Thêm mới invoice không thành công!');
                            //  debugger
                            // $('#createInvoiceModal').show();
                            // $('.createInvoicePartial').html(response);
                            //$('#createInvoiceModal').draggable();

                            //tourid = $(this).data('tourid');
                            //var url = '/Invoices/CreateInvoicePartial';
                            //$.get(url, { tourid: tourid }, function (response) {

                            //    $('#createInvoiceModal').show();
                            //    $('.createInvoicePartial').html(response);
                            //    $('#createInvoiceModal').draggable();

                            //});

                        }
                    }
                });
            }
        });

        //////////////////////////////////////////////////////////////////////////////// CreateInvoicePartial finish post

        //////////////////////////////////////////////////////////////////////////////// EfitInvoicePartial finish post

        // edit invoice
        $('#btnEditInvoice').off('click').on('click', function () {
            
            tourid = $(this).data('tourid');
            invoiceId = $(this).data('invoiceid');

            $('#tabs_KeToan_TourInfo').hide();
            $('#createInvoicePartial').hide();

            var url = '/Invoices/EditInvoicePartial';
            $.get(url, { tourid: tourid, invoiceId: invoiceId }, function (response) {

                $('#editInvoicePartial').show();

                $('#editInvoicePartial').html(response);

            });
        });
        // edit invoice

        // --> btn submit edit invoice in its partial

        //////////////////////////////////////////////////////////////////////////////// EfitInvoicePartial finish post

        //////////////////////////////////////////////////////////////////////////////// CTInvoicesCTVATsInInvoicePartial

        // create CTInvoice
        $('#btnNewCTInvoice').off('click').on('click', function () {
            
            invoiceId = $(this).data('invoiceid');

            $('#tabs_KeToan_TourInfo').hide();
            $('#createInvoicePartial').hide();
            $('#editInvoicePartial').hide();

            var url = '/CTVATs/CreateCTInvoicePartial';
            $.get(url, { invoiceId: invoiceId }, function (response) {

                $('#createCTInvoicePartial').show();
                $('#createCTInvoicePartial').html(response);

            });
        });
        // create CTInvoice
        
    $('#btnCreateCTInvoicePartial').off('click').on('click', function () {
        debugger
        // if frm valid
        if ($('#frmCTInvoiceCreate').valid()) {
            var invoice = $('#frmCTInvoiceCreate').serialize();
            $.ajax({
                type: "POST",
                url: "/CTVATs/CreateCTInvoicePartial",
                data: invoice,
                dataType: "json",
                success: function (response) {
                    if (response.status) {

                        toastr.success('Thêm mới CT invoice thành công!'); // toastr in admin/tour/indexController.js

                        $('#createCTInvoicePartial').hide();

                        $('#tabs_KeToan_TourInfo').show();
                        tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                        indexController.Load_KeToan_TourInfoByTourPartial(tourId);
                        debugger
                        invoiceIdReturn = $('#hidInvoiceIdInCreateCTInvoicePartial').val();
                        var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
                        $.get(url, { invoiceId: invoiceIdReturn }, function (response) {

                            $('.cTInVoiceCTVAT').html(response);

                        });

                    }
                    else {
                        toastr.error(response.message);
                        //  debugger
                        // $('#createInvoiceModal').show();
                        // $('.createInvoicePartial').html(response);
                        //$('#createInvoiceModal').draggable();

                        //tourid = $(this).data('tourid');
                        //var url = '/Invoices/CreateInvoicePartial';
                        //$.get(url, { tourid: tourid }, function (response) {

                        //    $('#createInvoiceModal').show();
                        //    $('.createInvoicePartial').html(response);
                        //    $('#createInvoiceModal').draggable();

                        //});

                    }
                }
            });
        }
    });

        //////////////////////////////////////////////////////////////////////////////// CTInvoicesCTVATsInInvoicePartial
    },
    Load_KeToan_TourInfoByTourPartial: function (tourId) {
        var url = '/Tours/KeToan_TourInfoByTourPartial';
        $.get(url, { tourId: tourId }, function (response) {

            $('#tabs_KeToan_TourInfo').html(response);
            $('#tabs_KeToan_TourInfo').show();

        });

    },
    Load_CTInvoice_CTVAT_Partial: function (invoiceId) {
        var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
        $.get(url, { invoiceId: invoiceId }, function (response) {

            $('.cTInVoiceCTVAT').html(response);

        });
    }


};
indexController.init();
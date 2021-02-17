
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
            "hideDuration": "2000",
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
            
            //$('#createInvoicePartial').hide();
            //$('#createCTInvoicePartial').hide();
            $('#editInvoicePartial').hide(500);

            tourId = $(this).data('id');
            var url = '/Tours/KeToan_TourInfoByTourPartial';
            $.get(url, { tourId: tourId }, function (response) {

                $('#tabs_KeToan_TourInfo').html(response);
                $('#tabs_KeToan_TourInfo').show(500);

            });

        });
        // tour click --> load tourpro in qltour

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

        //////////////////////////////////////////////////////////////////////////////// CreateKhachPartial

        // DSKhachHang

        $('#tab_DsKhachHang').off('click').on('click', function () {
            
            tourid = $(this).data('tourid');

            indexController.Load_DSKhachHang(tourid);
        });

        // DSKhachHang

        // create new
        //$('#btnNewKhachHang').off('click').on('click', function () {

        //    tourid = $(this).data('tourid');

        //    $('#sDSKhach').hide(500);

        //    var url = '/DSKhachHangs/KhachHangCreatePartial';
        //    $.get(url, { tourid: tourid }, function (response) {

        //        $('#khachHangCreatePartial').show(500);

        //        $('#khachHangCreatePartial').html(response);

        //    });
        //});
        // create new

        // close create partial
        $('#btnCloseKhachCreatePartial').off('click').on('click', function () {
            $('#sDSKhach').show(500);
        });
        $('#btnBackKhachHangCreatePartial').off('click').on('click', function () {
            $('#khachHangCreatePartial').hide(500);
            $('#sDSKhach').show(500);
        });
        // close create invoice partial

        //////////////////////////////////////////////////////////////////////////////// CreateKhachPartial
        
        //////////////////////////////////////////////////////////////////////////////// EditKhachPartial

        $('.btnEditKhachHang').off('click').on('click', function (e) {
            e.preventDefault();
            
            idKhachTour = $(this).data('id');

            $('#sDSKhach').hide(500);
            $('#khachHangCreatePartial').hide(500);
            //$('#createInvoicePartial').hide(500);

            var url = '/DSKhachHangs/KhachHangEditPartial';
            $.get(url, { id: idKhachTour }, function (response) {

                $('#khachHangEditPartial').show(500);

                $('#khachHangEditPartial').html(response);

            });
        });

        // close create partial
        $('#btnCloseKhachEditPartial').off('click').on('click', function () {
            $('#sDSKhach').show(500);
        });
        $('#btnBackKhachHangEditPartial').off('click').on('click', function () {
            $('#khachHangEditPartial').hide(500);
            $('#sDSKhach').show(500);
        });
        // close create invoice partial

        //////////////////////////////////////////////////////////////////////////////// EditKhachPartial

        //////////////////////////////////////////////////////////////////////////////// XoaKhachPartial
        
        $('.btnXoaKhachHangPartial').off('click').on('click', function () {

            id = $(this).data('id');
            
            $.ajax({
                url: '/DSKhachHangs/Delete',
                data: {
                    id: id
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status) {

                        toastr.success('Xóa thành công!'); // toastr in admin/tour/indexController.js

                        tourid = response.tourid;

                        indexController.Load_DSKhachHang(tourid);
                    }
                    else {
                        toastr.error(response.message);

                    }
                }
            });

        });
        
        //////////////////////////////////////////////////////////////////////////////// Xoa1KhachPartial

        //////////////////////////////////////////////////////////////////////////////// CreateInvoicePartial finish post

        // create new invoice
        $('#btnNewInvoice').off('click').on('click', function () {

            tourid = $(this).data('tourid');

            $('#tabs_KeToan_TourInfo').hide(500);

            var url = '/Invoices/CreateInvoicePartial';
            $.get(url, { tourid: tourid }, function (response) {

                $('#createInvoicePartial').show(500);

                $('#createInvoicePartial').html(response);

            });
        });
        // create new invoice
        // close crete invoice partial
        $('#btnCloseCreateInvoicePartial').off('click').on('click', function () {
            $('#tabs_KeToan_TourInfo').show(500);
        });
        $('#btnBackCreateInvoicePartial').off('click').on('click', function () {
            $('#createInvoicePartial').hide(500);
            $('#tabs_KeToan_TourInfo').show(500);
        });
        // close crete invoice partial

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

        //////////////////////////////////////////////////////////////////////////////// EditInvoicePartial finish post

        //// edit invoice

        $('.btnEditInvoice').on('click', function () {

            tourid = $(this).data('tourid');
            invoiceId = $(this).data('invoiceid');

            $('#tabs_KeToan_TourInfo').hide(500);
            //$('#createInvoicePartial').hide(500);

            var url = '/Invoices/EditInvoicePartial';
            $.get(url, { tourid: tourid, invoiceId: invoiceId }, function (response) {

                $('#editInvoicePartial').show(500);

                $('#editInvoicePartial').html(response);

            });
        });

        //// edit invoice

        // back editinvoicepartial

        // back editinvoicepartial
        // --> btn submit edit invoice in its partial

        // del invoice ( huy invoice)

        $('.btnHuyInvoice').off('click').on('click', function () {

            id = $(this).data('id');
            strUrl = $(this).data('url');

            $.get('/Invoices/HuyInvoicePartial', { id: id, strUrl: strUrl }, function (response) {


                $('#huyInvoiceModal').modal('show');
                $('.huyInvoicePartial').html(response);
                $('#huyInvoiceModal').draggable();
            });
        });
        // btnHuyInvoicePartialSubmit in its partial

        // del invoice ( huy invoice)

        //////////////////////////////////////////////////////////////////////////////// EditInvoicePartial finish post

        //////////////////////////////////////////////////////////////////////////////// CTInvoicesCTVATsInInvoicePartial

        // create CTInvoice
        $('#btnNewCTInvoice').off('click').on('click', function () {
            invoiceId = $(this).data('invoiceid');

            $('#tabs_KeToan_TourInfo').hide(500);
            $('#createInvoicePartial').hide();
            $('#editInvoicePartial').hide();

            var url = '/CTVATs/CreateCTInvoicePartial';
            $.get(url, { invoiceId: invoiceId }, function (response) {

                $('#createCTInvoicePartial').show(500);
                $('#createCTInvoicePartial').html(response);

            });
        });
        // create CTInvoice

        $('#btnCreateCTInvoicePartial').off('click').on('click', function () {

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

                            $('#tabs_KeToan_TourInfo').show(500);
                            //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                            //indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                            invoiceIdReturn = $('#hidInvoiceIdInCreateCTInvoicePartial').val();
                            var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
                            $.get(url, { invoiceId: invoiceIdReturn }, function (response) {

                                $('.cTInVoiceCTVAT').show(500);
                                $('.cTInVoiceCTVAT').html(response);

                            });

                        }
                        else {
                            toastr.error(response.message);

                        }
                    }
                });
            }
        });

        // del CTInvoice

        $('.btnDeleteInCTInvoiceCTVATPartial').off('click').on('click', function () {

            invoiceId = $(this).data('invoiceid');
            ctInvoiceId = $(this).data('ctinvoiceid');

            $.ajax({
                type: "POST",
                url: "/CTVATs/DeleteCTInvoiceInCTInvoicesCTVATsPartial",
                data: {
                    //invoiceId: invoiceId,
                    ctInvoiceId: ctInvoiceId
                },
                dataType: "json",
                success: function (response) {
                    if (response.status) {

                        toastr.success('Xóa CT invoice thành công!'); // toastr in admin/tour/indexController.js

                        $('#editCTInvoicePartial').hide();

                        //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                        //indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                        invoiceIdReturn = invoiceId;
                        indexController.Load_CTInvoice_CTVAT_Partial(invoiceIdReturn);
                    }
                    else {
                        toastr.error(response.message);

                    }
                }
            });
        });

        // BackCreateCTInvoicePartial
        $('#btnBackCreateCTInvoicePartial').off('click').on('click', function () {

            $('#createCTInvoicePartial').hide(500);

            $('#tabs_KeToan_TourInfo').show(500);
        });
        // BackCreateCTInvoicePartial
        //////////////////////////////////////////////////////////////////////////////// CTInvoicesCTVATsInInvoicePartial

        //////////////////////////////////////////////////////////////////////////////// editCTInvoicePartial

        // edit CTInvoice btnEditCTInvoice
        $('.btnEditCTInvoice').off('click').on('click', function () {

            invoiceId = $(this).data('invoiceid');
            ctInvoiceId = $(this).data('ctinvoiceid');

            //$('#tabs_KeToan_TourInfo').hide();
            $('#cTInVoiceCTVAT').hide();
            $('#createInvoicePartial').hide();
            $('#editInvoicePartial').hide();
            $('#createCTInvoicePartial').hide();

            var url = '/CTVATs/EditCTInvoicePartial';
            $.get(url, { invoiceId: invoiceId, ctInvoiceId: ctInvoiceId }, function (response) {

                $('#editCTInvoicePartial').show();
                $('#editCTInvoicePartial').html(response);

            });
        });
        // edit CTInvoice

        $('#btnBackEditCTInvoicePartial').off('click').on('click', function () {

            //tourId = $(this).data('tourid');
            //indexController.Load_KeToan_TourInfoByTourPartial(tourId);
            //invoiceId = $('#hidInvoiceId').val();
            //indexController.Load_CTInvoice_CTVAT_Partial(invoiceId);

            $('#editCTInvoicePartial').hide(500);
            $('#cTInVoiceCTVAT').show(500);
        });

        $('#btnSubmitEditCTInvoicePartial').off('click').on('click', function () {

            // if frm valid
            if ($('#frmCTInvoiceEdit').valid()) {
                var invoice = $('#frmCTInvoiceEdit').serialize();
                $.ajax({
                    type: "POST",
                    url: "/CTVATs/EditCTInvoicePartial",
                    data: invoice,
                    dataType: "json",
                    success: function (response) {
                        if (response.status) {

                            toastr.success('Cập nhật CT invoice thành công!'); // toastr in admin/tour/indexController.js

                            $('#editCTInvoicePartial').hide();

                            //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                            //indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                            invoiceIdReturn = $('#hidInvoiceIdInEditCTInvoicePartial').val();
                            indexController.Load_CTInvoice_CTVAT_Partial(invoiceIdReturn);
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

        // btnCopyCTVAT 
        $('#btnCopyCTVAT').off('click').on('click', function () {
            invoiceId = $(this).data('id');

            if (invoiceId === '') {

                bootbox.alert({
                    title: "Information",
                    size: "small",
                    message: "Bạn chưa chọn invoice nào!"
                });
            }
            else {

                $.ajax({
                    url: '/CTVATs/CopyCTInvoice_DS_To_CTVATPost',
                    data: {
                        invoiceId: invoiceId
                    },
                    dataType: 'json',
                    type: 'POST',
                    success: function (response) {
                        if (response.status) {

                            toastr.success('Copy thành công!'); // toastr in admin/tour/indexController.js

                            //$('#editCTInvoicePartial').hide();

                            //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                            //indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                            indexController.Load_CTInvoice_CTVAT_Partial(invoiceId);
                        }
                        else {
                            toastr.error(response.message);

                        }
                    }
                });

                //$('#frmCopyCTVAT').submit();
            }

        });

        // btnCopyCTVAT 

        // Delete CTVAT
        $('.btnDeleteCTVAT').off('click').on('click', function () {

            ctvatId = $(this).data('ctvatid');
            invoiceId = $(this).data('invoiceid');

            $.ajax({
                url: '/CTVATs/DeleteCTVATPost',
                data: {
                    ctvatId: ctvatId,
                    invoiceId: invoiceId
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status) {

                        toastr.success('Xóa thành công!'); // toastr in admin/tour/indexController.js

                        //$('#editCTInvoicePartial').hide();

                        //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                        //indexController.Load_KeToan_TourInfoByTourPartial(tourId);

                        indexController.Load_CTInvoice_CTVAT_Partial(invoiceId);
                    }
                    else {
                        toastr.error(response.message);

                    }
                }
            });

        });
        // Delete CTVAT

        //////////////////////////////////////////////////////////////////////////////// editCTInvoicePartial

        //////////////////////////////////////////////////////////////////////////////// BienNhan
        $('#hrefToTabBienNhans').off('click').on('click', function () {

            tourId = $(this).data('tourid');
            $('#createBienNhanPartial').hide(500);
            $('#editBienNhanPartial').hide(500);

            indexController.Load_BienNhan_CTBN_Partial(tourId);
        });
        // create new biennhan
        $('#btnNewBienNhan').off('click').on('click', function () {

            tourid = $(this).data('tourid');

            $('#BienNhanAndCTBNPartial').hide(500);

            var url = '/BienNhans/CreateBienNhanPartial';
            $.get(url, { tourid: tourid }, function (response) {

                $('#createBienNhanPartial').show(500);

                $('#createBienNhanPartial').html(response);

            });
        });
        // Validation and submit in its partial (CreateBienNhanPartial)

        // create new biennhan

        // edit biennhan
        $('.btnEditBN').off('click').on('click', function () {

            tourid = $(this).data('tourid');
            bienNhanId = $(this).data('biennhanid');

            $('#BienNhanAndCTBNPartial').hide(500);

            var url = '/BienNhans/EditBienNhanPartial';
            $.get(url, { tourId: tourid, bienNhanId: bienNhanId }, function (response) {

                $('#editBienNhanPartial').show(500);

                $('#editBienNhanPartial').html(response);

            });
        });
        // edit biennhan

        // del biennhan ( huy biennhan)
        $('.btnHuyBN').off('click').on('click', function () {
            id = $(this).data('id');
            strUrl = $(this).data('url');

            $.get('/BienNhans/HuyBNPartial', { id: id, strUrl: strUrl }, function (response) {

                $('#huyBNModal').modal('show');
                $('.huyBNPartial').html(response);
                $('#huyBNModal').draggable();
            });
        });

        // del biennhan ( huy biennhan)
        // Load CTBienNhanInBienNhanPartial
        $('.tdBNVal').off('click').on('click', function () {

            bienNhanId = $(this).data('id');

            $('#CreateCTBNPartial').hide(500);
            indexController.Load_CTBienNhanInBienNhanPartial(bienNhanId);

        });
        // Load CTBienNhanInBienNhanPartial

        // Create CTBN Partial
        $('#btnNewCTBienNhan').off('click').on('click', function () {

            //bienNhanId = $(this).data('bienNhanId');
            bienNhanId = $('#hidBienNhanId').val();

            $('#CTBienNhanInBienNhanPartial').hide(500);

            var url = '/ChiTietBNs/CreateCTBienNhanPartial';
            $.get(url, { bienNhanId: bienNhanId }, function (response) {

                $('#CreateCTBNPartial').show(500);

                $('#CreateCTBNPartial').html(response);

            });

        });
        // Create CTBN Partial
        // Create CTBN Partial submit in its partial

        // edit CTBN Partial
        $('.btnEditCTBN').off('click').on('click', function () {

            bienNhanId = $(this).data('biennhanid');
            ctBienNhanId = $(this).data('ctbiennhanid');

            $('#CTBienNhanInBienNhanPartial').hide(500);

            var url = '/ChiTietBNs/EditCTBienNhanPartial';
            $.get(url, { bienNhanId: bienNhanId, ctBienNhanId: ctBienNhanId }, function (response) {

                $('#EditCTBNPartial').show(500);

                $('#EditCTBNPartial').html(response);

            });

        });
        // edit CTBN Partial

        // xoa CTBN in CTBN Partial
        $('.btnXoaCTBienNhanInCTBienNhanPartial').off('click').on('click', function () {
            
            chitietbnid = $(this).data('chitietbnid');
            biennhanid = $(this).data('biennhanid');

            $.ajax({
                url: '/ChiTietBNs/DeleteChiTietBNPartialPost',
                data: {
                    chitietbnid: chitietbnid
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status) {

                        toastr.success('Xóa thành công!'); // toastr in admin/tour/indexController.js

                        //$('#editCTInvoicePartial').hide();

                        //tourId = tourIdInCreateCTInvoicePartial; // receive it from EditInvoicePartial
                        //indexController.Load_KeToan_TourInfoByTourPartial(tourId);


                        var url = '/Tours/BienNhanAndCTBNPartial';
                        $.get(url, { tourId: tourId }, function (response) {

                            $('#BienNhanAndCTBNPartial').html(response);
                            $('#BienNhanAndCTBNPartial').show(500);
                            indexController.Load_CTBienNhanInBienNhanPartial(bienNhanId)
                        });
                    }
                    else {
                        toastr.error(response.message);

                    }
                }
            });

        });
        // xoa CTBN in CTBN Partial
        //////////////////////////////////////////////////////////////////////////////// BienNhan

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
            $('.cTInVoiceCTVAT').show(5000);

        });
    }
    ,
    Load_BienNhan_CTBN_Partial: function (tourId) {

        var url = '/Tours/BienNhanAndCTBNPartial';
        $.get(url, { tourId: tourId }, function (response) {

            $('#BienNhanAndCTBNPartial').html(response);
            $('#BienNhanAndCTBNPartial').show(500);

        });

    },
    Load_CTBienNhanInBienNhanPartial: function (bienNhanId) {

        var url = '/BienNhans/CTBienNhanInBienNhanPartial';
        $.get(url, { bienNhanId: bienNhanId }, function (response) {

            $('#CTBienNhanInBienNhanPartial').html(response);
            $('#CTBienNhanInBienNhanPartial').show(500);
        });
    },
    Load_DSKhachHang: function (tourid) {

        $('#khachHangCreatePartial').hide(500);
        $('#khachHangEditPartial').hide(500);

        var url = '/DSKhachHangs/DSKhachHangPartial';
        $.get(url, { tourid: tourid }, function (response) {

            $('#sDSKhach').html(response);

            $('#sDSKhach').show(500);

        });
    }

};
indexController.init();
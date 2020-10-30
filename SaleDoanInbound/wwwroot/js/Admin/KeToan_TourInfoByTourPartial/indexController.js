var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
       
        // giu trang thai invoice click
        $('.invoice-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.invoice-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai invoice click

        // invoice click --> CTInvoices & CTVAT
        $('.tdInvoiceVal').click(function () {
            
            invoiceId = $(this).data('id');
            var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
            $.get(url, { invoiceId: invoiceId }, function (response) {

                $('.cTInVoiceCTVAT').html(response);

            });
        });
        // invoice click --> CTInvoices & CTVAT

        // giu trang thai CTInvoice click
        $('#cTInvoiceTbl .ctinvoice-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.ctinvoice-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai CTInvoice click

        // BienNhan click --> CTBienNhan
        $('.tdBNVal').click(function () {
            
            bienNhanId = $(this).data('id');
            var url = '/BienNhans/CTBienNhanInBienNhanPartial';
            $.get(url, { bienNhanId: bienNhanId }, function (response) {
                
                $('.cTietBN').html(response);

            });
        });
        // BienNhan click --> CTBienNhan

        // giu trang thai biennhan click
        $('#biennhansTbl .biennhan-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.biennhan-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai biennhan click

        // create new invoice
        $('#btnNewInvoice').off('click').on('click', function () {
            
            tourid = $(this).data('tourid');
            var url = '/Invoices/CreateInvoicePartial';
            $.get(url, { tourid: tourid }, function (response) {

                $('#createInvoiceModal').show();
                $('.createInvoicePartial').html(response);
                $('#createInvoiceModal').draggable();

            });
        });
        // create new invoice

        /// submit save --> load 
        //tourId = $(this).data('id');

        //var url = '/Tours/KeToan_TourInfoByTourPartial';
        //$.get(url, { tourId: tourId }, function (response) {

        //    $('#tabs_KeToan_TourInfo').html(response);

        //});

    }
};
indexController.init();
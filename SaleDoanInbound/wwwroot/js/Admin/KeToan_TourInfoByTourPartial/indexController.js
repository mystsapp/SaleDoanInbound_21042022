var indexController = {
    init: function () {
        $.each($('.invoice-cursor-pointer'), function (i, item) {

            var huy = $(item).data('huy');
            //console.log(huy);
            if (huy === 'True') {
                $(this).addClass('bg-secondary');
            }

        });

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

    }
};
indexController.init();
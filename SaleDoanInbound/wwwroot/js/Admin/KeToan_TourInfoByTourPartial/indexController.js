var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
       

        // hrefToTabInvoices click
        //$('#hrefToTabInvoices').click(function () {

        //    var tourId = $(this).data('tourid');
        //    indexController.loadInvoices(tourId);
        //});
        // hrefToTabInvoices click

        // giu trang thai invoice click
        $('.invoice-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('tr.hoverClass').removeClass("hoverClass");
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

        // BienNhan click --> CTBienNhan
        $('.tdBNVal').click(function () {

            bienNhanId = $(this).data('id');
            var url = '/BienNhan/CTBienNhanInBienNhanPartial';
            $.get(url, { bienNhanId: bienNhanId }, function (response) {

                $('.cTietBN').html(response);

            });
        });
        // BienNhan click --> CTBienNhan

        // hrefToTabBienNhans click
        //$('#hrefToTabBienNhans').click(function () {

        //    var tourId = $(this).data('tourid');
        //    indexController.loadBienNhans(tourId);
        //});
        // hrefToTabBienNhans click


    }
};
indexController.init();
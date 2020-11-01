var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
       
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
        
        // giu trang thai CTInvoice click
        $('#ctvatTbl .ctvat-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.ctvat-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai CTInvoice click


    }
};
indexController.init();
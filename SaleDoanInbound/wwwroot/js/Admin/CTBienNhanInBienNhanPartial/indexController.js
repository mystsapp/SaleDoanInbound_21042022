var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
       
        // giu trang thai CTBienNhan click
        $('#ctbnTbl .ctbn-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.ctbn-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai CTBienNhan click
        
    }
};
indexController.init();
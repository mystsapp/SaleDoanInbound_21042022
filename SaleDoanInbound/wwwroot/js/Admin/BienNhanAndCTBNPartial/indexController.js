var indexController = {
    init: function () {
        $.each($('.biennhan-cursor-pointer'), function (i, item) {

            var huy = $(item).data('huy');
            //console.log(huy);
            if (huy === 'True') {
                $(this).addClass('bg-secondary');
            }

        });

        indexController.registerEvent();
    },

    registerEvent: function () {
       
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
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


        $('.btnPrintBN').off('click').on('click', function () {
            
            var id = $(this).data('id');

            $('#hidBienNhanIdForPdfPrint').val(id);


            $.get('/BienNhans/PrintBNPartial', { id: id }, function (response) {

                //console.log(response);
                $('#PrintBNPartial').modal('show');
                $('.printBN').html(response);
                //$('#huyBNModal').draggable();
            });

            //$.ajax({
            //    url: '/BienNhans/GetDetailBN',
            //    data: {
            //        id: id
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {
            //        console.log(response.data.soBN);
            //        if (response.status) {
            //            $('#spanSoBN').val(response.data.soBN);
            //        }

            //    }
            //});

        });
        
        $('#btnPrintBNPartial').off('click').on('click', function () {
            debugger
            id = $('#hidBienNhanIdForPdfPrint').val();

            $.ajax({
                url: '/BienNhans/ExportPdfPartial',
                data: {
                    id: id
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    
                    if (response.status) {
                        toastr.success('Export thành công!');
                    }

                }
            });

        });

    }
};
indexController.init();
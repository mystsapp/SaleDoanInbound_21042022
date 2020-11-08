var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {
        $.each($('.biennhan-cursor-pointer'), function (i, item) {

            var huy = $(item).data('huy');
            //console.log(huy);
            if (huy === 'True') {
                $(this).addClass('bg-secondary');
            }

        });

        $('.tdVal').click(function () {
            id = $(this).data('id');
            $('#hidId').val(id);
            var page = $('.active .page-link').text();
            //var page = $('.active span').text();
            $('#hidPage').val(page);
            //$.ajax({
            //    url: '/CapThes/Index',
            //    data: {
            //        maCT: id
            //    },
            //    dataType: 'json',
            //    type: 'GET',
            //    success: function (response) {

            //    }
            //});

            $('#btnSubmit').click();
        });

        $('#btnNewCTBienNhan').off('click').on('click', function (e) {
            bienNhanId = $(this).data('id');
            if (bienNhanId === 0) {
                e.preventDefault();
                bootbox.alert({
                    title: "Information",
                    size: "small",
                    message: "Bạn chưa chọn biên nhận nào nào!"
                });
            }
        });

        $('.btnHuyBN').off('click').on('click', function () {
            id = $(this).data('id');
            strUrl = $(this).data('url');

            $.get('/BienNhans/HuyBNPartialIndex', { id: id, strUrl: strUrl }, function (response) {

                $('#huyBNModal').modal('show');
                $('.huyBNPartial').html(response);
                $('#huyBNModal').draggable();
            });
        });

        $('.btnPrintBN').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            
            strUrl = $(this).data('url');

            $('#hidBienNhanIdForPdfPrint').val(id);


            $.get('/BienNhans/PrintBNPartial', { id: id, strUrl: strUrl }, function (response) {

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


    }
};
indexController.init();
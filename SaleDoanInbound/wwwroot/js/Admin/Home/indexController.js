//$.formattedDate = function (dateToFormat) {
//    var dateObject = new Date(dateToFormat);

//    var day = dateObject.getDate();
//    var month = dateObject.getMonth() + 1;
//    var year = dateObject.getFullYear();
//    day = day < 10 ? "0" + day : day;
//    month = month < 10 ? "0" + month : month;
//    var formattedDate = day + "/" + month + "/" + year;
//    return formattedDate;
//};

//$.formattedDateTime = function (dateToFormat) {
//    var dateObject = new Date(dateToFormat);

//    if (dateObject.getHours() >= 12) {
//        var hour = parseInt(dateObject.getHours()) - 12;
//        var amPm = "PM";
//    } else {
//        var hour = dateObject.getHours();
//        var amPm = "AM";
//    }
//    var time = hour + ":" + dateObject.getMinutes() + ":" + dateObject.getSeconds() + " " + amPm;

//    var day = dateObject.getDate();
//    var month = dateObject.getMonth() + 1;
//    var year = dateObject.getFullYear();
//    day = day < 10 ? "0" + day : day;
//    month = month < 10 ? "0" + month : month;
//    var formattedDate = day + "/" + month + "/" + year + " " + time;
//    return formattedDate;
//};


//$.stringToDate = function (_date, _format, _delimiter) {
//    var formatLowerCase = _format.toLowerCase();
//    var formatItems = formatLowerCase.split(_delimiter);
//    var dateItems = _date.split(_delimiter);
//    var monthIndex = formatItems.indexOf("mm");
//    var dayIndex = formatItems.indexOf("dd");
//    var yearIndex = formatItems.indexOf("yyyy");
//    var month = parseInt(dateItems[monthIndex]);
//    month -= 1;
//    var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
//    return formatedDate;
//};


//$.getMyFormatDate = function (date) {
//    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
//    var d = date;
//    var hours = d.getHours();
//    var ampm = hours >= 12 ? 'PM' : 'AM';
//    return months[d.getMonth()] + ' ' + d.getDate() + " " + d.getFullYear() + ' ' + hours + ':' + d.getMinutes() + ' ' + ampm;
//}


var indexController = {
    init: function () {
        indexController.registerEvent();
    },

    registerEvent: function () {


        var abc = formatedDate(Date());
        console(abc);

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

            id = $(this).data('id');
            bootbox.confirm({
                title: "Restore Confirm?",
                message: "Bạn có muốn <b> khôi phục </b> item này không?",
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
                        $('#hidId').val(id);
                        $('#frmKhoiPhuc').submit();
                    }
                }

            });
        });
    }

};
indexController.init();
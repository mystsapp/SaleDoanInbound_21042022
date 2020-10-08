function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}


var CreateController = {
    init: function () {
        CreateController.registerEvent();
    },
    registerEvent: function () {

        // format .numbers
        $('input.numbers').keyup(function (event) {

            // Chỉ cho nhập số
            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return addCommas(value);
            });
        });

        //// btnSubmit 
        //$('#btnSubmit').off('click').on('click', function () {
        //    var fileExtension = ['xls', 'xlsx'];
        //    var filename = $('#fUpload').val();
        //    if (filename.length !== 0) {
        //        var extension = filename.replace(/^.*\./, '');
        //        if ($.inArray(extension, fileExtension) === -1) {
        //            bootbox.alert({
        //                size: "small",
        //                title: "Infomation",
        //                message: "Chọn chưa đúng định dạng <b> Excel! </b>"
        //            });
                    
        //            return false;
        //        }
        //    }
            
        //});
        //// btnSubmit 

    //    // ddl chinhanhDH change
    //    $('#ddlChiNhanhDH').off('change').on('change', function () {
    //        // spanChiNhanhAlert
    //        var chiNhanhTao = $('#hidChiNhanhTaoId').val();
    //        var chiNhanhDH = $(this).val();
    //        if (chiNhanhTao !== chiNhanhDH) {
    //            $('#spanChiNhanhAlert').prop('hidden', false);
                
    //        }
    //        else {
                
    //            $('#spanChiNhanhAlert').prop('hidden', true);
    //        }

    //    });
    //// ddl chinhanhDH change


        //$('#ddlMaKh').off('change').on('change', function () {
        //    var optionValue = $(this).val();
        //    CreateController.GetKHByMaKH(optionValue);
        //});

        // gan'  qua hidden field
        $('#ddlChiNhanhs').off('change').on('change', function () {
            chiNhanh = $(this).val();

            $('#hidDdlChiNhanhs').val(chiNhanh);
            
        });

    }
    //GetKHByMaKH: function (optionValue) {

    //    $.ajax({
    //        url: '/Tours/GetKHByMaKH',
    //        type: 'GET',
    //        data: {
    //            maKH: optionValue
    //        },
    //        dataType: 'json',
    //        success: function (response) {
    //            var khachHang = JSON.parse(response.khachHang)
    //            //console.log(khachHang.Address);
    //            //console.log(khachHang);
    //            $('#txtTenKH').val(khachHang.Name)
    //            $('#txtDienThoai').val(khachHang.Tel)
    //            $('#txtFax').val(khachHang.Fax)
    //            $('#txtDiaChi').val(khachHang.Address)
    //        }
    //    });
    //},
    //Upload: function () {
    //    var fileExtension = ['xls', 'xlsx'];
    //    var filename = $('#fUpload').val();
    //    if (filename.length === 0) {
    //        alert("Please select a file.");
    //        return false;
    //    }
    //    else {
    //        var extension = filename.replace(/^.*\./, '');
    //        if ($.inArray(extension, fileExtension) === -1) {
    //            alert("Please select only excel files.");
    //            return false;
    //        }
    //    }
    //    var fdata = new FormData();
    //    var fileUpload = $("#fUpload").get(0);
    //    var files = fileUpload.files;
    //    fdata.append(files[0].name, files[0]);
    //    $.ajax({
    //        type: "POST",
    //        //url: "/ImportExcel?handler=Import",
    //        url: "/Home/UploadExcel", //OnPostImport
    //        beforeSend: function (xhr) {
    //            xhr.setRequestHeader("XSRF-TOKEN",
    //                $('input:hidden[name="__RequestVerificationToken"]').val());
    //        },
    //        data: fdata,
    //        contentType: false,
    //        processData: false,
    //        success: function (response) {
    //            if (response.status)
    //                console.log('Upload success.');
    //            else {
    //                //$('#dvData').html(response);
    //                alert('Some error occured while uploading');
    //            }
    //        },
    //        error: function (e) {
    //            $('#dvData').html(e.responseText);
    //        }
    //    });
    //}
};
CreateController.init();
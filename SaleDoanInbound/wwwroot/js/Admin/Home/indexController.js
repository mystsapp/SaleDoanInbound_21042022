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
        // bar chart

        // doanhso
        var thiTruongLabels = JSON.parse($('#hidThiTruongLabels').val());
        var dataDoanhSoCurrent = JSON.parse($('#hidDataDoanhSoCurrent').val());
        var dataDoanhSoPrevious = JSON.parse($('#hidDataDoanhSoPrevious').val());
        
        var areaChartData = {
            labels: thiTruongLabels,
            datasets: [
                {
                    label: 'Doanh số hiện tại',
                    backgroundColor: 'rgba(60,141,188,0.9)',
                    borderColor: 'rgba(60,141,188,0.8)',
                    pointRadius: false,
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: dataDoanhSoCurrent
                },
                {
                    label: 'Doanh số tháng trước',
                    backgroundColor: 'rgba(210, 214, 222, 1)',
                    borderColor: 'rgba(210, 214, 222, 1)',
                    pointRadius: false,
                    pointColor: 'rgba(210, 214, 222, 1)',
                    pointStrokeColor: '#c1c7d1',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(220,220,220,1)',
                    data: dataDoanhSoPrevious
                },
            ]
        }
        var barChartCanvas = $('#barChart_DS').get(0).getContext('2d')
        var barChartData = jQuery.extend(true, {}, areaChartData)
        var temp0 = areaChartData.datasets[0]
        var temp1 = areaChartData.datasets[1]
        barChartData.datasets[0] = temp1
        barChartData.datasets[1] = temp0

        var barChartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            datasetFill: false
        }

        var barChart = new Chart(barChartCanvas, {
            type: 'bar',
            data: barChartData,
            options: {
                //legend: {
                //    display: false
                //},
                tooltips: {
                    mode: 'label',
                    label: 'mylabel',
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        },
                    },
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            callback: function (label, index, labels) { return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","); },
                            beginAtZero: true,
                            fontSize: 10,
                        },
                        gridLines: {
                            display: true
                        },
                        scaleLabel: {
                            display: true,
                            //labelString: '000\'s',
                            fontSize: 10,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 10
                        },
                        gridLines: {
                            display: true
                        },
                        scaleLabel: {
                            display: true,
                            fontSize: 10,
                        }
                    }]
                }
            }
        })
        // doanhso
        
        // SK
        
        var dataSKCurrent = JSON.parse($('#hidDataSKCurrent').val());
        var dataSKPrevious = JSON.parse($('#hidDataSKPrevious').val());


        var areaChartData_SK = {
            labels: thiTruongLabels,
            datasets: [
                {
                    label: 'SK hiện tại',
                    backgroundColor: 'rgba(60,141,188,0.9)',
                    borderColor: 'rgba(60,141,188,0.8)',
                    pointRadius: false,
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: dataSKCurrent
                },
                {
                    label: 'SK tháng trước',
                    backgroundColor: 'rgba(210, 214, 222, 1)',
                    borderColor: 'rgba(210, 214, 222, 1)',
                    pointRadius: false,
                    pointColor: 'rgba(210, 214, 222, 1)',
                    pointStrokeColor: '#c1c7d1',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(220,220,220,1)',
                    data: dataSKPrevious
                },
            ]
        }
        var barChartCanvas_SK = $('#barChart_SK').get(0).getContext('2d')
        var barChartData_SK = jQuery.extend(true, {}, areaChartData_SK)
        var temp0_SK = areaChartData_SK.datasets[0]
        var temp1_SK = areaChartData_SK.datasets[1]
        barChartData_SK.datasets[0] = temp1_SK
        barChartData_SK.datasets[1] = temp0_SK

        var barChartOptions_ = {
            responsive: true,
            maintainAspectRatio: false,
            datasetFill: false
        }

        var barChart_SK = new Chart(barChartCanvas_SK, {
            type: 'bar',
            data: barChartData_SK,
            options: {
                //legend: {
                //    display: false
                //},
                tooltips: {
                    mode: 'label',
                    label: 'mylabel',
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        },
                    },
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            callback: function (label, index, labels) { return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","); },
                            beginAtZero: true,
                            fontSize: 10,
                        },
                        gridLines: {
                            display: true
                        },
                        scaleLabel: {
                            display: true,
                            //labelString: '000\'s',
                            fontSize: 10,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,
                            fontSize: 10
                        },
                        gridLines: {
                            display: true
                        },
                        scaleLabel: {
                            display: true,
                            fontSize: 10,
                        }
                    }]
                }
            }
        })

        //var areaChartData_SK = {
        //    labels: thiTruongLabels,
        //    datasets: [
        //        {
        //            label: 'SK hiện tại',
        //            backgroundColor: 'rgba(60,141,188,0.9)',
        //            borderColor: 'rgba(60,141,188,0.8)',
        //            pointRadius: false,
        //            pointColor: '#3b8bba',
        //            pointStrokeColor: 'rgba(60,141,188,1)',
        //            pointHighlightFill: '#fff',
        //            pointHighlightStroke: 'rgba(60,141,188,1)',
        //            data: dataSKCurrent
        //        },
        //        {
        //            label: 'SK tháng trước',
        //            backgroundColor: 'rgba(210, 214, 222, 1)',
        //            borderColor: 'rgba(210, 214, 222, 1)',
        //            pointRadius: false,
        //            pointColor: 'rgba(210, 214, 222, 1)',
        //            pointStrokeColor: '#c1c7d1',
        //            pointHighlightFill: '#fff',
        //            pointHighlightStroke: 'rgba(220,220,220,1)',
        //            data: dataSKPrevious
        //        },
        //    ]
        //}
        //var barChartCanvas_SK = $('#barChart_SK').get(0).getContext('2d')
        //var barChartData_SK = jQuery.extend(true, {}, areaChartData_SK)
        //var temp0_SK = areaChartData_SK.datasets[0]
        //var temp1_SK = areaChartData_SK.datasets[1]
        //barChartData_SK.datasets[0] = temp0_SK
        //barChartData_SK.datasets[1] = temp1_SK

        //var barChartOptions_SK = {
        //    responsive: true,
        //    maintainAspectRatio: false,
        //    datasetFill: false
        //}

        //var barChart_SK = new Chart(barChartCanvas_SK, {
        //    type: 'bar',
        //    data: barChartData_SK,
        //    options: {
        //        //legend: {
        //        //    display: false
        //        //},
        //        tooltips: {
        //            mode: 'label',
        //            label: 'mylabel',
        //            callbacks: {
        //                label: function (tooltipItem, data) {
        //                    return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //                },
        //            },
        //        },
        //        scales: {
        //            yAxes: [{
        //                ticks: {
        //                    callback: function (label, index, labels) { return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","); },
        //                    beginAtZero: true,
        //                    fontSize: 10,
        //                },
        //                gridLines: {
        //                    display: true
        //                },
        //                scaleLabel: {
        //                    display: true,
        //                    //labelString: '000\'s',
        //                    fontSize: 10,
        //                }
        //            }],
        //            xAxes: [{
        //                ticks: {
        //                    beginAtZero: true,
        //                    fontSize: 10
        //                },
        //                gridLines: {
        //                    display: true
        //                },
        //                scaleLabel: {
        //                    display: true,
        //                    fontSize: 10,
        //                }
        //            }]
        //        }
        //    }
        //})
        // SK

        // bar chart
    }

};
indexController.init();
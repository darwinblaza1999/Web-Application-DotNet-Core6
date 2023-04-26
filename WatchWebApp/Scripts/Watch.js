//'use strict'
//$(document).ready(function () {
//    loadData();
//    alert("Success")
//});
//function loadData() {
//    $.ajax({
//        url: "WatchCatalog/ViewItem",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            $.each(result, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.itemName + '</td>';
//                html += '<td>' + item.Diameter + '</td>';
//                html += '<td>' + item.Movement + '</td>';
//                html += '<td>' + item.Jewel + '</td>';
//                html += '<td>' + item.Weight + '</td>';
//                html += '<td><a href="#" onclick="return getbyID(' + item.EmployeeID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.EmployeeID + ')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
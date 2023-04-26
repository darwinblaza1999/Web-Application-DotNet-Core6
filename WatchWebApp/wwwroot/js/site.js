//'use strict'
$(document).ready(function () {
    loadData();
});

$('#delete').on("click", function () {
    let itemID = $('#updateID').val();
    alert(itemID)

    //alert(Watch_Url.del_url)

    //$.ajax({
    //    url: "WatchCatalog/DeleteItems",
    //    type: "DELETE",
    //    contentType: "application/json;charset=utf-8",
    //    //dataType: "json",
    //    data: JSON.stringify({ id : itemID }),
    //    success: function (result) {
    //        //$("#myModal").hide();
    //        $('#updateID').val("");
    //        alert(result);
    //        alert("Delete");

    //    }
    //});

    //$.ajax({
    //    url: "WatchCatalog/ViewItem",
    //    type: "GET",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {
    //        alert(result)
    //    }
    //});
    
});

function mydelete(itemid) {
    console.log(id);
    alert("Success");
    alert(itemid);
    //$.ajax({
    //    type: "POST",
    //    url: "/WatchCatlog/DeleteItem",
    //    data: { id = itemid },
    //    success: function (result) {
    //        //$("#myModal").hide();
    //        $('#updateID').val("");
    //        alert(result);
    //        alert("Delete");

    //    }
    //});

    //$.ajax({
    //    type: "DELETE",
    //    //url: '@Url.Action("DeleteItem")',
    //    url: "/WatchCatlog/DeleteItem",
    //    data: JSON.stringify({ id: id }), //use id here
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (result) {
    //        alert(result);
    //         alert("Data has been deleted.");
    //        LoadData();
    //    },
    //    error: function () {
    //        alert("Error while deleting data");
    //    }
    //});
}
//function loadData() {
//    alert("success");
//}
function loadData() {
    //alert("proceed")
    //$.ajax({
    //    url: "WatchCatalog/ViewItem",
    //    type: "GET",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {
    //        console.log(result)
    //        console.log(result.data, "data");


    //        //var html = '';
    //        //$.each(result, function (key, item) {
    //        //    html += '<tr>';
    //        //    html += '<td>' + item.itemName + '</td>';
    //        //    html += '<td>' + item.Diameter + '</td>';
    //        //    html += '<td>' + item.Movement + '</td>';
    //        //    html += '<td>' + item.Jewel + '</td>';
    //        //    html += '<td>' + item.Weight + '</td>';
    //        //    html += '<td><a href="#" onclick="return getbyID(' + item.EmployeeID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.EmployeeID + ')">Delete</a></td>';
    //        //    html += '</tr>';
    //        //});
    //        //$('.tbody').html(html);
    //        alert("done");
    //    },
    //    error: function (errormessage) {
    //        alert(errormessage.responseText);
    //    }
    //});
}
$(document).ready(function () {
    ShowAllCategories();
    DisplayDataFiled();
});
function ShowAllCategories() {
    $.ajax({
        url: "http://localhost:5009/api/Products/Categories",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            var list = "";
            result.forEach(function (pro) {
                list += `<option value="${pro.categoryId}"> ${pro.categoryName}</option>`
            });
            $("#CategoryId").html(list);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}
function DisplayDataFiled() {
    var productId = parseInt($("#ProductId").val());
    console.log(productId);
    $.ajax({
        url: "http://localhost:5009/api/Products/id?id=" + productId,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            $("#ProductName").val(result.productName),
                $("#UnitsInStock").val(result.unitsInStock),
                $("#UnitPrice").val(result.unitPrice),
                $("#CategoryId").val(result.categoryId)
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}
function UpdateProduct() {
    var productId = parseInt($("#ProductId").val());
    var productName = $("#ProductName").val();
    var categoryId = parseInt($("#CategoryId").val());
    var unitsInStock = parseInt($("#UnitsInStock").val());
    var unitPrice = parseInt($("#UnitPrice").val());

    var data = {
        ProductName: productName,
        CategoryId: categoryId,
        UnitsInStock: unitsInStock,
        UnitPrice: unitPrice
    };

    $.ajax({
        url: "http://localhost:5009/api/Products/id?id=" + productId,
        type: "PUT",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            alert("Update Successfully!")
            window.location.href = "/Products/Index";
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}

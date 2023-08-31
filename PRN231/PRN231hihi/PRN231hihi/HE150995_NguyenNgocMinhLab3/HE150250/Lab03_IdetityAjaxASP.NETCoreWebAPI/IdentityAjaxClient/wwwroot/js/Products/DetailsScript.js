function DisplayDataFiled() {
    var productId = parseInt($("#ProductId").val());
    console.log(productId);
    $.ajax({
        url: "http://localhost:5009/api/Products/id?id=" + productId,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            $("#ProductName").html(result.productName),
                $("#UnitsInStock").html(result.unitsInStock),
                $("#UnitPrice").html(result.unitPrice),
                $("#Category").html(result.categoryId);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}
$(document).ready(function () {
    DisplayDataFiled();
});

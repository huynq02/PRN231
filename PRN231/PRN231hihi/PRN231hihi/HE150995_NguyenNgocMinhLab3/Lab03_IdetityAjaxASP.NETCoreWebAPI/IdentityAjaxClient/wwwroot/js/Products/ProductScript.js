var sortStatus = "asc"
var currentListProduct = "";

$(document).ready(function () {
    ShowAllProducts();
    $('.unit-price-column').click(function () {
        // Xử lý tại đây khi người dùng click vào cột "UnitPrice"
        //sortProductByPrice();
        currentListProduct.reverse();
        var list = "";
        currentListProduct.forEach(function (pro) {
            list += `<tr>
                                            <td>
                                                ${pro.productName}
                                            </td>
                                            <td>
                                                ${pro.unitsInStock}
                                            </td>
                                            <td>
                                                ${pro.unitPrice}
                                            </td>
                                            <td>
                                                ${pro.category.categoryName}
                                            </td>
                                            <td>
                                                <a id="edit" href="/Products/Edit/${pro.productId}">Edit</a> |
                                                <a id="detail" href="/Products/Details/${pro.productId}">Details</a> |
                                                <a id="delete" onclick="deleteProduct(${pro.productId})" href="#">Delete</a>
                                            </td>
                                        </tr>`
        });
        $("tbody").html(list);
    });
});
function ShowAllProducts() {
    $("table tbody").html("");
    $.ajax({
        url: "http://localhost:5009/api/Products",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            var list = "";
            result.forEach(function (pro) {
                list += `<tr>
                                            <td>
                                                ${pro.productName}
                                            </td>
                                            <td>
                                                ${pro.unitsInStock}
                                            </td>
                                            <td>
                                                ${pro.unitPrice}
                                            </td>
                                            <td>
                                                ${pro.category.categoryName}
                                            </td>
                                            <td>
                                                <a id="edit" href="/Products/Edit/${pro.productId}">Edit</a> |
                                                <a id="detail" href="/Products/Details/${pro.productId}">Details</a> |
                                                <a id="delete" onclick="deleteProduct(${pro.productId})" href="#">Delete</a>
                                            </td>
                                        </tr>`
            });
            currentListProduct = result;
            $("tbody").html(list);
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}
function deleteProduct(productId) {
    console.log(productId);
    $.ajax({
        url: "http://localhost:5009/api/Products/id?id=" + productId,
        type: "delete",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            // Xử lý dữ liệu trả về từ server
            ShowAllProducts();
            $("tbody").html(list);
        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });
}

function searchProduct() {
    let name = document.getElementById('nameSearch').value;
    $.ajax({
        url: "http://localhost:5009/api/Products/name?name=" + name,
        type: "Get",
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            // Xử lý dữ liệu trả về từ server
            var list = "";
            result.forEach(function (pro) {
                list += `<tr>
                                            <td>
                                                ${pro.productName}
                                            </td>
                                            <td>
                                                ${pro.unitsInStock}
                                            </td>
                                            <td>
                                                ${pro.unitPrice}
                                            </td>
                                            <td>
                                                ${pro.category.categoryName}
                                            </td>
                                            <td>
                                                <a id="edit" href="/Products/Edit/${pro.productId}">Edit</a> |
                                                <a id="detail" href="/Products/Details/${pro.productId}">Details</a> |
                                                <a id="delete" onclick="deleteProduct(${pro.productId})" href="#">Delete</a>
                                            </td>
                                        </tr>`
            });
            currentListProduct = result;
            $("tbody").html(list);
        },
        error: function (xhr, status, error) {
            console.log(xhr)
        }
    });
}



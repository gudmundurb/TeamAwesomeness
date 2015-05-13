$(document).ready(function () {

    var src = "'@Html.Raw(Url.Action("GetProductImage", new { productId = Model.Product.Id, pos = 1, size = 0 }))'";
    $(document.createElement("img")).attr("src", src);

    });
});


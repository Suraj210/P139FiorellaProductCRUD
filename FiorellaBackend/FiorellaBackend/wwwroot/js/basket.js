$(document).ready(function () {
    $(document).on("click", "#products .price a", function (e) {
        e.preventDefault();
        let id = parseInt($(this).closest(".product-item").attr("data-productId"))
        let basketCountArea = $(".shop-cart sup")


         $.ajax({
             url: `home/addbasket?id=${id}`,
             type: "Post",
             success: function (res) {
                 basketCountArea.html(Number(basketCountArea.html()) + 1)
             }
         })

    })

})

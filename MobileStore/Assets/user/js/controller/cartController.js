var cart = {
    init: function () {
        cart.regEvents();
    },

    regEvents: function () {
        $('#btnContinueShopping').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnUpdateCart').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    ItemId: $(item).data('id')
                })
            });
            $.ajax({
                url: '/ShoppingCart/UpdateCart',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShoppingCart/Index"
                    }
                }
            });
        });

        $('.btnDeleteItem').off('click').on('click', function () {
            $.ajax({
                url: '/ShoppingCart/DeleteItem',
                data: { itemId: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShoppingCart/Index"
                    }
                }
            });
        });
    }
}
cart.init();
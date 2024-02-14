function addToCart(productId) {
    // Use AJAX to send a request to the server
    $.post("/Carts/AddToCart", { productId: productId }, function (data) {
        // Handle the response if needed
        console.log(data);

        // Reload cart content in the modal
        $("#cartModal .modal-body").load("/Carts/CartContent");
    });
}

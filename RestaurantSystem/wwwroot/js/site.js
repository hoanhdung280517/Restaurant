var SiteController = function () {
    this.initialize = function () {
        regsiterEvents();
        loadCart();
    }
    function loadCart() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetListItems',
            success: function (res) {
                    $('#lbl_number_items_header').text(res.length);
            }
        });
    } 
    function regsiterEvents() {
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const culture = $('#hidCulture').val();
            const id = $(this).data('id');
            const table = $('#hidTableId').val();
            const name = $('#hidName').val();
            $.ajax({
                type: "POST",
                url: "/" + culture + '/Cart/AddToCart',
                data: {
                    id: id,
                    languageId: culture,
                    tableId: table,
                    username: name
                },
                success: function (res) {
                        $('#lbl_number_items_header').text(res.length);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    }
}


function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
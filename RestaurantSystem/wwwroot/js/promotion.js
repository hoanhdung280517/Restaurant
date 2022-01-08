var PromotionController = function () {
    this.initialize = function () {
        myFunction();
        checkFunction();
        Total();
    }
    function myFunction() {
        $('body').on('change', function (e) {
            e.preventDefault();
            const culture = $('#hidCulture').val();
            var x = document.getElementById("PromotionId").value;
            $.ajax({
                type: "Post",
                url: "/" + culture + '/Order/FindPromotion',
                data: {
                    id: x,
                },
                success: function (res) {
                    $.each(res, function (id, item) {
                        discountpercent = item.discountPercent
                    });
                    $('#promotion').text(numberWithCommas(discountpercent)+ "%");
                    Total(discountpercent);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    }
    function checkFunction() {
        $('body').on('click', '.btn-check', function (e) {
            e.preventDefault();
            const culture = $('#hidCulture').val();
            var c = document.getElementById("code").value;
            $.ajax({
                type: "Post",
                url: "/" + culture + '/Order/CheckCode',
                data: {
                    code : c,
                },
                success: function (res) {
                    $('#discountpercent').text(res.discountPercent + "%");
                    $('#status').text(res.status);
                    $('#promotion').text(res.discountpercent);                   
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    }
    function Total(discountpercent) {
        const totalafterdiscount = $("#total").text();
        var t = Number(totalafterdiscount);
        var total = 0;
        if (discountpercent == 0) {
            total = t;
        } else {
            var a = (100 - discountpercent) / 100;
            total = t * a;
        }
        $('#lbl_total').text(numberWithCommas(total) + "VND");     
    }
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
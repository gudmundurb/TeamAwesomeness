$(function () {

    $('.rating').click(function () {
        $('#rateform').submit();
    });

    $("#ajaxText form").submit(function (e) {
        var form = $(this);
        $.post(form.attr("action"),
                form.serialize(),
                function (data) {
                    var result = $(data).find('#reviewsUpdate');
                    $('#reviewsUpdate').html(result.html());
                   // form.find('textarea').val(''); //removes text from text box
                });
        e.preventDefault();
    });

    $("body").on('submit', '#rateID form', function (e) {
        var form = $(this);
        $.post(form.attr("action"),
                form.serialize(),
                function (data) {
                    var result = $(data).find('#RateContainer');

                    $('#RateContainer').html(result.html());
                });
        e.preventDefault();
    });
});
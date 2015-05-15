$(document).ready(function() {

    $('.trending_games').find('.item:first').addClass('active');

    $('#myCarousel').carousel({
        interval: 6000
    });

    // when the carousel slides, auto update
    $('#myCarousel').on('slid', function (e) {
        var id = $('.item.active').data('slide-number');
        id = parseInt(id);
        $('[id^=carousel-selector-]').removeClass('selected');
        $('[id^=carousel-selector-' + id + ']').addClass('selected');
    });
});
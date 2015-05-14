$(function () {
    
    $('.image_randomizer').each(function () {
        var images = ['group_bg_1.png', 'group_bg_2.png', 'group_bg_3.png'];
        $(this).css({ 'background-image': 'url(Content/Images/' + images[Math.floor(Math.random() * images.length)] + ')' });

    });
});
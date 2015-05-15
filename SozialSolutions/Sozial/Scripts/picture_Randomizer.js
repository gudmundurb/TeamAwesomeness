$(document).ready(function () {
    
    $('.image_randomizer').each(function () {

        var images = ['group_bg_1.png', 'group_bg_2.png', 'group_bg_3.png'];
        var url = "http://localhost:52194/Content/Images/group_bg_";
        var number = (Math.floor(Math.random() * images.length) + 1);
        $(this).css({ 'background-image' : 'url(' + url + number + '.png)'});
    });
});
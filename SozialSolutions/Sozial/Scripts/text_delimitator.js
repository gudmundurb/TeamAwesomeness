$(document).ready(function () {

    $('.text_limit').each(function ()
    {
        var myDiv = $('.text_limit');

        if ($(this).text().length > 300)
        {
            $(this).text($(this).text().substring(0, 300) + "(...)");       
        }
    });
});

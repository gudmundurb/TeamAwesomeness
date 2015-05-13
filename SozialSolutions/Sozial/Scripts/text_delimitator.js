$(document).ready(function () {

    var added = false;
    var pathName = window.location.pathname;
    var maxSize = 300;

    if (pathName == "/Home/Index" || pathName == "/")
    {
        maxSize = 200;
    }

    $('#readMore').hide();

    $('.text_limit').each(function ()
    {
        if ($(this).text().length > maxSize)
        {
            $(this).text($(this).text().substring(0, maxSize) + "...");            
            if (added == false)
            {
                $('#readMore').show();
                added = true;
            }           
        }
        else
        {
            $(this).next().css("display", "none");
        }
    });
});
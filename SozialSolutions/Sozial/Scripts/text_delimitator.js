$(document).ready(function () {

    var added = false;
    $('#readMore').hide();

    $('.text_limit').each(function ()
    {
        if ($(this).text().length > 300)
        {
            $(this).text($(this).text().substring(0, 300) + "...");            
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
$(document).ready(function () {

    $(".toToggleComments").hide();

    $(".toggleMeComments").click(function () {

            $(this).next().next(".toToggleComments").slideToggle();       
    });
});

$(document).ready(function () {

    $("#toToggle").hide();

    $("#toggleMe").click(function () {

        $("#toToggle").animate({width:'toggle'});
    });    

});

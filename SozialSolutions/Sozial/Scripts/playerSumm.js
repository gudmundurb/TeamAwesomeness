$( document ).ready(function () {
    //ask benni before you touch please.
    //currUserSteamID()
    var id = "";
    var url = "../Api/profileInfo?steamID="
    var url1 = "../Api/currUserSteamID"
    var what;
    $.get(url1, null, function (data) {
        id = data;
        url = url.concat(id);
            $.get(url, null, function (data) {
                var profile = data.response.players[0];
                
                $("#experiment").append("<p>" + profile.personaname + "</p>");
                $("#experiment").append("<a href= '" + profile.profileurl + "'> MY PROFILE </a>");
                $("#experiment").append("<img src= " + profile.avatarfull + "/>");

            });
    });
})
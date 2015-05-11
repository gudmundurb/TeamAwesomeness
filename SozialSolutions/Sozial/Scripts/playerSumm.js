function experiment() {
    //ask benni before you touch please.
    //currUserSteamID()
    var id = "";
    var url = "../Api/profileInfo?steamID="
    var url1 = "../Api/currUserSteamID"
    var what;
    $.get(url1, null, function (data) {
        id = data;
        url = url.concat(id);

        console.log(url);
            $.get(url, null, function (data) {
                console.log(data);
                var spick = data.response.players[0];
                console.log(spick);

                $("#experiment").append("<p>" + spick.personaname + "</p>");
                $("#experiment").append("<a href= '" + spick.profileurl + "'> MY PROFILE </a>");
                $("#experiment").append("<img src= " + spick.avatarfull + "/>");

            });
    });
    
    
}
function insertProfile () {
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
            $("#ProfilePicture").val(data.response.players[0].avatarfull);
        });
    });
}
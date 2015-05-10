function experiment() {
    //ask benni before you touch please.
    //currUserSteamID()
    var id = "";
    var url1 = "../Api/currUserSteamID"
    $.get(url1, null, function (data) {
        id = data;
    });

    var url = "../Api/profileInfo/"
    url = url.concat(id);
    console.log(url);
    $.get(url, null, function (data) {
        var dataaa = data;

        $("#experiment").append(data);

    });
}
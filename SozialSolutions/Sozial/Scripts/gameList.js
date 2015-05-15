$(function () {
    //ask benni before you touch please.
    var url = "../Api/gameList";
    $.get(url, null, function (data) {
        var datalist = data.applist.apps.app;
        $("#gameCount").append("There are ");
        $("#gameCount").append(datalist.length);
        $("#gameCount").append(" games in this list.");
        $("#loadingText").hide();
        $("#steamList").append("<table>");

        $.each(datalist, function (i, item) {
            //var str = "<tr><td>" + item.name + "</td></tr>";
            //var str = "<tr><td> <a href='steamCreate/?name=" + item.name + "&appId=" + item.appid + "'>" + item.name + "</a></td></tr>";
            var str = "<tr><td> <a href='#" + item.name + "&appId=" + item.appid + "'>" + item.name + "</a></td></tr>";
            $("#steamList").append(str);
        })

        $("#steamList").append("</table>");
    });
})
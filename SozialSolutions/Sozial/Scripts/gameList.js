$(function () {
    //ask benni before you touch please.
    var url = "../Api/gameList";
    $.get(url, null, function (data) {
        var datalist = data.applist.apps.app;
        

        $("#steamList").append("<table>");

        $.each(datalist, function (i, item) {
            //var str = "<tr><td>" + item.name + "</td></tr>";
            var str = "<tr><td> <a href='Game/Create/" + item.name + "'>" + item.name + "</a></td></tr>";
            $("#steamList").append(str);
        })

        $("#steamList").append("</table>");
    });
})
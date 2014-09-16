cs.processTnTVars = function (cmpId,expId) {

    var url = "<TNT_POST_URL>";

    jQuery.post(url, { "token": "<TOKEN>", "tntCId": cmpId, "tntEId": expId },
        function (resp) { }
        , "json");

}
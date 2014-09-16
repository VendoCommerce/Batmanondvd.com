// WIP: DO NOT USE!!

var WriteCSTrackingPixel = function () {
/*
    var cs_TrkUrl = "<CS_TRACKING_URL>";
    var cs_TrkData = "<CS_TRACKING_DATA>";

    var request;
    try {
        request = new XMLHttpRequest();
    } catch (e) { }

    if (request && typeof (request.withCredentials) != "undefined") {
        request.withCredentials = true;
        request.open('POST', cs_TrkUrl, true);
        request.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        request.send("data=" + encodeURIComponent(cs_TrkData));
    }
    else if (typeof (XDomainRequest) != "undefined") {
        request = new window.XDomainRequest();
        request.open('POST', cs_TrkUrl);        
        request.send("data=" + encodeURIComponent(cs_TrkData));
    }
    */
/*
    var cs_TrkUrl = "<CS_TRACKING_URL>";
    var cs_TrkData = "<CS_TRACKING_DATA>";
    var request;
    if (window.ActiveXObject) {
        request = new ActiveXObject('Microsoft.XMLHTTP');
    }
    else {
        request = new XMLHttpRequest();
    }
    request.onreadystatechange = function () { };
    request.withCredentials = true;
    request.open('POST', cs_TrkUrl, true);
    request.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    request.send("data=" + encodeURIComponent(cs_TrkData));
    */
}
WriteCSTrackingPixel();
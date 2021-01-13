function validateKey(e)
{
    document.write(JSON.stringify($('#voting-container').serializeArray()));
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

$(function () {
    var myCookie = getCookie("CookieConsent");
    if (myCookie == "") {
        $('#landing-page').css("visibility", "unset");
    }
});

$(function () {
    $("#consent-text").load("ConsentText.html");
});

$(function () { //shorthand document.ready function
    $('#consent-form').on('submit', function (e) {
        e.preventDefault();  //prevent form from submitting
        document.cookie = "CookieConsent=true";
        $('#landing-page').css("visibility", "hidden");
    });
});

window.onload = function () {
    /* FALL THROUGH */
    //document.getElementById("voting-container").addEventListener("submit", validateKey, false);
}

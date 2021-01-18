function validateKey(e) {
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
        //document.write($('#test').html());
        $('#dynamic-header-navbar').css("visibility", "unset");
    }
})

$('#consent-form').on('submit', function (e) {
    e.preventDefault();  //prevent form from submitting
    document.cookie = "CookieConsent=true";
    $('#dynamic-header-navbar').css("visibility", "hidden");
});

/* Takes the name of a supposed cookie and returns either:
 *   - Cookie data when a cookie with that name exists
 *   - An empty string when no cookie with that name exists
 */
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

/*
 * Once the DOM of the document is ready, this function is
 * called to determine if the Cookie/ToS-Popup should be
 * loaded or not.
 */
$(function () {
    var myCookie = getCookie("CookieConsent");
    if (myCookie == "") {
        //document.write($('#test').html());
        $('#landing-page').css("visibility", "unset");
    }
})

/*
 * After submitting the Cookie/ToS-Popup, a cookie will
 * be stored until the next time the browser is restarted,
 * to make sure the user does not have to fill it out over
 * and over again.
 */
$('#consent-form').on('submit', function (e) {
    e.preventDefault();  //prevent form from submitting
    document.cookie = "CookieConsent=true";
    $('#landing-page').css("visibility", "hidden");
});

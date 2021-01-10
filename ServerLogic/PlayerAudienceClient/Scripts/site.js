function validateKey(e)
{
    document.write(JSON.stringify($('form').serializeArray()));
}

window.onload = function () {
    /* FALL THROUGH */
    document.getElementById("sessionsubmit").addEventListener("submit", validateKey, false);
}

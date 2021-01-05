document.getElementById("SessionKey").addEventListener("keypress", forceKeyPressUppercase, false);

function forceKeyPressUppercase(e) {
    var charInput = e.keyCode;
    if (((charInput >= 97) && (charInput <= 122)) || ((charInput >= 65) && (charInput <= 90)) || ((charInput >= 48) && (charInput <= 57))) {
        if ((charInput >= 97) && (charInput <= 122)) { // lowercase
            if (!e.ctrlKey && !e.metaKey && !e.altKey) { // no modifier key
                var newChar = charInput - 32;
                var start = e.target.selectionStart;
                var end = e.target.selectionEnd;
                var newString = e.target.value.substring(0, start) + String.fromCharCode(newChar) + e.target.value.substring(end);
                if (newString.length <= 6) {
                    e.target.value = e.target.value.substring(0, start) + String.fromCharCode(newChar) + e.target.value.substring(end);
                    e.target.setSelectionRange(start + 1, start + 1);
                    e.preventDefault();
                }
            }
        }
    }
    else if (charInput === 13) {
        if (document.getElementById("SessionKey").value.length === 6) {
            /* FALL THROUGH */
        }
        else {

            /* ToDo: DISPLAY ERROR MESSAGE */
        }
    }
    else {
        e.target.value = document.getElementById("SessionKey").value;
        e.preventDefault();
    }
}
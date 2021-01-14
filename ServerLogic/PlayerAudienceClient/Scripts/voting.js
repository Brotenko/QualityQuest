// Returns the id of the clicked element
$(function () {
        $("input:button").click(function (event) {
            
            $("input:button").prop("disabled", true);
            $(this).addClass("voting-button-selected");

            //document.write($(this).attr("id")); // Future output to the server

        });
});

// Something something "set cookie according to question (or it's GUID), the answers, and the chosen option,
// and read that cookie every frame (if possible? Otherwise on new content load and button click), compare it 
// to the current question(GUID), and if it is the same one, set the style of the buttons again and disable the buttons
// --- The logic check if the client already shit, should for sure be done on the server-side tho ---
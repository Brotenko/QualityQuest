"use strict";

/* 
 * Establish connection to the ServerHub.cs class which is responsible
 * for the persistent communication between the clients and the server.
 */
var connection = new signalR.HubConnectionBuilder().withUrl("/serverhub").build();

var sessionkey;


// Disable send button until connection is established
document.getElementById("enter-room").disabled = true;

// Enable the send button since the connection has been established
connection.start().then(function () {
    document.getElementById("enter-room").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


/*
 * Upon submitting the sessionkey entered at the home page, the key is
 * being sent to the "ValidateKey" function in ServerHub.cs to confirm
 * that the key belongs to a valid/active session.
 */
document.getElementById("enter-room").addEventListener("click", function (event) {
    sessionkey = document.getElementById("sessionkey").value.toUpperCase();
    connection.invoke("ValidateKey", sessionkey).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

/* 
 * The entered sessionkey has been sent to the "ValidateKey" function in
 * ServerHub.cs, to confirm that the key belongs to a valid session, 
 * beforehand.
 * 
 * Afterwards this function is being called by the server to tell the
 * client if the key was valid or not, and to update the page accordingly.
 */
connection.on("KeyValidation", function (existingSessionkey) {
    if (existingSessionkey == true) {
        document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
            "Thank you for playing Quality Quest! The next vote will start shortly!" +
            "</div>";
    }
});


/*
 * This function is being called after the session of the client has
 * been sent a new voting prompt, after which the page's content
 * updated accordingly and a JQuery function is bound to the buttons.
 * 
 * Upon clicking one of the buttons, a vote is submitted and sent to
 * the server and the page content cleared.
 */
connection.on("NewPrompt", function (newPageContent) {
    document.getElementById("starting-page").innerHTML = newPageContent;

    $(function () {
        $("input:button").click(function (event) {
            var vote = this.attributes["name"].value;

            connection.invoke("SendVote", sessionkey, vote).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();

            document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                "You voted for: " + vote + "!" +
                "</div>";
        });
    });
});

/*
 * A function with the sole purpose of being used by the server to
 * force the page to be cleared via a push-message.
 */
connection.on("ClearPrompt", function () {
    document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
        "Thank you for playing Quality Quest! The next vote will start shortly!" +
        "</div>";
});

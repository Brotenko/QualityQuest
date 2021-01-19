"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/serverhub").build();

//Disable send button until connection is established
document.getElementById("enter-room").disabled = true;

connection.on("KeyValidation", function (existingSessionkey) {
    if (existingSessionkey == true) {
        document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
            "Thank you for playing Quality Quest! The next vote will start shortly!" +
            "</div>";
    }
});

connection.on("NewPrompt", function (newPageContent) {
    document.getElementById("starting-page").innerHTML = newPageContent;

    $(function () {
        $("input:button").click(function (event) {
            var vote = $(this).val();

            connection.invoke("SendVote", vote).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();

            document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                "You voted for: " + vote + "!" +
                "</div>";
        });
    });
});

connection.on("ClearPrompt", function () {
    document.getElementById("starting-page").innerHTML = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
        "Thank you for playing Quality Quest! The next vote will start shortly!" +
        "</div>";
});

connection.start().then(function () {
    document.getElementById("enter-room").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("enter-room").addEventListener("click", function (event) {
    var key = document.getElementById("sessionkey").value;
    connection.invoke("ValidateKey", key).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});







/*


document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
*/
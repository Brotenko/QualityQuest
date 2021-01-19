"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/serverhub").build();

//Disable send button until connection is established
document.getElementById("enter-room").disabled = true;

connection.on("KeyValidation", function (sessionkey) {
    if (sessionkey == true) {
        window.location.href = window.location + "Game";
    }
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
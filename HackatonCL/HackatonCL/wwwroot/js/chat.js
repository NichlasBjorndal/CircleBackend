"use strict";

window.onload = function() {


    document.getElementById("isdrawing").style.display = "none";
    document.getElementById("isguessing").style.display = "none";
};


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function(user, message) {
    console.log(message);
    var output = message.split("|")[1];

    var roomname = document.getElementById("group").value;
    var picturetosend = document.getElementById("picturetosend").value;

    if (message.startsWith("DRAW")) {
        document.getElementById("WordBox").innerHTML = output;
        document.getElementById("isdrawing").style.display = "block";
        document.getElementById("isguessing").style.display = "none";
    }
    if (message.startsWith("GUESS")) {
        document.getElementById("imagetest").src = "https://localhost:44302/api/picture/" + output;
        document.getElementById("isdrawing").style.display = "none";
        document.getElementById("isguessing").style.display = "block";
    }
    if (message.startsWith("PROCEED")) {

        var guessedWord = document.getElementById("messageInput").value;

        if (document.getElementById("isguessing").style.display == "block") {
            connection.invoke("SendMessageToTarget", "DRAW|" + document.getElementById("guessedWord").value, roomname).catch(function(err) {
                return console.error(err.toString());
            });
        } else {
            document.getElementById("guessedWord").value = "";
            connection.invoke("SendMessageToTarget", "GUESS|" + picturetosend, roomname).catch(function (err) {
                return console.error(err.toString());
            });
        }

            
        
    }

    if (message.startsWith("INITIATE")) {


        document.getElementById("isgrouping").style.display = "none";
        
        connection.invoke("InitiateGameClient").catch(function (err) {
                return console.error(err.toString());
            });
        
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


document.getElementById("joinButton").addEventListener("click", function (event) {
    var roomname = document.getElementById("groupInput").value;

    document.getElementById("group").value = roomname;
    document.getElementById("assigendroomname").innerHTML = roomname;

    connection.invoke("JoinRoom", roomname).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
document.getElementById("createButton").addEventListener("click", function (event) {
    var roomname = document.getElementById("groupInput").value;
    document.getElementById("group").value = roomname;
    console.log(roomname);

    document.getElementById("isgrouping").style.display = "none";
    connection.invoke("InitiateGame", roomname).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});

document.getElementById("updateButton").addEventListener("click", function (event) {
    var roomname = document.getElementById("group").value;
    connection.invoke("updateTask", roomname).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
    
});

document.getElementById("sendTargetButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var roomname = document.getElementById("group").value;

    connection.invoke("SendMessageToTarget", message , roomname).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});



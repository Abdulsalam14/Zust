"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/friendrequesthub").build();

connection.start().then(function () {
    console.log("Connected");

    //getHasntSeenMessages()
    //GetFriendRequests();
    //getYouKnowUsers();
    //GetFriends();
    //GetNotifications();
}).catch(function (err) {
    return console.error(err.toString());
})

let element = document.querySelector("#alert");
connection.on("Connect", function (info) {
    console.log("Connect Work");
    GetContacts();
    getHasntSeenMessages();
})
connection.on("Disconnect", function (info) {
    console.log("DisConnect Work");
    GetContacts();
})

async function AddRequestCall(id) {
    await connection.invoke("AddRequest",id);
}

async function AddNotificationCall(id) {
    await connection.invoke("AddNotification", id);
}


async function GetHasntSeenMessagesCall(id) {
    await connection.invoke("GetHasntSeenMessages",id);
}


connection.on("ReceiveRequest", async function (id) {
    GetFriendRequests();
    getYouKnowUsers();
    GetFriends();
    GetContacts();
})

connection.on("ReceiveNotification", async function (id) {
     GetNotifications();
})




connection.on("ReceiveHasntSeenMessages", async function (id) {
    getHasntSeenMessages()
})

//connection.on("Friends", function () {
//    getYouKnowUsers();
//})
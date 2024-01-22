var connection2 = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection2.start().then(function () {

    //GetFriendRequests();
    //getYouKnowUsers();
    //GetFriends();
    //GetNotifications();
}).catch(function (err) {
    return console.error(err.toString());
})

connection2.on("Connect", function (info) {
    console.log("Connect Messagessss");
    //getHasntSeenMessages()

})
connection2.on("Disconnect", function (info) {
    console.log("DisConnect Messagess");
    //getHasntSeenMessages()

})


async function GetMessageCall(receiverId, senderId, id) {
    await connection2.invoke("GetMessages", receiverId, senderId, id);
}

//async function GetHasntSeenMessagesCall(id) {
//    await connection2.invoke("GetHasntSeenMessages", id);
//}
//connection2.on("ReceiveHasntSeenMessages", async function (id) {
//    getHasntSeenMessages()
//})

connection2.on("ReceiveMessages", function (receiverId, senderId) {
    GetMessages(receiverId, senderId);
})

connection2.on("ReceiveMessagesNull", function (receiverId, senderId) {
    console.log("NULLGELDI");
})
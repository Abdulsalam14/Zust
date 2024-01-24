function getCurrentPageName() {
    var currentPageUrl = window.location.href;


    var currentPageName = currentPageUrl.substring(currentPageUrl.lastIndexOf("/") + 1);

    return currentPageName;
}

const currentPageName = getCurrentPageName();

var connection2 = new signalR.HubConnectionBuilder().withUrl(`/chathub`).build();

connection2.start().then(async function () {
    getHasntSeenMessages()

}).catch(function (err) {
    return console.error(err.toString());
})

connection2.on("Connect", async function (info) {
    

})
connection2.on("Disconnect", async function (info) {
    console.log("DisConnect Messagess");

})


async function GetMessageCall(receiverId, senderId, id) {

    await connection2.invoke("GetMessages", receiverId, senderId, id);
}


connection2.on("ReceiveMessages", function (receiverId, senderId) {
    GetMessages(receiverId, senderId);
})

connection2.on("ReceiveChatMessages", function (receiverId, senderId) {
    SelectChat(senderId);
})


connection2.on("ReceiveMessagesNull", function (receiverId, senderId,data) {
    console.log("NULLGELDI");
    console.log(data);
})
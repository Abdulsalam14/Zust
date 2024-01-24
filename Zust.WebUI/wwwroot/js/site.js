
function GetFriendRequests() {
    $.ajax({
        url: "/Friends/GetRequests",
        method: "GET",
        success: function (data) {
            let requestcontent = "<div class='row justify-content-center'>";
            let navrequestcontent = `<div class="friend-requests-header d-flex justify-content-between align-items-center">
                <h3>Friend Requests</h3 >
                    <iclass="flaticon-menu"></i>
            </div>
                <div class="friend-requests-body" data-simplebar>`;
            if (data.length > 0) {
                $("#friend-request-count").append(`<span>${data.length}</span>`)
                for (var i = 0; i < data.length; i++) {
                    const request = data[i];
                    let coverimg;
                    let img;
                    if (request.sender.imageUrl != null) {
                        img = request.sender.imageUrl;
                    }
                    else {
                        img ="/assets/images/friends/friends-1.jpg"
                    }
                    if (request.sender.coverImageUrl != null) {
                        coverimg = request.sender.coverImageUrl;
                    }
                    else {
                        coverimg ="/assets/images/friends/friends-bg-3.jpg"
                    }
                    let dateContent = "";
                    let style = '';
                    let subContent = "";
                    let item = `
                        <div class="col-lg-3 col-sm-6">
                                <div class="single-friends-card">
                                    <div class="friends-image">
                                        <a href="#">
                                            <img src="${coverimg}" height="111px" width="360px" alt="image">
                                        </a>
                                        <div class="icon">
                                            <a href="#"><i class="flaticon-user"></i></a>
                                        </div>
                                    </div>
                                    <div class="friends-content">
                                        <div class="friends-info d-flex justify-content-between align-items-center">
                                            <a href="#">
                                                <img src="${img}" height="100px" width="100px" alt="image">
                                            </a>
                                            <div class="text ms-3">
                                                <h3><a href="#">${request.sender.userName}</a></h3>
                                                <span>10 Mutual Friends</span>
                                            </div>
                                        </div>
                                        <ul class="statistics">
                                            <li>
                                                <a href="#">
                                                    <span class="item-number">862</span>
                                                    <span class="item-text">Likes</span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <span class="item-number">91</span>
                                                    <span class="item-text">Following</span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#">
                                                    <span class="item-number">514</span>
                                                    <span class="item-text">Followers</span>
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="button-group d-flex justify-content-between align-items-center">
                                            <div class="add-friend-btn">
                                                <button onclick="AcceptRequest('${data[i].id}','${data[i].receiverId}','${data[i].sender.id}')">Accept</button>
                                            </div>
                                            <div class="send-message-btn">
                                                <button onclick="DeleteRequest('${data[i].id}','${request.senderId}')">Delete</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `;
                    requestcontent += item;
                    let item2 = `
                         <div class="item d-flex align-items-center">
                            <div class="figure">
                                <a href="#"><img src="${img}" class="rounded-circle" alt="image"></a>
                            </div>

                            <div class="content d-flex justify-content-between align-items-center">
                                <div class="text">
                                    <h4><a href="#">${request.sender.userName}</a></h4>
                                    <span>26 Friends</span>
                                </div>
                                <div class="btn-box d-flex align-items-center">
                                    <button class="delete-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" onclick="DeleteRequest('${data[i].id}','${request.senderId}')" type="button"><i class="ri-close-line"></i></button>

                                    <button class="confirm-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Confirm" onclick="AcceptRequest('${data[i].senderId}','${data[i].receiverId}','${data[i].id}')" type="button"><i class="ri-check-line"></i></button>
                                </div>
                            </div>
                        </div>
                        `
                    navrequestcontent += item2;
                }
                navrequestcontent += `<div class="view-all-requests-btn">
                <a href="/Friends" class="default-btn">View All Requests</a>
                </div>`;
            }
            else {
                $("#friend-request-count span").remove();
            }
            requestcontent += "</div>"
            navrequestcontent += `</div>`;
            $("#friend-requests").html(requestcontent);
            $("#friend-request-nav").html(navrequestcontent);
        }
    })
}

function getYouKnowUsers() {
    $.ajax({
        url: "/Friends/GetYouKnowUsers",
        method: "GET",
        success: function (data) {
            console.log(data);
            let youKnowUsersContent = "<div class='row justify-content-center'>";
            for (var i = 0; i < data.length; i++) {
                const user = data[i];
                let img;
                let coverimg;
                if (user.imageUrl != null) {
                    img = user.imageUrl;
                }
                else {
                    img = "/assets/images/friends/friends-1.jpg"
                }
                if (user.coverImageUrl != null) {
                    coverimg = user.coverImageUrl;
                }
                else {
                    coverimg = "/assets/images/friends/friends-bg-1.jpg"
                }

                let dateContent = "";
                let style = '';
                let subContent = "";
                if (user.hasRequestPending == true) {
                    subContent = `<div class="add-friend-btn">
                    <button onclick="DeleteRequest('${data[i].requestId}','${data[i].id}')" type='submit'>RemoveRequest</button>
                    </div>
                    `
                }
                else {
                    subContent = `<div class="add-friend-btn">
                    <button onclick="AddFriend('${data[i].id}')" type='submit'>Add Friend</button>
                    </div>`
                }
                if (user.isFriend == true) {
                    subContent = `<div class="add-friend-btn">
                    <button onclick="AcceptRequest('${data[i].senderId}','${data[i].receiverId}','${data[i].id}')" type='submit'>Accept</button>
                    </div>
                    `
                }
                let item = `
                <div class="col-lg-3 col-sm-6">
                        <div class="single-friends-card">
                            <div class="friends-image">
                                <a asp-controller="MyProfile" asp-action="index" asp-route-id="@user.Id">
                                    <img src="${coverimg}" alt="image" height="100px" width="100px">
                                </a>
                                <div class="icon">
                                    <a href="#"><i class="flaticon-user"></i></a>
                                </div>
                            </div>
                            <div class="friends-content">
                                <div class="friends-info d-flex justify-content-between align-items-center">
                                    <a href="#">
                                      <img src="${img}" alt="image" height="100px" width="100px">

                                    </a>
                                    <div class="text ms-3">
                                        <h3><a href="#">${user.username} </a></h3>
                                        <span>10 Mutual Friends</span>
                                    </div>
                                </div>
                                <ul class="statistics">
                                    <li>
                                        <a href="#">
                                            <span class="item-number">0</span>
                                            <span class="item-text">Likes</span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <span class="item-number">0</span>
                                            <span class="item-text">Following</span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <span class="item-number">514</span>
                                            <span class="item-text">Followers</span>
                                        </a>
                                    </li>
                                </ul>
                                <div class="button-group d-flex justify-content-between align-items-center">
                                ${subContent}
                                    <div class="send-message-btn">
                                        <button type="submit">Send Message</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
                youKnowUsersContent += item;
            }
            youKnowUsersContent += "</div>"
            $("#people-you-know").html(youKnowUsersContent);
        },
        error: function (err) {
            console.log("ERER", err)
        }
    })


}




function AddFriend(id) {
    $.ajax({
        url: `/Friends/AddFriend/${id}`,
        method: "Post",
        success: function (data) {
            getYouKnowUsers();
            GetFriendRequests();
            AddRequestCall(id);
        }
    })
}
function RemoveFriend(id, id2) {
    console.log("REMOVE")
    $.ajax({
        url: `/Friends/RemoveFriend?id=${id}`,
        method: "GET",
        success: function (data) {
            AddNotification(id, "Remove you friends");
            GetFriends();
            AddRequestCall(id);
            AddNotificationCall(id)


        }
    })
}

function AcceptRequest(id, id2, requestId) {
    console.log("ACCEPT")
    $.ajax({
        url: `/Friends/AcceptRequest?userId=${id}&senderId=${id2}&requestId=${requestId}`,
        method: "GET",
        success: function (data) {
            AddNotification(id, "Accept your friend request");
            GetFriends();
            GetContacts();
            GetFriendRequests();
            getYouKnowUsers();
            AddRequestCall(id);
            AddNotificationCall(id)
        }
    })
}



function DeleteRequest(id, userid) {
    console.log("ID:", id);
    $.ajax({
        url: `/Friends/DeleteRequest?id=${id}&userid=${userid}`,
        method: "GET",
        success: function (data) {
            console.log("Data:", data)
            console.log("delete work");
            AddRequestCall(userid);
            getYouKnowUsers();
            GetFriendRequests();

        },
        error: function (err) {
            console.log(err);
        }
    })
}

function GetFriends() {
    console.log("FRIENDS WORK");
    $.ajax({
        url: `/Friends/GetMyFriends`,
        method: "GET",
        success: function (data) {
            console.log("getfriedns")
            //console.log(data)
            let content = `<div class="row justify-content-center">`;
            for (var i = 0; i < data.friends.length; i++) {
                const friend = data.friends[i];
                //console.log(friend)
                let item = `
                    <div class="col-lg-3 col-sm-6">
                                    <div class="single-friends-card">
                                        <div class="friends-image">
                                            <a href="#">
                                                <img src="/assets/images/friends/friends-bg-1.jpg" alt="image">
                                            </a>
                                            <div class="icon">
                                                <a href="#"><i class="flaticon-user"></i></a>
                                            </div>
                                        </div>
                                        <div class="friends-content">
                                            <div class="friends-info d-flex justify-content-between align-items-center">
                                                <a href="#">
                                                    <img src="/assets/images/friends/friends-1.jpg" alt="image">
                                                </a>
                                                <div class="text ms-3">
                                                    <h3><a href="#">${friend.yourFriend.userName}</a></h3>
                                                    <span>10 Mutual Friends</span>
                                                </div>
                                            </div>
                                            <ul class="statistics">
                                                <li>
                                                    <a href="#">
                                                        <span class="item-number">862</span>
                                                        <span class="item-text">Likes</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#">
                                                        <span class="item-number">91</span>
                                                        <span class="item-text">Following</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#">
                                                        <span class="item-number">514</span>
                                                        <span class="item-text">Followers</span>
                                                    </a>
                                                </li>
                                            </ul>
                                            <div class="button-group d-flex justify-content-between align-items-center">
                                                <div class="add-friend-btn">
                                                    <button onclick="RemoveFriend('${friend.yourFriend.id}','${data.id}')" type='submit'>Remove Friend</button>
                                                </div>
                                                <div class="send-message-btn">
                                                    <button type="submit">Send Message</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                `

                content += item;
            }

            content += `</div>`;
            $("#all-friends").html(content);

        }
    })
}



function AddNotification(id, content) {
    let vm = {
        Id: id,
        Content: content
    }
    console.log(vm)
    $.ajax({
        url: `/Notification/AddNotification`,
        method: "POST",
        data: JSON.stringify(vm),
        contentType: "application/json",
        success: function (data) {
            console.log("Added Notification")
        }
    })

}

function DeleteNotification(id) {
    $.ajax({
        url: `/Notification/DeleteNotification?id=${id}`,
        method: "GET",
        success: function () {
            GetNotifications();
        },
        error: function (err) {
            console.log(err);
        }
    })
}


function DeleteAllNotifications() {
    $.ajax({
        url: `/Notification/DeleteAllNotifications`,
        method: "GET",
        success: function () {
            console.log("ALL DELETED")
            GetNotifications();
        },
        error: function (err) {
            console.log(err);
        }
    })
}


function GetNotifications() {
    $.ajax({
        url: "/Notification/GetNotifications",
        method: "GET",
        success: function (data) {
            console.log("notiffications", data)
            let notificationContent = `
                <div class="all-notifications-header d-flex justify-content-between align-items-center">
                    <h3>Notifications</h3>
                    <div class="dropdown">
                        <button class="dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="flaticon-menu"></i></button>
                        <ul class="dropdown-menu">
                            <li onclick=HideNotifications()><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-private"></i> hide notifications</a></li>
                            <li onclick=DeleteAllNotifications()><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-trash"  ></i> delete notifications</a></li>
                        </ul>
                    </div>
                 </div>
            `;
            let navnNotificationContent = `<div class="notifications-header d-flex justify-content-between align-items-center">
                <h3>Notifications</h3 >
                    <i class="flaticon-menu"></i>
            </div>
                <div class="notifications-body" data-simplebar>`;
            if (data.length > 0) {
                $("#notification-count").append(`<span>${data.length}</span>`)
                for (var i = 0; i < data.length; i++) {
                    let img;
                    const notification = data[i];
                    if (notification.sender.imageUrl != null) {
                        img = notification.sender.imageUrl;
                    }
                    else {
                        img ="/assets/images/user/user-55.jpg"
                    }
                    let dateContent = "";
                    let style = '';
                    let subContent = "";
                    let time = new Date(notification.time);
                    let currentDate = new Date();
                    let diffTime = Math.abs(currentDate - time);
                    let diffMinutes = Math.ceil(diffTime / (1000 * 60));
                    if (diffMinutes >= 60) {
                        diffMinutes = Math.ceil(diffMinutes / 60);
                        dateContent = `${diffMinutes} hours ago`;
                    }
                    else {
                        dateContent = `${diffMinutes} minutes ago</span>`;
                    }
                    let item = `
                    <div class="item d-flex justify-content-between align-items-center forhide">
                        <div class="figure">
                            <a href="MyProfile/${notification.senderId}"><img src="${img}" height="100px" width="100px"  class="rounded-circle" alt="image"></a>
                        </div>
                        <div class="text">
                            <h4><a href="MyProfile/index/${notification.sender.id}">${notification.sender.userName}</a></h4>
                            <span>${notification.description}</span>
                            <span class="main-color">${dateContent}</span>
                        </div>
                        <div class="icon">
                        <a href="#"><i class="flaticon-x-mark" onclick=DeleteNotification(${notification.id})></i></a>
                        </div>
                    </div>
                        `;
                    notificationContent += item;
                    let item2 = `
                    <div class="item d-flex justify-content-between align-items-center">
                        <div class="figure">
                            <a href="MyProfile/Index/${notification.senderId}"><img src="${img}" height="100px" width="100px"  class="rounded-circle" alt="image"></a>
                        </div>
                        <div class="text">
                            <h4><a href="MyProfile/Index/${notification.senderId}">${notification.sender.userName}</a></h4>
                            <span style="width:100%">  ${notification.description}</span>
                            <span class="main-color">${dateContent}</span>
                        </div>
                    </div>
                        `
                    navnNotificationContent += item2;
                }
                navnNotificationContent += `<div class="view-all-notifications-btn">
                <a href="/Notification" class="default-btn">View All Notifications</a>
                </div>`;
            }
            else {
                $("#notification-count span").remove();
            }
            navnNotificationContent += `</div>`;
            $("#all-notifications").html(notificationContent);
            $("#notification-nav").html(navnNotificationContent);
        }
    })
}




function GetContacts() {
    $.ajax({
        url: `/Friends/GetMyFriends`,
        method: "GET",
        success: function (data) {
            console.log("CONTACTSSS",data)
            let content = ""/*`<div class="row justify-content-center">`*/;
            for (var i = 0; i < data.friends.length; i++) {
                let img;
                const friend = data.friends[i];
                let status = "offline";
                if (friend.yourFriend.imageUrl != null) {
                    img = friend.yourFriend.imageUrl
                }
                else {
                    img = "/assets/images/user/user-18.jpg";
                }
                if (friend.yourFriend.isOnline == true) {
                    status = "online"
                }
                let item = `
                    <div class="contact-item">
                        <a href="#"><img src="${img}" class="rounded-circle" alt="image"></a>
                        <span class="name"><a href="/Messages/Index/${friend.yourFriend.id}">${friend.yourFriend.userName}</a></span>
                        <span class="status-${status}"></span>
                    </div>
                `

                content += item;
            }

            //content += `</div>`;
            $("#contacts").html(content);

        }

    })
}

function CurrentPage() {
    var currentPageUrl = window.location.href;
    var pathParts = currentPageUrl.split("/");

    console.log(pathParts)
    return pathParts[3];
}

function GetMessages(receiverId, senderId) {
    //currentpagename = CurrentPage();
    //var currentPageUrl = window.location.href;

    controllername = CurrentPage();
    console.log("CURRENTPG", controllername)


    if (controllername.toLowerCase() == "chat" || controllername.toLowerCase() == "chat#") {
        var currentPageUrl = window.location.href;

        updateLiveChat(receiverId, senderId);
        return;
    }
    else if (controllername.toLowerCase() == "messages") {
        $.ajax({
            url: `/Messages/GetChat?receiverId=${receiverId}&senderId=${senderId}`,
            method: "GET",
            success: function (data) {
                console.log("DDAAATTTAA", data)
                if (data.islast == false) {
                    return;
                }
                console.log("CurrentUSerID", data.currentUserId)

                let content = "";
                for (var i = 0; i < data.messages.length; i++) {
                    const message = data.messages[i];
                    let time = new Date(message.dateTime)
                    let formattedTime = time.toLocaleTimeString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
                    if (message.content == null) message.content = ""
                    let item;
                    if (message.receiverId == data.currentUserId) {
                        item = `
                            <div class="chat">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src="/assets/images/user/user-11.jpg" width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${formattedTime} </span>
                                    </div>
                                </div>
                            </div>
                        `
                    }
                    else {

                        item = `
                            <div class="chat chat-left">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src="/assets/images/user/user-2.jpg" width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${formattedTime}</span>
                                    </div>
                                </div>
                            </div>
                        `
                    }
                    content += item

                }
                console.log(data);
                $("#current-messages").html(content);
                GetHasntSeenMessagesCall(receiverId);

            }
        })
    }

}


function getHasntSeenMessages() {
    $.ajax({
        url: `/Messages/GetHasntSeenMessages`,
        method: "GET",
        success: function (data) {
            console.log("DATA:", data);
            let navMessagesContent = `<div class="messages-header d-flex justify-content-between align-items-center">
                       <h3>Messages</h3>
                       <i class="flaticon-menu"></i>
                   </div>
                   <div class="messages-search-box">
                       <form>
                           <input type="text" class="input-search" placeholder="Search Message...">
                           <button type="submit"><i class="ri-search-line"></i></button>
                       </form>
                   </div>
                   <div class="messages-body" data-simplebar>`;
            if (data.count > 0) {
                $("#nav-messages-count").append(`<span>${data.count}</span>`)
            }
            else {
                $("#nav-messages-count span").remove();
            }
            for (var i = 0; i < data.chats.length; i++) {
                const chat = data.chats[i];
                let style = "";
                let img;
                if (chat.sender.imageUrl != null) {
                    img = chat.sender.imageUrl
                }
                else {
                    img = "/assets/images/user/user-11.jpg";
                }
                if (chat.hasntSeenCount > 0) {
                    style = ` <span style="display: inline-block; width: 20px; height: 20px; 
                border-radius: 50%; background-color: #3498db; color: green; text-align: center; line-height: 20px; font-weight: bold;">${chat.hasntSeenCount}</span>`;
                }
                let dateContent = "";
                let time = new Date(chat.time);
                let currentDate = new Date();
                let diffTime = Math.abs(currentDate - time);
                let diffMinutes = Math.ceil(diffTime / (1000 * 60));
                if (diffMinutes >= 60) {
                    diffMinutes = Math.ceil(diffMinutes / 60);
                    dateContent = `${diffMinutes} hours ago`;
                }
                else {
                    dateContent = `${diffMinutes} minutes ago</span>`;
                }
                let item2 = `
                    <div class="item d-flex justify-content-between align-items-center">
                       <div class="figure">
                           <a href="#"><img src="${img}" class="rounded-circle" alt="image"></a>
                       </div>
                       <div class="text">
                           <h4><a href="/Messages/Index/${chat.sender.id}">${chat.sender.userName}</a></h4>
                           <p>${chat.lastMessage}${style}</p>
                           <span class="main-color">${dateContent}</span>
                       </div>
                   </div>
                        `
                navMessagesContent += item2;
            }
            navMessagesContent += `<div class="view-all-messages-btn">
                <a href="/Notification" class="default-btn">View All Messages</a>
                </div>`;
            navMessagesContent += `</div>`;
            $("#messages-nav").html(navMessagesContent);
        }
    })
}

function SendMessage(receiverId, senderId, chatId) {
    let content = document.querySelector("#message-input");
    const id = Number(chatId);
    if (content.value == "") return;
    let obj = {
        receiverId: receiverId,
        senderId: senderId,
        content: content.value,
        chatId: chatId
    };
    $.ajax({
        url: `/Messages/AddMessage`,
        method: "POST",
        data: obj,
        success: function (data) {
            GetMessages(receiverId, senderId);
            GetHasntSeenMessagesCall(senderId);
            GetHasntSeenMessagesCall(receiverId);
            const currentPageName = getCurrentPageName();
            GetMessageCall(receiverId, senderId, id);
            content.value = "";
            console.log("SENDED")
        }
    })

}

function SendMessage2(receiverId, senderId, chatId) {
    let content = document.querySelector("#message-input2");
    const id = Number(chatId);
    if (content.value == "") return;
    let obj = {
        receiverId: receiverId,
        senderId: senderId,
        content: content.value,
        chatId: chatId
    };
    $.ajax({
        url: `/Messages/AddMessage`,
        method: "POST",
        data: obj,
        success: function (data) {
            SelectChat(receiverId)
            GetHasntSeenMessagesCall(senderId);
            GetHasntSeenMessagesCall(receiverId);
            GetMessageCall(receiverId, senderId, id);
            content.value = "";
            console.log("SENDED")
        }
    })

}



function SelectChat(id) {
    let senderid;
    $.ajax({
        url: `/Chat/SelectChat/${id}`,
        method: "GET",
        success: function (data) {
            senderid = data.senderId;
            console.log("SELECTCHAT:", data);
            let content = `<div class="live-chat-header d-flex justify-content-between align-items-center">
                        <div class="live-chat-info">
                            <a ><img src="/assets/images/user/user-11.jpg" class="rounded-circle" alt="image"></a>
                            <h3>
                                <a ">${data.receiver.userName}</a>
                            </h3>
                        </div>

                        <ul class="live-chat-right">
                            <li>
                                <button class="btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" type="button"><i class="ri-delete-bin-line"></i></button>
                            </li>
                        </ul>
                    </div>
                    <div class="live-chat-container">
                        <div class="chat-content" id="live-chat-body">`;
            let item = "";
            for (var i = 0; i < data.messages.length; i++) {
                const message = data.messages[i];
                if (message.receiverId == data.senderId) {
                    item = `
                    <div class="chat">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src="/assets/images/user/user-11.jpg" width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${message.dateTime}</span>
                                    </div>
                                </div>
                     </div>
                    `
                }
                else {
                    item = `
                    <div class="chat chat-left">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src="/assets/images/user/user-2.jpg" width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${message.dateTime}</span>
                                    </div>
                                </div>
                    </div>
                    `
                }
                content += item;
            }

            content += `
                            </div>
                        </div>
                        <div class="chat-list-footer">
                            <form class="d-flex align-items-center" onsubmit="return false">
                                <div class="btn-box d-flex align-items-center me-3">
                                    <button class="file-attachment-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="File Attachment" type="button"><i class="ri-attachment-2"></i></button>
                                    <button class="emoji-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Emoji" type="button"><i class="ri-user-smile-line"></i></button>
                                </div>

                                 <input id="message-input2" type="text" class="form-control" placeholder="Type your message...">

                                <button onclick="SendMessage2('${data.receiverId}','${data.senderId}','${data.id}')" class="send-btn d-inline-block">Send</button>
                            </form>
                        </div>
                    </div>
            `
            $("#live-chat-body").html(content);
            GetHasntSeenMessagesCall(senderid);
        }

    })
}



function updateLiveChat(receiverId,senderId) {

    $.ajax({
        url: `/Chat/GetChatReceiver?receiverId=${receiverId}&senderId=${senderId}`,
        method: "GET",
        success: function (data) {
            if (data.islast == false) {
                console.log("LASSTDDDOO")
                return;
            }

            let img;
            if (data.receiver.imageUrl != null) {
                img = data.receiver.imageUrl
            }
            else {
                img = "/assets/images/user/user-11.jpg";
            }
            senderid = data.chat.senderId;
            console.log("SELECTCHAT:", data);
            let content = `<div class="live-chat-header d-flex justify-content-between align-items-center">
                        <div class="live-chat-info">
                            <a ><img src="${img}" height="100px" width="100px" class="rounded-circle" alt="image"></a>
                            <h3> 
                                <a ">${data.chat.receiver.userName}</a>
                            </h3>
                        </div>

                        <ul class="live-chat-right">
                            <li>
                                <button class="btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" type="button"><i class="ri-delete-bin-line"></i></button>
                            </li>
                        </ul>
                    </div>
                    <div class="live-chat-container">
                        <div class="chat-content" id="live-chat-body">`;
            let item = "";
            for (var i = 0; i < data.chat.messages.length; i++) {
                const message = data.chat.messages[i];
                if (message.receiverId == data.chat.senderId) {
                    item = `
                    <div class="chat">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src=${img} width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${message.dateTime}</span>
                                    </div>
                                </div>
                     </div>
                    `
                }
                else {
                    item = `
                    <div class="chat chat-left">
                                <div class="chat-avatar">
                                    <a routerLink="/profile" class="d-inline-block">
                                        <img src="/assets/images/user/user-2.jpg" width="50" height="50" class="rounded-circle" alt="image">
                                    </a>
                                </div>

                                <div class="chat-body">
                                    <div class="chat-message">
                                        <p>${message.content}</p>
                                        <span class="time d-block">${message.dateTime}</span>
                                    </div>
                                </div>
                    </div>
                    `
                }
                content += item;
            }

            content += `
                            </div>
                        </div>
                        <div class="chat-list-footer">
                            <form class="d-flex align-items-center" onsubmit="return false">
                                <div class="btn-box d-flex align-items-center me-3">
                                    <button class="file-attachment-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="File Attachment" type="button"><i class="ri-attachment-2"></i></button>
                                    <button class="emoji-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Emoji" type="button"><i class="ri-user-smile-line"></i></button>
                                </div>

                                 <input id="message-input2" type="text" class="form-control" placeholder="Type your message...">

                                <button onclick="SendMessage2('${data.chat.receiverId}','${data.chat.senderId}','${data.chat.id}')" class="send-btn d-inline-block">Send</button>
                            </form>
                        </div>
                    </div>
            `
            $("#live-chat-body").html(content);
            GetHasntSeenMessagesCall(senderid);
        }

    })
}

getHasntSeenMessages();
GetFriendRequests();
getYouKnowUsers();
GetFriends();
GetNotifications();
GetContacts();



function like(postId) {
    $.ajax({
        type: "POST",
        url: "/Home/Like",
        data: { postId: postId },
        success: function (data) {
            console.log("Success Like", data);
            if (data.postOwner.id !== data.user.id) {
                AddNotification(data.postOwner.id, "Liked Your Post")
                AddNotificationCall(data.postOwner.id)
            }
        },
        error: function (error) {
            console.error("ERror", error);
        }
    });
}
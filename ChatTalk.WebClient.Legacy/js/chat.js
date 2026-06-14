import * as common from "../common/common.js";

const userInfo = common.getUserInfo();
const userNameText = document.getElementById("userNameText");
const sendButton = document.getElementById("sendButton");
const statusText = document.getElementById("statusText");
const messageTextArea = document.getElementById("messageTextArea");
const menuToggleButton = document.getElementById("menuToggleButton");
const menuPanel = document.getElementById("menuPanel");
const whisperButton = document.getElementById("whisperButton");
const whisperPopup = document.getElementById("whisper-popup");
const userListContainer = document.getElementById("userListContainer");
const userListDiv = document.getElementById("userListDiv");

const sendMessageIds = new Set();

var socket = null;

window.onload = () => {
    userNameText.textContent = userInfo.userId;
    connectSocket();
};

window.addEventListener("message", (event) => {
    popupCallback(event.data);
});

menuToggleButton.addEventListener("click", () => {
    if(menuPanel.classList.contains("hidden")) {
        menuPanel.classList.remove("hidden");
    } else {
        menuPanel.classList.add("hidden");
    }
});

whisperButton.addEventListener("click", () => {
    common.openModalPopup("whisper-popup");
});

sendButton.addEventListener("click", ()  => {
    const message = messageTextArea.value;
    sendMessage(message);
    messageTextArea.value = "";

    addMessage(message, common.MessageDirection.SENT);
});

function connectSocket() {
    socket = new WebSocket(common.SERVER.CHAT_WS);

    socket.onopen = () => {
        statusText.textContent = `상태: 접속됨 (${userInfo.userId})`;

        const joinMessage = {
              type   : "JOIN"
            , userId : userInfo.userId
        };

        socket.send(JSON.stringify(joinMessage));
    };

    socket.onmessage = (event) => {
        const receivedMessage = JSON.parse(event.data);

        console.log("서버 메시지:", receivedMessage);

        if(receivedMessage.type === "MSG") {
            if(sendMessageIds.has(receivedMessage.messageId)) return;

            addMessage(receivedMessage.content, common.MessageDirection.RECEIVED);
        } else if(receivedMessage.type === "USRLIST") {
            refreshUserList(receivedMessage.users);
        }
    };

    socket.onclose = () => {
        statusText.textContent = "상태: 연결 종료";
    };

    socket.onerror = () => {
        statusText.textContent = "상태: 오류 발생";
    };
};

function sendMessage(message) {    
    const messageId = crypto.randomUUID();
    const sendMessage = {
          type      : "MSG"
        , sender    : userInfo.userName
        , content   : message
        , messageId : messageId
    };

    sendMessageIds.add(messageId);
    socket.send(JSON.stringify(sendMessage));
};

function sendWhisperMessage(targetUser, message) {
    const sendMessage = {
          type      : "WHISPER"
        , sender    : userInfo.userName
        , target    : targetUser
        , content   : message
    };

    socket.send(JSON.stringify(sendMessage));
};

function addMessage(message, messageDirection) {
    const chatConatiner = document.getElementById("chatContainer");
    const messageRowDiv = document.createElement("div");
    const messageDiv    = document.createElement("div");

    messageDiv.textContent = message;
    messageDiv.className = "message";
    
    if(messageDirection == common.MessageDirection.SENT) {
        messageRowDiv.className = "message-row sent";
    } else if(messageDirection == common.MessageDirection.RECEIVED) {
        messageRowDiv.className = "message-row received";
    }

    chatConatiner.appendChild(messageRowDiv);
    messageRowDiv.appendChild(messageDiv);
    chatConatiner.scrollTop = chatConatiner.scrollHeight;
};

function popupCallback(json) {
    if(json.type === "CLOSE") {
        common.closeModalPopup(json.data.popupId);
    } else if(json.type === "APPLY") {
        messageTextArea.value = `/w ${json.data.targetUser}`;
        common.closeModalPopup(json.data.popupId);
    }
};

function refreshUserList(userNames) {
    userListDiv.replaceChildren();

    for(const userName of userNames) {
        const userNameDiv = document.createElement("div");
        userNameDiv.textContent = userName;
        userNameDiv.id = `user-${userName}`;
        userListDiv.appendChild(userNameDiv);
    }
};
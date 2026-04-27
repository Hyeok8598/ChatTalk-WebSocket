const userNameText = document.getElementById("userNameText");
const sendButton = document.getElementById("sendButton");
const statusText = document.getElementById("statusText");
const messageTextArea = document.getElementById("messageTextArea");
const userName = sessionStorage.getItem("userName");

var socket = null;

window.onload = () => {

    userNameText.textContent = userName;
    connectSocket();
};

sendButton.addEventListener("click", ()  => {
    const message = messageTextArea.value;
    sendMessage(message);
    messageTextArea.value = "";
});

function connectSocket() {
    socket = new WebSocket("ws://localhost:5174/ws");

    socket.onopen = () => {
        statusText.textContent = `상태: 접속됨 (${userName})`;

        const joinMessage = {
              type     : "JOIN"
            , userName : userName
        };

        socket.send(JSON.stringify(joinMessage));
    };

    socket.onmessage = (event) => {
        console.log("서버 메시지:", event.data);
    };

    socket.onclose = () => {
        statusText.textContent = "상태: 연결 종료";
    };

    socket.onerror = () => {
        statusText.textContent = "상태: 오류 발생";
    };
};

function sendMessage(message) {    
    const sendMessage = {
          type      : "MSG"
        , sender    : userName
        , content   : message
        , messageId : crypto.randomUUID()
    }

    socket.send(JSON.stringify(sendMessage));
}
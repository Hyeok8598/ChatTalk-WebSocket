const userNameText = document.getElementById("userNameInput");
const loginForm = document.getElementById("loginForm");
const statusText = document.getElementById("statusText");

let socket = null;

loginForm.addEventListener("submit", (e) => {
    e.preventDefault();

    const userName = userNameText.value.trim();

    if (!userName) {
        alert("사용자 이름을 입력해주세요.");
        userNameText.focus();
        return;
    }

    // socket = new WebSocket("ws://localhost:5174/ws");

    // socket.onopen = () => {
    //     statusText.textContent = `상태: 접속됨 (${userName})`;

    //     const joinMessage = {
    //         type : "JOIN",
    //         userName : userName
    //     };

    //     socket.send(JSON.stringify(joinMessage));
    // };

    // socket.onmessage = (event) => {
    //     console.log("서버 메시지:", event.data);
    // };

    // socket.onclose = () => {
    //     statusText.textContent = "상태: 연결 종료";
    // };

    // socket.onerror = () => {
    //     statusText.textContent = "상태: 오류 발생";
    // };

    sessionStorage.setItem("userName", userName);
    location.href = "pages/chat.html";
});

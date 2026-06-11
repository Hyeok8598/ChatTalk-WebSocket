import * as common from "../common/common.js";

const loginForm = document.getElementById("loginForm");
const userIdInput = document.getElementById("userIdInput");
const passwordInput = document.getElementById("passwordInput");
const message = document.getElementById("message");

loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    const userId = userIdInput.value;
    const password = passwordInput.value;

    const data = await common.post("api/users/login", {
          userId   : userId
        , password : password
    });

    sessionStorage.setItem("userId", data.userId);
    sessionStorage.setItem("userName", data.userName);

    message.innerText = "로그인 성공";
    location.href = "chat.html";
});
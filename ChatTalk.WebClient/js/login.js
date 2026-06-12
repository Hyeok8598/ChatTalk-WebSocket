import * as common from "../common/common.js";

const loginForm = document.getElementById("loginForm");
const userIdInput = document.getElementById("userIdInput");
const passwordInput = document.getElementById("passwordInput");
const message = document.getElementById("message");

loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    const userId = userIdInput.value;
    const password = passwordInput.value;

    try {
        const data = await common.post("api/users/login", {
              userId   : userId
            , password : password
        });
    
        common.setUserInfo(data);

        message.innerText = "로그인 성공";
        location.href = "chat.html";
    }
    catch {
        alert("사용자 정보가 일치하지 않습니다.");
        message.innerText = "로그인 실패";
    }
});
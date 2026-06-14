import * as common from "../common/common.js";

const userIdInput = document.getElementById("userIdInput");
const passwordInput = document.getElementById("passwordInput");
const userNameInput = document.getElementById("userNameInput");
const signupButton = document.getElementById("signupButton");
const loginPageButton = document.getElementById("loginPageButton");

signupButton.addEventListener("click", async () => {
    if(userIdInput.value == "") {
        alert("유저 ID는 필수입니다.");
        userIdInput.focus();
        return;
    }

    if(userNameInput.value == "") {
        alert("유저명은 필수입니다.");
        userNameInput.focus();
        return;
    }

    if(passwordInput.value == "") {
        alert("비밀번호는 필수입니다.");
        passwordInput.focus();
        return;
    }
    
    try {
        const data = {
                userId : userIdInput.value
            , password : passwordInput.value
            , userName : userNameInput.value
        };
        await common.post("api/users/signup", data);
        alert("회원가입 완료되었습니다.");
    } catch(error) {
        alert("회원가입에 실패하였습니다.");
    }
});

loginPageButton.addEventListener("click", () => {
    location.href = "../pages/login.html";
});

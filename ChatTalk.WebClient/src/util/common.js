export const MESSAGE_DIRECTION = {
    SENT    : "sent",
    RECEIVE : "receive",
    SYSTEM  : "system"
};

export const SERVER = {
    AUTH_API : "http://localhost:8080",
    CHAT_WS  : "http://localhost:5174/ws"
};

const AUTH_API_URL = SERVER.AUTH_API + "/api/users/";

async function request(url, options={}) {
    const response = await fetch(AUTH_API_URL + url, {
        headers: {
            "Content-Type": "application/json",
            ...(options.headers || {})
        },
        ...options
    });

    if(!response.ok) {
        throw new Error("API 요청 실패");
    }

    return await response.json();
};

export function post(url, body) {
    return request(url, {
        method: "POST",
        body: JSON.stringify(body)
    });
};

export function get(url) {
    return request(url, {
        method: "GET"
    });
};

export function setUserInfo(userInfo) {
    sessionStorage.setItem(
        "USER_INFO",
        JSON.stringify(userInfo)
    );
};

export function getUserInfo() {
    const value = sessionStorage.getItem("USER_INFO");

    if(!value) {
        return null;
    }

    return JSON.parse(value);
};
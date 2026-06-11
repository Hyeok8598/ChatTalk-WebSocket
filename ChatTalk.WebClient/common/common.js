export const MessageDirection = {
    SENT     : "sent",
    RECEIVED : "received",
    SYSTEM   : "system"
};

export const SERVER = {
    AUTH_API : "http://localhost:8080",
    CHAT_WS  : "http://localhost:5174/ws"
}

export function openModalPopup(popupNm) {
    const modalPopup = document.getElementById(popupNm);
    modalPopup.classList.remove("hidden");
};

export function closeModalPopup(popupNm) {
    const modalPopup = document.getElementById(popupNm);
    modalPopup.classList.add("hidden");
};


export async function post(url, body) {
    var server_url = SERVER.AUTH_API + "/" + url;

    const response = await fetch(server_url, {
        method : "POST",
        headers : {
            "Content-Type" : "application/json"
        },
        body   : JSON.stringify(body)
    });

    if(!response.ok) {
        throw new Error(await response.text());
    }

    return await response.json();
};
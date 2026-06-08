export const MessageDirection = {
    SENT: "sent",
    RECEIVED: "received",
    SYSTEM: "system"
};

export function openModalPopup(popupNm) {
    const modalPopup = document.getElementById(popupNm);
    modalPopup.classList.remove("hidden");
}

export function closeModalPopup(popupNm) {
    const modalPopup = document.getElementById(popupNm);
    modalPopup.classList.add("hidden");
}
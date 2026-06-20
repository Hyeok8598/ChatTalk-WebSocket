import { ref } from "vue";
import { MESSAGE_DIRECTION } from "../util/common";

export function userChatMessage({ userInfo, send }) {
    const msgs = ref([]);
    const messageIds = new Set();

    function receiveMessage(data) {
        console.info("서버 메시지:", data);

        if(data.type == "MSG") {
            if(messageIds.has(data.messageId)) return;
            addMessage(data, MESSAGE_DIRECTION.RECEIVE);
            return;
        }
        
        if(data.type == "WHISPER") {
            if(messageIds.has(data.messageId)) return;
            addMessage(data, MESSAGE_DIRECTION.RECEIVE, true);
            return;
        }
    }

    function sendMessage(message) {
        const messageId = crypto.randomUUID();

        let data = {
            type           : "MSG",
            messageId,
            senderUserId   : userInfo.userId,
            senderUserName : userInfo.userName,
            content        : message
        };

        messageIds.add(messageId);
        addMessage(data, MESSAGE_DIRECTION.SENT);
        
        send(data);
    };

    function sendWhisperMessage(message, targetUser) {
        const messageId = crypto.randomUUID();

        let data = {
            type           : "WHISPER",
            messageId,
            senderUserId   : userInfo.userId,
            senderUserName : userInfo.userName,
            targetUserId   : targetUser.userId,
            targetUserName : targetUser.userName,
            content        : message
        };

        messageIds.add(messageId);
        addMessage(data, MESSAGE_DIRECTION.SENT, true);

        send(data);
    };

    function addMessage(data, messageDirection, isWhisper=false) {
        msgs.value.push({
            messageId      : data.messageId,
            senderUserId   : data.senderUserId,
            senderUserName : data.senderUserName,
            targetUserId   : data.targetUserId,
            targetUserName : data.targetUserName,
            direction      : messageDirection,
            content        : data.content,
            isWhisper
        });
    };

    // function sendLeaveMessage() {
    //     const data = {
    //           type     : "LEAVE"
    //         , userName :
    //     };
    // };

    return { 
        msgs,
        receiveMessage,
        sendMessage,
        sendWhisperMessage
    }
};
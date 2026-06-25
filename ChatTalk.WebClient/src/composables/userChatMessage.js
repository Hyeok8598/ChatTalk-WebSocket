import { ref } from "vue";
import { uploadFile } from "../common/api/fileApi.js";
import { MESSAGE_DIRECTION } from "../common/util/commonUtil";

export function userChatMessage({ userInfo, send }) {
    const msgs = ref([]);
    const messageIds = new Set();

    function receiveMessage(data) {
        console.info("서버 메시지:", data);

        if(data.type === "MSG" || data.type === "FILE") {
            if(messageIds.has(data.messageId)) return;
            addMessage(data, MESSAGE_DIRECTION.RECEIVE);
            return;
        }
        
        if(data.type === "WHISPER") {
            if(messageIds.has(data.messawgeId)) return;
            addMessage(data, MESSAGE_DIRECTION.RECEIVE, true);
            return;
        }

        // if(data.type === "SYSTEM") {
        //     if(data.systemType === "JOIN") {
        //         // 입장 유저 출력
        //         console.log("[SYSTEM] 입장", data);
        //     }

        //     if(data.systemType === "LEAVE") {
        //         // 퇴장 유저 출력
        //         console.log("[SYSTEM] 퇴장", data);
        //         console.log(data);
        //     }
        // }
    }

    function sendMessage(message) {
        const messageId = crypto.randomUUID();

        let data = {
            type           : "MSG",
            messageId,
            senderUserId   : userInfo.userId,
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

    function sendLeaveMessage() {
        const messageId = crypto.randomUUID();

        const data = {
            type     : "LEAVE",
            messageId,
            senderUserId   : userInfo.userId
        };
        messageIds.add(messageId);
        send(data);
    };

    async function sendFileMessage(file, refType) {
        const fileId = crypto.randomUUID();
        const messageId = crypto.randomUUID();

        const fileApiRequest = {
            refType : refType,
            refId   : fileId
        };
        const fileApiResonse = await uploadFile(file, fileApiRequest);

        const data = {
            type         : "FILE",
            messageId,
            senderUserId : userInfo.userId,
            refType      : fileApiRequest.refType,
            refId        : fileApiRequest.refId,
            originalName : file.name
        };

        messageIds.add(messageId);
        addMessage(data, MESSAGE_DIRECTION.SENT);
        send(data);
    };

    function addMessage(data, messageDirection, isWhisper=false) {
        msgs.value.push({
            ...data,
            direction : messageDirection,
            isWhisper : isWhisper
        });
    };

    return { 
        msgs,
        receiveMessage,
        sendMessage,
        sendWhisperMessage,
        sendLeaveMessage,
        sendFileMessage
    }
};
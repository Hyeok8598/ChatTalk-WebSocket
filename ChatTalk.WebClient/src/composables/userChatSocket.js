import { ref } from "vue";
import { SERVER } from "../util/common";

export function userChatSocket() {
    const status = ref('연결중...');
    let socket = null;

    function connect({ userId, onMessage }) {
        socket = new WebSocket(SERVER.CHAT_WS);
    
        socket.onopen = () => {
            status.value = "접속됨";

            let data = {
                type   : "JOIN",
                userId : userId
            };
        
            send(data);
        };

        socket.onclose = () => {
            status.value = "연결 종료";
        };

        socket.onmessage = (event) => {
            onMessage(event);
        };
    };

    function send(data) {
        if (!socket || socket.readyState !== WebSocket.OPEN) {
            console.warn("[WEBSOCKET] WebSocket is not open.");
            return;
        }

        socket.send(JSON.stringify(data));
    };

    function close() {
        socket?.close();
    };

    return {
        status,
        connect,
        send,
        close
    };
};
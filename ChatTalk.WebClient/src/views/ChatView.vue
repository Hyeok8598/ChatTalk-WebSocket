<template>
    <div class="flex h-screen bg-slate-950 text-white">

        <!-- Left -->
        <aside class="w-60 bg-slate-800 p-4">
            <h1 @click="login" class="text-3xl font-bold">
                ChatTalk
            </h1>

            <p class="text-sm text-gray-400 mt-2">
                상태 : {{ status }}
            </p>
        </aside>

        <!-- Center -->
        <main class="flex flex-col flex-1 bg-slate-900">

            <header
                class="
                    h-18
                    border-b
                    border-slate-700
                    px-6
                    flex
                    justify-between
                    items-center
                    bg-slate-900
                ">

                <div>
                    <h2 class="font-bold">
                        # 전체 채팅
                    </h2>

                    <p class="text-sm text-gray-400">
                        실시간 채팅방
                    </p>
                </div>

                <div>
                    {{ userId }}
                </div>

            </header>

            <section class="flex-1 p-6 overflow-y-auto bg-slate-900">
                <div
                    v-for="message in messages"
                    :key="message.messageId"
                    class="mb-3 flex"
                    :class="message.direction == MESSAGE_DIRECTION.SENT ? 'justify-end' : 'justify-start'"
                >
                    <div
                        class="max-w-md rounded-xl px-4 py-3"
                        :class="message.direction == MESSAGE_DIRECTION.SENT ? 'bg-violet-500' : 'bg-slate-700'"
                    >
                        {{ message.content }}
                    </div>
                </div>
            </section>

            <footer
                class="
                    h-20
                    border-t
                    border-slate-700
                    px-6
                    flex
                    items-center
                    gap-3
                    bg-slate-900
                ">

                <button
                    class="
                        w-12
                        h-12
                        rounded-xl
                        bg-slate-700
                        text-white
                    ">
                    +
                </button>

                <input
                    v-model="message"
                    type="text"
                    placeholder="메시지를 입력하세요."
                    class="
                        flex-1
                        h-12
                        rounded-xl
                        bg-slate-700
                        px-4
                        text-white
                        placeholder-gray-400
                        outline-none
                    " />

                <button
                    @click="clickSendButton"
                    class="
                        w-24
                        h-12
                        rounded-xl
                        bg-violet-500
                        text-white
                        font-bold
                    ">
                    전송
                </button>

            </footer>

        </main>

        <!-- Right -->
        <aside class="w-64 bg-slate-800 p-4">

            <h3 class="text-sm text-gray-400 mb-4">
                접속자 목록
            </h3>

            <div v-for="user in userList" class="space-y-1">
                <div @click="openUserPopup($event, user)" class="rounded-lg px-3 py-2 hover:bg-slate-700">{{ user }}</div>
                <UserPopupVue v-if="isUserMenuOpen" @close="closeUserPopup" :props="userPopupProps"></UserPopupVue>
            </div>

            <footer>
                <button
                    @click="openSetting" 
                    class="fixed bottom-6 right-6 w-12 h-12 rounded-full bg-slate-700 hover:bg-slate-600 flex items-center justify-center shadow-lg transition"
                >
                    ⚙️
                </button>
                <div
                    v-if="isSettingOpen || isMyInfoOpen"
                    class="fixed inset-0 bg-black/50 flex justify-center items-center"
                >
                    <SettingPopupVue v-if="isSettingOpen" @close="closeSetting" @open-my-info="openMyInfo"></SettingPopupVue>
                    <MyInfoPopupVue v-if="isMyInfoOpen" @close="closeMyInfo"></MyInfoPopupVue>                    
                </div>
            </footer>
            
        </aside>

    </div>
</template>

<script setup>
import { reactive, ref } from "vue";
import { getUserInfo, MESSAGE_DIRECTION, SERVER } from "../util/common";
import { useRouter } from "vue-router";
import MyInfoPopupVue from "./popup/MyInfoPopup.vue";
import SettingPopupVue from "./popup/SettingPopup.vue";
import UserPopupVue from "./popup/UserPopup.vue";

const userId = getUserInfo().userId;
const status = ref('연결중...');
const message = ref('');
const userList = ref([]);
const messages = ref([]);
const messageIds = new Set();
const router = useRouter();
const isSettingOpen = ref(false);
const isMyInfoOpen = ref(false);
const isUserMenuOpen = ref(false);
const selectedUser = ref(null);
const userPopupProps = ref({ x: 0, y: 0});

var socket = null;
connectSocket();

async function clickSendButton() {
    sendMessage(message.value);
};

function connectSocket() {
    socket = new WebSocket(SERVER.CHAT_WS);
    
    socket.onopen = () => openSocket();
    socket.onclose = () => closeSocket();
    socket.onmessage = (event) => receiveMessage(event);
};

function openSocket() {
    status.value = "접속됨";

    const data = {
        type   : "JOIN",
        userId : userId
    };
    
    socket.send(JSON.stringify(data));
};

function closeSocket() {
    status.value = "연결 종료";
};

function receiveMessage(event) {
    const data = JSON.parse(event.data);
    console.log("서버 메시지:", data);

    if(data.type == "MSG") {
        if(messageIds.has(data.messageId)) return;
        addMessage(data, MESSAGE_DIRECTION.RECEIVE);
    } else if(data.type == "USRLIST") {
        refreshUserList(data);
    }
};

function sendMessage(message) {
    const messageId = crypto.randomUUID();

    const data = {
        type      : "MSG",
        sender    : getUserInfo().userName,
        content   : message,
        messageId : messageId
    };
    messageIds.add(messageId, MESSAGE_DIRECTION.SENT);
    addMessage(data, MESSAGE_DIRECTION.SENT);
    socket.send(JSON.stringify(data));
};

function sendWhisperMessage(targetUser, message) {
    const data = {
          type      : "WHISPER"
        , sender    : userInfo.userName
        , target    : targetUser
        , content   : message
    };

    socket.send(JSON.stringify(data));
};

// function sendLeaveMessage() {
//     const data = {
//           type     : "LEAVE"
//         , userName :
//     };
// };

function refreshUserList(data) {
    userList.value = data.users;
};

function addMessage(data, messageDirection) {
    messages.value.push({
        messageId : data.messageId,
        direction : messageDirection,
        content   : data.content
    });
};

function login() {
    router.replace("/");
};

function openSetting() {
    isSettingOpen.value = true;
};

function closeSetting() {
    isSettingOpen.value = false;
};

function openMyInfo() {
    isSettingOpen.value = false;
    isMyInfoOpen.value = true;
};

function closeMyInfo() {
    isSettingOpen.value = true;
    isMyInfoOpen.value = false
};

function openUserPopup(event, user) {
    selectedUser.value = user;

    userPopupProps.value = {
        userName : user,
        x : event.clientX,
        y : event.clientY
    };

    isUserMenuOpen.value = true;
};

function closeUserPopup() {
    isUserMenuOpen.value = false;
};
</script>
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
                    {{ userInfo.userId }}
                </div>

            </header>

            <section class="flex-1 p-6 overflow-y-auto bg-slate-900">
                <div
                    v-for="msg in msgs"
                    :key="msg.messageId"
                    class="mb-3 flex"
                    :class="msg.direction == MESSAGE_DIRECTION.SENT ? 'justify-end' : 'justify-start'"
                >
                    <div
                        class="max-w-md rounded-xl px-4 py-3"
                        :class="[
                            msg.direction == MESSAGE_DIRECTION.SENT ? 'bg-violet-500' : 'bg-slate-700',

                            msg.isWhisper ? 'bg-slate-800 border border-violet-400' : ''
                        ]"
                    >
                        <div 
                            v-if="msg.isWhisper"
                            class="mb-1 text-xs text-violet-300"
                        >
                            🔒 귓속말
                            {{ 
                                msg.direction == MESSAGE_DIRECTION.SENT ?
                                "[to. " + msg.senderUserName + "]" : "[from. " + msg.targetUserName + "]"
                            }}
                        </div>
                        {{ msg.content }}
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

                <!-- <div ref="messageInput"
                    contenteditable="true"
                    class="flex-1 min-h-12 rounded-xl bg-slate-700 px-4 py-3 text-white outline-none"
                    @input="onInput"
                >
                    <MentionTag v-if="metionUser" :user-name="metionUser.userName"/>
                </div> -->
                <div
                    class="
                        flex-1
                        min-h-12
                        rounded-xl
                        bg-slate-700
                        px-4
                        py-2
                        flex
                        items-center
                        gap-2
                    "
                >
                    <MentionTag
                        v-if="mentionUser"
                        :props="mentionTagProps"
                        :user-name="mentionUser.userName"
                    />
                    <WhisperTag
                        v-if="whisperUser"
                        :props="whisperTagProps"
                        :user-name="whisperUser.userName"
                    />

                    <input
                        ref="msgContentInput"
                        v-model="msgContent"
                        class="
                            flex-1
                            bg-transparent
                            outline-none
                            text-white
                        "
                        @input="onMsgContentInput"
                        @keydown="onMsgContentKeydown"
                    />
                </div>

                <UserPickerPopupVue v-if="isUserPickerOpen" :props="userPickerProps" @select="selectUser"/>

                <button
                    @click="clickSendButton"
                    class="
                        w-24
                        h-12
                        rounded-xl
                        bg-violet-500
                        text-white
                        font-bold"
                    >
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
                <div @click="openUserPopup($event, user)" class="rounded-lg px-3 py-2 hover:bg-slate-700">{{ user.userName }}</div>
            </div>
            <UserPopupVue v-if="isUserMenuOpen" @close-user-popup="closeUserPopup" @click-whisper="clickWhisper" :props="userPopupProps"></UserPopupVue>

            <footer>
                <button
                    @click="openSettingPopup" 
                    class="fixed bottom-6 right-6 w-12 h-12 rounded-full bg-slate-700 hover:bg-slate-600 flex items-center justify-center shadow-lg transition"
                >
                    ⚙️
                </button>
                <div
                    v-if="isSettingOpen || isMyInfoOpen"
                    class="fixed inset-0 bg-black/50 flex justify-center items-center"
                >
                    <SettingPopupVue v-if="isSettingOpen" @close="closeSettingPopup" @open-my-info="openMyInfoPopup"></SettingPopupVue>
                    <MyInfoPopupVue v-if="isMyInfoOpen" @close="closeMyInfoPopup"></MyInfoPopupVue>                    
                </div>
            </footer>
            
        </aside>

    </div>
</template>

<script setup>

/**
 * ======================================================================
 *  1. Variable
 *  2. Lifecycle
 *  3. Event
 *  4. WebSocket
 *  5. Business
 *  6. Util
 *  7. Popup
 * ======================================================================
 */

import { nextTick, onMounted, onUnmounted, reactive, ref } from "vue";
import { getUserInfo, INPUT_MESSAGE_MODE, MESSAGE_DIRECTION, SERVER } from "../util/common";
import { useRouter } from "vue-router";
import MyInfoPopupVue from "./popup/MyInfoPopup.vue";
import SettingPopupVue from "./popup/SettingPopup.vue";
import UserPopupVue from "./popup/UserPopup.vue";
import UserPickerPopupVue from "./popup/UserPickerPopup.vue";
import MentionTag from "../components/MentionTag.vue";
import WhisperTag from "../components/WhisperTag.vue";
import { userChatSocket } from "../composables/userChatSocket.js";
import { userChatMessage } from "../composables/userChatMessage.js";

/**
 * ======================================================================
 *  1. Variable
 * ======================================================================
 */

const userInfo = getUserInfo();
const router = useRouter();

/**
 * Component
 */
const msgContentInput = ref();

/**
 * State
 */
const msgContent = ref("");
const userList = ref([]);
const filterUsers = ref([]);
const selectedUser = ref(null);
const mentionUser = ref(null);
const whisperUser = ref(null);
const inputModeState = ref({
    mode     : INPUT_MESSAGE_MODE.NONE,
    keyword  : ""
});

/**
 * Popup
 */
const isSettingOpen = ref(false);
const isMyInfoOpen = ref(false);
const isUserMenuOpen = ref(false);
const isUserPickerOpen = ref(false);
const userPopupProps = ref(null);
const userPickerProps = ref(null);
const mentionTagProps = ref(null);
const whisperTagProps = ref(null);

/**
 * Composable
 */
const {
    status, connect, send, close
} = userChatSocket();

const {
    msgs, receiveMessage, sendMessage, sendWhisperMessage
} = userChatMessage({userInfo, send});

/**
 * ======================================================================
 *  2. Lifecycle
 * ======================================================================
 */

onMounted(() => {
    connect({
        userId    : userInfo.userId,
        onMessage : handleSocketMessage
    });
});

onUnmounted(() => {
    close();
});

/**
 * ======================================================================
 *  3. Event
 * ======================================================================
 */

async function clickSendButton() {
    if(inputModeState.value.mode === INPUT_MESSAGE_MODE.WHISPER) {
        await sendWhisperMessage(msgContent.value, whisperTagProps.value);
        clearMessage();
        return;
    } 

    else {
        await sendMessage(msgContent.value);
        clearMessage();
        mentionUser.value = null;
        resetInputModeState();

        return;
    }
};

function onMsgContentInput() {
    const text = msgContent.value;
    openMentionMenu(text);
    openWhisperMenu(text);
};

function onMsgContentKeydown(event) {
    const cursorPosition = event.target.selectionStart;

    if(event.key === "Backspace") {
        if(cursorPosition === 0) {
            mentionUser.value = null;
            whisperUser.value = null;
            whisperTagProps.value = null;
            resetInputModeState();
        }

        closeUserPickerPopup();
    }

    if(event.key === "Enter") {
        clickSendButton();
    }
};

/**
 * ======================================================================
 *  4. Business
 * ======================================================================
 */

function handleSocketMessage(event) {
    const data = JSON.parse(event.data);
    
    if(data.type === "USRLIST") {
        refreshUserList(data);
    } else {
        receiveMessage(data);
    }
};

function openMentionMenu(text) {
    const mentionMatch = text.match(/@([^\s@]*)$/);

    if(!mentionMatch) return;
    if(mentionUser.value || whisperTagProps.value) return;

    inputModeState.value = {
        mode : INPUT_MESSAGE_MODE.MENTION,
        keyword : mentionMatch[1]
    };

    filterUsers.value = userList.value.filter(
        user => user.userName?.includes(inputModeState.value.keyword)
    );

    const rect = msgContentInput.value.getBoundingClientRect();

    userPickerProps.value = {
        users : filterUsers.value,
        left  : rect.left,
        top   : rect.top - 80
    };

    showUserPickerPopup();
};

function openWhisperMenu(text) {
    const whisperMatch = text.match(/\/w([^\s]*)$/);

    if(!whisperMatch) return;
    if(mentionUser.value || whisperTagProps.value) return;

    inputModeState.value = {
        mode : INPUT_MESSAGE_MODE.WHISPER,
        keyword : whisperMatch[1]
    };

    filterUsers.value = userList.value.filter(
        user => user.userName?.includes(inputModeState.value.keyword)
    );

    const rect = msgContentInput.value.getBoundingClientRect();

    userPickerProps.value = {
        users : filterUsers.value,
        left  : rect.left,
        top   : rect.top - 80
    };

    showUserPickerPopup();
};

function refreshUserList(data) {
    userList.value = data.users;
};

async function selectUser(user) {
    if(inputModeState.value.mode === INPUT_MESSAGE_MODE.MENTION) {
        mentionUser.value = user;
    } else if(inputModeState.value.mode === INPUT_MESSAGE_MODE.WHISPER) {
        whisperUser.value = user;

        whisperTagProps.value = {
            userId   : user.userId,
            userName : user.userName
        };
    }

    isUserPickerOpen.value = false;
    await nextTick();
    msgContentInput.value.focus();
    msgContent.value = "";
};

function clickWhisper(user) {
    inputModeState.value = {
        mode    : INPUT_MESSAGE_MODE.WHISPER,
        keyword : user.userId
    };

    whisperUser.value = user;

    whisperTagProps.value = {
        userId   : user.userId,
        userName : user.userName
    };
    closeUserPopup();
};

function clearMessage() {
    msgContent.value = "";
};

function resetInputModeState() {
    inputModeState.value = {
        mode    : INPUT_MESSAGE_MODE.NONE,
        keyword : ""
    };
};

/**
 * ======================================================================
 *  7. Popup
 * ======================================================================
 */

function openSettingPopup() {
    isSettingOpen.value = true;
};

function closeSettingPopup() {
    isSettingOpen.value = false;
};

function openMyInfoPopup() {
    isSettingOpen.value = false;
    isMyInfoOpen.value = true;
};

function closeMyInfoPopup() {
    isSettingOpen.value = true;
    isMyInfoOpen.value = false
};

function openUserPopup(event, user) {
    selectedUser.value = user;

    userPopupProps.value = {
        userName : user.userName,
        userId   : user.userId,
        x        : event.clientX,
        y        : event.clientY
    };

    isUserMenuOpen.value = true;
};

function closeUserPopup() {
    isUserMenuOpen.value = false;
};

function showUserPickerPopup() {
    isUserPickerOpen.value = true;
};

function closeUserPickerPopup() {
    isUserPickerOpen.value = false;
};

/**
 * ======================================================================
 *  8. Router
 * ======================================================================
 */

function login() {
    router.replace("/");
};

</script>
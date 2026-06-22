<template>
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
                            "[to. " + msg.targetUserName + "]" : "[from. " + msg.senderUserName + "]"
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
                    :mention-user-name="mentionUserName"
                />
                <WhisperTag
                    v-if="whisperUser"
                    :whisper-user-name="whisperUserName"
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

            <UserPickerPopup 
                v-if="isUserPickerOpen" 
                :users="users" 
                :left="left" 
                :top="top" 
                @pick-user="pickUser"
            />

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
</template>

<script setup>
import { nextTick, onMounted, onUnmounted, ref, watch } from 'vue';
import { getUserInfo, INPUT_MESSAGE_MODE, MESSAGE_DIRECTION } from '../../util/common.js';
import { userChatMessage } from '../../composables/userChatMessage.js';
import { userChatSocket } from '../../composables/userChatSocket.js';
import MentionTag from './MentionTag.vue';
import WhisperTag from './WhisperTag.vue';
import UserPickerPopup from '../popup/UserPickerPopup.vue';

const userInfo = getUserInfo();

/**
 * Component
 */
const msgContentInput = ref();

/**
 * State
 */
const msgContent = ref("");
const filterUsers = ref([]);
const selectedUser = ref(null);
/**
 * Popup
 */
const isUserPickerOpen = ref(false);
const users = ref([]);
const left = ref(null);
const top = ref(null);

const whisperUserName = ref(null);
const mentionUserName = ref(null);

/**
 * Composable
 */
const {
    status, connect, send, close
} = userChatSocket();

const {
    msgs, receiveMessage, sendMessage, sendWhisperMessage, sendLeaveMessage
} = userChatMessage({userInfo, send});

const props = defineProps({
    mode            : String,
    userList        : Array,
    inputModeState  : Object,
    whisperUser     : Object,
    mentionUser     : Object
});

const emit = defineEmits([
    'update-user-list',
    'reset-input-mode-state',
    'select-user',
    'update-input-mode-state',
    'clear-mention-user',
    'update-whisper-user',
    'clear-whisper-user'
]);

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
    sendLeaveMessage();
});

watch(
    () => props.whisperUser,
    () => {
        if(!props.whisperUser) return;
        whisperUserName.value = props.whisperUser.userName
    }
);

/**
 * ======================================================================
 *  3. Event
 * ======================================================================
 */

 async function clickSendButton() {
    if(props.inputModeState.mode === INPUT_MESSAGE_MODE.WHISPER) {
        await sendWhisperMessage(msgContent.value, props.whisperUser);
        clearMessage();
        return;
    } 

    else {
        await sendMessage(msgContent.value);
        clearMessage();
        emit('clear-whisper-user')
        emit('reset-input-mode-state');

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
            emit('clear-mention-user');
            emit('clear-whisper-user');
            emit('reset-input-mode-state');
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
        emit('update-user-list', data.users);
        return;
    }
    
    if(data.type === "SYSTEM") {
        if(data.systemType === "LEAVE") {
            if(data.userId === userInfo.userId) {
                close();
                return;
            } else {
                receiveMessage(data);
                return;
            }
        } 

        receiveMessage(data);
        return;
    }   
    
    receiveMessage(data);
};

function openMentionMenu(text) {
    const mentionMatch = text.match(/@([^\s@]*)$/);
    if(!mentionMatch) return;
    const keyword = mentionMatch[1];
    if(props.whisperUser || whisperUserName.value) return;
    emit('update-input-mode-state', INPUT_MESSAGE_MODE.MENTION, keyword);

    filterUsers.value = props.userList.filter(
        user => user.userName?.includes(keyword)
    );

    const rect = msgContentInput.value.getBoundingClientRect();

    users.value = filterUsers.value;
    left.value  = rect.left;
    top.value   = rect.top - 80;

    showUserPickerPopup();
};

function openWhisperMenu(text) {
    const whisperMatch = text.match(/\/w([^\s]*)$/);
    if(!whisperMatch) return;
    const keyword = whisperMatch[1];
    if(props.whisperUser || mentionUserName.value) return;
    emit('update-input-mode-state', INPUT_MESSAGE_MODE.WHISPER, keyword);

    filterUsers.value = props.userList.filter(
        user => user.userName?.includes(keyword)
    );

    const rect = msgContentInput.value.getBoundingClientRect();

    users.value = filterUsers.value;
    left.value  = rect.left;
    top.value   = rect.top - 80;

    showUserPickerPopup();
};

async function pickUser(user) {
    emit('select-user', user);

    if(props.inputModeState.mode === INPUT_MESSAGE_MODE.MENTION) {
        mentionUserName.value = user.userName
    } else if(props.inputModeState.mode === INPUT_MESSAGE_MODE.WHISPER) {
        whisperUserName.value = user.userName
    }

    isUserPickerOpen.value = false;
    await nextTick();
    msgContentInput.value.focus();
    msgContent.value = "";
};

function clearMessage() {
    msgContent.value = "";
};

/**
 * ======================================================================
 *  7. Popup
 * ======================================================================
 */

function showUserPickerPopup() {
    isUserPickerOpen.value = true;
};

function closeUserPickerPopup() {
    isUserPickerOpen.value = false;
};
</script>
<template>
    <div class="flex h-screen bg-slate-950 text-white">
        
        <ChatLeftPanel
            @click-chat-room="clickChatRoom"
            @click-logo="clickLogo"
            :user-info="userInfo"
            :is-select-chat-room="isSelectChatRoom"
            :mode="mode"
            :whisper-user="whisperUser"
            :mention-user="mentionUser"
        />

        <ChatCenterPanel
            :mode="mode"
            :user-list="userList"
            :input-mode-state="inputModeState"
            :whisper-user="whisperUser"
            :mention-user="mentionUser"
            :file="file"
            
            @reset-input-mode-state="resetInputModeState"
            @update-user-list="updateUserList"
            @select-user="selectUser"
            @clear-mention-user="clearMentionUser"
            @clear-whisper-user="clearWhisperUser"
            @update-input-mode-state="updateInputModeState"
            @update-whisper-user="updateWhisperUser"
            @select-file="selectFile"
            @remove-file="removeFile"
        />

        <ChatRightPanel
            :mode="mode"
            :user-list="userList"
            :input-mode-state="inputModeState"

            @update-input-mode-state="updateInputModeState"
            @update-whisper-user="updateWhisperUser"
        />
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { getUserInfo, INPUT_MESSAGE_MODE } from '../../common/util/commonUtil.js';
import ChatCenterPanel from '../../components/layout/ChatCenterPanel.vue';
import ChatLeftPanel from '../../components/layout/ChatLeftPanel.vue';
import ChatRightPanel from '../../components/layout/ChatRightPanel.vue';

const router = useRouter();
const userInfo = getUserInfo();
const mode = ref("MAIN");
const isSelectChatRoom = ref(false);
const userList = ref([]);
const whisperUser = ref(null);
const mentionUser = ref(null);
const file = ref(null);
const inputModeState = ref({
    mode     : INPUT_MESSAGE_MODE.NONE,
    keyword  : ""
});

function clickChatRoom() {
    mode.value = "ROOM";
    isSelectChatRoom.value = true;
};

function clickLogo() {
    mode.value = "MAIN"
    isSelectChatRoom.value = false;
};

function updateUserList(users) {
    userList.value = users;
};

function resetInputModeState() {
    inputModeState.value = {
        mode    : INPUT_MESSAGE_MODE.NONE,
        keyword : ""
    };
};

function selectUser(user) {
    if(inputModeState.value.mode === INPUT_MESSAGE_MODE.MENTION) {
        mentionUser.value = user;
    } else if(inputModeState.value.mode === INPUT_MESSAGE_MODE.WHISPER) {
        whisperUser.value = user;
    }
};

function clearMentionUser() {
    mentionUser.value = null;
};

function clearWhisperUser() {
    whisperUser.value = null;
};

function updateInputModeState(mode, keyword) {
    inputModeState.value = {
        mode : mode,
        keyword : keyword
    };
};

function updateWhisperUser(user) {
    whisperUser.value = user;
};

function selectFile(data) {
    file.value = data;
    updateInputModeState(INPUT_MESSAGE_MODE.FILE, "");
};

function removeFile() {
    file.value = null
};
</script>
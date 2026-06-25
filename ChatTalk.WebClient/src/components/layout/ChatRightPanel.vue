<template>
    <aside v-if="mode === 'MAIN'" class="w-64 bg-[#10182B] border-l border-[#1C2742] p-4 select-none">
        <h3 class="text-sm text-gray-400 mb-4">
            시작하기
        </h3>

        <div class="rounded-xl bg-[#141B2D] border border-[#253250] p-4 mb-4">
            <div class="text-sm text-gray-400 mb-1">
                선택된 채팅방
            </div>
            <div class="font-bold text-white">
                없음
            </div>
            <p class="text-xs text-gray-400 mt-2">
                왼쪽에서 채팅방을 선택하면 대화를 시작할 수 있습니다.
            </p>
        </div>

        <div class="space-y-2">
            <button 
                @click="clickEnterRoom"
                class="w-full text-left rounded-lg px-3 py-2 bg-[#182238] hover:bg-[#202C46]">
                # 전체 채팅방 입장
            </button>

            <button 
                @click="clickEnterRoom"
                class="w-full text-left rounded-lg px-3 py-2 bg-[#182238] hover:bg-[#202C46]">
                내 프로필 설정
            </button>

            <button
                @click="clickEnterRoom"
                class="w-full text-left rounded-lg px-3 py-2 bg-[#182238] hover:bg-[#202C46]">
                ChatTalk 사용법
            </button>
        </div>

        <div class="mt-6 border-t border-[#1C2742] pt-4">
            <h4 class="text-sm text-gray-400 mb-3">
                지원 기능
            </h4>

            <div class="space-y-2 text-sm text-gray-300">
                <div>💬 실시간 채팅</div>
                <div>🔒 귓속말</div>
                <div>📣 멘션</div>
            </div>
        </div>
    </aside>

    <aside v-if="mode === 'ROOM'" class="w-64 bg-slate-800 p-4">

        <h3 class="text-sm text-gray-400 mb-4">
            접속자 목록
        </h3>

        <div v-for="user in userList" class="space-y-1">
            <div @click="openUserPopup($event, user)" class="rounded-lg px-3 py-2 hover:bg-slate-700">{{ user.userName }}</div>
        </div>
        <UserPopup v-if="isUserMenuOpen" @close-user-popup="closeUserPopup" @click-whisper="clickWhisper" :props="userPopupProps"></UserPopup>

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
                <SettingPopup v-if="isSettingOpen" @close="closeSettingPopup" @open-my-info="openMyInfoPopup"></SettingPopup>
                <MyInfoPopup v-if="isMyInfoOpen" @close="closeMyInfoPopup"></MyInfoPopup>                    
            </div>
        </footer>
        
    </aside>
</template>

<script setup>
import { ref } from 'vue';
import { INPUT_MESSAGE_MODE } from '../../common/util/commonUtil.js';
import MyInfoPopup from '../popup/MyInfoPopup.vue';
import SettingPopup from '../popup/SettingPopup.vue';
import UserPopup from '../popup/UserPopup.vue';


const selectedUser = ref(null);

const isUserMenuOpen = ref(false);

const userPopupProps = ref(null);
const whisperTagProps = ref(null);

const isSettingOpen = ref(false);
const isMyInfoOpen = ref(false);
const isUserPickerOpen = ref(false);

const props = defineProps({
    mode     : String,
    userList : Array,
    inputModeState : Object,
    whisperUser : Object,
    mentionUser : Object
});

const emit = defineEmits([
    'update-input-mode-state',
    'update-whisper-user'
]);
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

function openMyInfoPopup() {
    isSettingOpen.value = false;
    isMyInfoOpen.value = true;
};

function openSettingPopup() {
    isSettingOpen.value = true;
};

function closeSettingPopup() {
    isSettingOpen.value = false;
};

function closeMyInfoPopup() {
    isSettingOpen.value = true;
    isMyInfoOpen.value = false
};

function clickWhisper(user) {
    emit('update-input-mode-state', INPUT_MESSAGE_MODE.WHISPER, user.userId);
    emit('update-whisper-user', user);

    whisperTagProps.value = {
        userId   : user.userId,
        userName : user.userName
    };
    closeUserPopup();
};

function clickEnterRoom() {
    alert("준비중입니다.");
};

</script>
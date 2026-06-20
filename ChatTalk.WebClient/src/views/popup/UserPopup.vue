<template>
    <div
        class="fixed inset-0"
        @click="closeUserPopup"
    >
        <div
            class="fixed z-50 w-40 rounded-lg bg-slate-700 shadow-lg p-2"
            :style="{
                top: props.y + 'px',
                left: props.x + 'px'
            }"
            @click.stop
        >
            <div class="px-3 py-2 font-bold">
                {{ user.userName }}
            </div>

            <button
                @click="openUserInfo"
                class="w-full text-left px-3 py-2 hover:bg-slate-600 rounded"
            >
                정보 보기
            </button>
            <UserInfoPopupView v-if="isUserInfoOpen" :props="userInfoProps" @close-user-info="closeUserInfo"></UserInfoPopupView>

            <button
                @click="clickWhisper(user)"
                class="w-full text-left px-3 py-2 hover:bg-slate-600 rounded"
            >
                귓속말하기
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">

/**
 * UserPopup Props
 *
 * @typedef {Object} Props
 * @property {string} userId
 * @property {string} userName
 * @property {number} x
 * @property {number} y
 */

import UserInfoPopupView from './UserInfoPopup.vue';
import { ref } from 'vue';

const isUserInfoOpen = ref(false);
const userInfoProps = ref({});
const emit = defineEmits([
    'clickWhisper',
    'closeUserPopup'
]);

const { props } = defineProps({
    props : Object
});

const user = {
    userId   : props.userId,
    userName : props.userName
};

function openUserInfo() {
    userInfoProps.value = {
        userId   : props.userId,
        userName : props.userName
    };

    isUserInfoOpen.value = true;
};

function closeUserPopup() {
    emit("closeUserPopup");
};

function closeUserInfo() {
    isUserInfoOpen.value = false;
};

function clickWhisper(user) {
    emit("clickWhisper", user);
};
</script>
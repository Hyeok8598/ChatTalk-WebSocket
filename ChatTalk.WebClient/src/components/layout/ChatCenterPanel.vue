<template>
    <ChatMain
        v-if="mode === 'MAIN'"
    />

    <ChatRoom
        v-if="mode === 'ROOM'"
        :user-list="userList"
        :input-mode-state="inputModeState"
        :whisper-user="whisperUser"
        :mention-user="mentionUser"

        @reset-input-mode-state="$emit('reset-input-mode-state')"
        @update-user-list="(users) => $emit('update-user-list', users)"
        @select-user="(user) => $emit('select-user', user)"
        @clear-mention-user="$emit('clear-mention-user')"
        @clear-whisper-user="$emit('clear-whisper-user')"
        @update-whisper-user="(user) => $emit('update-whisper-user', user)"
        @update-input-mode-state="(mode, keyword) => $emit('update-input-mode-state', mode, keyword)"
    />
</template>

<script setup>

import ChatMain from '../chat/ChatMain.vue';
import ChatRoom from '../chat/ChatRoom.vue';

const props  = defineProps({
    mode : String,
    userList : Array,
    inputModeState : Object,
    whisperUser : Object,
    mentionUser : Object
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

</script>


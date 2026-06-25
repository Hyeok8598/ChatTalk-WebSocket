<template>
    <div class="relative">
        <button
            type="button"
            @click="toggleMenu"
            class="
                w-10 h-10
                rounded-full
                bg-slate-700
                text-white text-xl
                flex items-center justify-center
                hover:bg-slate-600
                transition
                cursor-pointer
            "
        >
        +
        </button>
        
        <ChatInputActionMenu
            v-if="isActionButtonOpen"
            :file="file"
            :input-state-mode="inputStateMode"
            @select-file="(data) => $emit('select-file', data)"
            @close="toggleMenu"
        />
    </div>
</template>

<script setup>
import { ref } from 'vue';
import ChatInputActionMenu from './ChatInputActionMenu.vue';

const props = defineProps({
    file : File,
    inputStateMode : Object,
    disableClickFile : Boolean
});

const emit = defineEmits([
    'select-file'
]);

const isActionButtonOpen = ref(false);

function toggleMenu() {
    isActionButtonOpen.value = !isActionButtonOpen.value;
};
</script>
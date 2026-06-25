<template>
    <div
        ref="popupRef"
        @click="handleClickOutside"
        @click.stop
        class="
            absolute
            left-0
            bottom-14
            w-80
            rounded-2xl
            bg-slate-800
            border border-slate-700
            shadow-xl
            p-4
            grid grid-cols-4
            gap-4
            z-50
        "
    >
        <button
            @click="clickFile"
            @change="changeFile"
            class="flex flex-col items-center gap-2 text-white text-xs cursor-pointer"
        >
            <div
                class="w-12 h-12 rounded-2xl bg-slate-700 flex items-center justify-center text-xl">
                📎
            </div>
            <input 
                ref="fileInputRef"
                type="file"
                class="hidden"
                cursor-pointer
            />
            파일
        </button>

        <button
            class="flex flex-col items-center gap-2 text-white text-xs cursor-pointer"
        >
            <div class="w-12 h-12 rounded-2xl bg-slate-700 flex items-center justify-center text-xl">
                🔒
            </div>

            귓속말
        </button>
    </div>
</template>

<script setup>
import { onMounted, onUnmounted, ref } from 'vue';

const fileInputRef = ref(null);
const popupRef = ref(null);
const props = defineProps({
    file : File,
    inputStateMode : Object,
    disableClickFile : Boolean
});
const emit = defineEmits([
    'select-file',
    'remove-file',
    'close'
]);

const file = ref(null);

function clickFile() {
    fileInputRef.value?.click();
    emit('close')
};

function changeFile(event) {
    const data = event.target.files[0];
    if(!data) return;

    emit('select-file', data);
    event.target.value = "";
    emit('close');
};

function handleClickOutside(event) {
    if(!popupRef.value) return;
    if(!popupRef.value.contains(event.target)) {
        emit('close');
    }
};

onMounted(() => {
    setTimeout(() => {
        document.addEventListener('click', handleClickOutside);
    });
});

onUnmounted(() => {
    document.addEventListener('click', handleClickOutside);
});
</script>
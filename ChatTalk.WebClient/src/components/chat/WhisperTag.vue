<template>
    <span
        contenteditable="false"
        class="inline-flex items-center px-2 py-0.5 rounded-md border border-blue-500/20 text-blue-300 text-sm font-medium"
    >
        👂{{ whisperUserName }}
    </span>
</template>
<script setup>
const props = defineProps({
    whisperUserName : String
});
</script>
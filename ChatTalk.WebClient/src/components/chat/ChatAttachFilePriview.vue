<template>
    <div
        v-if="file"
        class="
            mb-2
            flex
            items-center
            justify-between
            rounded-lg
            border
            border-slate-700
            bg-slate-800
            px-3
            py-2
        "
    >
        <div class="flex items-center gap-2">
            📎

            <div>
                <div class="text-sm text-white">
                    {{ file.name }}
                </div>

                <div class="text-xs text-slate-400">
                    {{ formatSize(file.size) }}
                </div>
            </div>
        </div>

        <button
            @click="$emit('remove-file')"
            class="text-slate-400 hover:text-red-400"
        >
            ✕
        </button>
    </div>
</template>

<script setup>
const props = defineProps({
    file: Object
});

const emit = defineEmits([
    'remove-file'
]);

function formatSize(size) {
    return `${(size/1024).toFixed(1)} KB`;
};
</script>
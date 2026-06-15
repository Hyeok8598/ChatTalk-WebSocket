<template>
    <div
        class="fixed inset-0 bg-black/50 flex justify-center items-center"
        @click="$emit('close')"
    >
        <div
            class="w-96 bg-slate-800 rounded-lg p-6"
            @click.stop
        >
            <h2 class="text-xl font-bold mb-4">
                설정
            </h2>

            <button
                class="w-full p-2 text-left hover:bg-slate-700 rounded"
                @click="$emit('open-my-info')"
            >
                내 정보
            </button>
        </div>
    </div>
</template>
<template>
    <aside
        class="
            w-60
            bg-[#10182B]
            border-r
            border-[#1C2742]
            p-4
            flex
            flex-col
            select-none
        "
    >
        <!-- Logo -->
        <div @click="clickLogo" class="pb-4 mb-4 border-b border-[#1C2742] cursor-pointer">
            <div class="flex items-center gap-3">
                <div class="text-3xl font-bold">
                    <span class="text-white">Chat</span>
                    <span class="text-[#B388FF]">Talk</span>
                </div>
            </div>

            <p class="text-xs text-gray-400 mt-2">
                실시간 채팅 플랫폼
            </p>
        </div>

        <!-- Room List -->
        <div class="flex-1">
            <div class="text-xs text-gray-500 tracking-wider mb-3">
                채팅방
            </div>

            <button
                @click="clickChatRoom"
                :class="[
                    mode === 'ROOM'
                        ? 'bg-violet-500 text-white' : 'bg-[#182238] text-white hover:bg-[#22304d]'
                ]"
                class="
                    w-full
                    flex
                    items-center
                    gap-3
                    px-3
                    py-2
                    rounded-lg
                    bg-[#182238]
                    text-white
                    font-medium

                    cursor-pointer
                    hover:bg-[#22304d]
                    transition-colors
                    duration-200
                "
            >
                <span>#</span>
                전체 채팅
            </button>
        </div>

        <!-- User -->
        <div
            @click="clickMyInfoMenu"
            class="
                border-t
                border-[#1C2742]
                pt-4
                flex
                items-center
                gap-3
                cursor-pointer
            "
        >
            <div
                class="
                    w-10
                    h-10
                    rounded-full
                    bg-[#7C4DFF]
                    flex
                    items-center
                    justify-center
                    font-bold
                "
            >
                {{ userInfo.userInitial }}
            </div>

            <div>
                <div class="font-medium">
                    {{ userInfo.userName }}
                </div>

                <div class="text-xs text-green-400">
                    Online
                </div>
            </div>
        </div>
    </aside>
</template>

<script setup>

const props = defineProps({
    userInfo : Object,
    mode     : String
});

const emit = defineEmits([
    "click-chat-room",
    "click-logo"
]);

function clickChatRoom() {
    emit("click-chat-room");
};

function clickLogo() {
    emit("click-logo");
};

function clickMyInfoMenu() {
    alert("준비중입니다.")
};

</script>
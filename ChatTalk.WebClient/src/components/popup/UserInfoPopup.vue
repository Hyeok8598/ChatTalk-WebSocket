<template>
    <div
    class="fixed inset-0 bg-black/50 flex items-center justify-center z-50"
    @click="closeUserInfo"
>
    <div
        class="w-96 rounded-lg bg-slate-800 p-6 shadow-lg"
        @click.stop
    >
        <div class="flex justify-between items-center mb-4">
            <h2 class="text-xl font-bold text-white">
                사용자 정보
            </h2>

            <button
                @click="closeUserInfo"
                class="text-gray-400 hover:text-white"
            >
                ✖
            </button>
        </div>

        <div class="space-y-3 text-sm">
            <div class="space-y-1 hidden">
                <p class="text-gray-400">
                    유저 ID
                </p>

                <div
                    class="
                        w-full
                        rounded-lg
                        border
                        border-slate-600
                        bg-slate-700
                        px-3
                        py-2
                        text-white
                        font-bold
                    "
                >
                    {{ props.userId }}
                </div>
            </div>

            <div class="space-y-1">
                <p class="text-gray-400">
                    닉네임
                </p>

                <div
                    class="
                        w-full
                        rounded-lg
                        border
                        border-slate-600
                        bg-slate-700
                        px-3
                        py-2
                        text-white
                        font-bold
                    "
                >
                    {{ props.userName }}
                </div>
            </div>
        </div>

        <div class="flex gap-2 pt-4">
            <button
                @click="closeUserInfo"
                class="
                    flex-1
                    rounded-lg
                    bg-slate-700
                    px-4
                    py-2
                    font-semibold
                    text-white
                    hover:bg-slate-600
                    transition
                "
            >
                닫기
            </button>
        </div>
    </div>
</div>
</template>

<script setup>
import { post } from '../../util/common';
import { onMounted, ref } from "vue";

const emit = defineEmits([
    'closeUserInfo'
]);

onMounted(() => {
    search();
});

const { props } = defineProps({
    props : Object
});

async function search() {
    var data = {
        userId : props.userId
    };

    var result = await post("search", data);
};

function closeUserInfo() {
    emit("closeUserInfo");
};

</script>
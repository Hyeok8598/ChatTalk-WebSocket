<template>
    <div class="flex h-screen bg-slate-950 text-white">

        <section class="hidden w-1/2 flex-col justify-between bg-slate-900 p-12 lg:flex">

            <div>
                <h1 class="text-4xl font-bold text-violet-400">
                    ChatTalk
                </h1>

                <p class="mt-4 text-gray-400">
                    실시간 채팅 서비스
                </p>
            </div>

            <div>
                <h2 class="text-3xl font-bold">
                    ChatTalk 회원가입
                </h2>

                <p class="mt-4 text-gray-400">
                    계정을 생성하고 채팅에 참여하세요.
                </p>
            </div>

        </section>

        <section class="flex flex-1 items-center justify-center px-6">

            <div class="w-full max-w-md rounded-2xl bg-slate-900 p-8 shadow-2xl">

                <div class="mb-8 text-center">

                    <h2 class="text-3xl font-bold">
                        회원가입
                    </h2>

                    <p class="mt-2 text-sm text-gray-400">
                        ChatTalk 계정을 생성합니다.
                    </p>

                </div>

                <div class="space-y-5">

                    <div>
                        <label class="mb-2 block text-sm text-gray-400">
                            아이디
                        </label>

                        <input
                            v-model="userId"
                            type="text"
                            class="h-12 w-full rounded-xl bg-slate-800 px-4 outline-none"
                            placeholder="아이디 입력"
                        />
                    </div>

                    <div>
                        <label class="mb-2 block text-sm text-gray-400">
                            이름
                        </label>

                        <input
                            v-model="userName"
                            type="text"
                            class="h-12 w-full rounded-xl bg-slate-800 px-4 outline-none"
                            placeholder="이름 입력"
                        />
                    </div>

                    <div>
                        <label class="mb-2 block text-sm text-gray-400">
                            비밀번호
                        </label>

                        <input
                            v-model="password"
                            type="password"
                            class="h-12 w-full rounded-xl bg-slate-800 px-4 outline-none"
                            placeholder="비밀번호 입력"
                        />
                    </div>

                    <button
                        @click="signup"
                        class="h-12 w-full rounded-xl bg-violet-500 font-bold hover:bg-violet-600"
                    >
                        회원가입
                    </button>

                </div>

                <div class="mt-6 text-center text-sm text-gray-400">

                    이미 계정이 있으신가요?

                    <button
                        @click="router.replace('/login')"
                        class="ml-2 font-bold text-violet-400"
                    >
                        로그인
                    </button>

                </div>

            </div>

        </section>

    </div>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { getUserInfo, post } from "../util/common.js";

const router = useRouter();

const userId = ref("");
const userName = ref("");
const password = ref("");

async function signup() {

    const request = {
        userId: userId.value,
        userName: userName.value,
        password: password.value
    };

    try {

        await post("signup", request);

        alert("회원가입 완료");

        router.replace("/login");

    } catch(error) {

        alert("회원가입 실패");

        console.error(error);
    }
}
</script>
<template>
    <div class="flex h-screen bg-slate-950 text-white">

        <section class="hidden w-1/2 flex-col justify-between bg-slate-900 p-12 lg:flex">
            <div>
                <h1 class="text-4xl font-bold text-violet-400">
                    ChatTalk
                </h1>

                <p class="mt-4 text-gray-400">
                    실시간 채팅과 귓속말을 지원하는 WebSocket 기반 채팅 서비스
                </p>
            </div>

            <div>
                <h2 class="text-3xl font-bold leading-tight">
                    Discord 감성의<br />
                    실시간 채팅 클라이언트
                </h2>

                <p class="mt-4 max-w-md text-gray-400">
                    로그인 후 전체 채팅방에 접속하고, 현재 접속자 목록과 메시지를 실시간으로 확인할 수 있습니다.
                </p>
            </div>

            <div class="text-sm text-gray-500">
                ChatTalk WebClient
            </div>
        </section>

        <section class="flex flex-1 items-center justify-center px-6">
            <div class="w-full max-w-md rounded-2xl bg-slate-900 p-8 shadow-2xl">

                <div class="mb-8 text-center">
                    <h2 class="text-3xl font-bold">
                        로그인
                    </h2>

                    <p class="mt-2 text-sm text-gray-400">
                        ChatTalk 계정으로 접속하세요.
                    </p>
                </div>

                <form class="space-y-5">
                    <div>
                        <label class="mb-2 block text-sm text-gray-400">
                            아이디
                        </label>

                        <input
                            v-model="userId"
                            type="text"
                            placeholder="아이디를 입력하세요"
                            class="h-12 w-full rounded-xl bg-slate-800 px-4 text-white outline-none placeholder:text-gray-500 focus:ring-2 focus:ring-violet-500"
                        />
                    </div>

                    <div>
                        <label class="mb-2 block text-sm text-gray-400">
                            비밀번호
                        </label>

                        <input
                            v-model="password"
                            type="password"
                            placeholder="비밀번호를 입력하세요"
                            class="h-12 w-full rounded-xl bg-slate-800 px-4 text-white outline-none placeholder:text-gray-500 focus:ring-2 focus:ring-violet-500"
                        />
                    </div>

                    <button
                        @click="login"
                        type="button"
                        class="h-12 w-full rounded-xl bg-violet-500 font-bold text-white hover:bg-violet-600"
                    >
                        로그인
                    </button>
                </form>

                <div class="mt-6 text-center text-sm text-gray-400">
                    계정이 없으신가요?
                    <button
                        @click="signUp"
                        class="font-bold text-violet-400 hover:text-violet-300">
                        회원가입
                    </button>
                </div>

            </div>
        </section>
    </div>
</template>

<script setup>
import { ref } from "vue"
import { post, setUserInfo } from "../util/common.js"
import { useRouter } from "vue-router";

const userId = ref("");
const password = ref("");
const router = useRouter();

async function login() {
    var request = {
        userId   : userId.value,
        password : password.value
    };
    
    try {
        var response = await post("login", request);
        router.push("/chat");
        setUserInfo(response);
    } catch {
        alert("[ERRROR] 서버가 실행중이 아닙니다.");
        return;
    }
};

function signUp() {
    router.push("/signUp");
}
</script>
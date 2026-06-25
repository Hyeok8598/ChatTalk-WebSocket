<template>
    <div class="w-96 bg-slate-800 rounded-lg p-6 shadow-lg">
        <!-- 내 정보 페이지 -->
        
        <div class="flex justify-between items-center mb-4">
            <button @click="$emit('close')">←</button>
            <h2 class="text-xl font-bold">내 정보</h2>
            <button @click="$emit('close')">✖</button>
        </div>

        <div class="space-y-3 text-sm">
            <div>
                <p class="text-gray-400">아이디</p>
                <p class="text-white font-bold">{{ userInfo.userId }}</p>
            </div>

            <div class="">
                <p class="text-gray-400">닉네임</p>
                <input
                    v-model="userName"
                    type="text"
                    class="mt-2 w-full rounded bg-slate-500 px-3 py-2 text-white"
                />
            </div>

            <button
                v-if="!isPasswordChange"
                @click="changePassword"
                class="
                    w-full
                    mt-1
                    rounded-lg
                    bg-slate-700
                    px-4
                    py-2
                    font-semibold
                    text-white
                    hover:bg-slate-600
                    transition
                "
            >비밀번호 변경</button>
            <div v-if="isPasswordChange" class="space-y-2">
                <p class="text-gray-400">이전 비밀번호</p>
                <input
                    v-model="beforePassword"
                    type="password"
                    class="w-full rounded bg-slate-500 px-3 py-2 text-white"
                />

                <p class="text-gray-400">변경할 비밀번호</p>
                <input
                    v-model="newPassword"
                    type="password"
                    class="w-full rounded bg-slate-500 px-3 py-2 text-white"
                />

                <p class="text-gray-400">비밀번호 확인</p>
                <input
                    v-model="confirmPassword"
                    type="password"
                    class="w-full rounded bg-slate-500 px-3 py-2 text-white"
                />
            </div>
            <div class="flex gap-2 pt-1">
                <button 
                    @click="change"
                    class=" flex-1
                    rounded-lg
                    bg-blue-600
                    px-4
                    py-2
                    font-semibold
                    text-white
                    hover:bg-blue-500
                    transition">변경</button>

                <button v-if="isPasswordChange" 
                        @click="cancel"
                    class="flex-1
                    rounded-lg
                    bg-slate-700
                    px-4
                    py-2
                    font-semibold
                    text-white
                    hover:bg-slate-600
                    transition">취소</button>
            </div>

            <!-- <div>
                <p class="text-gray-400">패스워드</p>
                <p class="text-white font-bold">{{ userInfo.password }}</p>
            </div> -->
        </div>
    </div>
    
</template>

<script setup>
import { ref } from "vue";
import { getUserInfo, post, setUserInfo } from "../../common/util/commonUtil";

const userInfo = getUserInfo();
const userName = ref(userInfo.userName);
const isPasswordChange = ref(false);
const beforePassword = ref('');
const newPassword = ref('');
const confirmPassword = ref('');

function changePassword() {
    isPasswordChange.value = !isPasswordChange.value;
};

function cancel() {
    // beforePassword.value = "";
    // isPasswordChange.value = "";
    // confirmPassword.value = "";
    changePassword();
};

async function change() {
    if(newPassword.value !== confirmPassword.value) {
        alert("비밀 번호가 일치하지 않습니다.");
        return;
    }

    var data = {
        userId         : userInfo.userId,
        userName       : userName.value,
        beforePassword : beforePassword.value,
        afterPassword  : newPassword.value
    };

    var result = await post("change", data);
    if(result) alert("저장하였습니다."); setUserInfo()
};
</script>
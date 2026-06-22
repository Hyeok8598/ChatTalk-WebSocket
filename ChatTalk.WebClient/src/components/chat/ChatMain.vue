<template>
    <section class="flex-1 bg-slate-900 flex items-center justify-center px-6 select-none">
        <div >
            <div class="max-w-xl text-center">
                <div class="text-5xl mb-6">💬</div>
    
                <h2 class="text-2xl font-bold mb-3">
                    ChatTalk에 오신 것을 환영합니다
                </h2>
    
                <p class="text-gray-400 mb-6">
                    왼쪽에서 채팅방을 선택하거나, 전체 채팅방에서 대화를 시작해보세요.
                </p>
    
                <div class="grid grid-cols-3 gap-3 text-sm">
                    <div class="rounded-xl bg-[#10182B] border border-[#1C2742] p-4">
                        <div class="text-[#B388FF] font-bold mb-1">전체 채팅</div>
                        <div class="text-gray-400">모든 사용자와 대화</div>
                    </div>
    
                    <div class="rounded-xl bg-[#10182B] border border-[#1C2742] p-4">
                        <div class="text-[#B388FF] font-bold mb-1">귓속말</div>
                        <div class="text-gray-400">특정 사용자에게 전송</div>
                    </div>
    
                    <div class="rounded-xl bg-[#10182B] border border-[#1C2742] p-4">
                        <div class="text-[#B388FF] font-bold mb-1">멘션</div>
                        <div class="text-gray-400">@로 사용자 호출</div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script setup>
</script>
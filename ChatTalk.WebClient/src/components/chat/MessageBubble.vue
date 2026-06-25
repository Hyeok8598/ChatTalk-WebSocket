<template>
    <div
        class="max-w-md rounded-xl px-4 py-3"
        :class="[
            msg.direction == MESSAGE_DIRECTION.SENT ? 'bg-violet-500' : 'bg-slate-700',

            msg.isWhisper ? 'bg-slate-800 border border-violet-400' : ''
        ]"
    >
        <div 
            v-if="msg.isWhisper"
            class="mb-1 text-xs text-violet-300"
        >
            🔒 귓속말
            {{ 
                msg.direction == MESSAGE_DIRECTION.SENT ?
                "[to. " + msg.targetUserName + "]" : "[from. " + msg.senderUserName + "]"
            }}
        </div>

        <div
            v-if="file"
            
        >

        </div>

        {{ msg.content }}
    </div>
</template>
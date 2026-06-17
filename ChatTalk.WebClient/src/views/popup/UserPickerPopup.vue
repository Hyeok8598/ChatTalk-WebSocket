<template>
    <div 
        class="absolute w-72 bg-white rounded-xl shadow-xl border border-gray-200 overflow-hidden z-50"
        :style="{
            left: props.left + 'px',
            top: props.top + 'px'
        }"
    >
        <div
            v-for="user in props.users"
            @click="selectUser(user)"
            class="flex items-center gap-3 px-4 py-3 cursor-pointer hover:bg-gray-100 transition"
        >
            <!-- <img
                :src="user.profileImage" 
                alt="profile"
                class="w-10 h-10 rounded-full object-cover"
            /> -->

            <div>
                <div class="font-medium text-gray-900">
                    {{ user.userName }}
                </div>

                <div class="text-xs text-gray-500">
                    {{ user.userId }}
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
/**
 * UserPopup Props
 *
 * @typedef {Object} Props
 * @property {Array<string>} users
 * @property {number} left
 * @property {number} top
 */
const emit = defineEmits(['select']);

const { props } = defineProps({
    props : Object
});

function selectUser(user) {
    emit("select", user);
};
</script>
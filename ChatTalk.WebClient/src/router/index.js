import { createRouter, createWebHistory } from "vue-router";

import LoginView from "../views/auth/LoginView.vue";
// import ChatView from "../views/chat/ChatView.vue";
import signUpView from "../views/auth/SignUpView.vue";
// import MainView from "../views/MainView.vue";
import ChatView from "../views/chat/ChatView.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            redirect: "/login"
        },
        {
            path: "/login",
            component: LoginView
        },
        // {
        //     path: "/main",
        //     component: MainView
        // },
        {
            path: "/chat",
            component: ChatView
        },
        {
            path: "/signUp",
            component: signUpView
        },
    ]
});

export default router;
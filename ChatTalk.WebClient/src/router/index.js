import { createRouter, createWebHistory } from "vue-router";

import LoginView from "../views/LoginView.vue";
import ChatView from "../views/ChatView.vue";
import signUpView from "../views/SignUpView.vue";

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
        {
            path: "/chat",
            component: ChatView
        },
        {
            path: "/signUp",
            component: signUpView
        }
    ]
});

export default router;
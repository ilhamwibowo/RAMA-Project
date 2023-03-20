import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: () => import("../views/HomeView.vue")
        },
        {
            path: "/login",
            name: "login",
            component: () => import("../views/LoginView.vue")
        },
        {
            path: "/register",
            name: "register",
            component: () => import("../views/RegisterView.vue")
        },
        {
            path: "/logout",
            name: "logout",
            component: () => import("../views/LogoutView.vue"),
            beforeEnter: () => {
                if (localStorage.getItem("token") === null) {
                    return false;
                }
                localStorage.removeItem("token");
            }
        },
        {
            path: "/profile",
            name: "profile",
            component: () => import("../views/ProfileView.vue")
        },
        {
            path: "/profile/edit",
            name: "edit-profile",
            component: () => import("../views/EditProfileView.vue")
        },
        {
            path: "/forgot",
            name: "forgot",
            component: () => import("../views/ForgotView.vue")
        },
        {
            path: "/album",
            name: "album",
            component: () => import("../views/AlbumView.vue")
        }
    ]
});

router.beforeEach(async (to) => {
    if (
        localStorage.getItem("token") === null &&
        to.name !== "home" &&
        to.name !== "login" &&
        to.name !== "register" &&
        to.name !== "forgot"
    ) {
        return { name: "login" };
    }
});

export default router;

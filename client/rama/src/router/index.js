import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            redirect: "/login"
            // component: HomeView,
        },
        {
            path: "/about",
            name: "about",
            // route level code-splitting
            // this generates a separate chunk (About.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () => import("../views/AboutView.vue")
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
        to.name !== "login" &&
        to.name !== "register" &&
        to.name !== "forgot"
    ) {
        return { name: "login" };
    }
});

export default router;

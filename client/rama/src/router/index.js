import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: () => import("../views/user/HomeView.vue")
        },
        {
            path: "/login",
            name: "login",
            component: () => import("../views/user/LoginView.vue")
        },
        {
            path: "/register",
            name: "register",
            component: () => import("../views/user/RegisterView.vue")
        },
        {
            path: "/logout",
            name: "logout",
            component: () => import("../views/user/LogoutView.vue"),
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
            component: () => import("../views/user/ProfileView.vue")
        },
        {
            path: "/profile/edit",
            name: "edit-profile",
            component: () => import("../views/user/EditProfileView.vue")
        },
        {
            path: "/forgot",
            name: "forgot",
            component: () => import("../views/user/ForgotView.vue")
        },
        {
            path: "/album",
            name: "album",
            component: () => import("../views/user/AlbumView.vue")
        },
        {
            path: "/event",
            name: "event",
            component: () => import("../views/admin/AdminEventView.vue")
        },
        {
            path: "/event/:id",
            name: "detailEvent",
            component: () => import("../views/admin/AdminEventDetailView.vue")
        },
        {
            path: "/albumAdmin",
            name: "albumAdmin",
            component: () => import("../views/admin/AdminAlbumView.vue")
        },
        {
            path: "/albumDetail/:id",
            name: "albumDetail",
            component: () => import("../views/admin/AdminAlbumDetailView.vue")
        },
        {
            path: "/listPhoto/:id",
            name: "listPhoto",
            component: () => import("../views/admin/AdminAlbumPhotoView.vue")
        },
        {
            path: "/events",
            name: "userEvent",
            component: () => import("../views/user/UserEventView.vue")
        },
        {
            path: "/events/:id",
            name: "userEventDetail",
            component: () => import("../views/user/UserEventDetail.vue")
        },
        { 
            path: "/registrant/:id",
            name: "EventRegistrant",
            component: () => import("../views/admin/AdminEventRegistransView.vue")
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

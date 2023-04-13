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
        },
        {
            path: "/event",
            name: "Event",
            component: () => import("../views/EventView.vue")
        },
        {
            path: "/event/:id",
            name: "detailEvent",
            component: () => import("../views/DetailEventView.vue")
        },
        {
            path: "/albumAdmin",
            name: "albumAdmin",
            component: () => import("../views/albumAdminView.vue")
        },
        {
            path: "/albumDetail/:id",
            name: "albumDetail",
            component: () => import("../views/AlbumDetailView.vue")
        },
        {
            path: "/listPhoto/:id",
            name: "listPhoto",
            component: () => import("../views/AlbumPhotoView.vue")
        },
        {
            path: "/events",
            name: "userEvent",
            component: () => import("../views/UserEventView.vue")
        },
        {
            path: "/events/:id",
            name: "userEventDetail",
            component: () => import("../views/UserEventDetail.vue")
        },
        { 
            path: "/registrant/:id",
            name: "EventRegistrant",
            component: () => import("../views/EventRegistransView.vue")
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

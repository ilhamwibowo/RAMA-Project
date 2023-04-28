import { createRouter, createWebHistory } from "vue-router";
import axios from "axios";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        // User
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
            path: "/album/:id",
            name: "album",
            component: () => import("../views/user/AlbumView.vue")
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
            path: "/albums",
            name: "albums",
            component: () => import("../views/user/UserAlbumView.vue")
        },

        // Admin
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
            path: "/admin/album",
            name: "albumAdmin",
            component: () => import("../views/admin/AdminAlbumView.vue")
        },
        {
            path: "/admin/album/detail/:id",
            name: "albumDetail",
            component: () => import("../views/admin/AdminAlbumDetailView.vue")
        },
        {
            path: "/admin/album/photos/:id",
            name: "listPhoto",
            component: () => import("../views/admin/AdminAlbumPhotoView.vue")
        },
        { 
            path: "/registrant/:id",
            name: "EventRegistrant",
            component: () => import("../views/admin/AdminEventRegistransView.vue")
        }
    ]
});

router.beforeEach(async (to) => {
    const publicPages = ["home", "login", "register", "forgot", "albums", "album"];
    const authRequired = !publicPages.includes(to.name);
    const loggedIn = localStorage.getItem("token") !== null;

    if (authRequired && !loggedIn) {
        return { name: "login" };
    }

    // Restrict access to admin routes based on a user's role
    const adminPages = [
        "event",
        "detailEvent",
        "albumAdmin",
        "albumDetail",
        "listPhoto",
        "EventRegistrant"
    ];

    console.log("laksdj");
    if (loggedIn) {
        const isAdminRoute = adminPages.includes(to.name);
        // const userRole = localStorage.getItem("userRole");
        const userRole = await getRole();
        console.log(userRole);
        if (isAdminRoute && userRole !== "Admin") {
            return { name: "home" };
        }
    }
});

// Helper function
// As this is not very good, might want to change it to store role in local storage
async function getRole() {
    const token = localStorage.getItem("token");

    // Configuration for API
    const config = {
        headers: { Authorization: `Bearer ${token}` }
    };

    try {
        // Axios Get
        const response = await axios.get(import.meta.env.VITE_API_URI + "/User", config);
        if (response.status !== 200) {
            return "User";
        } else {
            return response.data.role;
        }
    } catch (error) {
        console.log(error);
    }
}


export default router;

<template>
    <header>
        <div class="navpage">
            <NavigationPage />
        </div>
        <div class="title">
            <RouterLink class="text" to="/">Rama</RouterLink>
        </div>
        <div class="profile">
            <div v-if="isLogin">
                <RouterLink to="/profile"><img src="/navbar-profile.png" ></RouterLink>
            </div>
            <div v-else>
                <RouterLink to="/login">Login</RouterLink>
            </div>
        </div>
    </header>
</template>

<script>
import { mapState } from "pinia";

import { useProfileStore } from "@/stores/profile";

import NavigationPage from "./NavigationPage.vue";

export default {
    name: "NavigationBar",

    data() {
        return {
            home: import.meta.env.VITE_APP_URI,
            isLogin: false
        };
    },

    components: {
        NavigationPage
    },

    computed: {
        ...mapState(useProfileStore, {
            username: "name"
        })
    },

    methods: {
        checkLogin() {
            this.isLogin = localStorage.getItem("token") !== null;
            console.log(this.isLogin);
        }
    },
    created() {
        this.checkLogin()
    }
};
</script>

<style scoped>
header {
    width: 100%;
    height: 100px;
    display: grid;
    margin: 100px 0;
    grid-template-columns: 200px minmax(auto, 900px) 200px;
    grid-template-rows: 100px;
    grid-template-areas: 
    "navpage title profile";
    justify-content: center;
}

.navpage {
    grid-area: navpage;
}
.title {
    grid-area: title;
    display: flex;
    justify-content: center;
    align-items: center;
}
.title .text {
    text-decoration: none;
    color: black;
    font-family: "Bebas Neue";
    font-size: 48;
}

.profile {
    grid-area: profile;
    display: flex;
    justify-content: center;
    align-items: center;
}

.profile img {
    width: 80px;
}
</style>

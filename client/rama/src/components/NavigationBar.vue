<template>
    <header>
        <!-- <div class="app-title subtitle">
            <a :href="home">Rama</a>
        </div>
        <nav class="subtitle navbar">
            <RouterLink to="/">Home</RouterLink>
            <RouterLink to="/album">Album</RouterLink>
        </nav>
        <nav class="subtitle user-manager" v-if="checkLogin">
            <RouterLink to="/login">Login</RouterLink>
            <RouterLink to="/register">Register</RouterLink>
        </nav>
        <nav class="subtitle profile" v-if="!checkLogin">
            <RouterLink to="/profile">Hello, {{ this.username }}!</RouterLink>
        </nav> -->
        <div class="navpage">
            <NavigationPage />
        </div>
        <div class="title">
            <h>Rama</h>
        </div>
        <div class="profile">

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
            home: import.meta.env.VITE_APP_URI
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
            return localStorage.getItem("token") === null;
        }
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
    background-color:yellow;
    display: flex;
    justify-content: center;
    align-items: center;
    font-family: "Bebas Neue";
    font-size: 48;
}
.profile {
    grid-area: profile;
    background-color:wheat;
}
</style>

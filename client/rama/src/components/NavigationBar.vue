<template>
    <header>
        <div class="app-title subtitle">
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
        </nav>
    </header>
</template>

<script>
import { mapState } from "pinia";

import { useProfileStore } from "@/stores/profile";

export default {
    name: "NavigationBar",

    data() {
        return {
            home: import.meta.env.VITE_APP_URI
        };
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
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    padding: 20px 10px;
    justify-content: space-between;
}

.app-title {
    display: flex;
    flex: 1;
    justify-content: flex-start;
}

.app-title a {
    color: var(--color-text);
    text-decoration: none;
}

.navbar {
    display: flex;
    flex: 1;
    justify-content: center;
}

.navbar a {
    color: var(--color-text);
    padding: 0 1rem;
    border-left: 1px solid var(--color-border);
    text-decoration: none;
}

.user-manager {
    display: flex;
    flex: 1;
    justify-content: flex-end;
}

.user-manager a {
    color: var(--color-text);
    padding: 0 1rem;
    border-left: 1px solid var(--color-border);
    text-decoration: none;
}

.navbar a:first-of-type,
.user-manager a:first-of-type {
    padding-left: 0;
    border: 0;
}

.navbar a:last-of-type,
.user-manager a:last-of-type {
    padding-right: 0;
}
</style>

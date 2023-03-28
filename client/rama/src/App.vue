<script setup>
import { RouterView } from "vue-router";
</script>

<template>
    <div class="app">
        <NavigationBar v-if="isUser" class="navbar"/>
        <RouterView class="content"/>
    </div>
</template>

<script>
import axios from "axios";
import NavigationBar from "./components/NavigationBar.vue";

export default {
    name: "App",
    data() {
        return{
         isUser: true,
        }
    },
    components: {
        NavigationBar
    },
    async created() {
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        // Axios Get
        await axios
            .get(import.meta.env.VITE_API_URI + "/User", config)
            .then((response) => {
                if (response.status !== 200) {
                    console.log(response);
                } else {
                    // if user show navbar
                    this.isUser = response.data.role == "User" | !response.data.role;
                    console.log(this.isUser);
                }
            })
            .catch((err) => {
                console.log(err);
            });
    }
};
</script>

<style scoped>

.app {
    display: flex;
    flex-direction: column;
    height: 100vh;
    width: 100vw;
    grid-template-rows: 75px auto;
    grid-template-areas:
        "nav"
        "content";
}
.navbar {
    grid-area: nav;
}

.content {
    grid-area: content;
}

</style>

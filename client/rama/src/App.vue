<script setup>
import { RouterView } from "vue-router";
</script>

<template>
    <div class="app">
        <RouterView class="content"/>
    </div>
</template>

<script>
import axios from "axios";

export default {
    name: "App",
    data() {
        return{
         isUser: false,
        }
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
                }
            })
            .catch((err) => {
                console.log(err);
            });
    }
};
</script>

<style scoped>

</style>

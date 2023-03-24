<script setup>
import { RouterView } from "vue-router";
</script>

<template>
    <div class="app">
        <NavigationBar class="navbar"/>
        <RouterView class="content"/>
    </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar.vue";

export default {
    name: "App",

    components: {
        NavigationBar
    }
};
</script>

<style scoped>

.app {
    display: grid;
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

<script setup>
import { RouterView } from "vue-router";
</script>

<template>
    <NavigationBar />
    <RouterView />
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
NavigationBar {
    grid-area: nav;
}

RouterView {
    grid-area: content;
}
</style>

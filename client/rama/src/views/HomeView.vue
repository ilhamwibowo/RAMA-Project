<template>
    <main class="layout">
        <NavigationBar class="navbar" />
        <div class="body">
            <Home />
            <Leaderboard />
        </div>
        <Footer class="footer" />
    </main>
</template>

<script>
import NavigationBar from "../components/NavigationBar.vue";
import Footer from "../components/Footer.vue";
import Leaderboard from "@/components/Leaderboard.vue";
import Home from "../components/Home.vue"

export default {
    name: "HomeView",

    components: {
        Leaderboard,
        NavigationBar,
        Footer,
        Home
    },
};
</script>

<style scoped>
.layout {
    display: grid;
    grid-template-rows: auto auto 240px;
    grid-template-areas:
        "header"
        "body"
        "footer";
    min-height: 100vh;
}

.header {
    grid-area: header;
}

.body {
    grid-area: body;
}

.footer {
    grid-area: footer;
}
</style>
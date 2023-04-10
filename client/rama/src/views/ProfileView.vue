<template>
    <div class="layout">
        <NavigationBar class="header" />
        <Profile class="body" />
        <Footer class="footer" />
    </div>
</template>

<script>
import NavigationBar from "../components/NavigationBar.vue";
import Profile from "../components/Profile.vue";
import Footer from "../components/Footer.vue";

export default {
    name: "ProfileView",
    components: {
        Profile,
        NavigationBar,
        Footer
    }
};
</script>

<style>
@media (min-width: 1024px) {
    .about {
        min-height: 100vh;
        display: flex;
        align-items: center;
    }
}

.layout {
    display: grid;
    grid-template-rows: 50px auto 240px;
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

<template>
    <div class="layout">
        <NavigationBar class="header" />
        <EditProfile class="body" />
        <Footer class="footer" />
    </div>
</template>

<script>
import EditProfile from "../components/EditProfile.vue";
import Footer from '../components/Footer.vue';
import NavigationBar from '../components/NavigationBar.vue';
export default {
    name: "EditProfileView",
    components: {
        EditProfile,
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

.view {
    display: grid;
    grid-template-rows: 50px auto 240px;
    grid-template-areas:
        "header"
        "body"
        "footer";
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

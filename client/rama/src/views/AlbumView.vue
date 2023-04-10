<template>
    <div class="layout">
        <NavigationBar class="navbar" />
        <Album class="body" />
        <Footer class="footer" />
    </div>
</template>

<script>
import Album from "../components/Album.vue";
import Footer from '../components/Footer.vue';
import NavigationBar from "../components/NavigationBar.vue";
export default {
    name: "AlbumView",
    components: {
        Album,
        Footer,
        NavigationBar
    }
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
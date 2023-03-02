<template>
    <div class="view">
        <Header class="header" />
        <EditProfile class="body" />
        <div class="footer"></div>
    </div>
</template>

<script>
import EditProfile from "../components/EditProfile.vue";
import Header from "../components/Header.vue";
export default {
    name: "EditProfileView",
    components: {
        EditProfile,
        Header
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
    grid-template-rows: 50px auto 100px;
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

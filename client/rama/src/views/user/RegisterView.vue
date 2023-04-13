<template>
    <div class="layout">
        <NavigationBar class="navbar"/>
        <div class="register-image">
            <img src="registerImage.png" alt="registerImage" />
        </div>
        <Register class="register-form" />
    </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar.vue";
import Register from '@/components/Register.vue';

export default {
    name: "RegisterView",
    components: {
        NavigationBar,
        Register
    },
};
</script>

<style scoped>
.layout {
    height: 100%;
    width: 100%;
    display: grid;
    grid-template-rows: 220px auto;
    grid-template-columns: 1fr 1fr;
    grid-template-areas:
    "navbar navbar"
    "image form";
    align-items: center;
    justify-content: center;
}
.navbar {
    grid-area: navbar;
}

.register-image {
    grid-area: image;
    text-align: center;
}

.register-image > img {
    max-width: 65%;
    margin: 20px 30px 20px 30px;
}

.register-form {
    grid-area: form;
}
</style>

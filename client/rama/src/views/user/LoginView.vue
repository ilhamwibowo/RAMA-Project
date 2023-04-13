<template>
    <div class="layout">
        <NavigationBar class="navbar"/>
        <div class="login-image">
            <img src="loginImage.png" alt="loginImage" />
        </div>
        <Login class="login-form"/>
    </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar.vue";
import Login from "@/components/Login.vue";

export default {
    name: "LoginView",
    data() {
        return {
            email: "",
            password: "",
            invalidEmail: "",
            invalidPassword: "",
            response: "",
            showPw: false
        };
    },
    components: {
        NavigationBar,
        Login
    }
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

.login-image {
    grid-area: image;
    text-align: center;
}

.login-image > img {
    max-width: 80%;
    margin: 20px 30px 20px 30px;
}

.login-form {
    grid-area: form;
}

</style>

<template>
    <div class="layout">
        <NavigationBar class="navbar"/>
        <div class="recovery-image">
            <img src="/forgotPasswordImage.png" alt="loginImage" />
        </div>
        <div class="form-container">
            <div class="title">
                <h2>FORGOT PASSWORD</h2>
            </div>
            <div class="subtext">
                <p>
                    Please enter the email address you'd like your password reset information sent
                    to
                </p>
            </div>
            <div class="recovery">
                <div class="form" id="recovery-email">
                    <label>Email</label>
                    <input class="input" type="text" name="email" v-model="email" />
                </div>
                <div class="alert" id="invalid-email" v-if="invalidEmail">
                    <img src="/alert.png">
                    <p>{{ invalidEmail }}</p>
                </div>
                <div class="alert" id="request-response" v-if="response">
                    <img src="/alert.png">
                    <p>{{ response }}</p>
                </div>
                <div id="recover-button">
                    <button type="button" id="btn-recover" v-on:click="request()">SEND</button>
                </div>
            </div>
            <div id="back-link">
                <hr />
                <p>Back to <RouterLink to="/login">Sign In</RouterLink></p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
import NavigationBar from "./NavigationBar.vue";

export default {
    name: "AccountRecovery",

    emits: ["email", "isRequested"],

    data() {
        return {
            email: "",
            invalidEmail: "",
            response: ""
        };
    },

    components: {
        NavigationBar
    },

    methods: {
        request() {
            let reEmail =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!this.email.toLowerCase().match(reEmail)) {
                this.invalidEmail = "Invalid e-mail!";
            } else {
                this.invalidEmail = "";
                this.response = "";
                axios
                    .put(
                        import.meta.env.VITE_API_URI +
                            "/Account/forgotpassword/requestotp?Email=" +
                            this.email
                    )
                    .then(() => {
                        this.response = "";
                        this.$emit("email", this.email);
                        this.$emit("isRequested", true);
                    })
                    .catch(() => {
                        this.response = "Request failed! Please try again later.";
                    });
            }
        }
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

.recovery-image {
    grid-area: image;
    text-align: center;
}

.recovery-image > img {
    max-width: 65%;
    margin: 20px 30px 20px 30px;
}

.form-container {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.title {
    font-size: 20px;
    font-family: 'Bebas Neue';
}

.subtext {
    width: 250px;
    font-family: "Darker Grotesque";
    font-size: 18px;
    text-align: center;
    margin-bottom: 50px;
}

.form {
    margin-bottom: 10px;
}

.form label {
    font-family: "Darker Grotesque";
    font-size: 18px;
    margin-left: 40px;
}


.input {
    width: 350px;
    height: 60px;
    background: rgba(255, 255, 255, 0);
    border: 1px solid #353642;
    border-radius: 100px;
    padding: 10px 25px;
    margin: 5px 0px 5px 0px;
}

input[type="text"] {
    font-family: "Darker Grotesque";
    font-size: 125%;
}

#btn-recover {
    font-family: "Bebas Neue";
    color: white;
    font-size: 24px;
    width: 350px;
    height: 50px;
    border-radius: 100px;
    border-color: transparent;
    background: #353642;
    margin:10px 0px 25px 0px;
}

#btn-recover:hover {
    background: #000000;
    transition: 0.3s;
}

#recovery-email {
    text-align: left;
    width: 300px;
}

#back-link {
    font-family: "Darker Grotesque";
    font-size: 18px;
    width: 350px;
    text-align: center;
}

#back-link hr {
    border: 1px solid;
}

#back-link a {
    text-decoration: none;
    font-weight: 700;
}


.alert {
    color: #F24E1E;
    display: flex;
    align-items: center;
    column-gap: 10px;
}
.alert img {
    width: 24px;
    height: 24px;
    margin-left: 10px
}

</style>

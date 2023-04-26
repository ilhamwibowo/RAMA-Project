<template>
    <div class="layout">
        <NavigationBar class="navbar" />
        <div class="reset-image">
            <img src="/forgotPasswordImage.png" alt="loginImage" />
        </div>
        <div class="reset-form">
            <div class="title">
                <h2>Reset Password</h2>
            </div>
            <div class="subtext">
                <h4>An OTP code has been sent to your e-mail!</h4>
            </div>
            <div class="reset">
                <div id="new-password" class="form">
                    <label>New Password</label> <br>
                    <input
                        type="password"
                        class="input"
                        name="new-password"
                        v-model="newPassword"
                    />
                </div>
                <div id="confirm-password" class="form">
                    <label>Confirm Password</label> <br>
                    <input
                        type="password"
                        class="input"
                        name="confirm-password"
                        v-model="confirmPassword"
                    />
                </div>
                <div class="alert" id="invalid-password" v-if="invalidPassword">
                    <img src="/alert.png">
                    <p>{{ invalidPassword }}</p>
                </div>
    
                <div id="otp-code" class="form">
                    <label>OTP Code</label> <br>
                    <input type="text" class="input" name="otp-code" v-model="OTPCode" />
                </div>
    
                <div class="alert" id="request-response" v-if="response">
                    <img src="/alert.png">
                    <p>{{ response }}</p>
                </div>
                <div id="reset-button">
                    <button type="button" id="btn-reset" v-on:click="reset()">Reset Password</button>
                </div>
            </div>
            <div id="back-forget">
                <hr />
                <p>Back to <RouterLink to="/forgot">forget password</RouterLink></p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
import NavigationBar from './NavigationBar.vue';

export default {
    name: "ResetPassword",

    emits: ["isRecovered"],

    props: {
        email: String
    },

    data() {
        return {
            newPassword: "",
            confirmPassword: "",
            invalidPassword: "",
            OTPCode: "",
            response: ""
        };
    },

    components: {
        NavigationBar
    },

    methods: {
        reset() {
            if (this.newPassword !== this.confirmPassword || this.password === "") {
                if (this.newPassword !== this.confirmPassword) {
                    this.invalidPassword = "Password does not match!";
                }
                if (this.password === "") {
                    this.invalidPassword = "Please fill in your password!";
                }
            } else {
                this.invalidPassword = "";
                axios
                    .put(import.meta.env.VITE_API_URI + "/Account/forgotpassword/changepassword", {
                        email: this.email,
                        otp: this.OTPCode,
                        newPassword: this.newPassword
                    })
                    .then(() => {
                        this.response = "";
                        this.$emit("isRecovered", true);
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

.reset-image {
    grid-area: image;
    text-align: center;
}

.reset-image > img {
    max-width: 65%;
    margin: 20px 30px 20px 30px;
}
.reset-form {
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

input {
    font-family: "Darker Grotesque";
    font-size: 125%;
}

input[type="text"],
input[type="password"] {
    font-family: "Darker Grotesque";
    font-size: 125%;
}
.reset-image {
    flex: 1;
    min-width: 300px;
}

.reset-image img {
    max-width: 100%;
    height: auto;
}
.reset-form {
    flex: 1;
    min-width: 300px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

#btn-reset {
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

#btn-reset:hover {
    background: #000000;
    transition: 0.3s;
}

#back-forget {
    font-family: "Darker Grotesque";
    font-size: 18px;
    width: 350px;
    text-align: center;
}

#back-forget hr {
    border: 1px solid;
}

#back-forget a {
    text-decoration: none;
    font-weight: 700;
}

</style>

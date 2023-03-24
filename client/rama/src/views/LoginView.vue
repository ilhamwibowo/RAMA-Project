<template>
    <div class="layout">
        <div class="loginImange">
            <img src="loginImage.png" alt="loginImage" />
        </div>
        <div class="form-container">
            <div class="title">
                <h1>LOG IN</h1>
            </div>
            <div id="login">
                <div id="email">
                    <label>Email</label><br />
                    <input
                        type="text"
                        id="email-form"
                        name="email"
                        v-model="email"
                        placeholder="Email"
                    />
                </div>
                <div id="invalid-email" v-if="invalidEmail">
                    <p>{{ invalidEmail }}</p>
                </div>
                <div id="password">
                    <label>Password</label><br />
                    <div class="password-wrap">
                        <input
                            type="password"
                            id="password-form"
                            name="password"
                            v-model="password"
                            placeholder="Password"
                        />
                        <button type="button" id="show-pw" v-on:click="tooglePassword()">
                            <img src="../assets/eye-outline.svg" alt="open eye" v-if="!showPw" />
                            <img
                                src="../assets/eye-off-outline.svg"
                                alt="close eye"
                                v-if="showPw"
                            />
                        </button>
                    </div>
                </div>
                <div id="invalid-password" v-if="invalidPassword">
                    <p>{{ invalidPassword }}</p>
                </div>
                <div id="login-response" v-if="response">
                    <p>{{ response }}</p>
                </div>
                <div id="forget-password">
                    <a href="http://localhost:5173/forgot">Forget password</a>
                </div>
                <div id="login-button">
                    <button type="button" id="submit-login" v-on:click="login()">Login</button>
                </div>
            </div>
            <div id="register-container">
                <p>Don't have an account? <a href="http://localhost:5173/register">Sign Up</a></p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";

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

    methods: {
        login() {
            let reEmail =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!this.email.toLowerCase().match(reEmail) || this.password === "") {
                if (!this.email.toLowerCase().match(reEmail)) {
                    this.invalidEmail = "Invalid e-mail!";
                }
                if (this.password === "") {
                    this.invalidPassword = "Please fill in your password!";
                }
            } else {
                this.invalidEmail = "";
                this.invalidPassword = "";
                axios
                    .post(import.meta.env.VITE_API_URI + "/Account/login", {
                        email: this.email,
                        password: this.password
                    })
                    .then((response) => {
                        this.response = "";
                        localStorage.setItem("token", response.data.token);
                        this.$router.push("/profile");
                    })
                    .catch((error) => {
                        this.response = error.response.data;
                    });
            }
        },
        tooglePassword() {
            const passwordInput = document.getElementById("password-form");
            const showButton = document.getElementById("show-pw");

            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                this.showPw = true;
                // showButton.innerHTML = '<img src="../assets/eye-off-outline.svg" alt="closed eye" />';
            } else {
                passwordInput.type = "password";
                this.showPw = false;
                // showButton.innerHTML = '<img src="../assets/eye-outline.svg" alt="open eye" />';
            }
        }
    }
};
</script>

<style scoped>
.layout {
    height: 100%;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
}

img {
    width: 70%;
    height: auto;
    display: block;
}

form-container {
    width: 30%;
    height: auto;
    display: block;
}
.title {
}

#email {
    text-align: left;
}

#password {
    margin-top: 8%;
    text-align: left;
}

label {
    margin-left: 5%;
}

#email-form,
#password-form {
    margin-top: 1%;
    width: 100%;
    height: 2vw;
    font-size: 14px;
    background: rgb(255, 255, 255);
    border: 1px solid grey;
    border-radius: 15px;
    padding: 0 10px 0 10px;
}

input::placeholder {
    font-size: 0.8rem;
}

#forget-password {
}

#login-button {
    margin-top: 10%;
    margin-bottom: 10%;
}

#submit-login {
    color: white;
    width: 100%;
    background: #1f2220;
    height: 2.5vw;
    border-radius: 0.9rem;
    border-color: transparent;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 3px;
}

#submit-login:hover {
    background: #ffa801;
    transition: 0.3s;
}

#register-container {
    border-top: 2px solid #000;
}

.password-wrap {
    position: relative;
}

#show-pw {
    position: absolute;
    top: 55%;
    right: 0.5rem;
    transform: translateY(-50%);
    cursor: pointer;
    width: 14%;
    display: flex;
    align-items: center;
    background-color: #fff;
    outline: none;
    border: none;
}

#show-pw img {
    width: 100%;
    height: auto;
    left: 0;
}
</style>

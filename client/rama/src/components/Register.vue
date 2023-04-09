<template>
    <div class="register-container">
        <div class="title">
            <h1>SIGN UP</h1>
        </div>
        <div class="register">
            <div class="form" id="email">
                <label>Email</label><br />
                <input
                    type="text"
                    class="input"
                    id="email-form"
                    name="email"
                    v-model="email"
                    placeholder=""
                />
            </div>
            <div class="alert" id="invalid-email" v-if="invalidEmail">
                <img src="alert.png">
                <p>{{ invalidEmail }}</p>
            </div>
            <div class="form" id="password">
                <label>Password</label><br />
                <div class="password-wrap">
                    <input
                        type="password"
                        class="input password"
                        id="password-form"
                        name="password"
                        v-model="password"
                        placeholder=""
                    />
                    <button type="button" id="show-pw" v-on:click="tooglePassword()">
                        <img
                            src="unshow.png"
                            alt="open eye"
                            v-if="!showPw"
                        />
                        <img
                            src="show.png"
                            alt="close eye"
                            v-if="showPw"
                        />
                    </button>
                </div>
            </div>
            <div class="form" id="confirm-password">
                <label>Confirm Password</label><br />
                <div class="password-confirm-wrap">
                    <input
                        type="password"
                        class="input password"
                        id="password-confirm"
                        name="confirm-password"
                        v-model="confirmPassword"
                        placeholder=""
                    />
                    <button
                        type="button"
                        id="show-pw"
                        v-on:click="tooglePasswordConfirm()"
                    >
                        <img
                            src="unshow.png"
                            alt="open eye"
                            v-if="!showPwConfirm"
                        />
                        <img
                            src="show.png"
                            alt="close eye"
                            v-if="showPwConfirm"
                        />
                    </button>
                </div>
            </div>
            <div class="alert" id="invalid-password" v-if="invalidPassword">
                <img src="alert.png">
                <p>{{ invalidPassword }}</p>
            </div>
            <div class="alert" id="register-response" v-if="response">
                <img src="alert.png">
                <p>{{ response }}</p>
            </div>
            <div id="register-button">
                <button type="button" id="submit-register" v-on:click="register()">
                    Sign Up
                </button>
            </div>
        </div>
        <div class="login-container">
            <hr>
            <p>Already have an account? <a href="http://localhost:5173/login">Login</a></p>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
    name: "Register",
    data() {
        return {
            email: "",
            password: "",
            confirmPassword: "",
            invalidEmail: "",
            invalidPassword: "",
            response: "",
            showPw: false,
            showPwConfirm: false
        };
    },
    methods: {
        register() {
            let reEmail =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (
                !this.email.toLowerCase().match(reEmail) ||
                this.password !== this.confirmPassword ||
                this.password === ""
            ) {
                if (!this.email.toLowerCase().match(reEmail)) {
                    this.invalidEmail = "Invalid e-mail!";
                }
                if (this.password !== this.confirmPassword) {
                    this.invalidPassword = "Password does not match!";
                }
                if (this.password === "") {
                    this.invalidPassword = "Please fill in your password!";
                }
            } else {
                this.invalidEmail = "";
                this.invalidPassword = "";
                axios
                    .post(import.meta.env.VITE_API_URI + "/Account/register", {
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
            } else {
                passwordInput.type = "password";
                this.showPw = false;
            }
        },
        tooglePasswordConfirm() {
            const passwordInput = document.getElementById("password-confirm");
            if (passwordInput.type == "password") {
                passwordInput.type = "text";
                this.showPwConfirm = true;
            } else {
                passwordInput.type = "password";
                this.showPwConfirm = false;
            }
        }
    }
}
</script>

<style scoped>

.register-container {
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

.register {
    font-size: 18px;
    font-family: 'Darker Grotesque';
}

.form {
    margin-bottom: 10px;
}

.register label {
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

.password {
    padding: 10px 65px 10px 25px;
}

#show-pw {
    position: absolute;
    right: 10px;
    top: 25px;
    border:none;
}

#show-pw img {
    width: 32px;
    height: auto;
}

#submit-register {
    font-family: "Bebas Neue";
    color: white;
    font-size: 24px;
    width: 350px;
    height: 50px;
    border-radius: 100px;
    border-color: transparent;
    background: #353642;
    margin:25px 0px 25px 0px;
}

#submit-register:hover {
    background: #000000;
    transition: 0.3s;
}

.login-container {
    font-family: "Darker Grotesque";
    font-size: 18px;
    width: 350px;
    text-align: center;
}

.login-container hr {
    border: 1px solid;
}

.login-container a {
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
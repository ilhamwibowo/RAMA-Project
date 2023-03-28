<template>
    <div class="layout">
        <div class="registerImage">
            <img src="registerImage.png" alt="registerImage" />
        </div>
        <div class="form-container">
            <div class="register-container">
                <div class="title">
                    <h1>SIGN UP</h1>
                </div>
                <div id="register">
                    <div id="email">
                        <label>Email</label><br />
                        <input
                            type="text"
                            id="email-form"
                            name="email"
                            v-model="email"
                            placeholder="E-mail"
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
                                <img
                                    src="../assets/eye-outline.svg"
                                    alt="open eye"
                                    v-if="!showPw"
                                />
                                <img
                                    src="../assets/eye-off-outline.svg"
                                    alt="close eye"
                                    v-if="showPw"
                                />
                            </button>
                        </div>
                    </div>
                    <div id="confirm-password">
                        <label>Confirm Password</label><br />
                        <div class="password-confirm-wrap">
                            <input
                                type="password"
                                id="password-confirm"
                                name="confirm-password"
                                v-model="confirmPassword"
                                placeholder="Re-enter new password"
                            />
                            <button
                                type="button"
                                id="show-pw-confirm"
                                v-on:click="tooglePasswordConfirm()"
                            >
                                <img
                                    src="../assets/eye-outline.svg"
                                    alt="open eye"
                                    v-if="!showPwConfirm"
                                />
                                <img
                                    src="../assets/eye-off-outline.svg"
                                    alt="close eye"
                                    v-if="showPwConfirm"
                                />
                            </button>
                        </div>
                    </div>
                    <div id="invalid-password" v-if="invalidPassword">
                        <p>{{ invalidPassword }}</p>
                    </div>
                    <div id="register-response" v-if="response">
                        <p>{{ response }}</p>
                    </div>
                    <div id="register-button">
                        <button type="button" id="submit-register" v-on:click="register()">
                            Sign Up
                        </button>
                    </div>
                </div>
                <div class="login-container">
                    <p>Already have an account? <a href="http://localhost:5173/login">Login</a></p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
    name: "RegisterView",

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

.registerImage {
    width: 65%;
    height: 100%;
}
img {
    top: 15%;
    left: 20%;
    width: 50%;
    height: auto;
    display: block;
}

.form-container {
    width: 35%;
    height: 100;
    align-items: center;
    justify-content: center;
}

.register-container {
    margin-top: 10%;
    width: 50%;
    height: auto;
}
#email {
    text-align: left;
}

#password {
    margin-top: 8%;
    text-align: left;
}

#confirm-password {
    margin-top: 8%;
    text-align: left;
}

label {
    margin-left: 4%;
}
#email-form,
#password-form,
#password-confirm {
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

#register-button {
    margin-top: 10%;
    margin-bottom: 10%;
}

#submit-register {
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

.login-container {
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

.password-confirm-wrap {
    position: relative;
}

.password-wrap {
    position: relative;
}

#show-pw-confirm {
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

#show-pw-confirm img {
    width: 100%;
    height: auto;
    left: 0;
}

#submit-register:hover {
    background: #ffa801;
    transition: 0.3s;
}
/* .title {
    font-weight: 500;
    margin-bottom: 1rem;
    color: var(--color-heading);
}

.register,
#email,
#invalid-email,
#password,
#confirm-password,
#invalid-password,
#register-button,
#register-response {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-bottom: 0.5rem;
}

#invalid-email,
#invalid-password,
#register-response {
    color: red;
} */
</style>

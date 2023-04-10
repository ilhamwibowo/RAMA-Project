<template>
    <div class="form-container">
        <div class="title">
            <h1>LOG IN</h1>
        </div>
        <div class="login">
            <div class="form" id="email">
                <label>Email</label><br/>
                <input
                    type="text"
                    class="input"
                    id="email-form"
                    name="email"
                    v-model="email"
                    placeholder=""
                />
                <div class="alert" id="invalid-email" v-if="invalidEmail">
                    <img src="alert.png">
                    <p>{{ invalidEmail }}</p>
                </div>
            </div>
            <div class="form" id="password">
                <label>Password</label><br />
                <div class="password-wrap">
                    <input
                        type="password"
                        class="input"
                        id="password-form"
                        name="password"
                        v-model="password"
                        placeholder=""
                    />
                    <button type="button" id="show-pw" v-on:click="tooglePassword()">
                        <img src="unshow.png" alt="open eye" v-if="!showPw" />
                        <img
                            src="show.png"
                            alt="close eye"
                            v-if="showPw"
                        />
                    </button>
                </div>
                <div class="alert" id="invalid-password" v-if="invalidPassword">
                    <img src="alert.png">
                    <p>{{ invalidPassword }}</p>
                </div>
            </div>
            <div class="alert" id="login-response" v-if="response">
                <img src="alert.png">
                <p>{{ response }}</p>
            </div>
            <div id="forgot-password">
                <a href="http://localhost:5173/forgot">Forgot password</a> <br>
            </div> 
            <button type="button" id="submit-login" v-on:click="login()">Login</button>
        </div>
        <div id="register-container">
            <hr>
            <p>Don't have an account? <a href="http://localhost:5173/register">Sign Up</a></p>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
    name: "Login",
    data() {
        return {
            email: "",
            password: "",
            invalidEmail: "",
            invalidPassword: "",
            response: "",
            showPw: false
        }
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
}
</script>

<style scoped>

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

.login {
    font-size: 18px;
    font-family: 'Darker Grotesque';
}

.form {
    margin-bottom: 10px;
}

.login label {
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

input[type="text"], input[type="password"] {
    font-family: "Darker Grotesque";
    font-size: 125%;
}

#show-pw {
    position: absolute;
    right: 10px;
    top: 25px;
    border:none;
    background-color: rgba(255, 255, 255, 0);
}

#show-pw img {
    width: 30px;
    height: auto;
}

#forgot-password {
    text-align: center;
}

#forgot-password a {
    text-decoration: none;
    font-weight: 700;
}

#submit-login {
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

#submit-login:hover {
    background: #000000;
    transition: 0.3s;
}

#register-container {
    font-family: "Darker Grotesque";
    font-size: 18px;
    width: 350px;
    text-align: center;
}

#register-container hr {
    border: 1px solid;
}

#register-container a {
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
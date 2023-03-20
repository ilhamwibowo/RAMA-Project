<template>
    <div class="reset-password">
        
        <div class="left-column">
            <img src="forgotPasswordImage.png" alt="loginImage"/>
        </div>

        <div class="right-column">

            <div class="title">
                <h2>Reset Password</h2>
            </div>
            <div class="subtext">
                <h4>An OTP code has been sent to your e-mail!</h4>
            </div>
            <div id="new-password" class="input-field">
                <label class="ml-2">New Password</label>
                <input
                type="password"
                name="new-password"
                v-model="newPassword"
                placeholder="New password"
                />
            </div>
            <div id="confirm-password" class="input-field">
                <label class="ml-2">Confirm Password</label>
                <input
                type="password"
                name="confirm-password"
                v-model="confirmPassword"
                placeholder="Re-enter new password"
                />
            </div>
            <div id="invalid-password" v-if="invalidPassword">
                <p>{{ invalidPassword }}</p>
            </div>

            <div id="otp-code" class="input-field">
                <label class="ml-2">OTP Code</label>
                <input 
                    type="text" 
                    name="otp-code" 
                    v-model="OTPCode" 
                    placeholder="OTP code" />
            </div>

            <div id="request-response" v-if="response">
                <p>{{ response }}</p>
            </div>
            <div id="reset-button">
                <button type="button" id="btn-reset" v-on:click="reset()">Reset Password</button>
            </div>
            <hr>
            <div id="back-forget">
                <p>Back to <a href="http://localhost:5173/forgot">Forget Password</a></p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";

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
/* .title,
.subtitle {
    font-weight: 500;
    margin-bottom: 0.5rem;
    color: var(--color-heading);
} */

.title {
    font-weight: 300;
    font-size: 30px;
    width: 100%;
    color: var(--color-heading);
}

a {
    color:black;
}

.ml-2 {
    margin-left: 20px;
}

.reset-password {
    display:flex;
    flex-direction: row;
    width: 100%;
    flex-wrap: wrap;
}

hr {
    background-color: black;
    margin-top: 20px;
    margin-bottom: 20px;
    height: 2px;
    border: none;
    width: 300px;
}

input[type="text"], input[type="password"] {
  width: 100%;
  padding: 10px;
  margin-bottom: 20px;
  border-radius: 20px;
  border: 2px solid rgb(105, 105, 105);

}

.input-field {
    text-align: left;
    width: 300px;
    color: var(--color-heading);
}

.left-column {
  flex: 1;
  min-width: 300px;
}

.subtext {
    width: 200px;
    color:var(--color-heading);
    font-size: 1rem;
    margin-bottom: 30px;
}

.left-column img {
  max-width: 100%;
  height: auto;
}

.right-column {
  flex: 1;
  min-width: 300px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

#btn-reset {
  height: 3vw;
  width: 300px;
  padding: 10px;
  background-color: #1f2220;
  color: #fff;
  border-radius: 30px;
  border: none;
  cursor: pointer;
  font-family: 'Montserrat', sans-serif;
    font-weight: bold;
  transition: background-color 0.2s;
}

#btn-reset:hover {
  background-color: #1b1d1b;
}

h4,
/* #new-password,
#confirm-password, */
#invalid-password,
/* #otp-code, */
#request-response,
#reset-button {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-bottom: 0.5rem;
}

h4 {
    margin-bottom: 0 !important;
}

#invalid-password,
#request-response {
    color: red;
}
</style>

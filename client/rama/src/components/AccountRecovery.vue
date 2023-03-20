<template>
    <div class="email-recovery">
        <div class="left-column">
            <img src="forgotPasswordImage.png" alt="loginImage"/>
        </div>
        <div class="right-column ">
            <div class="title">
                <h2>FORGOT PASSWORD</h2>
            </div>
            <div class="subtext">
                <p>Please enter the email address you'd like your password reset information sent to</p>
            </div>
            <div id="recovery-email">
                <label class="ml-2">Email</label>
                <input type="text" name="email" v-model="email" />
            </div>
            <div id="invalid-email" v-if="invalidEmail">
                <p>{{ invalidEmail }}</p>
            </div>
            <div id="request-response" v-if="response">
                <p>{{ response }}</p>
            </div>
            <div id="recover-button">
                <button type="button" id="btn-recover" v-on:click="request()">SEND</button>
            </div>
            <hr>
            <div id="back-link">
                <p>Back to <a href="http://localhost:5173/login">Sign In</a></p>
            </div>
            <br>
        </div>
    </div>
</template>

<script>
import axios from "axios";

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

    methods: {
        request() {
            let reEmail =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!this.email.toLowerCase().match(reEmail)) {
                this.invalidEmail = "Invalid e-mail!";
            } else {
                this.invalidEmail = "";
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
.email-recovery {
    display:flex;
    flex-direction: row;
    width: 100%;
    flex-wrap: wrap;
}

a {
    color:black;
}
.ml-2 {
    margin-left: 20px;
}

.left-column {
  flex: 1;
  min-width: 300px;
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


.title {
    font-weight: 300;
    font-size: 30px;
    width: 100%;
    color: var(--color-heading);
}

.subtext {
    width: 200px;
    color:var(--color-heading);
    font-size: 1rem;
    margin-bottom: 50px;
}

hr {
    background-color: black;
    margin-top: 20px;
    margin-bottom: 20px;
    height: 2px;
    border: none;
    width: 300px;
}

input[type="text"] {
  width: 100%;
  padding: 10px;
  margin-bottom: 20px;
  border-radius: 20px;
  border: 2px solid grey;

}

#btn-recover {
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

#btn-recover:hover {
  background-color: #1b1d1b;
}

#recovery-email {
    text-align: left;
    width: 300px;
}

#recover-button {
    width: 300px;
    align-items: center;
    justify-content: center;
}

#invalid-email,
#request-response {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-bottom: 0.5rem;
}

#invalid-email,
#request-response {
    color: red;
}
</style>

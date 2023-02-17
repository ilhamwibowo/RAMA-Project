<template>
  <div class="login">
    <div class="title">
      <h1>
        Rama
      </h1>
    </div>
    <div id="login">
      <div id="email">
        <input type="text" name="email" v-model="email" placeholder="E-mail" />
      </div>
      <div id="invalid-email" v-if="invalidEmail">
        <p>{{ invalidEmail }}</p>
      </div>
      <div id="password">
        <input type="password" name="password" v-model="password" placeholder="Password" />
      </div>
      <div id="invalid-password" v-if="invalidPassword">
        <p>{{ invalidPassword }}</p>
      </div>
      <div id="login-response" v-if="response">
        <p>{{ response }}</p>
      </div>
      <div id="login-button">
        <button type="button" v-on:click="login()">Login</button>
      </div>
      <div id="register-from-login">
        <a href="http://localhost:5173/register">Register</a>
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
      response: ""
    }
  },
  
  methods: {
    login() {
      let reEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      if (!this.email.toLowerCase().match(reEmail) || this.password === "") {
        if (!this.email.toLowerCase().match(reEmail)) {
          this.invalidEmail = "Invalid e-mail!";
        } if (this.password === "") {
          this.invalidPassword = "Please fill in your password!";
        }
      } else {
        this.invalidEmail = "";
        this.invalidPassword = "";
        axios.post(import.meta.env.VITE_API_URI + "/Account/login", {
          email: this.email,
          password: this.password
        }).then((response) => {
          this.response = "";
          localStorage.setItem("token", response.data.token);
          this.$router.push("/profile");
        }).catch((error) => {
          this.response = error.response.data;
        });
      }
    }
  }
}
</script>

<style scoped>
.title {
  font-weight: 500;
  margin-bottom: 1rem;
  color: var(--color-heading);
}

.login, #email, #invalid-email, 
#password, #invalid-password, #login-button, 
#login-response, #register-from-login {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}

#invalid-email, #invalid-password, #login-response {
  color: red;
}
</style>

<template>
  <div class="register">
    <div class="title">
      <h1>
        Register into Rama
      </h1>
    </div>
    <div id="register">
      <div id="email">
        <input type="text" name="email" v-model="email" placeholder="E-mail" />
      </div>
      <div id="invalid-email" v-if="invalidEmail">
        <p>{{ invalidEmail }}</p>
      </div>
      <div id="password">
        <input type="password" name="password" v-model="password" placeholder="Password" />
      </div>
      <div id="confirm-password">
        <input type="password" name="confirm-password" v-model="confirmPassword" placeholder="Re-enter new password" />
      </div>
      <div id="invalid-password" v-if="invalidPassword">
        <p>{{ invalidPassword }}</p>
      </div>
      <div id="register-response" v-if="response">
        <p>{{ response }}</p>
      </div>
      <div id="register-button">
        <button type="button" v-on:click="register()">Register</button>
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
      response: ""
    }
  },
  
  methods: {
    register() {
      let reEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      if (!this.email.toLowerCase().match(reEmail) || this.password !== this.confirmPassword || this.password === "") {
        if (!this.email.toLowerCase().match(reEmail)) {
          this.invalidEmail = "Invalid e-mail!";
        } if (this.password !== this.confirmPassword) {
          this.invalidPassword = "Password does not match!";
        } if (this.password === "") {
          this.invalidPassword = "Please fill in your password!";
        }
      } else {
        this.invalidEmail = "";
        this.invalidPassword = "";
        // TODO: Change POST target url if needed
        // TODO: Handle CORS
        axios.post(import.meta.env.VITE_API_URI + "/Account/register", {
          email: this.email,
          password: this.password
        }).then((response) => {
          if (response.status !== 200) {
            this.response = response.data;
          } else {
            this.response = "";
            localStorage.setItem("token", response.data.token);
            // TODO: Redirect to profile page
            this.$router.push("/profile");
          }
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

.register, #email, #invalid-email,
#password, #confirm-password, #invalid-password, 
#register-button, #register-response {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}

#invalid-email, #invalid-password, #register-response {
  color: red;
}
</style>

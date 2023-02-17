<template>
  <div class="register">
    <div class="title">
      <h1>
        Register into Rama
      </h1>
    </div>
    <div id="register">
      <div id="username">
        <input type="text" name="email" v-model="email" placeholder="E-mail" />
      </div>
      <div id="password">
        <input type="password" name="password" v-model="password" placeholder="Password" />
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
      response: ""
    }
  },
  
  methods: {
    register() {
      // TODO: Change POST target url if needed
      // TODO: Handle CORS
      axios.post(import.meta.env.VITE_API_URI + "/Account/register", {
        email: this.email,
        password: this.password
      }).then((response) => {
        if (response.status !== 200) {
          this.response = response.data;
        } else {
          localStorage.setItem("token", response.data.token);
          // TODO: Redirect to profile page
          // this.$router.push("/profile");
        }
      });
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

.register, #username, #password,
#register-button, #register-response {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}
</style>

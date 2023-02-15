<template>
  <div class="login">
    <div class="title">
      <h1>
        Rama
      </h1>
    </div>
    <div id="login">
      <div id="username">
        <input type="text" name="email" v-model="email" placeholder="E-mail" />
      </div>
      <div id="password">
        <input type="password" name="password" v-model="password" placeholder="Password" />
      </div>
      <div id="login-response" v-show="">
        <p>{{ response }}}</p>
      </div>
      <div id="login-button">
        <button type="button" v-on:click="login()">Login</button>
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
      response: ""
    }
  },
  
  methods: {
    login() {
      // TODO: Change POST target url if needed
      // TODO: Handle CORS
      axios.post("https://localhost:5226/api/Account/login", {
        email: this.email,
        password: this.password
      }).then((response) => {
        if (response.status !== 200) {
          this.response = response.data;
        }
        localStorage.setItem("token", response.data.token);
        // TODO: Redirect to profile page
        // this.$router.push("/profile");
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

.login, #username, #password, #login-button, #login-response {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

#username, #password {
  margin-bottom: 0.2rem;
}
</style>
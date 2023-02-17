<template>
  <div class="email-recovery">
    <div class="title">
      <h2>
        Recover Account
      </h2>
    </div>
    <div class="subtitle">
      <h3>
        Enter the email associated with your account below!
      </h3>
    </div>
    <div id="recovery-email">
      <input type="text" name="email" v-model="email" placeholder="E-mail" />
    </div>
    <div id="invalid-email" v-if="invalidEmail">
      <p>{{ invalidEmail }}</p>
    </div>
    <div id="request-response" v-if="response">
      <p>{{ response }}</p>
    </div>
    <div id="recover-button">
      <button type="button" v-on:click="request()">Submit</button>
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
    }
  },
  
  methods: {
    request() {
      let reEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      if (!this.email.toLowerCase().match(reEmail)) {
        this.invalidEmail = "Invalid e-mail!";
      } else {
        this.invalidEmail = "";
        axios.put(import.meta.env.VITE_API_URI + "/Account/forgotpassword/requestotp", {
          email: this.email
        }).then(() => {
          this.response = "";
          this.$emit("email", this.email);
          this.$emit("isRequested", true);
        }).catch(() => {
          this.response = "Request failed! Please try again later.";
        });
      }
    }
  }
}
</script>

<style scoped>
.title, .subtitle {
  font-weight: 500;
  margin-bottom: 0.5rem;
  color: var(--color-heading);
}

.email-recovery, #recovery-email, #invalid-email, 
#request-response, #recover-button {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}

#invalid-email, #request-response {
  color: red;
}
</style>

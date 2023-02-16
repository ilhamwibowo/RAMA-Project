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
    <div id="request-response" v-if="response">
      <p>{{ response }}}</p>
    </div>
    <div id="recover-button">
      <button type="button" v-on:click="request()">Submit</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

const env = import.meta.env;

export default {
  name: "AccountRecovery",
  
  data() {
    return {
      email: "",
      response: ""
    }
  },
  
  methods: {
    request() {
      axios.put(env.VITE_API_URI + "/Account/forgotpassword/requestotp", {
        email: this.email
      }).then((response) => {
        if (response.status !== 200) {
          this.response = "Request failed! Please try again later.";
        } else {
          this.$emit("isRequested", true);
        }
      });
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

.email-recovery, #recovery-email, 
#request-response, #recover-button {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}
</style>
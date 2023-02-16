<template>
  <div class="reset-password">
    <div class="title">
      <h2>
        Reset Password
      </h2>
    </div>
    <div class="subtitle">
      <h4>
        Enter your new password!
      </h4>
    </div>
    <div id="new-password">
      <input type="password" name="new-password" v-model="newPassword" placeholder="New password" />
    </div>
    <div class="subtitle">
      <h4>
        Confirm your new password!
      </h4>
    </div>
    <div id="confirm-password">
      <input type="password" name="confirm-password" v-model="confirmPassword" placeholder="Re-enter new password" />
    </div>
    <div class="subtitle">
      <h4>
        An OTP code has been sent to your e-mail!
      </h4>
      <h4>
        Enter the sent code below!
      </h4>
    </div>
    <div id="otp-code">
      <input type="text" name="otp-code" v-model="OTPCode" placeholder="OTP code" />
    </div>
    <div id="request-response" v-if="invalid">
      <p>{{ invalid }}}</p>
    </div>
    <div id="reset-button">
      <button type="button" v-on:click="reset()">Reset Password</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

const env = import.meta.env;

export default {
  name: "ResetPassword",
  
  data() {
    return {
      email: "",
      newPassword: "",
      confirmPassword: "",
      OTPCode: "",
      invalid: ""
    }
  },
  
  methods: {
    reset() {
      // TODO: Implement password validation
      // this.invalid = () => {
      //   //
      // };
      axios.put(env.VITE_API_URI + "/Account/forgotpassword/changepassword", {
        email: this.email,
        otp: this.OTPCode,
        newPassword: this.newPassword
      }).then((response) => {
        if (response.status === 200) {
          this.$emit("isRecovered", true);
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

h4, .reset-password, #new-password,
#confirm-password, #otp-code,
#request-response, #reset-button {
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
</style>
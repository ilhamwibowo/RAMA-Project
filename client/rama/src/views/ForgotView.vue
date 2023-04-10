<template>
    <div class="forgot">
        <AccountRecovery
            v-if="!isRequested && !isRecovered"
            @email="(value) => (this.email = value)"
            @isRequested="(value) => (this.isRequested = value)"
        />
        <ResetPassword
            v-else-if="isRequested && !isRecovered"
            @isRecovered="(value) => (this.isRecovered = value)"
            :email="email"
        />
        <SuccessfulRecovery v-else-if="isRequested && isRecovered" />
    </div>
</template>

<script>
import AccountRecovery from "@/components/AccountRecovery.vue";
import ResetPassword from "@/components/ResetPassword.vue";
import SuccessfulRecovery from "@/components/SuccessfulRecovery.vue";

export default {
    name: "ForgotView",

    components: {
        AccountRecovery,
        ResetPassword,
        SuccessfulRecovery
    },

    data() {
        return {
            email: "",
            isRequested: false,
            isRecovered: false
        };
    }
};
</script>

<style scoped>
.forgot {
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-bottom: 0.5rem;
}
</style>

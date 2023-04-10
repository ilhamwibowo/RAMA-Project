<template>
    <div class="successful-recovery">
        <img src="check.png">
        <div class="title">
            <h1>Your password has been reset!</h1>
        </div>
    </div>
</template>

<script>
export default {
    name: "SuccessfulRecovery",

    mounted() {
        setTimeout(() => {
            this.$router.push("/login");
        }, 3000);
    }
};
</script>

<style scoped>
.successful-recovery {
    width: 100%;
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    bottom: 50px;
}

.title {
    color: black;
    font-weight: 300;
    font-size: 20px;
}
</style>

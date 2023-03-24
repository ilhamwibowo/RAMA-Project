<template>
    <div class="successful-recovery">
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
    display: flex;
    flex-wrap: wrap;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-bottom: 0.5rem;
    width: 100%;
    height: 720px;
}

.title {
    font-weight: 300;
    font-size: 20px;
    margin-bottom: 1rem;
    color: var(--color-heading);
}
</style>

<template>
  <div class="logout">
    <div class="title">
      <h1>
        You have successfully log out!
      </h1>
    </div>
  </div>
</template>

<script>
export default {
  name: "LogoutView",
  
  mounted() {
    setTimeout(() => {
      this.$router.push("/login");
    }, 3000);
  }
}
</script>

<style scoped>
.logout {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-bottom: 0.5rem;
}

.title {
  font-weight: 500;
  margin-bottom: 1rem;
  color: var(--color-heading);
}
</style>
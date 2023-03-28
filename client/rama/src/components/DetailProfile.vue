<template>
    <div class="detail-profile">
        <h3 class="title">{{ title }}</h3>
        <p class="text">{{ text }}</p>
    </div>
</template>
>

<script>
export default {
    name: "DetailProfile",
    props: {
        title: String,
        text: String
    }
};
</script>

<style scoped>
.title {
    color: rgb(28, 28, 29);
    font-size: 1.5rem;
    margin-left: 20px;
}

.detail-profile {
    text-align: left;
}
.text {
    border: 2px solid #5f5f5f;
    padding: 5px 10px;
    width: 100%;
    padding: 10px;
    margin-bottom: 20px;
    border-radius: 20px;
    border: 2px solid grey;
    color: rgb(63, 62, 62);
    font-size: 1.5rem;
    font-weight: 600;
}
.detail-profile {
    margin: 10px 15px 10px 15px;
}
</style>

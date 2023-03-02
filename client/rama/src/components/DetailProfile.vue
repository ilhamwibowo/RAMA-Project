<template>
    <div class="detail-profile">
        <h3 class="title">{{ title }}</h3>
        <p class="text">{{ text }}</p>
    </div>
</template>>

<script>
export default {
    name: "DetailProfile",
    props: {
        title: String,
        text: String
    }
}
</script>

<style scoped>
    .title {
        color: black;
        font-weight: bold;
        font-size:medium
    }
    .text {
        border: 2px solid #5f5f5f;
        border-radius: 5px;
        padding: 5px 10px;
    }
    .detail-profile {
        margin: 10px 0px 
    }
</style>
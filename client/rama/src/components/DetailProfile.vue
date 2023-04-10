<template>
    <div class="detail-profile">
        <h3 class="label">{{ title }}</h3>
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
.detail-profile {
    text-align: left;
}
.label {
    margin-left: 40px;
    font-family: 'Darker Grotesque';
    font-size: 18px;
}

.text {
    font-family: 'Darker Grotesque';
    font-size: 18px;
    width: 350px;
    height: 60px;
    background: rgba(255, 255, 255, 0);
    border: 1px solid #353642;
    border-radius: 100px;
    padding: 10px 25px;
    margin: 5px 0px 5px 0px;
}
</style>

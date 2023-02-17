<template>
    <div class="edit-detail-profile">
        <h3 class="title">{{ title }}</h3>
        <input class="input" v-model="data" placeholder="Edit me" />
        <p>{{ data }}</p>
    </div>
</template>>

<script>
export default {
    name: "EditDetailProfile",
    props: {
        title: String,
        text: String
    },
    data() {
        return {
            data: this.text
        }
    }
}
</script>

<style scoped>
    .title {
        color:aliceblue;
        font-weight: bold;
        font-size:medium
    }
    .input {
        width:auto;
        font-size: medium;
        border: 2px solid #5f5f5f;
        border-radius: 5px;
        padding: 5px 10px;
        background-color: rgba(0,0,0,0);
        color:rgba(235,235,235,0.64);
    }
    .edit-detail-profile {
        margin: 10px 0px 
    }
</style>
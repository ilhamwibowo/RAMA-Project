<template>
    <div class="photo-detail-container">
        <div class="photo-detail">
            <img :src="url" >
            <button @click="onClickClose">Close</button>
        </div>
    </div>    
</template>
<script>
export default {
    name: "AlbumPhotoDetail",
    props: {
        id: String,
        url: String
    },
    methods: {
        onClickClose() {
            this.$emit("closeClicked")
        }
    }
}
</script>
<style scoped>
.photo-detail-container {
    position: fixed;
    top: 0;
    left: 0;
    background-color: rgba(0,0,0,0.1);
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center
}

.photo-detail {
    background-color: #FFFFFF;
    padding: 25px;
    border-radius: 10px;
}

img {
    display: block;
    max-height: 750px;
    max-width: 800px;
    border-radius: 5px;
}

</style>
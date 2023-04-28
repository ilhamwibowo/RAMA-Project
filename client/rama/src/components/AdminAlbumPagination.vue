<template>
    <div class="photo-container">
        <AdminAlbumPhoto
            v-for="photo in this.photosInput"
            :key="photo.id"
            :photoId="photo.id"
            :photoUrl="photo.url"
        />
    </div>    
</template>

<script>
import AdminAlbumPhoto from "@/components/AdminAlbumPhoto.vue";
export default {
    name: "AdminAlbumPagination",
    components: {
        AdminAlbumPhoto
    },
    props: {
        photosInput: Array
    }
}
</script>

<style scoped>
.photo-container {
    display: flex;
    flex-wrap: wrap;
    column-gap: 25px;
    row-gap: 15px;
    margin-top: 50px;
    margin-bottom: 50px;
}

</style>
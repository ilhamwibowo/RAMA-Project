<template>
    <div class="photo-container">
            <AlbumPhoto
            v-for="photo in this.photosInput"
            :key="photo.id"
            :photoId="photo.id"
            :photoUrl="photo.url"/>
            
        </div>
</template>

<script>
import AlbumPhoto from '../components/AlbumPhoto.vue';
export default {
    name: "AlbumPagination",
    components: {
        AlbumPhoto
    },
    props: {
        photosInput: Array,
        key: String
    }
    /** TODO: cek setiap foto ada di list atau nggak, kalau ada nanti send check ke album photo, trus nanti di handle di photo kalau nilainya true atau false */
}
</script>

<style scoped>
.photo-container {
    column-count: 3;
    column-gap: 25px;
}
</style>
<template>
    <div>
        <p class="title">Album Page</p>
        <div class="photo-container">
            <AlbumPhoto photoId="1" photoUrl="../../public/1.jpg"/>
            <AlbumPhoto photoId="2" photoUrl="../../public/2.jpg"/>
            <AlbumPhoto photoId="3" photoUrl="../../public/3.jpg"/>
            <AlbumPhoto photoId="4" photoUrl="../../public/4.jpg"/>
            <AlbumPhoto photoId="5" photoUrl="../../public/5.jpg"/>
            <AlbumPhoto photoId="6" photoUrl="../../public/6.jpg"/>
            <AlbumPhoto photoId="7" photoUrl="../../public/7.jpg"/>
            <AlbumPhoto photoId="8" photoUrl="../../public/8.jpg"/>
            <AlbumPhoto photoId="9" photoUrl="../../public/9.jpg"/>
            <AlbumPhoto photoId="10" photoUrl="../../public/10.jpg"/>
            <AlbumPhoto photoId="11" photoUrl="../../public/11.jpg"/>
            <AlbumPhoto photoId="12" photoUrl="../../public/12.jpg"/>
        </div>
    </div>
</template>

<script>
import AlbumPhoto from '../components/AlbumPhoto.vue'
export default {
    name: "Album",
    components: {
        AlbumPhoto,
    }
}
</script>

<style scoped>

.photo-container {
    /* display: grid;
    justify-content: center;
    height: auto;
    grid-template-columns: repeat(3, 1fr);
    grid-template-areas: masonry;
    column-gap: 45px;
    break-inside: avoid; */
    column-count: 3;
    column-gap: 45px;
}
</style>
<template>
    <div>
        <Album/>
    </div>
</template>

<script>
import Album from '../components/Album.vue'
export default {
    name: "AlbumView",
    components: {
        Album
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
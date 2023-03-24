<template>
    <div class="album">
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
.album {
    margin: 0 300px 0 300px;
}
</style>
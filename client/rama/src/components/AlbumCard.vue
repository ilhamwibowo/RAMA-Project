<template>
    <div class="event-card" @click="goToAlbum(album.albumId)">
      <img v-if="album.albumPhotos[0]" :src="album.albumPhotos[0].url" alt="Album Thumbnail" />
      <h3>{{ album.albumName }}</h3>
    </div>
</template>
<script>
  export default {
    props: {
      album: {
        type: Object,
        required: true
      }
    },
    methods: {
      goToAlbum(albumId) {
        this.$router.push({ name: 'album', params: { id: albumId } });
      }
    }
  };
  </script>
  <style scoped>
  /* Styles for the event card component */
  .event-card {
    width: 100%;
    max-width: 200px;
    border: 1px solid #ccc;
    border-radius: 4px;
    padding: 12px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    cursor: pointer;
    text-align: left;
    background-color: #ececec;
  }
  
  .event-card:hover {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  }
  
  .event-card img {
    width: 100%;
    height: 200px;
    border-radius: 10px;
    object-fit: cover;
  }
  
  .event-card h3 {
    margin: 10px 0;
    font-weight: bold;
  }
  
  .event-card p {
    margin: 0;
  }
  
  .registration-fee {
    position: absolute;
    bottom: 0;
    right: 0;
    width: 100%;
    font-size: 20px;
    color: #ffa801;
    text-align: right;
    margin-right: 20px;
    margin-bottom: 10px;
}

.registration-fee p {
font-weight: bold;
}
</style>
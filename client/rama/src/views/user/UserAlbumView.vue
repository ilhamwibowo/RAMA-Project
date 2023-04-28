<template>
    <div class="layout">
      <NavigationBar class="navbar" />
      <div class="body">
        <div class="event-list">
          <album-card
            v-for="album in albums"
            :key="album.albumId"
            :album="album"
          />
        </div>
      </div>
      <Footer class="footer" />
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  import NavigationBar from '@/components/NavigationBar.vue';
  import Footer from '@/components/Footer.vue';
  import AlbumCard from '@/components/AlbumCard.vue';

  export default {
    name: "UserEventView",
    components: {
      NavigationBar,
      Footer,
      AlbumCard
    },
    data() {
      return {
        albums: []
      };
    },
    async created() {
      const token = localStorage.getItem("token");
  
      // Configuration for API
      const config = {
        headers: { Authorization: `Bearer ${token}` }
      };
  
      try {
        const response = await axios.get(
          import.meta.env.VITE_API_URI + "/Album",
          config
        );
        this.albums = response.data.albums;

        / * warning : hardcoded data, needs to be deleted */
        this.albums.push(
            {
                "albumId" : "1230j12-123j12-3jh1d2j-12j3-j",
                "albumName" : "Hardcoded Album",
                "albumPhotos" : [
                    {"url" : "2.jpg"}
                ]
            }
        )
      } catch (error) {
        console.log(error);
      }
    }
  };
  </script>
  
  <style scoped>
  .layout {
    display: grid;
    grid-template-rows: auto auto 240px;
    grid-template-areas: "header" "body" "footer";
    min-height: 100vh;
  }
  
  .header {
    grid-area: header;
  }
  
  .body {
    grid-area: body;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin: 0 100px;
    margin-bottom: 100px;
  }
  
  .footer {
    grid-area: footer;
  }
  
  .header {
    position: relative;
    width: 100%;
    max-width: 1100px;
    height: 300px;
    overflow: hidden;
    border-radius: 20px;
  }
  
  .header img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
  
  .event-list {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 16px;
    margin-top: 16px;
    max-width: 1100px;
  }
  
  @media screen and (min-width: 1024px) {
    .event-list {
      width: 80%;
    }
  }
  
  @media screen and (min-width: 768px) {
    .event-list {
      width: 90%;
    }
  }
  
  @media screen and (max-width: 768px) {
    .body {
      margin: 0 20px;
    }
  
    .header-title {
      font-size: 24px;
    }
  }
  
  .event-card {
    width: 100%;
    max-width: 350px;
    border: 1px solid #ccc;
    border-radius: 4px;
    padding: 12px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    cursor: pointer;
    text-align: left;
    background-color: #ececec;
    transition: 0.2s;
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
  
  .header-title {
  position: absolute;
  bottom: 20px;
  left: 20px;
  color: #000;
  font-size: 36px;
  font-weight: bold;
  text-align: left;
  font-family: "Montserrat", sans-serif;
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
  
  /* Media queries */
  @media screen and (max-width: 1024px) {
  .layout {
  margin: 0 50px;
  }
  
  .header-title {
  font-size: 28px;
  }
  }
  </style>
  
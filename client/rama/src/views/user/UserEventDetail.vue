<template>
    <div class="event-detail">
      <confirm-modal
        :show="showModal"
        @close="showModal = false"
        title="Confirmation"
        message="Are you sure you want to register for this event?"
        @confirm="registerForEvent"
      ></confirm-modal>

      <div class="left-side">
        <h1>{{ eventData.raceName }}</h1>
        <hr>
        <img class="event-image" v-if="eventData.raceThumbnail" :src="eventData.raceThumbnail.url" alt="Event Image" />
        <!-- <img class="event-image" :src="eventData.image" alt="Event Image" /> -->
        <h2>About</h2>
        <p>{{ eventData.raceDesc }}</p>
        <h2>Route Map</h2>
        <div id="route-map">
          <!-- Insert map component or implementation here -->
        </div>
      </div>
      <div class="right-side">
        <div class="sticky-container">
          <div class="sticky-card">
            <h2>{{ eventData.raceName }}</h2>
            <div class="details">
              <p v-if="eventData.startLocation">{{ eventData.startLocation.name }}</p>
              <p>{{formatDate(eventData.startTime)}}</p>
              <p>{{ eventData.distance }}</p>
              <p>{{ eventData.registrationFee }}</p>
            </div>
            <button class="register-button" @click="showModal = true">Register</button>
          </div>
          <button class="view-album-button" @click="viewAlbum">View Album</button>
        </div>
      </div>
    </div>
  </template>
  
<script>
  import axios from 'axios';
  import { Loader } from "@googlemaps/js-api-loader"
  
  import ConfirmModal from "@/components/ConfirmModal.vue";

  let loader;
  let map;

  export default {
    components: {
      ConfirmModal,
    },
    
    data() {
      return {
        showModal: false,
        eventId: this.$route.params.id,
        eventData: {
          raceThumbnail: {url : "../1.jpg"},
          raceName: "Jawa Timur Marathon Festival",
          raceDesc: "lobortis ut ultrices quis, laoreet eu leo.",
          distance: "23km",
          registrationFee: "10000",
          startTime:new Date(),
        },
      };
    },
    
    created() {
      this.fetchEventData();
    },
    
    async mounted() {
      let processedPoints = this.eventData.points.split(";");
      
      loader = new Loader({
        apiKey: import.meta.env.VITE_MAPS_API_KEY,
        version: "weekly",
      });

      loader.load().then(async () => {
        const { Map } = await google.maps.importLibrary("maps");

        map = new Map(document.getElementById("route-map"), {
          center: { lat: parseFloat(processedPoints[0].split(",")[0]), lng: parseFloat(processedPoints[0].split(",")[1]) },
          zoom: 8,
        });
      });

      await this.drawRoute(processedPoints);
    },

    methods: {
      async drawRoute(processedPoints) {
        await axios.get("https://roads.googleapis.com/v1/snapToRoads?path=" + processedPoints.join("|") + "&interpolate=true&key=" + import.meta.env.VITE_MAPS_API_KEY)
          .then((response) => {
            let snappedCoordinates = [];

            loader.load().then(async () => {
              for (let i = 0; i < response.data.snappedPoints.length; i++) {
                let point = new google.maps.LatLng(response.data.snappedPoints[i].location.latitude, response.data.snappedPoints[i].location.longitude);
                snappedCoordinates.push(point);
              }

              let snappedPolyline = new google.maps.Polyline({
                path: snappedCoordinates,
                strokeColor: "#ffa801",
                strokeWeight: 4,
                strokeOpacity: 0.9
              });
              snappedPolyline.setMap(map);
            });
          })
          .catch((error) => {
            console.log(error);
          });
      },
      
      async fetchEventData() {
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        await axios.get(import.meta.env.VITE_API_URI + "/Race/"+ this.eventId, config)
          .then(response => {
            this.eventData = response.data;
            console.log(response.data);
          })
          .catch(error => {
            console.log(error);
          });
      },
      
      registerForEvent() {
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        axios.post(import.meta.env.VITE_API_URI + "/Registration/"+ this.eventId, null,  config)
          .then(response => {
            console.log(response.data);
          })
          .catch(error => {
            console.log(error);
            console.log("p");
          });
      },
      
      viewAlbum() {
        this.$router.push({ name: 'album', params: { id: this.eventData.raceAlbum.albumId } });
      },formatDate(dateString) {
        const months = [
          "January", "February", "March", "April", "May", "June", 
          "July", "August", "September", "October", "November", "December"
        ];
        const date = new Date(dateString);
        const day = date.getDate();
        const monthIndex = date.getMonth();
        const year = date.getFullYear();

        return `${day} ${months[monthIndex]} ${year}`;
      },
    },
  };
  </script>
  
<style scoped>
.event-detail {
  display: flex;
  margin:20px 50px 0 50px;
}

.left-side {
  flex: 3;
  padding-right: 40px;
  text-align: left;
}

.right-side {
  flex: 1;
  justify-content: center;
  text-align: center;
}

.sticky-container {
  position: sticky;
  top: 20px;
}
.sticky-card {
  width: 100%;
  border: 1px solid #ccc;
  border-radius: 10px;
  padding: 12px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  background-color: #fff;
}

.details {
  text-align: left;
}
.event-image {
  max-width: 100%; /* This will ensure the image does not exceed the container's width */
  height: auto; /* This will maintain the aspect ratio of the image */
}
h1 {
  font-weight: bold;
}
hr {
  border : 2px solid #000000;
  margin-bottom:20px;
  margin-top:10px;
}

.register-button {
  width: 100px;
  height: 30px;
  background-color: #FFA801;
  color:white;
  border:none;
  border-radius: 5px;
  cursor:pointer;
  transition: 0.3s;
}

.register-button:hover {
  background-color: #df9205;
}

.view-album-button {
  width: 200px;
  height: 40px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);;
  border:none;
  margin-top: 20px;
  cursor: pointer;
  background-color: white;
  border-radius: 5px;
  transition:0.3s;
}

.view-album-button:hover {
  box-shadow: 0 8px 8px rgba(0, 0, 0, 0.1);;
}

#route-map {
    height: 100%;
}

/* Media query for smaller screens */
@media (max-width: 768px) {
  .event-detail {
    flex-direction: column;
  }

  .left-side {
    padding-right: 0;
  }

  .right-side {
    order: 1;
  }

  .sticky-container {
    margin-top: 20px;
    position: static;
  }
}
</style>
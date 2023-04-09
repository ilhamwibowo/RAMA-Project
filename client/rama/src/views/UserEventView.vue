<template>
    <div class="layout">
      <div class="header">
        <img src="eventHeader.png" alt="Header Image" />
        <h1 class="header-title">Where does running take you next?</h1>
      </div>

      <div class="event-list">
        <div
          v-for="event in events"
          :key="event.id"
          class="event-card"
          @click="goToEvent(event.raceId)"
        >
          <img :src="event.raceThumbnail" alt="Event Image" />
          <h3>{{ event.raceName }}</h3>
          <p>{{ event.startLocation }}</p>
            <p>{{ formatDate2(event.startTime) }}</p>
            <p>{{ event.distance }}</p>
            <div class = "registration-fee">
                <p>{{ event.registrationFee }}</p>
            </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>

  import axios from 'axios';

  export default {
    data() {
      return {
        events: [
          // Populate this array with your events
          // Example:
          {
            raceId: 1,
            raceName: 'JAWA TIMUR MARATHON SPECIAL',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: '2001-01-01T00:00:00',
            distance: '42KM',
            raceThumbnail: '3.jpg',
          },          {
            raceId: 2,
            raceName: 'Jawa Timur Marathon Festival',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: new Date(),
            distance: '42KM',
            raceThumbnail: '2.jpg',
          },
          {
            raceId: 3,
            raceName: 'Jawa Timur Marathon Festival',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: new Date(),
            distance: '42KM',
            raceThumbnail: '1.jpg',
          },          {
            raceId: 4,
            raceName: 'Jawa Timur Marathon Festival',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: new Date(),
            distance: '42KM',
            raceThumbnail: '4.jpg',
          },          {
            raceId: 3,
            raceName: 'Jawa Timur Marathon Festival',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: new Date(),
            distance: '42KM',
            raceThumbnail: '5.jpg',
          },          {
            raceId: 4,
            raceName: 'Jawa Timur Marathon Festival',
            startLocation: 'Bandung, Jawa Barat', 
            startTime: new Date(),
            distance: '42KM',
            raceThumbnail: '6.jpg',
          },
        ],
      };
    },  
    created() {
      const token = localStorage.getItem("token");

      // Configuration for API
      const config = {
          headers: { Authorization: `Bearer ${token}` }
      };

      axios.get(import.meta.env.VITE_API_URI + "/Race", config)
        .then(response => {
          console.log(response.data);
          this.events.push(...response.data.races);
        })
        .catch(error => {
          console.log(error);
        });

    },
    methods: {
      formatDate(date) {
        return new Intl.DateTimeFormat('en-US').format(date);
      },
      formatDate2(dateString) {
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
      goToEvent(eventId) {
        // Replace 'event-details' with the name of the route to the event details page
        this.$router.push({ name: 'userEventDetail', params: { id: eventId } });
      },
    },
  };
  </script>
  
  <style scoped>
  .layout {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin : 0 100px 0 100px;
  }
  
  .header {
  position: relative;
  width: 1100px;
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
    width: 100%;
  }

  .event-card {
    width: 350px;
    border: 1px solid #ccc;
    border-radius: 4px;
    padding: 12px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    cursor: pointer;
    text-align: left;
    background-color: #ECECEC;
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
  bottom: 20px; /* Adjust the value to set the distance from the bottom */
  left: 20px; /* Adjust the value to set the distance from the left */
  color: #000;
  font-size: 36px;
  font-weight: bold;
  text-align: left;
  font-family: "Montserrat", sans-serif;
  /* text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.6); */
}
.registration-fee {
    position: absolute;
  bottom: 0;
  right:0;
  width: 100%;
  font-size: 20px;
  color: #FFA801;
  text-align: right;
  margin-right: 20px;
  margin-bottom: 10px;
}

.registration-fee p {
    font-weight: bold;
}
  </style>
  
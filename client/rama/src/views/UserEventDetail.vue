<template>
    <div class="event-detail">
      <div class="left-side">
        <h1>{{ eventData.raceName }}</h1>
        <hr>
        <img class="event-image" :src="eventData.image" alt="Event Image" />
        <h2>About</h2>
        <p>{{ eventData.about }}</p>
        <h2>Route Map</h2>
        <div class="route-map">
          <!-- Insert map component or implementation here -->
        </div>
      </div>
      <div class="right-side">
        <div class="sticky-container">
          <div class="sticky-card">
            <h2>{{ eventData.raceName }}</h2>
            <div class="details">
              <p>{{eventData.startLocation}}</p>
              <p>{{formatDate(eventData.startTime)}}</p>
              <p>{{ eventData.distance }}</p>
              <p>{{ eventData.registrationFee }}</p>
            </div>
            <button class="register-button" @click="registerForEvent">Register</button>
          </div>
          <button class="view-album-button" @click="viewAlbum">View Album</button>
        </div>
      </div>
    </div>
  </template>
  
<script>
  export default {
    props: {
      eventId: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        eventData: {
          image: "../1.jpg",
          raceName: "Jawa Timur Marathon Festival",
          about: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas nec eros tincidunt, sodales dui quis, congue enim. Morbi tristique ex a lectus tempus, ut iaculis velit lobortis. Nullam sagittis nunc in metus mollis consequat. Praesent bibendum accumsan dolor, vel iaculis mi tincidunt ut. Praesent nec dolor non risus viverra dignissim. Nunc dui sem, lobortis sit amet risus non, euismod euismod purus. Etiam augue metus, lobortis ut ultrices quis, laoreet eu leo.",
          distance: "23km",
          registrationFee: "10000",
          startTime:new Date(),
        },
      };
    },
    created() {
      this.fetchEventData();
    },
    methods: {
      async fetchEventData() {
        // Fetch event data from your API or data source using this.eventId
        // For example:
        // const response = await axios.get(`/api/events/${this.eventId}`);
        // this.eventData = response.data;
  
        // Use the fetched data to populate eventData
      },
      registerForEvent() {
        // Implement registration logic here
      },
      viewAlbum() {
        // Implement navigation to the album page here
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
    margin:0 50px 0 50px;

  }
  
  .left-side {
    flex: 3;
    padding-right: 40px;
    text-align: left;
  }
  
  .right-side {
    flex: 1;
    
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
  </style>
  
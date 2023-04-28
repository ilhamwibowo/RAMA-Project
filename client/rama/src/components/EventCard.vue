<template>
    <div class="event-card" @click="goToEvent(event.raceId)">
      <img v-if="event.raceThumbnail" :src="event.raceThumbnail.url" alt="Event Image" />
      <h3>{{ event.raceName }}</h3>
      <p v-if="event.startLocation">{{ event.startLocation.name }}</p>
      <p>{{ formattedDate }}</p>
      <p>{{ event.distance }}</p>
      <div class="registration-fee">
        <p>{{ event.registrationFee }}</p>
      </div>
    </div>
  </template>
  <script>
  export default {
    props: {
      event: {
        type: Object,
        required: true
      }
    },
    computed: {
      formattedDate() {
        const date = new Date(this.event.startTime);
        const options = { month: 'long', day: 'numeric', year: 'numeric' };
        return date.toLocaleDateString('en-US', options);
      }
    },
    methods: {
      goToEvent(eventId) {
        // Replace 'event-details' with the name of the route to the event details page
        this.$router.push({ name: 'userEventDetail', params: { id: eventId } });
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
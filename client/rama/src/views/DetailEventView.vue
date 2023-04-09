<template>
    <div class="detail-event">
        <AdminSidebar class="sidebar"/>
        <div class="layout">
            <div class="edit-button-container">
                <button class="edit-button" @click="toggleForm()" >EDIT</button>
                <form v-if="showForm">
                   <!-- <div class="container-image">
                       <input
                           type="file"
                           id="image"
                           accept="image/*"
                           @change="changePhoto"
                       />
                       <img :src="previewImageUrl" />
                   </div> -->
                   <div class="form-grid">
                       <!-- Left column -->
                       <div class="grid-item">
                           <label for="name">Name</label>
                           <input type="text" id="name" v-model="name">
                           
                           <label for="city">City</label>
                           <input type="text" id="city" v-model="city" placeholder="TBI">
                           
                           <label for="start-date">Start Date</label>
                           <input type="datetime-local" id="start-date" v-model="startDate">
                           
                       </div>
                       
                       <!-- Right column -->
                       <div class="grid-item">
                           <label for="startlocation">Description</label>
                           <input type="text" id="startlocation" v-model="description">

                           <label for="latitude">Start Registration</label>
                           <input type="datetime-local" id="latitude" v-model="startRegis">

                           <label for="longitude">End Registration</label>
                           <input type="datetime-local" id="longitude" v-model="endRegis">

                       </div>
                       
                       <!-- Bottom row -->
                       <div class="grid-item">
                           <div class="row">
                               <div class="row-item">
                                   <label for="course-map">Course Map</label>
                                   <input type="text" id="course-map" v-model="courseMap" placeholder="TBI">
                               </div>

                           </div>           
                           

                       </div>

                       <div class="grid-item">
                           <div class="row">
                               <label for="isPublish">Published</label>
                               <input id="isPublish" type="checkbox" v-model="isPublish">
                           </div>
                       </div>

                       <div class="grid-item">
                           <div class="row">
                               <div class="row-item"> 
                                   <label for="distance"> Distance</label>
                                   <input type="number" id="distance" v-model="distance">
                               </div>

                               <div class="row-item"> 
                                   <label for="price">Price</label>
                                   <input type="number" id="price" v-model="price">
                               </div>
                           </div>
                       </div>

                       <!-- <div class="grid-item"></div> -->
                       <div class="grid-item">
                           <div class="button-container"> 
                               <button class="btn-cancel" @click="toggleForm">CANCEL</button>
                               <button class="btn-save" @click.prevent="saveEvent">SAVE</button>
                           </div> 
                       </div>                      
                       </div>
               </form>
                <div class="overlay" v-if="showForm"></div>
            </div>
                   <!-- END OF POP UP FORM -->
            <div class="delete-button-container">
                <button class="delete-button" @click="showDeleteForm = true">DELETE</button>
                <div class="delete-form-overlay" v-if="showDeleteForm">
                    <div class="delete-form">
                        <p>Are you sure you want to delete this event?</p>
                        <div class="delete-form-buttons">
                            <button class="delete-form-button cancel" @click="showDeleteForm = false">Cancel</button>
                            <button class="delete-form-button confirm" @click="deleteRace">Confirm</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="image-container">
                <img src="../../public/contohGambar.png" alt="GambarEvent" id="race-photo">
            </div>
            <div class="information-container">
                <div class="row-one">
                    <div class="race-name">
                        <label class="label-race-name">Name</label>
                        <div class="race-name-container">
                            <p v-text="this.event.raceName"></p>
                        </div>
                    </div>
                    <div class="race-date">
                        <label class="label-race-date">Date</label>
                        <div class="race-date-container">
                            <p v-text="this.event.startTime?.slice(0,10)"></p>
                        </div>
                    </div>
                </div>
                <div class="row-two">
                    <div class="race-province">
                        <label class="label-race-province">Province</label>
                        <div class="race-province-container">
                            <p v-text="this.event.startLocation?.name"></p>
                        </div>
                    </div>
                    <div class="race-city">
                        <label class="label-race-city">City</label>
                        <div class="race-city-container">
                            <p v-text="this.event.startLocation?.name"></p>
                        </div>
                    </div>
                </div>
                <div class="row-three">
                    <div class="race-start-regis">
                        <label class="label-race-start-regis">Start Date Registration</label>
                        <div class="race-start-regis-container">
                            <p v-text="this.event?.StartDateRegistration"></p>
                        </div>
                    </div>
                    <div class="race-end-regis">
                        <label class="label-race-end-regis">End Date Registration</label>
                        <div class="race-end-regis-container">
                            <p v-text="this.event?.EndDateRegistration"></p>
                        </div>
                    </div>
                </div>
            </div>
            <h2>Course Map</h2>
            <div class="maps">
                <img src="/maps.png" alt="maps maraton" id="maps-image" />
            </div>
        </div>
    </div>
</template>


<script>
import axios from 'axios';
import AdminSidebar from '../components/AdminSidebar.vue';
export default {
    name: "detailEvent",
    components: {
        AdminSidebar
    },
    data(){
        return {
            event: [],
            id :this.$route.params.id,
            isRegistered: false,
            isPublished: true,
            showForm: false,
            name: "", 
            city: "", 
            startDate: "",
            startLocation: "",
            latitude: "",
            longitude: "",
            courseMap: "",
            isOpen: false,
            isPublish:false,
            previewImageUrl: "",
            showDeleteForm : false
        };
    },

    methods: {
        async getData(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.event = response.data;
                    // for debug
                    // console.log(this.event);
                    // console.log(this.events[0].raceName);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        }, toggleForm() {
            this.showForm = !this.showForm;
            if (this.showForm) {
                // add event listener to close form on escape key press
                document.addEventListener("keydown", this.handleEscapeKey);
            } else {
                // remove event listener when form is closed
                document.removeEventListener("keydown", this.handleEscapeKey);
            }
        }, handleEscapeKey(event) {
            if (event.keyCode === 27) {
                this.showForm = false;
            }
        },deleteRace(){
            // console.log("event di delete")
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios.delete(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.$router.push("/event");
                    // for debug
                    // console.log(this.event);
                    // console.log(this.events[0].raceName);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        }
    },
    mounted(){
        this.getData();
    }
}
</script>
<style scoped>

.detail-event{
    height: 100%;
    width: 100%;
    position: relative;
    display: grid;
    grid-template-columns: 333px auto;
    grid-template-areas: 
    "sidebar main";
}
.sidebar {
    grid-area: sidebar;
} 
.layout {
    grid-area: main;
    background: #fff;
    width: 100%;
    margin: 30px 20px 30px 20px;
    position: absolute;
    justify-content: center;
    border-radius: 15px;
}

.edit-button {
    position: absolute;
    right: 2%;
    margin-top: 2%;
    height: 2rem;
    width: 4.5rem;
    background: #000;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
}

.delete-button-container{
    position: absolute;
    right: 2%;
    margin-top: 5%;
}

.delete-button{
    height: 2rem;
    width: 4.5rem;
    background: rgb(234, 54, 54);
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    /* letter-spacing: 2px; */
    color: #fff;
}
.image-container {
    margin-top: 2%;
    height: 20%;
    width: 60%;
    left: 20%;
}

#race-photo{
    height: auto;
    width: 50%;
}
.information-container {
    margin-top: 2%;
    width: 100%;
    height: auto;
    margin-bottom: 2%;
}

.row-one,
.row-two,
.row-three {
    width: 100%;
    height: auto;
    display: flex;
    align-items: center;
    justify-content: center;
}

.race-name,
.race-date,
.race-province,
.race-city,
.race-start-regis,
.race-end-regis {
    width: 50%;
    height: auto;
    text-align: left;
}

.race-name-container,
.race-province-container,
.race-start-regis-container {
    background: #fff;
    border-radius: 15px;
    border: 2px solid #000;
    width: 50%;
    height: 100%;
    left: 45%;
    height: 2rem;
}

.race-date-container,
.race-city-container,
.race-end-regis-container {
    background: #fff;
    border-radius: 15px;
    border: 2px solid #000;
    width: 50%;
    height: 2rem;
    left: 5%;
}

p {
    color: #000;
    left: 5%;
    bottom: 6%;
    font-size: 1.25rem;
}

label {
    font-size: 1.25rem;
    color: #000;
}
.label-race-name,
.label-race-province,
.label-race-start-regis {
    left: 46%;
}

.label-race-date,
.label-race-city,
.label-race-end-regis {
    left: 6%;
}

h2 {
    font-size: 1.25rem;
    color: #000;
}

.category-container {
    width: 50%;
    left: 25%;
    margin-bottom: 2.5rem;
}

.category-table {
    border-collapse: collapse;
    text-align: center;
    margin-bottom: 1rem;
    width: 100%;
    table-layout: fixed;
    color: #000;
}

.table-row-header {
    border-bottom: 2px solid #272626;
    font-family: "Montserrat", sans-serif;
    font-size: 1.25rem;
}

.table-row-body {
    font-size: 1.25rem;
}

.maps {
    margin-bottom: 10%;
    height: 30%;
}

#maps-image{
    width: 40%;
    height: auto;
    border-radius: 15px;
}

form {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: white;
  padding: 20px;
  z-index: 999;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  text-align: left;
}

.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 998;
}

.form-grid {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    grid-column-gap: 20px;
    grid-row-gap: 20px;

  }
  
.form-grid .grid-item {
    display: grid;
    grid-template-columns: 1fr;
    grid-row-gap: 10px;
}
  

  /* Optional styling for labels and inputs */
label {
    font-weight: bold;
}
  
input {
    width: 100%;
    padding: 5px;
    border-radius: 5px;
    border: 1px solid #ccc;
  }

  .form-grid input[type=text], 
  .form-grid input[type=number],
  .form-grid input[type=datetime-local] {
    border: 2px solid #5f5f5f;
    padding: 5px 10px;
    width: 100%;
    border-radius: 20px;
    border: 2px solid grey;
    color: rgb(63, 62, 62);
    font-size: 20px;
  }

  .form-grid label {
    margin-left:10px;
  }
  
.form-grid button {
    width: 100px;
    height: 40px;
    padding: 10px;
    border-radius: 30px;
    cursor: pointer;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    transition: background-color 0.2s;
    margin: 5px;
}

.btn-save {
    background-color: #1f2220;
    color: #fff;
}
.btn-cancel {
    background-color: transparent;
    color: #000000;
    border:2px solid grey;
}
.btn-save:hover  {
  background-color: #171817;
}
.btn-cancel:hover {
    background-color: #b5b9b5
}
.row {
  display: flex;
  align-items: center;
  flex-wrap: nowrap;
  gap:10px;
}

.button-container {
    display: flex;
    justify-content: flex-end;
    align-items: right;
    flex-wrap: nowrap;
    gap:10px;
}
.row label {
  margin-right: 10px;
}
.row input {
  flex-grow: 1;
}


.delete-form-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 1000;
  display: flex;
  align-items: center;
  justify-content: center;
}

.delete-form {
  width: 300px;
  height: 150px;
  background-color: white;
  border-radius: 5px;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.delete-form p {
  margin-bottom: 20px;
}

.delete-form-buttons {
  display: flex;
  align-items: center;
  justify-content: center;
}

.delete-form-button {
  width: 100px;
  height: 40px;
  border-radius: 20px;
  margin: 0 10px;
  font-weight: bold;
  cursor: pointer;
  outline: none;
  border: none;
}

.delete-form-button.cancel {
  background-color: lightgray;
  color: black;
}

.delete-form-button.confirm {
  background-color: red;
  color: white;
}

</style>
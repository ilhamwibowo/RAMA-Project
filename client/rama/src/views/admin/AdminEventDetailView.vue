<template>
    <div class="detail-event">
        <!-- Toast -->
        <Transition name="toast">
            <Toast v-if="showToastSuccess" type="success" :message="message"/>
        </Transition>
        <Transition name="toastError">
            <Toast v-if="showToastError" type="error" :message="message"/>
        </Transition> 

        <AdminSidebar class="sidebar"/>
        <AdminEventDetailEdit v-if="showForm" :event="event"/>
        <div class="layout">
            <div class="tabs">
                <RouterLink class="tab page" :to="{params: {id: this.id}, name: 'detailEvent'}">Detail Event</RouterLink> 
                <RouterLink class="tab" :to="{params: {id: this.id}, name: 'EventRegistrant'}">Registrant</RouterLink> 
                <RouterLink class="back" to="/event" ><img src="/left-arrow.png"></RouterLink>
            </div>
            <div class="body">
                <div class="buttons-container">
                    <div class="edit-button-container">
                        <button class="edit-button" @click="toggleForm">EDIT</button> 
                    </div>
                        <!-- END OF POP UP FORM -->
                    <div class="delete-button-container">
                        <button class="delete-button" @click="showDeleteForm = true">DELETE</button>
                        <div class="delete-form-overlay" v-if="showDeleteForm">
                            <div class="delete-form">
                                <p>Are you sure you want to delete this event?</p>
                                <div class="delete-form-buttons">
                                    <button class="cancel" @click="showDeleteForm = false">Cancel</button>
                                    <button class="confirm" @click="deleteRace">Confirm</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="image-container">
                    <input
                        type="file"
                        id="image"
                        accept="image/*"
                        @change="changePhoto"
                        style="display: none"
                    />
                    <img id="race-photo" :src="basePreviewImageUrl" />
                    <div class="edit-image" @click="uploadPhoto">
                        <div class="circle"></div>
                    </div>
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
                                <p v-text="this.event.startDateRegistration"></p>
                            </div>
                        </div>
                        <div class="race-end-regis">
                            <label class="label-race-end-regis">End Date Registration</label>
                            <div class="race-end-regis-container">
                                <p v-text="this.event.endDateRegistration"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <h2>Course Map</h2>
                <div id="maps">
                    <!-- <img src="/maps.png" alt="maps maraton" id="maps-image" /> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import axios from 'axios';
import { Loader } from "@googlemaps/js-api-loader";

import AdminSidebar from '@/components/AdminSidebar.vue';
import AdminEventDetailEdit from '@/components/AdminEventDetailEdit.vue';
import Toast from '@/components/Toast.vue';

let map;
let loader;

export default {
    name: "detailEvent",
    components: {
        AdminSidebar,
        AdminEventDetailEdit,
        Toast
    },
    data(){
        return {
            event: [],
            album: [],
            id :this.$route.params.id,
            showForm: false,
            isOpen: false,
            previewImageUrl: "",
            basePreviewImageUrl:"", 
            showDeleteForm : false,
            name: "", 
            city: "", 
            startDate: "",
            startLocation: "",
            latitude: "",
            longitude: "",
            courseMap: "",
            isPublish:false,
            price: "",
            description: "",
            startRegis: "",
            endRegis: "",
            albumId: "",
            photo:Object,
            showToastError: false,
            showToastSuccess: false,
            message: "",
        };
    },

    methods: {
        changePhoto(event) {
            const image = event.target.files[0];
            this.photo = image;
            this.previewImageUrl = URL.createObjectURL(image);
        },
        async getData(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            await axios
            .get(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
            .then((response) => {
                if (response.status !== 200){
                    console.log(response);
                } else {
                    this.event = response.data;
                    console.log(typeof(this.event));
                    this.name = this.event.raceName;
                    this.description = this.event.raceDesc;
                    this.city = this.event.startLocation.name;
                    this.startRegis = this.event.startDateRegistration;
                    this.endRegis = this.event.endDateRegistration;
                    this.courseMap = "-6.898906,107.612737;-6.898922,107.613627";
                    this.startDate = this.event.startTime;
                    this.isPublish = this.event.isPublished;
                    this.distance = this.event.distance;
                    this.price = this.event.registrationFee;
                    this.previewImageUrl = this.event.raceThumbnail.url;
                    this.basePreviewImageUrl = this.event.raceThumbnail.url;
                }
            })
            .catch((err) => {
                console.log(err);
            });
        }, 
        loadMap() {
            let processedPoints = this.courseMap.split(";");
            console.log("Course map: " + this.courseMap);
            console.log(processedPoints);
            
            loader = new Loader({
                apiKey: import.meta.env.VITE_MAPS_API_KEY,
                version: "weekly",
            });

            loader.load().then(async () => {
                const { Map } = await google.maps.importLibrary("maps");

                map = new Map(document.getElementById("maps"), {
                    center: { lat: parseFloat(processedPoints[0].split(",")[0]), lng: parseFloat(processedPoints[0].split(",")[1]) },
                    zoom: 17,
                });
            });
            
            this.drawRoute(processedPoints);
        },
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
        toggleForm() {
            console.log("Tes")
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
        },async deleteRace(){
            const token = localStorage.getItem("token");
            var status;

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            await axios.delete(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
            .then((response) => {
                status = response.status;
            })
            .catch((err) => {
                status = err.response.status;
                console.log(err);
            });

            // Status Alert
            console.log(status);
            if (status === 200) {
                this.message = "Data has been deleted!"
                this.showToastSuccess = true; 
                clearTimeout();
                setTimeout(() => {
                    this.showToastSuccess = false
                }, 3000);
                setTimeout(() => {
                    this.$router.push("/event");
                }, 3500);
            }
            else if (status === 400) {
                this.message = errorMsg;
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            } else {
                this.message = "Sorry, there is an error on the server"
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            }

        }, async getAlbum(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/Album", config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.album = response.data.albums;
                }
            })
            .catch((err) => {
                console.log(err);
            });
        }
    },
    async mounted(){
        await this.getData();
        await this.getAlbum();
        await this.loadMap();
    }
}
</script>
<style scoped>

.detail-event{
    height: 100%;
    position: relative;
    display: grid;
    grid-template-columns: 333px auto;
    grid-template-areas: 
    "sidebar main";
    min-height: 100vh;
}
.sidebar {
    grid-area: sidebar;
    position: fixed;
    height: 100%;
} 
.layout {
    grid-area: main;
    display: grid;
    margin:40px 50px 40px 40px;
    grid-template-rows: 70px auto;
    row-gap: 20px;
    grid-template-areas: 
    "tabs"
    "body"
}
.tabs {
    grid-area: tabs;
    display: flex;
    column-gap: 100px;
    align-items: center;
    background: #fff;
    padding: 5px 20px 10px 80px;
    width: 100%;
    height: 100%;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-size: 30px;
}

.tabs .tab {
    text-decoration: none;
    color: black;
}

.page {
    font-weight: 700;
}

.back {
    position: absolute;
    top: 15px;
    right: 25px;
    background-color: rgba(0,0,0,0);
    border: none;
    cursor: pointer;
}

.body {
    grid-area: body;
    background: #fff;
    padding: 30px 20px 30px 20px;
    width: 100%;
    justify-content: center;
    border-radius: 15px;
}
.buttons-container {
    position: absolute;
    right: 50px;
    display: flex;
    column-gap: 10px
}
.edit-button {
    height: 30px;
    width: 100px;
    background: #000;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
    cursor:pointer;
    transition:0.3s;
}
.edit-button:hover {
    background-color: #1d1c1c;
}

.delete-button-container{
    width: 100%;
}

.delete-button{
    height: 30px;
    width: 100px;
    background: rgb(234, 54, 54);
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    /* letter-spacing: 2px; */
    color: #fff;
    cursor:pointer;
    transition: 0.3s;
}

.delete-button:hover {
    background-color: rgb(216, 50, 50);
}
.image-container {
    margin-top: 2%;
    width: 60%;
    height: 20%;
    text-align: center;
    margin-left: auto;
    margin-right: auto;
}

#race-photo{
    max-width: 1024px;
    height: 100%;
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
    font-size: 20px;
    font-family: 'Darker Grotesque';
}

label {
    font-size: 24px;
    color: #000;
    font-family: 'Darker Grotesque';
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
    font-size: 24px;
    color: #000;
    text-align:center;
    font-family: 'Darker Grotesque';
}

#maps {
    margin-bottom: 10%;
    height: 50%;
    text-align: center;
}

#maps-image{
    width: 40%;
    height: auto;
    border-radius: 15px;
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

#image {
    border: 2px solid #5f5f5f;
    padding: 5px 10px;
    width: 100%;
    padding: 10px;
    margin-bottom: 20px;
    border-radius: 20px;
    border: 2px solid grey;
    color: rgb(63, 62, 62);
    font-size: 20px;
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


#image {
  position: absolute;
  opacity: 0;
  max-width: 100%;
  height: 100%;
  cursor: pointer;
}

.container-image img {
    max-height: 250px;
    max-width: 100%;
}

img {
    display: inline;
    margin: 0 auto;
    height: 100%;
    max-width: auto;
}

.edit-image {
    position: absolute;
    width: 150px;
    height: 150px;
}
.circle {
    background-color: #d3c5c5;
    width: 100%;
    height: 100%;
    opacity: 0;
    transition: 0.3s;
}

.circle:hover {
    opacity: 0.3;
}

/** Toast */
.toast-enter-from,
.toast-leave-to {
    opacity: 0;
    transform: translateY(-60px);
}
.toast-enter-to,
.toast-leave-from {
    opacity: 1;
    transform: translateY(0px);
}
.toast-enter-active,
.toast-leave-active {
    transition: all 0.3s ease;
}

.toastError-enter-from,
.toastError-leave-to {
    opacity: 0;
    transform: translateY(-60px);
}
.toastError-enter-to,
.toastError-leave-from {
    opacity: 1;
    transform: translateY(0px);
}
.toastError-enter-active {
    animation: wobble 0.5s;
}
.toastError-leave-active {
    transition: all 0.3s ease;
}

@keyframes wobble {
    0% { transform: translateY(-60px); opacity: 0; }
    50% { transform: translateY(0px); opacity: 1; }
    60% { transform: translateX(8px);}
    70% { transform: translateX(-8px);}
    80% { transform: translateX(4px);}
    90% { transform: translateX(-4px);}
    100% { transform: translateX(0px);}
}

</style>
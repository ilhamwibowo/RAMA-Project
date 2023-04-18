<template>
    <div class="layout">
        <AdminSidebar class="sidebar"/>
        <div class="main">
            <div class="header">
                <div class="title-page-container">
                    <h1 class="title-page">EVENTS</h1>
                </div>
                <div class="button-add-container">
                    <button class="add-button" @click="toggleForm">ADD</button>
                
                <!-- POP UP FORM FOR CREATING NEW EVENT -->
                <form v-if="showForm">
                   <div class="container-image">
                       <input
                           type="file"
                           id="image"
                           accept="image/*"
                           @change="changePhoto"
                       />
                       <img :src="previewImageUrl" />
                   </div>
                   <div class="form-grid">
                       <!-- Left column -->
                       <div class="grid-item">
                           <label for="name">Name</label>
                           <input type="text" id="name" v-model="name">
                           
                           <label for="city">City</label>
                           <input type="text" id="city" v-model="city">
                           
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

               <!-- END OF POP UP FORM -->
                </div>
            </div>
            <div class="table-container">
                <table class="event-table">
                    <thead class="table-head">
                        <tr class="table-row-header">
                            <th class="table-header" scope="col">Title</th>
                            <th class="table-header" scope="col">Location</th>
                            <th class="table-header" scope="col">Date</th>
                            <!-- <th class="table-header" scope="col">Category</th> -->
                            <th class="table-header" scope="col">Registration</th>
                            <th class="table-header" scope="col">Status</th>
                            <th class="table-header" scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody class="table-body">
                        <tr class="table-row-body" v-for="(event) in events" :key="event.raceId" >
                            <td class="table-data" v-text="event.raceName"></td>
                            <td class="table-data" v-text="event.startLocation?.name"></td>
                            <td class="table-data" v-text="event.startTime.slice(0,10)"></td>
                            <!-- <td class="table-data">h</td> -->
                            <td class="table-data">
                                <p class="open-regis" id="open-regis" v-if="event.isOpened">Opened</p>
                                <p class="close-regis" id="close-regis" v-if="!event.isOpened">Closed</p>
                            </td>
                            <td class="table-data">
                                <p class="status-publish" id="published" v-if="event.isPublished">Published</p>
                                <p class="status-publish-not" id="notPublished" v-if="!event.isPublished">Unpublished</p>
                            </td>
                            <td class="table-data">
                                <button class="detail-button">
                                    <router-link :to="{params: {id : event.raceId}, name: 'detailEvent'}">
                                        Detail
                                    </router-link>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '@/components/AdminSidebar.vue';
export default {
    name: "EventView",
    data() {
        return {
            events: [],
            isRegistered: false,
            isPublished: false,
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
            category: "",
            distance: "",
            price: "",
            previewImageUrl: "",
            description: "",
            startRegis: "",
            endRegis: "",
            profilePhoto:Object
        };
    },
    components: {
        AdminSidebar
    },
    methods: {
        async getEvent() {
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/Race", config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.events = response.data.races;
                }
            })
            .catch((err) => {
                console.log(err);
            });
        },
        // This method is for creating a new event which is used 
        // in the "Save" button on the form
        saveEvent() {
            // this.uploadphoto();
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}`, 'Content-Type': 'multipart/form-data' }
            };

            // Convert local time to UTC time
            const startTime = new Date(this.startDate).toISOString();

            //convert start registration date to object format
            const regisDate = new Date(this.startRegis);
            var yearRegis = regisDate.getFullYear();
            var monthRegis = regisDate.getMonth() + 1;
            var dayRegis = regisDate.getDate();
            const startRegistration = yearRegis+"-"+monthRegis+"-"+dayRegis;
            console.log

            //convert end registration date to object format
            const EndDate = new Date(this.endRegis);
            var yearEnd = EndDate.getFullYear();
            var monthEnd = EndDate.getMonth() + 1;
            var dayEnd = EndDate.getDate();
            const endRegistration = yearEnd+"-"+monthEnd+"-"+dayEnd;


            // console.log(myDate)
            //ini baru bagian yang wajib diisi
            let formData = new FormData();
            formData.append('RaceName', this.name);
            formData.append('RaceDesc', this.description);
            formData.append('StartLocation[name]', this.city);
            formData.append('StartTime', startTime);
            formData.append('Distance', this.distance);
            formData.append('RegistrationFee', this.price);
            formData.append('isPublished', this.isPublish);
            formData.append('StartDateRegistration', startRegistration);
            formData.append('EndDateRegistration', endRegistration);
            formData.append('file', this.profilePhoto)
            // formData.append('isOpened', this.isOpen);
            
            console.log([...formData]);
            axios
            .post(import.meta.env.VITE_API_URI + "/Race", formData, config)
            .then((response) => {
                alert("Success");
                this.showForm = false;

                // Clear input valeus.
                this.name = "", 
                this.city = "", 
                this.startDate = "",
                this.startLocation = "",
                this.latitude = "",
                this.longitude = "",
                this.courseMap = "",
                this.isOpen = false,
                this.isPublish = false,
                this.category = "",
                this.distance = "",
                this.price = "",
                this.previewImageUrl = "",
                this.description = "",
                this.startRegis = "",
                this.endRegis = "",
                console.log(response.data);
            })
            .catch((err) => {
                console.log(err);
            });
        }, toggleForm() {
            this.showForm = !this.showForm;
            if (this.showForm) {
                // add event listener to close form on escape key press
                // document.addEventListener("keydown", this.handleEscapeKey);
            } else {
                // remove event listener when form is closed
                // document.removeEventListener("keydown", this.handleEscapeKey);
            }
        },handleEscapeKey(event) {
            if (event.keyCode === 27) {
                this.showForm = false;
            }
        },
        changePhoto(event) {
        const image = event.target.files[0];
        this.profilePhoto = image;
        this.previewImageUrl = URL.createObjectURL(image);
    },
    uploadphoto() {
        const token = localStorage.getItem("token");
        // Configuration for post api
        const configPhoto = {
            headers: {
                Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data"
                }
            };

        // Create FormData file for post api
        var formData = new FormData();
        formData.append("file", this.previewImage);

        // Axios Post
        axios
            .post(import.meta.env.VITE_API_URI + "/User/add-photo", formData, configPhoto)
            .then((response) => (console.log(this.profilePhoto = response.data)))
            .catch((err) => console.log(err));
    },
    },
    
    mounted() {
        this.getEvent();
    }
};
</script>

<style scoped>
.layout {
    height: 100%;
    width: 100%;
    display: grid;
    grid-template-columns: 333px auto;
    grid-template-areas: 
    "sidebar main";
    min-height: 100vh;
}

.sidebar {
    grid-area: sidebar;
}

.main {
    grid-area: main;
}

/********************** FORMS ****************/
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

.container-image {
    width: 100%;
    text-align: center;
}

img {
    max-width: 400px;
    max-height:200px;
}

/********************** Base (Table and else) ****************/
.sidebar {
    grid-area: sidebar;
}

.main {
    grid-area: main;
}

.header {
    width: 100%;
    height: 10%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-top: 1%;
    margin-bottom: 1%;
}

.title-page-container {
    width: 50%;
    height: 100%;
    text-align: left;
}

.title-page {
    left: 20%;
    height: 100%;
}
.button-add-container {
    width: 50%;
    height: 100%;
    text-align: right;
}
.add-button {
    width: 10%;
    height: 80%;
    right: 20%;
    top: 10%;
    background: #353642;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
}

.add-button:hover {
    background-color: #171817;
}

.table-container{
    width: 80%;
    left: 10%;
    background: #fff;
    border-radius: 10px;
}

.event-table {
    border-collapse: collapse;
    text-align: center;

    margin-bottom: 1rem;
    color: #000;
    padding: 20px;

    width: 100%;
    table-layout: fixed;
}

.table-row-header {
    border-bottom: 2px solid #272626;
    font-family: "Montserrat", sans-serif;
    font-size: 120%;
}

.table-header {
    width: 100% / 7;
}

.open-regis {
    background: #72e48b;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
}

.close-regis {
    background: #ec7b7b;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
}

.status-publish {
    background: #72e48b;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
}

.status-publish-not {
    background: #ec7b7b;
    border-radius: 15px;
    left: 25%;
    width: 60%;
    font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
}

.detail-button {
    width: 50%;
    background: #deddd8;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
}
</style>
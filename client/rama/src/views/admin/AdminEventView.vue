<template>
    <div class="layout">
        <!-- Toast -->
        <Transition name="toast">
            <Toast v-if="showToastSuccess" type="success" :message="message"/>
        </Transition>
        <Transition name="toastError">
            <Toast v-if="showToastError" type="error" :message="message"/>
        </Transition> 

        <AdminSidebar class="sidebar" event="true"/>
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
                                   <input type="text" id="course-map" v-model="courseMap" placeholder="-33.8667,151.19;60.170880,24.94279">
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
                               <button class="btn-save" @click.prevent="validationInput">SAVE</button>
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
                            <td class="table-data-name" v-text="event.raceName"></td>
                            <td class="table-data-location" v-text="event.startLocation?.name"></td>
                            <td class="table-data-time" v-text="event.startTime.slice(0,10)"></td>
                            <!-- <td class="table-data">h</td> -->
                            <td class="table-data-regis">
                                <p class="open-regis" id="open-regis" v-if="event.isOpened">Opened</p>
                                <p class="close-regis" id="close-regis" v-if="!event.isOpened">Closed</p>
                            </td>
                            <td class="table-data-status">
                                <p class="status-publish" id="published" v-if="event.isPublished">Published</p>
                                <p class="status-publish-not" id="notPublished" v-if="!event.isPublished">Unpublished</p>
                            </td>
                            <td class="table-data-action">
                                <button class="detail-button">
                                    <router-link :to="{params: {id : event.raceId}, name: 'detailEvent'}">
                                        Detail
                                    </router-link>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <pagination
                    class="pagination"
                    :totalPage="totalPage"
                    :pager="pager"
                    :page="page"
                    @changePage="updatePage"
                    @changePager="updatePager"
                />
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '@/components/AdminSidebar.vue';
import Toast from '@/components/Toast.vue';
import Pagination from '@/components/Pagination.vue';
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
            profilePhoto:Object,
            showToastError: false,
            showToastSuccess: false,
            message: "",
            // for pagination
            totalEvent: 0,
            totalPage: 1,
            page: 1,
            pager: 10,
            eventShow: [],
        };
    },
    components: {
        AdminSidebar,
        Toast,
        Pagination,
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
                    this.totalEvent = this.events.length;
                    this.updatePage(this.page);
                    this.updatePager(this.pager);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        },

        // This method is for creating a new event which is used 
        // in the "Save" button on the form
        validationInput() {
            const reMap = /^-?\d+(\.\d+)?,-?\d+(\.\d+)?(;-?\d+(\.\d+)?,-?\d+(\.\d+)?)*$/
            try {
                if (this.name === "") throw "Name";
                if (this.city === "") throw "City";
                if (this.startDate === "") throw "Start date";
                if (this.distance === "") throw "Distance";
                if (this.price === "") throw "Price";
                if (this.description === "") throw "Description";
                if (this.startRegis === "") throw "Start registration date";
                if (this.endRegis === "") throw "End registration date";
                if (!this.courseMap.match(reMap)) throw "Course map";
                if (this.profilePhoto === null) throw "Image";
                this.saveEvent()
            }
            catch (err) {
                this.message = err + " is empty";
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false;
                }, 3000)
            }
        },

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

            //ini baru bagian yang wajib diisi
            let formData = new FormData();
            formData.append('RaceName', this.name);
            formData.append('RaceDesc', this.description);
            formData.append('StartLocation[name]', this.city);
            formData.append('StartTime', startTime);
            formData.append('Distance', this.distance);
            formData.append('RegistrationFee', this.price);
            formData.append("Points", this.courseMap);
            formData.append('isPublished', this.isPublish);
            formData.append('StartDateRegistration', startRegistration);
            formData.append('EndDateRegistration', endRegistration);
            formData.append('file', this.profilePhoto)
            
            console.log([...formData]);
            axios
            .post(import.meta.env.VITE_API_URI + "/Race", formData, config)
            .then((response) => {
                console.log(response.data);

                // Alert message
                this.message = "Data has been saved!";
                this.showToastSuccess = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastSuccess = false;
                }, 3000);
                setTimeout (() => {
                    // Reload when success
                    this.showForm = false;
                    location.reload()
                    this.clearForm();
                }, 3500)
                
            })
            .catch((err) => {
                console.log(err);

                // Alert message
                if (err.response.status === 400) {
                    this.message = err.response.data;
                } else {
                    this.message = "Sorry, there is an error on the server";
                }
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false;
                }, 3000);
            });
        }, 

        // clear input values.
        clearForm() {
            this.name = "";
            this.city = "";
            this.startDate = "";
            this.startLocation = "";
            this.latitude = "";
            this.longitude = "";
            this.courseMap = "";
            this.isOpen = false;
            this.isPublish = false;
            this.category = "";
            this.distance = "";
            this.price = "";
            this.previewImageUrl = "";
            this.description = "";
            this.startRegis = "";
            this.endRegis = "";
        },
        toggleForm() {
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
        //for pagination
        updatePage(n) {
            this.page = n;

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.eventShow = this.events.slice(start,end);
        },
        updatePager(n) {
            this.pager = n;
            this.totalPage = Math.ceil(this.totalEvent / this.pager);

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.eventShow = this.events.slice(start,end);
        }
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
    margin-left: 50px;
    height: 100%;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    font-size: 48px;
}
.button-add-container {
    width: 50%;
    height: 100%;
    text-align: right;
}
.add-button {
    right: 13%;
    top: 25px;
    background: #353642;
    border: 1px solid grey;
    border-radius: 15px;
    font-size: 20px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 1px;
    display: inline-block;
    padding: 3px 10px;
    color: white;
}

.add-button:hover {
    background-color: #171817;
}

.table-container{
    width: 90%;
    background: #fff;
    border-radius: 10px;
    margin-left: 50px;
    margin-top: 10px;
    padding: 20px 20px 10px 20px;
}

.event-table {
    border-collapse: collapse;
    text-align: center;

    color: #000;
    padding: 20px;

    width: 100%;
    /* table-layout: fixed; */
}

.table-row-header {
    border-bottom: 2px solid #272626;
    font-family: 'Darker Grotesque';
    font-size: 24px;
}

.table-header {
    width: 14.285%;
}

.table-row-body{
    text-align: center;
    font-size: 20px;
    font-family: 'Darker Grotesque';
    border-bottom: 2px solid #272626;
}

td{
    padding: 8px;
}

.table-data-name{
    text-align: left;
    left: 5px;
}
.table-data-location{
    text-align: left;
    left: 5px;
}
.open-regis {
    background: #72e48b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    display: inline-block;
    padding: 0 10px 0 10px;
    font-size: 20px;
}

.close-regis {
    background: #ec7b7b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    display: inline-block;
    padding: 0 10px 0 10px;
    font-size: 20px;
}

.status-publish {
    background: #72e48b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    display: inline-block;
    padding: 0 10px 0 10px;
    font-size: 20px;
}

.status-publish-not {
    background: #ec7b7b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    display: inline-block;
    padding: 0 10px 0 10px;
    font-size: 20px;
}

.detail-button {
    width: 50%;
    background: #deddd8;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 1px;
    font-size: 20px;
}

.pagination{
    justify-content: end;
    margin-top: 15px;
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
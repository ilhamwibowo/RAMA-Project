<template>
    <div>
        <!-- Toast -->
        <Transition name="toast">
            <Toast v-if="showToastSuccess" type="success" :message="message"/>
        </Transition>
        <Transition name="toastError">
            <Toast v-if="showToastError" type="error" :message="message"/>
        </Transition> 

        <form>
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
                    <input type="text" id="name" v-model="name" >
                    
                    <label for="city">City</label>
                    <input type="text" id="city" v-model="city">
                    
                    <label for="start-date">Start Date</label>
                    <input type="datetime-local" id="start-date" v-model="startDate">

                    <label for="course-map">Course Map</label>
                    <input type="text" id="course-map" v-model="courseMap" placeholder="-33.8667,151.19;60.170880,24.94279">
                    
                </div>
                
                <!-- Right column -->
                <div class="grid-item">
                    <label for="startlocation">Description</label>
                    <input type="text" id="startlocation" v-model="description">

                    <label for="latitude">Start Registration</label>
                    <input type="date" id="latitude" v-model="startRegis">

                    <label for="longitude">End Registration</label>
                    <input type="date" id="longitude" v-model="endRegis">

                </div>
                
                <!-- Bottom row -->
                <div class="grid-item">
                    <div class="row">
                        <div class="row-item">
                            <label for="albumid">Album</label>
                            <select id="albumid" v-model="albumId">
                                <option value="">Pilih Album</option>
                                <option v-for="album in this.albums" :key="album.albumId" :value="album.albumId">{{ album.albumName }}</option>
                            </select>
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
                            <input type="number" id="price" v-model="price" >
                        </div>
                    </div>
                </div>

                <!-- <div class="grid-item"></div> -->
                <div class="grid-item">
                    <div class="button-container"> 
                        <button class="btn-cancel" >CANCEL</button>
                        <button class="btn-save" @click.prevent="validationInput">SAVE</button>
                    </div> 
                </div>                      
                </div>
        </form>
        <div class="overlay"></div>
    </div>    
</template>

<script>
import axios from 'axios';
import Toast from '@/components/Toast.vue';
export default {
    name: "AdminEventDetailEdit",
    props: {
        event: Object
    },
    components: {
        Toast
    },
    data() {
        return {
            albums: [],
            id: "",
            photo: "",
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
            distance: 0,
            price: 0,
            previewImageUrl: "",
            basePreviewImageUrl: "",
            showToastError: false,
            showToastSuccess: false,
            message: "",
        }
    },
    methods: {
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
                if (this.photo === "") throw "Image";
                console.log(this.photo);
                this.saveEvent();
            }
            catch (err) {
                this.message = err + " is empty";
                console.log(this.message);
                this.showToastError = true;
                console.log(this.showToastError);
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false;
                }, 3000)
            }
        },
        saveEvent() {
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
            if(this.albumId != ""){
                formData.append('AlbumId', this.albumId);
            }
            formData.append('StartDateRegistration', startRegistration);
            formData.append('EndDateRegistration', endRegistration);
            formData.append('file', this.photo);

            console.log(this.albumId);
            
            console.log([...formData]);
            axios
            .put(import.meta.env.VITE_API_URI + "/Race/" + this.id, formData, config)
            .then((response) => {
                // Alert message
                this.message = "Data has been saved!";
                this.showToastSuccess = true;
                setTimeout(() => {
                    this.showToastSuccess = false;
                }, 3000);
                setTimeout (() => {
                    // Reload when success
                    this.showForm = false;
                    location.reload()
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
                setTimeout(() => {
                    this.showToastError = false;
                }, 3000);
            });
        },
        changePhoto(event) {
            const image = event.target.files[0];
            this.photo = image;
            this.previewImageUrl = URL.createObjectURL(image);
        },
        print(event) {
            console.log(this.startDate);
            const startTime = new Date(this.startDate).toISOString();
            console.log(startTime);            
            var now = new Date(startTime);
            now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
            console.log(now.toISOString().slice(0,16));
        },
        async getAlbum() {
            const token = localStorage.getItem("token");
            var status;

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            await axios.get(import.meta.env.VITE_API_URI + "/Album", config)
                .then((response) => {
                    status = response.status;
                    this.albums = response.data.albums;
                })
                .catch((err) => {
                    status = err.response.status;
                    console.log(err);
                });
        }
    },
    async created() {
        this.id = this.event.raceId;
        var time = new Date(this.event.startTime);
        time.setMinutes(time.getMinutes() - time.getTimezoneOffset());
        this.startDate = time.toISOString().slice(0,16)
        this.name = this.event.raceName;
        this.city = this.event.startLocation.name;
        this.isPublish = this.event.isPublished;
        this.description = this.event.raceDesc;
        this.startRegis = this.event.startDateRegistration;
        this.endRegis = this.event.endDateRegistration;
        this.courseMap = this.event.points;
        this.albumId = this.event.raceAlbum.albumId;
        this.distance = this.event.distance;
        this.price = this.event.registrationFee;
        this.previewImageUrl = this.event.raceThumbnail ? this.event.raceThumbnail.url : "";
        this.basePreviewImageUrl = this.event.raceThumbnail ? this.event.raceThumbnail.url : "";
        this.photo = this.event.raceThumbnail;
        this.getAlbum();
    }
}
</script>

<style scoped>
.container-image {
    width: 100%;
    text-align: center;
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

img {
    max-width: 400px;
    max-height:200px;
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
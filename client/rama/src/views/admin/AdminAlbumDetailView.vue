<template>
    <div class="detail-album">
        <AdminSidebar class="sidebar" album="true"/>
        <div class="layout">
            <div class="tabs">
                <RouterLink class="back" :to="{ name: 'albumAdmin'}" ><img src="/left-arrow.png"></RouterLink>
                <RouterLink class="tab page" :to="{params: {id: this.id}, name: 'albumDetail'}">Detail Album</RouterLink> 
                <RouterLink class="tab" :to="{params: {id: this.id}, name: 'listPhoto'}">List Photo</RouterLink> 
            </div>
            <div class="body">
                <div class="edit-button-container">
                    <button class="edit-button" @click="showPopup = true">EDIT</button>
                    <button class="delete-button" @click="deleteAlbum">DELETE</button>
                </div>
                <div class="information-container">
                    <div class="album-name">
                        <label class="label-album-name">Name</label>
                        <div class="album-name-container">
                            <p v-text="album.albumName"></p>
                        </div>
                    </div>
                    <!-- <div class="album-status">
                        <label class="label-album-status">Status</label>
                        <div class="album-status-container">
                            <p v-text="this.status"></p>
                        </div>
                    </div> -->
                </div>
            </div>
        </div>
        <div class="popup" v-if="showPopup">
            <div class="popup-content">
                <h2 class="popup-title">Edit Album</h2>
                <form class="form-edit-album" @submit.prevent="submitForm">
                    <div class="form-group">
                        <label class="form-label" for="album-name">Album Name</label>
                        <input class="form-input" type="text" id="album-name" v-model="albumName" :placeholder="album.albumName">
                    </div>
                    <!-- <div class="form-group">
                        <label class="form-label" for="status">Status</label>
                        <select class="form-input" id="status" v-model="statusEdit">
                            <option value="published">Published</option>
                            <option value="unpublished">Unpublished</option>
                        </select>
                    </div> -->
                    <div class="form-buttons">
                        <button class="form-button form-button-save" @click="saveAlbum">Save</button>
                        <button class="form-button form-button-cancel" @click="showPopup = false">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '@/components/AdminSidebar.vue';
export default {
    name: "AlbumDetailView",
    data() {
        return {
            album : [],
            id :this.$route.params.id,
            showPopup: false,
            albumName: '',
            status: "Published",
            statusEdit: ''
        };
    },
    components: {
        AdminSidebar
    },
    methods: {
        async getAlbum(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/Album/" + this.id, config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.album = response.data;
                    console.log(this.album);
                    // for debug
                    // console.log(this.event);
                    // console.log(this.events[0].raceName);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        },
        saveAlbum(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}`}
            };
            axios.put(import.meta.env.VITE_API_URI + "/Album/" + this.id, {albumName : this.albumName}, config)
            .then((responese) => {
                this.showPopup = false;
                location.reload();
            })
            .catch((err) =>{
                console.log(err);
            })
        },
        deleteAlbum(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}`}
            };
            axios.delete(import.meta.env.VITE_API_URI + "/Album/" + this.id, config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.$router.push("/admin/album");
                }
            }).catch((err) =>{
                console.log(err);
            })
        }
    },
    mounted(){
        this.getAlbum();
    }
};
</script>

<style scoped>
.detail-album{
    height: 100%;
    width: 100%;
    position: relative;
    display: grid;
    grid-template-columns: 333px auto;
    grid-template-areas: 
    "sidebar main";
    min-height: 100vh;
}
.sidebar {
    grid-area: sidebar;
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
    padding: 5px 20px 10px 125px;
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
    left: 25px;
    background-color: rgba(0,0,0,0);
    border: none;
    cursor: pointer;
}

.body {
    grid-area: body;
    background: #fff;
    padding: 20px 20px 20px 20px;
    width: 100%;
    justify-content: center;
    border-radius: 15px;
}
.edit-button-container{
    display: flex;
    justify-content: flex-end;
    align-items: center;
    width: 100%;
}
.edit-button {
    height: 2rem;
    width: 5rem;
    background: #353642;
    border: 1px solid grey;
    border-radius: 20px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
    margin-right: 5px;
}

.delete-button {
    height: 2rem;
    width: 5rem;
    background: #EC7B7B;
    border: 1px solid grey;
    border-radius: 20px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
}


.information-container {
    width: 100%;
}

.album-name{
    /* width: 50%; */
    height: auto;
    text-align: center;
    display: flex;
    flex-direction: column;
    /* justify-content: center; */
}
.album-name-container{
    background: #fff;
    border-radius: 15px;
    border: 2px solid #000;
    width: 40%;
    height: 2rem;
    text-align: left;
    justify-content: end;
    margin: 0 auto;
}

p {
    color: #000;
    left: 20px;
    font-size: 20px;
    font-family: 'Darker Grotesque';
}

label {
    font-size: 24px;
    color: #000;
}

.label-album-name{
    font-family: 'Darker Grotesque';
    text-align: center;
}


.popup {
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 9999;
}

.popup-content {
  background-color: #fff;
  padding: 1rem;
  border-radius: 15px;
  width: 500px;
  font-family: 'Darker Grotesque';
}

.popup-title {
  font-size: 48px;
  margin-bottom: 1rem;
  text-align: center;
}

.form-edit-album {
  display: flex;
  flex-direction: column;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 1rem;
}

.form-label {
  font-weight: bold;
  margin-bottom: 0.5rem;
}

.form-input {
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 20px;
}

.form-buttons {
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
}

.form-button {
  padding: 0.5rem 1rem;
  border-radius: 5px;
  font-size: 20px;
  cursor: pointer;
}

.form-button-save {
  background-color: #5cb85c;
  color: #fff;
  border: none;
  margin-right: 1rem;
}

.form-button-cancel {
  background-color: #d9534f;
  color: #fff;
  border: none;
}


</style>
<template>
    <div class="detail-album">
        <AdminSidebar class="sidebar"/>
        <!-- <div class="bar">
            <div class="detail-album-container">
                <h2>Detail Album</h2>
            </div>
            <div class="list-Photo-container">
                <h2>Liat Photo</h2>
            </div>
        </div> -->
        <div class="layout">
            <div class="edit-button-container">
                <button class="edit-button" >EDIT</button>
            </div>
            <div class="photos-button-container">
                <button class="photos-button">PHOTOS</button>
            </div>
            <div class="information-container">
                <div class="row">
                    <div class="album-name">
                        <label class="label-album-name">Name</label>
                        <div class="album-name-container">
                            <p></p>
                        </div>
                    </div>
                    <div class="album-status">
                        <label class="label-album-status">Status</label>
                        <div class="album-status-container">
                            <p></p>
                        </div>
                    </div>
                </div>
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
            id :this.$route.params.id
        };
    },
    components: {
        AdminSidebar
    },
    methods: {
        // async getAlbum(){
        //     const token = localStorage.getItem("token");

        //     // Configuration for API
        //     const config = {
        //         headers: { Authorization: `Bearer ${token}` }
        //     };

        //     axios
        //     .get(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
        //     .then((response) => {
        //         if(response.status !== 200){
        //             console.log(response);
        //         }else{
        //             this.album = response.data;
        //             console.log(this.album);
        //             // for debug
        //             // console.log(this.event);
        //             // console.log(this.events[0].raceName);
        //         }
        //     })
        //     .catch((err) => {
        //         console.log(err);
        //     });
        // }
    },
    mounted(){
        // this.getAlbum();
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
    width: 5rem;
    background: #000;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
}

.photos-button {
    position: absolute;
    right: 2%;
    margin-top: 5%;
    height: 2rem;
    width: 5rem;
    background: #b7ed06;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
    color: #fff;
}

.information-container {
    margin-top: 2%;
    width: 100%;
    height: auto;
    margin-bottom: 2%;
}
.row{
    width: 100%;
    height: auto;
    display: flex;
    align-items: center;
    justify-content: center;
}

.album-name,
.album-status{
    width: 50%;
    height: auto;
    text-align: left;
}
.album-name-container{
    background: #fff;
    border-radius: 15px;
    border: 2px solid #000;
    width: 50%;
    height: 100%;
    left: 45%;
    height: 2rem;
}

.album-status-container{
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

.label-album-name{
    left: 46%;
}
.label-album-status{
    left: 6%;
}

</style>
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
                    <button class="edit-button" >EDIT</button>
                </div>
                <div class="information-container">
                    <div class="row">
                        <div class="album-name">
                            <label class="label-album-name">Name</label>
                            <div class="album-name-container">
                                <p v-text="album.albumName"></p>
                            </div>
                        </div>
                        <div class="album-status">
                            <label class="label-album-status">Status</label>
                            <div class="album-status-container">
                                <!-- ubah ini jika sudah ada di back end -->
                                <p>Published</p>
                            </div>
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
                    // console.log(this.album);
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

.edit-button {
    position: absolute;
    right: 2%;
    margin-top: 2%;
    height: 2rem;
    width: 5rem;
    background: #353642;
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
    left: 20px;
    font-size: 20px;
    font-family: 'Darker Grotesque';
}

label {
    font-size: 24px;
    color: #000;
}

.label-album-name{
    left: 46%;
    font-family: 'Darker Grotesque';
}
.label-album-status{
    left: 6%;
    font-family: 'Darker Grotesque';
}

</style>
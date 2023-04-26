<template>
    <div class="layout">
        <AdminSidebar class="sidebar" album="true"/>
        <div class="main">
            <div class="header">
                <div class="title-page-container">
                    <h1 class="title-page">Album</h1>
                </div>
                <div class="button-add-container">
                    <button class="add-button">ADD</button>
                </div>
            </div>
            <div class="table-container"> 
                <table class="event-table">
                    <thead class="table-head">
                        <tr class="table-row-header">
                            <th class="table-header" scope="col">Title</th>
                            <th class="table-header" scope="col">Status</th>
                            <th class="table-header" scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody class="table-body">
                        <tr class="table-row-body" v-for="album in albums" :key="album.albumId">
                            <td class="table-data" v-text="album.albumName"></td>
                            <td class="table-data">
                                <p class="status-publish" id="published" v-if="this.isPublished">Published</p>
                                <p class="status-publish-not" id="notPublished" v-if="!this.isPublished">Unpublished</p>
                            </td>
                            <td class="table-data">
                                <button class="detail-button">
                                    <router-link :to="{params: {id : album.albumId}, name: 'albumDetail'}">
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
    name: "AlbumAdminView",
    data() {
        return {
            albums : [],
            isPublished: true
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
            .get(import.meta.env.VITE_API_URI + "/Album", config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.albums = response.data.albums;
                    console.log(this.albums);
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
    height: 40%;
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
.detail-button {
    width: 50%;
    background: #deddd8;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
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
</style>
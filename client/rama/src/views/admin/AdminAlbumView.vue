<template>
    <div class="layout">
        <AdminSidebar class="sidebar" album="true"/>
        <div class="main">
            <div class="title-page-container">
                <h1 class="title-page">Album</h1>
            </div>
            <div class="table-container"> 
                <table class="event-table">
                    <thead class="table-head">
                        <tr class="table-row-header">
                            <th class="table-header-title" scope="col">Title</th>
                            <th class="table-header-status" scope="col">Status</th>
                            <th class="table-header-action" scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody class="table-body">
                        <tr class="table-row-body" v-for="album in albums" :key="album.albumId">
                            <td class="table-data-title" v-text="album.albumName"></td>
                            <td class="table-data-status">
                                <p class="status-publish" id="published" v-if="this.isPublished">Published</p>
                                <p class="status-publish-not" id="notPublished" v-if="!this.isPublished">Unpublished</p>
                            </td>
                            <td class="table-data-action">
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
                    console.log(response.data);
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
    width: 100%;
    text-align: left;
    margin-top: 20px;
}

.title-page {
    font-family: 'Darker Grotesque';
    font-weight: bold;
    font-size: 48px;
    margin-left: 50px;
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
    /* text-align: center; */
    color: #000;
    padding: 20px;
    width: 100%;
    /* table-layout: fixed; */
}
.table-row-body{
    text-align: center;
}

.table-row-header {
    border-bottom: 2px solid #272626;
    font-family: 'Darker Grotesque';
    font-size: 24px;
    text-align: center;
}

/* for table title */
.table-header-title{
    width: 60%;
    text-align: left;
    left: 50px;
}

/* fot table row data */

.table-data-title{
    width: 60%;
    text-align: left;
    font-family: 'Darker Grotesque';
    font-size: 20px;
    left: 5px;
}


.table-header-status, .table-data-status,
.table-header-action, .table-data-action{
    width: 20%;
}

.detail-button {
    background: #deddd8;
    border: 1px solid grey;
    border-radius: 15px;
    font-size: 20px;
    font-family: 'Darker Grotesque';
    font-weight: bold;
    letter-spacing: 1px;
    display: inline-block;
    padding: 0 10px 0 10px;
}

.status-publish {
    background: #72e48b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-size: 20px;
    display: inline-block;
    padding: 0px 10px 0px 10px;
}

.status-publish-not {
    background: #ec7b7b;
    border-radius: 15px;
    font-family: 'Darker Grotesque';
    font-size: 20px;
    display: inline-block;
    padding: 0px 10px 0px 10px;
}
</style>
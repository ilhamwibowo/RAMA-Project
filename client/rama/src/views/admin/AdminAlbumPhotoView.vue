<template>
    <div class="album-photo">
        <AdminSidebar class="sidebar"/>
        <div class="layout">
            <div class="upload-button-container">
                <button class="upload-button">Upload</button>
            </div>
            <div class="delete-button-container">
                <button class="delete-button">Delete</button>
            </div>
            <div class="header">
                <input type="checkbox" name="select-all">
                <label for="select-all">select all</label>
            </div>
            <div class="photo-container">
                <h1>disini foto</h1>
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
        
    },
    mounted(){

    }
};
</script>

<style scoped>
.album-photo{
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
    background: #fff;
    width: 100%;
    margin: 30px 20px 30px 20px;
    position: absolute;
    justify-content: center;
    border-radius: 15px;
}

.header {
    width: 100%;
    height: auto;
    margin-top: 2%;
    text-align: left;
    margin-left: 2%;
}

.upload-button {
    position: absolute;
    margin-top: 2%;
    right: 10%;
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

.delete-button {
    position: absolute;
    margin-top: 2%;
    right: 2%;
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
.photo-container{
    margin-top: 2%;
}

</style>
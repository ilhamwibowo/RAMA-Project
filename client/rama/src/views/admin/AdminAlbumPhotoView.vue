<template>
    <div class="album-photo">
        <AdminSidebar class="sidebar"/>
        <div class="layout">
            <div class="tabs">
                <RouterLink class="back" :to="{ name: 'albumAdmin'}" ><img src="/left-arrow.png"></RouterLink>
                <RouterLink class="tab" :to="{params: {id: this.id}, name: 'albumDetail'}">Detail Album</RouterLink> 
                <RouterLink class="tab page" :to="{params: {id: this.id}, name: 'listPhoto'}">List Photo</RouterLink> 
            </div>
            <div class="body">
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
    padding: 30px 20px 30px 20px;
    width: 100%;
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
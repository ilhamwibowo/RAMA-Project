<template>
    <div class="layout">
        <AdminSidebar class="sidebar"/>
        <div class="main">
            <div class="header">
                <div class="title-page-container">
                    <h1 class="title-page">Race Participan</h1>
                </div>
                
            </div>
            <div class="table-container">
                <table class="event-table">
                    <thead class="table-head">
                        <tr class="table-row-header">
                            <th class="table-header" scope="col">Name</th>
                            <th class="table-header" scope="col">Email</th>
                            <th class="table-header" scope="col">Phone Number</th>
                            <th class="table-header" scope="col">Birthday</th>
                            <th class="table-header" scope="col">Status</th>
                        </tr>
                    </thead>
                    <tbody class="table-body">
                        <tr class="table-row-body"  >
                            <td class="table-data">Ilham</td>
                            <td class="table-data">pilham902@gmail.com</td>
                            <td class="table-data">092284741304</td>
                            <td class="table-data">21/09/2001</td>
                            <td class="table-data">Completed</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '../components/AdminSidebar.vue';
export default {
    name: "RaceParticipant",
    data() {
        return {

        };
    },
    components: {
        AdminSidebar
    },
    methods: {
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
}

.sidebar {
    grid-area: sidebar;
}

.main {
    grid-area: main;
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


</style>
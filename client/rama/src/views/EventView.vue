<template>
    <div class="layout">
        <div class="header">
            <div class="title-page-container">
                <h1 class="title-page">EVENTS</h1>
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
                        <th class="table-header" scope="col">Location</th>
                        <th class="table-header" scope="col">Date</th>
                        <th class="table-header" scope="col">Category</th>
                        <th class="table-header" scope="col">Registration</th>
                        <th class="table-header" scope="col">Status</th>
                        <th class="table-header" scope="col">Action</th>
                    </tr>
                </thead>
                <tbody class="table-body">
                    <tr class="table-row-body" v-for="(event, index) in events" :key="index" >
                        <td class="table-data" v-text="event.raceName"></td>
                        <td class="table-data" v-text="event.startLocation"></td>
                        <td class="table-data" v-text="event.startTime"></td>
                        <td class="table-data">h</td>
                        <td class="table-data">
                            <p class="open-regis" id="open-regis" v-if="isRegistered">Opened</p>
                            <p class="close-regis" id="close-regis" v-if="!isRegistered">Closed</p>
                        </td>
                        <td class="table-data">
                            <p class="status-publish" id="published" v-if="isPublished">Published</p>
                            <p class="status-publish-not" id="notPublished" v-if="!isPublished">Unpublished</p>
                        </td>
                        <td class="table-data">
                            <button class="detail-button">
                                <router-link :to="{params: {id : index+1}, name: 'detailEvent'}">
                                    Detail
                                </router-link>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>


<script>
import axios from 'axios';
export default {
    name: "EventView",
    data(){
        return {
            events: [],
            isRegistered: false,
            isPublished: true
        };
    },
    methods: {
        async getEvent(){
            const token = localStorage.getItem("token");

        // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/race", config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.events = response.data.races;
                    // for debug
                    // console.log(this.events);
                    // console.log(this.events[0].raceName);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        },
        seeDetail(){

        }
    },
    mounted(){
        this.getEvent();
    }
}
</script>

<style scoped>
.layout{
    height: 100%;
    width: 100%;
}

.header{
    width: 100%;
    height: 30%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-top: 1%;
    margin-bottom: 1%;
}

.title-page-container{
    width: 50%;
    height: 100%;
    text-align: left;
}

.title-page{
    left: 20%;
    height: 100%;
}
.button-add-container{
    width: 50%;
    height: 100%;
    text-align: right;
}
.add-button{
    width: 10%;
    height: 80%;
    right: 20%;
    top: 10%;
    background: #353642 ;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    letter-spacing: 2px;

}
.table-container{
    width: 80%;
    left: 10%;
    background: #fff;
    border-radius: 10px;
}

.event-table{
    border-collapse: collapse;
    text-align: center;

    margin-bottom: 1rem;
    color: #000;
    padding: 20px;

    width: 100%;
    table-layout: fixed;
}

.table-row-header{
    border-bottom: 2px solid #272626;
    font-family: 'Montserrat', sans-serif;
    font-size: 120%;
}

.table-header{
    width: 100% / 7;
}

.open-regis{
    background: #72E48B;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.close-regis{
    background: #EC7B7B;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.status-publish{
    background: #72E48B;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.status-publish-not{
    background: #EC7B7B;
    border-radius: 15px;
    left: 25%;
    width: 60%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.detail-button{
    width: 50%;
    background: #DEDDD8 ;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
}




</style>
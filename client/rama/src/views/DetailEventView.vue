<template>
    <div class="layout">
        <div class="edit-button-container">
            <button class="edit-button">EDIT</button>
        </div>
        <div class="image-container">
            <img src="./contohGambar.png" alt="GambarEvent">
        </div>
        <div class="information-container">
            <div class="row-one">
                <div class="race-name">
                    <label class="label-race-name">Name</label>
                    <div class="race-name-container">
                        <p v-text="this.event.raceName"></p>
                    </div>
                </div>
                <div class="race-date">
                    <label class="label-race-date">Date</label>
                    <div class="race-date-container">
                        <p v-text="this.event.startTime"></p>
                    </div>
                </div>
            </div>
            <div class="row-two">
                <div class="race-province">
                    <label class="label-race-province">Province</label>
                    <div class="race-province-container">
                        <p v-text="this.event.startLocation"></p>
                    </div>
                </div>
                <div class="race-city">
                    <label class="label-race-city">City</label>
                    <div class="race-city-container">
                        <p></p>
                    </div>
                </div>
            </div>
            <div class="row-three">
                <div class="race-start-regis">
                    <label class="label-race-start-regis">Start Date Registration</label>
                    <div class="race-start-regis-container">
                        <p></p>
                    </div>
                </div>
                <div class="race-end-regis">
                    <label class="label-race-end-regis">End Date Registration</label>
                    <div class="race-end-regis-container">
                        <p></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="category-container">
            <table class="category-table">
                <thead class="table-head">
                    <tr class="table-row-header">
                        <th class="table-header">Category</th>
                        <th class="table-header">Distance</th>
                        <th class="table-header">Price</th>
                    </tr>
                </thead>
                <tbody class="table-body">
                    <tr class="table-row-body">
                        <th class="table-item">Ultramaraton</th>
                        <th class="table-item">50</th>
                        <th class="table-item">150000</th>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="maps">
            <img src="./contohGambar.png" alt="maps maraton">
        </div>
    </div>
</template>


<script>
import axios from 'axios';
export default {
    name: "detailEvent",
    data(){
        return {
            event: [],
            id :this.$route.params.id
        };
    },

    methods: {
        async getData(){
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            axios
            .get(import.meta.env.VITE_API_URI + "/Race/" + this.id, config)
            .then((response) => {
                if(response.status !== 200){
                    console.log(response);
                }else{
                    this.event = response.data;
                    // for debug
                    // console.log(this.event);
                    // console.log(this.events[0].raceName);
                }
            })
            .catch((err) => {
                console.log(err);
            });
        },
    },
    mounted(){
        this.getData();
    }
}
</script>
<style scoped>

.layout{
    background: #FFF;
    width: 90%;
    height: 150%;
    left: 5%;
    justify-content: center;
    border-radius: 15px;
}


.edit-button{
    position: absolute;
    right: 2%;
    margin-top: 2%;
    height: 2rem;
    width: 4rem;
    background: #000 ;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
    color: #FFF;
}

.image-container{
    margin-top: 2%;
}
.information-container{
    margin-top: 2%;
    width: 100%;
    height: auto;
    margin-bottom: 2%;
}

.row-one, .row-two, .row-three{
    width: 100%;
    height: auto;
    display: flex;
    align-items: center;
    justify-content: center;
}

.race-name, .race-date, .race-province, .race-city, .race-start-regis, .race-end-regis{
    width: 50%;
    height: auto;
    text-align: left;
}

.race-name-container, .race-province-container, .race-start-regis-container{
    background: #FFF;
    border-radius: 15px;
    border: 2px solid #000;
    width: 50%;
    height: 100%;
    left: 45%;
    height: 2rem;
}

.race-date-container, .race-city-container, .race-end-regis-container{
    background: #FFF;
    border-radius: 15px;
    border: 2px solid #000;
    width: 50%;
    height: 2rem;
    left: 5%;
}

p{
    color: #000;
    left: 5%;
    bottom: 6%;
    font-size: 1.25rem;
}

label{
    font-size : 1.25rem;
    color: #000;
}
.label-race-name, .label-race-province, .label-race-start-regis{
    left: 46%;
}

.label-race-date, .label-race-city, .label-race-end-regis{
    left: 6%;
}

.category-container{
    width: 50%;
    left: 25%;
    margin-bottom: 2.5rem;
}

.category-table{
    border-collapse: collapse;
    text-align: center;
    margin-bottom: 1rem;
    width: 100%;
    table-layout: fixed;
    color: #000;
}

.table-row-header{
    border-bottom: 2px solid #272626;
    font-family: 'Montserrat', sans-serif;
    font-size: 1.25rem;
}

.table-row-body{
    font-size: 1.25rem;
}

.maps{
    margin-bottom: 10%;
}

</style>
<template>
    <div class="layout">
        <AdminSidebar class="sidebar"/>
            <div class="search">
                <input type="text" id="input-search"/>
                <button @click="search($event)">Search</button>
            </div>
        <EventRegistrantTable :id="this.id" :data="this.registrans" :names="this.names"/>
    </div>
</template>

<script>
import AdminSidebar from '../components/AdminSidebar.vue';
import EventRegistrantTable from '../components/EventRegistrantTable.vue';
import axios from 'axios';


export default {
    name: "EventRegistransView",
    data(){
        return {
            registrans: [],
            names: [],
            id :this.$route.params.id
        };
    },
    methods : {
            async getData(query=""){
                const token = localStorage.getItem("token");

                    // Configuration for API
                const config = {
                    headers: { Authorization: `Bearer ${token}` }
                };

                axios
                .get(import.meta.env.VITE_API_URI + `/Registration/all/${this.id}?query=${encodeURI(query.toLowerCase())}`, config)
                .then((response) => {
                    this.registrans = response.data.histories
                }).
                then( () =>
                    {
                        this.registrans.forEach((e, i) => {
                            axios.get(import.meta.env.VITE_API_URI + `/user/${e.accId}`, config)
                            .then((response) => {
                                console.log(response.data)
                                this.names[i] = response.data.name
                            })
                            .catch((err) => {
                                console.log(err);
                            });
                        });
                    }
                )
                .catch((err) => {
                    console.log(err);
                });
            },
        search(event){
            let query = document.getElementById("input-search").value;
            this.getData(query)
        }

    },
    mounted(){
         this.getData()
    },
    components: {
        AdminSidebar,
        EventRegistrantTable
    }
};
</script>

<style scoped></style>
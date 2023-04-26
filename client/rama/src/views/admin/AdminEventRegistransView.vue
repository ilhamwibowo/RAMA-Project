<template>
    <div class="layout">
        <AdminSidebar class="sidebar" event="true"/>
        <div class="body">
            <div class="tabs">
                <RouterLink class="tab" :to="{params: {id: this.id}, name: 'detailEvent'}">Detail Event</RouterLink> 
                <RouterLink class="tab page" :to="{params: {id: this.id}, name: 'EventRegistrant'}">Registrant</RouterLink> 
                <RouterLink class="back" to="/event" ><img src="/left-arrow.png"></RouterLink>
            </div>
            <div class="search">
                <input type="text" id="input-search"/>
                <button @click="search($event)">Search</button>
            </div>
            <EventRegistrantTable class="content" :id="this.id" :data="this.registransShow" :names="this.names"/>
            <Pagination 
                class="pagination"
                :totalPage="totalPage"
                :pager="pager"
                :page="page"
                @changePage="updatePage"
                @changePager="updatePager"
            />
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '@/components/AdminSidebar.vue';
import EventRegistrantTable from '@/components/EventRegistrantTable.vue';
import Pagination from '../../components/Pagination.vue';

export default {
    name: "EventRegistransView",
    data(){
        return {
            registrans: [],
            names: [],
            id :this.$route.params.id,
            page: 1,
            pager: 1,
            totalPage: 10,
            totalRegistrans: 0,
            paginationKey: 0,
            registransShow: [],
        };
    },
    
    components: {
        AdminSidebar,
        EventRegistrantTable,
        Pagination
    },
    methods : {
            async getData(query=""){
                const token = localStorage.getItem("token");

                    // Configuration for API
                const config = {
                    headers: { Authorization: `Bearer ${token}` }
                };

                await axios
                .get(import.meta.env.VITE_API_URI + `/Registration/all/${this.id}?query=${encodeURI(query.toLowerCase())}`, config)
                .then((response) => {
                    console.log("Response: ", response.data);
                    this.registrans = response.data.histories;
                }).
                then( () =>
                    {
                        this.registrans.forEach((e, i) => {
                            axios.get(import.meta.env.VITE_API_URI + `/user/${e.accId}`, config)
                            .then((response) => {
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

                /** Pagination */
                this.totalRegistrans = this.registrans.length;
                this.updatePage(this.page);
                this.updatePager(this.pager);
            },
        search(event){
            let query = document.getElementById("input-search").value;
            this.getData(query)
        },
        
        /** Pagination */
        updatePage(n) {
            this.page = n;

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            console.log(this.registrans);
            this.registransShow = this.registrans.slice(start, end);

            console.log("Page", this.page)
        },
        updatePager(n) {
            this.pager = n;
            this.totalPage = Math.ceil(this.totalRegistrans / this.pager);
            
            let page = this.page;
            let pager = this.pager;
            
            let start = pager * (page - 1);
            let end = pager * page;
            this.registransShow = this.registrans.slice(start, end);
            console.log("Pager", this.pager)
        }
    },
    created(){
         this.getData()
    },
};
</script>

<style scoped>
.layout {
    height: 100%;
    margin-right: 50px;
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

.body {
    grid-area: main;
    display: grid;
    margin:40px 50px 40px 40px;
    grid-template-rows: 70px 70px auto;
    row-gap: 20px;
    grid-template-areas: 
    "tabs"
    "search"
    "content"
}
.tabs {
    grid-area: tabs;
    display: flex;
    column-gap: 100px;
    align-items: center;
    background: #fff;
    padding: 5px 20px 10px 80px;
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
    right: 25px;
    background-color: rgba(0,0,0,0);
    border: none;
    cursor: pointer;
}
.search {
    grid-area: search;
    text-align: center;
}
.content {
    grid-area: content;
}
</style>
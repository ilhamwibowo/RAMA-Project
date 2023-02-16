<template>
    <div class="profile">
        <h1>Edit Profile</h1>
        <div class="container-detail">
            <h3 class="title">Name</h3>
            <input class="input" v-model="user.Name"/>
        </div>
        <div class="container-detail">
            <h3 class="title">Email</h3>
            <input class="input" v-model="user.Email"/>
        </div>
        <div class="container-detail">
            <h3 class="title">Identity Card</h3>
            <input class="input" v-model="user.KTP"/>
        </div>
        <div class="container-detail"> 
            <h3 class="title">Phone Number</h3>
            <input class="input" v-model="user.No_HP"/>
        </div>
        <div class="container-detail">
            <h3 class="title">Birthday</h3>
            <input class="input" v-model="user.Birthday"/>
        </div>

        <router-link :to="{path: '/profile'}">
            <button class="btn save" @click="updateUser">Save</button>
        </router-link>
        <router-link :to="{path: `/profile`}">
            <button class="btn cancel">Cancel</button>
        </router-link>
    </div>
</template>

<script>
import DetailProfile from './DetailProfile.vue';
import EditDetailProfile from './EditDetailProfile.vue';
import axios from 'axios'



export default {
    name: 'Profile',
    components: {
        DetailProfile,
        EditDetailProfile
    },
    props: {
        
    },
    data() {
        return {
            user: {
                AccId: 1,
                Name: 'Muhammad Akmal Arifin',
                Email: 'akmal@gmail.com',
                Role: 'Runner',
                KTP: '3515072509020001',
                No_HP: '082235171080',
                Birthday: '2001-09-25',
            },
            id: 1 // get from cache
        }
    },
    methods: {
        updateUser() {
            const body = {
                // Name: this.user.Name,
                // Email: this.user.Name,
                Role: this.user.Role,
                KTP: this.user.KTP,
                No_HP: this.user.No_HP,
                Birthday: this.user.Birthday
            }
            
            // Post current {Role, KTP, No_HP, Birthday}
            axios
                .post(`http://localhost:5226/api/User/edit/${this.id}`, body)
                .then(response => this.id = response.data.id)

        },

        cancel() {

        }
    }

    // Fetch data API
    // created() {
    //     axios
    //         .get(`http://localhost:5226/api/User/${this.id}`)
    //         .then(response => {
    //             // JSON responses automatically parsed
    //             this.user = response.data
    //         })
    //         .catch(e => {
    //             console.log(e)
    //         });
    //     // console.log(this.user)
    // }
}
</script>

<style scoped>
    .title {
        color:aliceblue;
        font-weight: bold;
        font-size:medium
    }
    .input {
        width:auto;
        font-size: medium;
        border: 2px solid #5f5f5f;
        border-radius: 5px;
        padding: 5px 10px;
        background-color: rgba(0,0,0,0);
        color:rgba(235,235,235,0.64);
    }
    .edit-detail-profile {
        margin: 10px 0px 
    }
</style>
<template>
    <div class="profile">
        <h1>Edit Profile</h1>
        <div class="container-image">
            <input type="file" id="image" accept="image/*" @change="photoChange" style="display:none;"/>
            <img :src="user.image" />
            <div class="edit" @click="uploadPhoto"> 
                <div class="circle"></div>
            </div>
        </div>
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
                image: 'https://images.pexels.com/photos/771742/pexels-photo-771742.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500'
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
        uploadPhoto() {
            document.getElementById("image").click()
        },
        photoChange(event) {
            const image = event.target.files[0]
            this.user.image = URL.createObjectURL(image)
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
    .container-image {
    display: flex;
    margin: 0 auto;
    width: 150px;
    height: 150px;
    position: relative;
    align-items: center;
    overflow: hidden;
    border-radius: 50%;
    }

    img {
        display: inline;
        margin: 0 auto;
        height: 100%;
        width: auto;
    }

    .edit {
        position: absolute;
        width: 150px;
        height: 150px;

    }
    .circle {
        background-color:#d3c5c5;
        width: 100%;
        height: 100%;
        opacity:0;
        transition: 0.3s;
    }
    
    .circle:hover {
        opacity:0.3;
    }   
</style>
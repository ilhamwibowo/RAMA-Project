<template>
    <div class="profile">
        <h1>Edit Profile</h1>
        <div class="container-image">
            <input type="file" id="image" accept="image/*" @change="photoChange" style="display:none;"/>
            <img :src="previewImageUrl" />
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
            <input class="input" v-model="user.email"/>
        </div>
        <div class="container-detail">
            <h3 class="title">Identity Card</h3>
            <input class="input" v-model="user.ktp"/>
        </div>
        <div class="container-detail"> 
            <h3 class="title">Phone Number</h3>
            <input class="input" v-model="user.no_HP"/>
        </div>
        <div class="container-detail">
            <h3 class="title">Birthday</h3>
            <input class="input" v-model="user.birthday"/>
        </div>

        <!-- <router-link :to="{path: '/profile'}"> -->
        <button class="btn save" @click="updateUser">Save</button>
        <!-- </router-link> -->
        <router-link :to="{path: `/profile`}">
            <button class="btn cancel">Cancel</button>
        </router-link>
    </div>
</template>

<script>
import DetailProfile from './DetailProfile.vue';
import EditDetailProfile from './EditDetailProfile.vue';
import axios from 'axios'

const env = import.meta.env;


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
            user: Object,
            previewImage: Object,
            previewImageUrl: ''
        }
    },
    methods: {
        async updateUser() {
            const token = localStorage.getItem('token')

            // Upload Photo
            var profilePhoto
            var formData = new FormData();
            // var imageFile = document.querySelector('#image')
            formData.append('file', this.previewImage)
            const configPhoto = {
                headers: { 
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'multipart/form-data'
                },
            }

            await axios
                .post(env.VITE_API_URI + "/User/add-photo", formData, configPhoto)
                .then(response => profilePhoto = response.data)
                .catch(err => console.log(err))

            console.log(profilePhoto)

            // Upload User
            const configUser = {
                headers: { 
                    Authorization: `Bearer ${token}`
                },
            }
            const body = {
                role: this.user.role,
                ktp: this.user.ktp,
                no_HP: this.user.no_hp,
                birthday: this.user.birthday,
                photoUrl: profilePhoto.url,
                profilePhoto: {
                    id: profilePhoto.id,
                    url: profilePhoto.url
                }
            }
            
            // Post current {Role, KTP, No_HP, Birthday}
            axios
                .put(env.VITE_API_URI + '/User/edit', body, configUser)
                .then(response => this.id = response.data.id)
                .catch(err => console.log(err))

        },
        uploadPhoto() {
            document.getElementById("image").click()
        },
        photoChange(event) {
            const image = event.target.files[0]
            this.previewImage = image
            this.previewImageUrl = URL.createObjectURL(image)
        }
    },

    // Fetch data API
    async created() {
        const token = localStorage.getItem("token");

        const config = {
            headers : { Authorization : `Bearer ${token}`}
        }
        await axios
            .get(env.VITE_API_URI + "/User", config)
            .then(response => {
                if (response.status !== 200) {
                    console.log(repsonse);
                }
                else {
                    this.user = response.data;
                }
            })
            .catch(err => {console.log(err)});
        console.log(this.user)
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : ''
    }
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
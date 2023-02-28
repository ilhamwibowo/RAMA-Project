<template>
    <div class="profile">
        <h1>Profile</h1>

        <!-- Container Image -->
        <div class="container-image">
            <img :src="previewImageUrl" />
        </div>

        <!-- Container Detail -->
        <div class="container-detail">
            <DetailProfile title="Name" :text="user.name"/>
            <DetailProfile title="Email" :text="user.email"/>
            <DetailProfile title="KTP" :text="user.ktp"/>
            <DetailProfile title="Phone Number" :text="user.no_HP"/>
            <DetailProfile title="Birthday" :text="user.birthday"/>
        </div>

        <!-- Container Button -->
        <div class="container-btn">
            <router-link :to="{ path: '/profile/edit/'}">
                <button class="btn edit">Edit</button>
            </router-link>
        </div>
    </div>
</template>

<script>
import DetailProfile from './DetailProfile.vue';
import axios from 'axios';

const env = import.meta.env

export default {
    name: 'Profile',
    components: {
        DetailProfile
    },
    data() {
        return {
            user: Object,
            previewImageUrl: String
        }
    },

    // Get Data User
    async created() {
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers : { Authorization : `Bearer ${token}`}
        }

        // Axios Get
        await axios
            .get(env.VITE_API_URI + "/User", config)
            .then(response => {
                if (response.status !== 200) {
                    console.log(response);
                }
                else {
                    this.user = response.data;
                }
            })
            .catch(err => {console.log(err)});
            
        // handle if there is no image
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : ''
    }
}
</script>

<style scoped>
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
</style>
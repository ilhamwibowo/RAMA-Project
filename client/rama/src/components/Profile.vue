<template>
    <div class="profile">
        <!-- Container Image -->
        <div class="container-image">
            <img :src="previewImageUrl" />
        </div>

        <!-- Container Detail -->
        <div class="container-detail">
            <DetailProfile title="Name" :text="user.name" />
            <DetailProfile title="Email" :text="user.email" />
            <DetailProfile title="Identity Card" :text="user.ktp" />
            <DetailProfile title="Phone Number" :text="user.no_HP" />
            <DetailProfile title="Birthday" :text="user.birthday" />
        </div>

        <!-- Container Button -->
        <div class="container-btn">
            <router-link :to="{ path: '/profile/edit/' }">
                <button class="btn-edit">Edit</button>
            </router-link>
        </div>

        <div class="container-bg">
            <img src="profile-background.png">
        </div>
    </div>
</template>

<script>
import axios from "axios";
import { mapWritableState } from "pinia";

import DetailProfile from "./DetailProfile.vue";
import { useProfileStore } from "@/stores/profile";

export default {
    name: "Profile",
    components: {
        DetailProfile
    },
    data() {
        return {
            user: Object,
            previewImageUrl: String
        };
    },

    computed: {
        ...mapWritableState(useProfileStore, {
            username: "name",
            role: "role"
        })
    },

    // Get Data User
    async created() {
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        // Axios Get
        await axios
            .get(import.meta.env.VITE_API_URI + "/User", config)
            .then((response) => {
                if (response.status !== 200) {
                    console.log(response);
                } else {
                    console.log(response.data);
                    this.user = response.data;
                    this.username = response.data.name;
                    this.role = response.data.role;
                }
            })
            .catch((err) => {
                console.log(err);
            });

        // handle if there is no image
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : "profile.png";
    }
};
</script>

<style scoped>
.profile {
    display: grid;
    grid-template-columns: 220px 500px 220px;
    grid-template-rows: auto 150px;
    grid-gap: 50px;
    grid-template-areas: 
    "photo detail button"
    "bg bg bg";
    justify-content: center;
}

.container-image {
    display: flex;
    margin: 0 auto;
    width: 220px;
    height: 220px;
    position: relative;
    align-items: center;
    overflow: hidden;
    border-radius: 50%;
    grid-area: photo;
    justify-self: center;
}

img {
    display: inline;
    margin: 0 auto;
    height: 100%;
    width: auto;
}


.container-detail {
    grid-area: detail;
    margin-top: 100px;
    margin-left: auto;
    margin-right: auto;
}

.container-btn {
    grid-area: button;
    justify-self: center;
    margin-top: 15px;
}

.container-bg {
    grid-area: bg;
    justify-self: center;
    position: absolute;
    bottom: 70px;
    left: -25px;
}

.btn-edit {
    width: 112px;
    height: 50px;
    padding: 10px;
    background-color: #353642;
    color: #fff;
    border-radius: 100px;
    border: none;
    cursor: pointer;
    font-family: "Bebas Neue";
    font-size: 24px;
    transition: background-color 0.3s;
}

.btn-edit:hover {
    background-color: #000000;
}



/** Make the website responsive */
@media screen and (max-width: 768px) {
    .profile {
        grid-template-columns: 1fr;
        grid-template-rows: auto auto auto;
        margin-left: 20px;
        margin-right: 20px;
    }

    .container-image {
        grid-column: 1 / 2;
        grid-row: 1 / 2;
        justify-self: center;
    }

    .container-detail {
        grid-column: 1 / 2;
        grid-row: 2 / 3;
    }

    .container-btn {
        grid-column: 1 / 2;
        grid-row: 3 / 4;
        justify-self: center;
        margin-bottom: 100px;
    }

    .container-bg {
        display: none;
    }
}
</style>
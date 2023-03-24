<template>
    <div class="profile">
        <div class="flex-container">
            <!-- Container Image -->
            <div class="container-image">
                <img :src="previewImageUrl" />
            </div>

            <!-- Container Detail -->
            <div class="container-detail">
                <DetailProfile title="Name" :text="user.name" />
                <DetailProfile title="Email" :text="user.email" />
                <DetailProfile title="KTP" :text="user.ktp" />
                <DetailProfile title="Phone Number" :text="user.no_HP" />
                <DetailProfile title="Birthday" :text="user.birthday" />
            </div>

            <!-- Container Button -->
            <div class="container-btn">
                <router-link :to="{ path: '/profile/edit/' }">
                    <button class="btn-edit">Edit</button>
                </router-link>
            </div>
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
                    this.user = response.data;
                    this.username = response.data.name;
                    // this.role = response.data.role;
                }
            })
            .catch((err) => {
                console.log(err);
            });

        // handle if there is no image
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : "";
    }
};
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
    grid-column: 1 / 2;
    grid-row: 1 / 2;
    justify-self: center;
}

img {
    display: inline;
    margin: 0 auto;
    height: 100%;
    width: auto;
}

.flex-container {
    display: grid;
    grid-template-columns: 1fr 2fr 1fr;
    grid-template-rows: auto;
    grid-gap: 20px;
    margin-left: 125px;
    margin-right: 125px;
}

.container-detail {
    grid-column: 2 / 3;
    grid-row: 1 / 2;
}

.container-btn {
    grid-column: 3 / 4;
    grid-row: 1 / 2;
    justify-self: center;
}

.btn-edit {
    width: 100px;
    height: 40px;
    padding: 10px;
    background-color: #1f2220;
    color: #fff;
    border-radius: 30px;
    border: none;
    cursor: pointer;
    font-family: "Montserrat", sans-serif;
    font-weight: bold;
    transition: background-color 0.2s;
}

.btn-edit:hover {
    background-color: #171817;
}

/** Make the website responsive */
@media screen and (max-width: 768px) {
    .flex-container {
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
    }
}
</style>

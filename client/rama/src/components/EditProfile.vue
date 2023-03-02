<template>
    <div class="profile">
        <h1>Edit Profile</h1>

        <!-- Container Image -->
        <div class="container-image">
            <input
                type="file"
                id="image"
                accept="image/*"
                @change="changePhoto"
                style="display: none"
            />
            <img :src="previewImageUrl" />
            <div class="edit" @click="uploadPhoto">
                <div class="circle"></div>
            </div>
        </div>

        <!-- Container Detail Profile -->
        <div class="container-detail">
            <h3 class="title">Name</h3>
            <input class="input" v-model="user.name" />
        </div>
        <div class="container-detail">
            <h3 class="title">Email</h3>
            <input class="input" v-model="user.email" />
        </div>
        <div class="container-detail">
            <h3 class="title">Identity Card</h3>
            <input class="input" v-model="user.ktp" />
        </div>
        <div class="container-detail">
            <h3 class="title">Phone Number</h3>
            <input class="input" v-model="user.no_HP" />
        </div>
        <div class="container-detail">
            <h3 class="title">Birthday</h3>
            <input class="input" v-model="user.birthday" />
        </div>

        <!-- Container Button -->
        <div class="container-btn">
            <!-- <router-link :to="{path: '/profile'}"> -->
            <button class="btn save" @click="updateUser">Save</button>
            <!-- </router-link> -->
            <router-link :to="{ path: `/profile` }">
                <button class="btn cancel">Cancel</button>
            </router-link>
        </div>
    </div>
</template>

<script>
import DetailProfile from "./DetailProfile.vue";
import EditDetailProfile from "./EditDetailProfile.vue";
import axios from "axios";

export default {
    name: "Profile",
    components: {
        DetailProfile,
        EditDetailProfile
    },
    data() {
        return {
            user: Object,
            previewImage: Object,
            previewImageUrl: "",
            flagPhoto: false
        };
    },
    methods: {
        async updateUser() {
            const token = localStorage.getItem("token");

            // UPLOAD PHOTO
            var profilePhoto;
            // console.log(this.previewImageUrl)
            console.log(this.flagPhoto);

            if (this.flagPhoto) {
                console.log("A");
                // Configuration for post api
                const configPhoto = {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        "Content-Type": "multipart/form-data"
                    }
                };

                // Create FormData file for post api
                var formData = new FormData();
                formData.append("file", this.previewImage);

                // Axios Post
                await axios
                    .post(import.meta.env.VITE_API_URI + "/User/add-photo", formData, configPhoto)
                    .then((response) => (profilePhoto = response.data))
                    .catch((err) => console.log(err));
            } else {
                console.log("B");
                console.log(this.user);
                profilePhoto = this.user.profilePhoto;
            }

            console.log(profilePhoto);

            // UPDATE USER
            var status;

            // Configuration for put api
            const configUser = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };

            // Body request for put api
            const body = {
                name: this.user.name,
                role: this.user.role,
                ktp: this.user.ktp,
                no_HP: this.user.no_HP,
                birthday: this.user.birthday,
                photoUrl: profilePhoto.url,
                profilePhoto: {
                    id: profilePhoto.id,
                    url: profilePhoto.url
                }
            };

            // Axios Put
            await axios
                .put(import.meta.env.VITE_API_URI + "/User/edit", body, configUser)
                .then((response) => (status = response.status))
                .catch((err) => {
                    console.log(err);
                    status = response.status;
                });

            // Status Alert
            if (status === 200) {
                alert("Update Profile Success");
            } else {
                console.log(status);
                alert("Update Profile Failed");
            }
            this.$router.push("/profile");
        },
        // File Upload
        uploadPhoto() {
            document.getElementById("image").click();
        },
        changePhoto(event) {
            const image = event.target.files[0];
            this.previewImage = image;
            this.previewImageUrl = URL.createObjectURL(image);
            this.flagPhoto = true;
        }
    },

    // Fetch data API
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
                    console.log(repsonse);
                } else {
                    this.user = response.data;
                }
            })
            .catch((err) => {
                console.log(err);
            });

        // Handle if there is no photo
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : "";
    }
};
</script>

<style scoped>
.title {
    color: aliceblue;
    font-weight: bold;
    font-size: medium;
}
.input {
    width: auto;
    font-size: medium;
    border: 2px solid #5f5f5f;
    border-radius: 5px;
    padding: 5px 10px;
    background-color: rgba(0, 0, 0, 0);
    color: rgba(235, 235, 235, 0.64);
}
.edit-detail-profile {
    margin: 10px 0px;
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
    background-color: #d3c5c5;
    width: 100%;
    height: 100%;
    opacity: 0;
    transition: 0.3s;
}

.circle:hover {
    opacity: 0.3;
}
</style>

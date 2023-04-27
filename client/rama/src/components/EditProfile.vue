<template>
    <div class="profile">
        <!-- Toaster -->
        <Transition name="toast">
            <Toast v-if="showToastSuccess" type="success" :message="message"/>
        </Transition>
        <Transition name="toast">
            <Toast v-if="showToastError" type="error" :message="message"/>
        </Transition> 

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
        <div class="input-container">
            <div class="container-detail">
                <h3 class="label">Name</h3>
                <input class="input" v-model="user.name" />
            </div>
            <div class="container-detail">
                <h3 class="label">Email</h3>
                <input class="input" v-model="user.email" />
            </div>
            <div class="container-detail">
                <h3 class="label">Identity Card</h3>
                <input class="input" v-model="user.ktp" />
            </div>
            <div class="container-detail">
                <h3 class="label">Phone Number</h3>
                <input class="input" v-model="user.no_HP" />
            </div>
            <div class="container-detail">
                <h3 class="label">Birthday</h3>
                <input class="input" v-model="user.birthday" />
            </div>
        </div>

        <!-- Container Button -->
        <div class="container-btn">
            <!-- <router-link :to="{path: '/profile'}"> -->
            <button class="btn-save" @click="updateUser">Save</button>
            <!-- </router-link> -->
            <router-link :to="{ path: `/profile` }">
                <button class="btn-cancel">Cancel</button>
            </router-link>
        </div>

        <!-- Container Image -->
        <div class="container-bg">
            <img src="/profile-background.png">
        </div>
    </div>
</template>

<script>
import axios from "axios";
import Toast from "./Toast.vue";

const env = import.meta.env;

export default {
    name: "Profile",
    data() {
        return {
            user: Object,
            previewImage: Object,
            previewImageUrl: "",
            flagPhoto: false,
            showToastError: false,
            showToastSuccess: false,
            message: "",
        };
    },
    methods: {
        async updateUser() {
            const token = localStorage.getItem("token");
            // UPLOAD PHOTO
            var profilePhoto;
            // console.log(this.previewImageUrl)
            console.log(this.flagPhoto);
            // If user upload new photo
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
                for (const value of formData.values()) {
                    console.log(value);
                }
                // Axios Post
                await axios
                    .post(import.meta.env.VITE_API_URI + "/User/add-photo", formData, configPhoto)
                    .then((response) => {
                        profilePhoto = response.data;
                        
                    })
                    .catch((err) => console.log(err));
            }
            else {
                console.log("B");
                console.log(this.user);
                let defaultProfilePhoto = {
                    url: "/profile.png"
                };
                profilePhoto = this.user.profilePhoto ? this.user.profilePhoto : defaultProfilePhoto;
            }
            console.log(profilePhoto);
            // UPDATE USER
            var status;
            var errorMsg;
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
                photoUrl: profilePhoto.url ? profilePhoto.url : "",
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
                    errorMsg = err.response.data;
                    status = err.response.status;
            });
            // Status Alert
            if (status === 200) {
                this.message = "Data has been updated!"
                this.showToastSuccess = true; 
                setTimeout(() => {
                    this.showToastSuccess = false
                }, 3000);
                setTimeout(() => {
                    this.$router.push("/profile");
                }, 3500);
            }
            else if (status === 400) {
                this.message = errorMsg;
                this.showToastError = true;
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            } else {
                this.message = "Sorry, there is an error on the server"
                this.showToastError = true;
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            }
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
            }
            else {
                this.user = response.data;
            }
        })
            .catch((err) => {
            console.log(err);
        });
        // Handle if there is no photo
        this.previewImageUrl = this.user.profilePhoto ? this.user.profilePhoto.url : "/profile.png";
    },
    components: { Toast }
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


.input-container {
    grid-area: detail;
    margin-top: 105px;
    margin-left: auto;
    margin-right: auto;
    z-index: 100;
}

.container-btn {
    grid-area: button;
    display: flex;
    column-gap: 8px;
    justify-self: center;
    margin-top: 15px;
}

.container-bg {
    grid-area: bg;
    justify-self: center;
    position: absolute;
    bottom: 70px;
    left: -25px;
    z-index: 5;
}


.btn-save {
    width: 112px;
    height: 50px;
    padding: 10px;
    background-color: #353642;
    color: #FFFFFF;
    border-radius: 100px;
    border: none;
    cursor: pointer;
    font-family: "Bebas Neue";
    font-size: 24px;
    transition: background-color 0.3s;
}

.btn-cancel {
    width: 112px;
    height: 50px;
    padding: 10px;
    background-color: #DEDDD8;
    color: #000000;
    border-radius: 100px;
    border: none;
    cursor: pointer;
    font-family: "Bebas Neue";
    font-size: 24px;
    border: 1px solid #353642;
    transition: background-color 0.3s;
}

.btn-save:hover {
    background-color: #171817;
}
.btn-cancel:hover {
    background-color: #b5b9b5;
}

.container-detail {
    text-align: left;
    margin-top: 5px;
}

.label {
    margin-left: 40px;
    font-family: 'Darker Grotesque';
    font-size: 18px;
}

.input {
    font-family: 'Darker Grotesque';
    font-size: 18px;
    width: 350px;
    height: 60px;
    background: rgba(255, 255, 255, 0);
    border: 1px solid #353642;
    border-radius: 100px;
    padding: 10px 25px;
    margin: 5px 0px 5px 0px;
}

.edit {
    position: absolute;
    width: 220px;
    height: 220px;
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

/** Toast */
.toast-enter-from,
.toast-leave-to {
    opacity: 0;
    transform: translateY(-60px);
}
.toast-enter-to,
.toast-leave-from {
    opacity: 1;
    transform: translateY(0px);
}
.toast-enter-active,
.toast-leave-active {
    transition: all 0.3s ease;
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

    .input-container {
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

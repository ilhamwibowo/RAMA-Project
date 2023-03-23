<template>
    <div class="profile">
        <div class="flex-container"> 
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
    color: rgb(10, 10, 10);
    font-size: 1.5rem;
    margin-left:20px
}
.input {
    border: 2px solid #5f5f5f;
    padding: 5px 10px;
    width: 100%;
    padding: 10px;
    margin-bottom: 20px;
    border-radius: 20px;
    border: 2px solid grey;
    color: rgb(63, 62, 62);
    font-size: 20px;
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

.container-detail {
    text-align: left;
}
.flex-container {
    display: grid;
    grid-template-columns: 1fr 2fr 1fr;
    grid-template-rows: auto;
    grid-gap: 20px;
    margin-left:125px;
    margin-right: 125px;
  }

.input-container {
    grid-column: 2 / 3;
    grid-row: 1 / 2;
}

.container-btn {
    grid-column: 3 / 4;
    grid-row: 1 / 2;
    justify-self: center;
}


.btn-save, .btn-cancel {
  width: 100px;
  height: 40px;
  padding: 10px;

  border-radius: 30px;
  border: none;
  cursor: pointer;
  font-family: 'Montserrat', sans-serif;
    font-weight: bold;
  transition: background-color 0.2s;
  margin: 5px;
}

.btn-save {
    background-color: #1f2220;
    color: #fff;
}

.btn-cancel {
    background-color: transparent;
    color: #000000;
    border:2px solid grey;
}

.btn-save:hover  {
  background-color: #171817;
}
.btn-cancel:hover {
    background-color: #b5b9b5
}
  /** Make the website responsive */
  @media screen and (max-width: 768px) {
    .flex-container {
      grid-template-columns: 1fr;
      grid-template-rows: auto auto auto;
      margin-left:20px;
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

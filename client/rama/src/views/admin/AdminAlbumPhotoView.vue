<template>
    <div class="album-photo">
        <!-- Toast -->
        <Transition name="toast">
            <Toast v-if="showToastSuccess" type="success" :message="message"/>
        </Transition>
        <Transition name="toastError">
            <Toast v-if="showToastError" type="error" :message="message"/>
        </Transition> 
        <Transition name="toast">
            <Toast v-if="showToastWarning" type="warning" :message="message"/>
        </Transition> 

        <AdminSidebar class="sidebar" album="true"/>
        <div class="layout">
            <div class="tabs">
                <RouterLink class="back" :to="{ name: 'albumAdmin'}" ><img src="/left-arrow.png"></RouterLink>
                <RouterLink class="tab" :to="{params: {id: this.id}, name: 'albumDetail'}">Detail Album</RouterLink> 
                <RouterLink class="tab page" :to="{params: {id: this.id}, name: 'listPhoto'}">List Photo</RouterLink> 
            </div>
            <div class="body">
                <div class="btn-container">
                    <input
                        type="file"
                        id="image"
                        accept="image/*"
                        multiple="multiple"
                        @change="uploadPhoto"
                        style="display: none"
                    />
                    <button class="btn light" @click="deleteClicked">Delete</button>
                    <button class="btn dark" @click="uploadClicked">Upload</button>
                </div>
                <div class="check-container">
                    <input type="checkbox" name="select-all" class="checkbox" @click="selectAllClicked($event)">
                    <label for="select-all">select all</label>
                </div>
                <div class="album">
                    <AdminAlbumPagination :photosInput="photoShow" :key="albumPaginationKey" />
                </div>
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
    </div>
</template>

<script>
import axios from 'axios';
import AdminSidebar from '@/components/AdminSidebar.vue';
import AdminAlbumPagination from '@/components/AdminAlbumPagination.vue';
import Pagination from '@/components/Pagination.vue';
import Toast from '@/components/Toast.vue';

import { useProductStore } from "@/stores/photos";
import { forEach } from 'jszip';
const photos = useProductStore();
export default {
    name: "AlbumDetailView",
    data() {
        return {
            listPhotos : [],
            id :this.$route.params.id,
            totalPhoto: 0,
            totalPage: 1,
            page: 1,
            pager: 10,
            photoShow: [],
            timer: 0,
            albumPaginationKey: 0,
            showToastError: false,
            showToastSuccess: false,
            showToastWarning: false,
            message: "",
        };
    },
    components: {
        AdminSidebar,
        AdminAlbumPagination,
        Pagination,
        Toast
},
    methods: {
      selectAllClicked(event) {
        if (event.target.checked) {
            this.listPhotos.forEach(function(photo) {
                const temp = {
                    id: photo.photoId,
                    url: photo.url
                }
                photos.addPhoto(temp);
            })
        } else {
            this.listPhotos.forEach(function(photo) {
                const temp = {
                    id: photo.photoId,
                    url: photo.url
                }
                photos.deletePhoto(temp);
            })
        }
        this.albumPaginationKey += 1;
      },
      async deleteClicked() {
        const listCheck = photos.PhotosCheckId();
        console.log(listCheck);
        for (let i = 0; i < listCheck.length; i++) {
            await this.deletePhoto(listCheck[i]);
        }
        location.reload();
      },
      async deletePhoto(id) {
        const token = localStorage.getItem("token");
        var status;
        var errorMsg;

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        await axios.delete(import.meta.env.VITE_API_URI + "/Album/photos/" + id, config)
        .then((response) => {
            status = response.status;
            console.log(response);
        })
        .catch((err) => {
            status = err.response.status;
            errorMsg = err.response.data;
            console.log(err);
        });

        // Status Alert
        console.log(status);
            if (status === 200) {
                this.message = "Data has been deleted!"
                this.showToastSuccess = true; 
                clearTimeout();
                setTimeout(() => {
                    this.showToastSuccess = false
                }, 3000);
                setTimeout(() => {
                    location.reload();
                }, 3500);
            }
            else if (status === 400) {
                this.message = errorMsg;
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            } else {
                this.message = "Sorry, there is an error on the server"
                this.showToastError = true;
                clearTimeout();
                setTimeout(() => {
                    this.showToastError = false
                }, 3000);
            }
      },
      uploadClicked() {
        document.getElementById("image").click();
      },
      async uploadPhoto(event) {
            // get input photos
            const inputPhotos = Array.from(event.target.files);
            this.message = "Uploading"
            this.showToastWarning = true;

            for (let i = 0; i < inputPhotos.length; i++) {
                await this.sendPostPhoto(inputPhotos[i]);
            }

            this.showToastWarning = false;
            this.message = "Data has been uploaded!"
                this.showToastSuccess = true; 
                clearTimeout();
                setTimeout(() => {
                    this.showToastSuccess = false;
                }, 3000);
                setTimeout(() => {
                    location.reload();
                }, 3500);
        },
        async sendPostPhoto(file) {
            var status;

            // get token
            const token = localStorage.getItem("token");

            // create form data
            var formData = new FormData();
            formData.append("file", file);

            // configuration for post api
            const configPhoto = {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data"
                }
            };

            await axios
                .post(
                    import.meta.env.VITE_API_URI + "/Album/" + this.id,
                    formData,
                    configPhoto
                )
                .then((response) => console.log(response))
                .catch((err) => console.log(err));
            if (this.timer) {
                clearTimeout(this.timer);
                this.timer = null;
            }
            this.timer = setTimeout(() => {
                this.albumPaginationKey += 1;
            }, 200)
        },

        /** Pagination */
        updatePage(n) {
            this.page = n;

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.photoShow = this.listPhotos.slice(start, end);
            this.albumPaginationKey += 1;
            console.log(this.photoShow);
        },
        updatePager(n) {
            this.pager = n;
            this.totalPage = Math.ceil(this.totalPhoto / this.pager);

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.photoShow = this.listPhotos.slice(start, end);
            this.albumPaginationKey += 1;
        }
    },
    async created() {
        console.log(this.id);
        // Get data
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        // Axios get Photo
        await axios
            .get(import.meta.env.VITE_API_URI + "/Album/" + this.id, config)
            .then((response) => {
                this.listPhotos = response.data.albumPhotos;
            })
            .catch((err) => {
                console.log(err);
            })
        
        this.totalPhoto = this.listPhotos.length;
        this.updatePage(this.page);
        this.updatePager(this.pager);
    }
};
</script>

<style scoped>
.album-photo{
    height: 100%;
    width: 100%;
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
.layout {
    grid-area: main;
    display: grid;
    margin:40px 50px 40px 40px;
    grid-template-rows: 70px auto;
    row-gap: 20px;
    grid-template-areas: 
    "tabs"
    "body"
}

.tabs {
    grid-area: tabs;
    display: flex;
    column-gap: 100px;
    align-items: center;
    background: #fff;
    padding: 5px 20px 10px 125px;
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
    left: 25px;
    background-color: rgba(0,0,0,0);
    border: none;
    cursor: pointer;
}

.body {
    grid-area: body;
    background: #fff;
    padding: 30px 20px 30px 20px;
    width: 100%;
    justify-content: center;
    border-radius: 15px;
}

.btn-container {
    position: absolute;
    top: 15px;
    right: 25px;
    z-index: 100;
}
.btn {
    font-family: "Bebas Neue";
    font-size: 20px;
    width: 112px;
    height: 49px;
    margin: 5px;
    padding: 10px 15px;
    border-radius: 100px;
    border-width: 1px;
    cursor: pointer;
}

.dark {
    background-color: #353642;
    color: #ffffff;
    border-color: #000000;
    /* border-block-width: ; */
}
.dark:hover {
    background-color: #0c0c0f;
}
.light {
    border-color: #353642;
    background-color: #f3f2ee;
}
.light:hover {
    background-color: #deddd8;
}

.album {
    max-width: 1100px;
    width: auto;
    margin: 0 auto; 
}

.check-container {
    font-family: "Darker Grotesque";
    font-size: 24px;
    display: grid;
    grid-template-columns: 27px auto;
    grid-template-rows: 30px;
    column-gap: 36px;
    align-items: center;
    margin-left: 78px
}

.checkbox {
    width: 27px;
    height: 27px;
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

.toastError-enter-from,
.toastError-leave-to {
    opacity: 0;
    transform: translateY(-60px);
}
.toastError-enter-to,
.toastError-leave-from {
    opacity: 1;
    transform: translateY(0px);
}
.toastError-enter-active {
    animation: wobble 0.5s;
}
.toastError-leave-active {
    transition: all 0.3s ease;
}

@keyframes wobble {
    0% { transform: translateY(-60px); opacity: 0; }
    50% { transform: translateY(0px); opacity: 1; }
    60% { transform: translateX(8px);}
    70% { transform: translateX(-8px);}
    80% { transform: translateX(4px);}
    90% { transform: translateX(-4px);}
    100% { transform: translateX(0px);}
}

</style>
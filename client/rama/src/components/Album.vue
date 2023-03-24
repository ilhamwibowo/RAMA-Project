<template>
    <div>
        <p class="title">Album Page</p>
        <br />
        <div id="box">
            <AlbumSearch @searchInput="search" id="search" />
            <div id="button">
                <input
                    type="file"
                    id="image"
                    accept="image/*"
                    multiple="multiple"
                    @change="uploadPhoto"
                    style="display: none"
                />
                <button class="btn upload" @click="uploadClicked">Upload</button>
                <button class="btn download" @click="download">Download</button>
            </div>
        </div>
        <br /><br /><br /><br />
        <AlbumPagination :photosInput="photoShow" :key="albumPaginationKey" />
        <br /><br /><br /><br />
        <Pagination
            :totalPage="totalPage"
            :pager="pager"
            :page="page"
            @changePage="updatePage"
            @changePager="updatePager"
        />
    </div>
</template>

<script>
import JSZip from "jszip";
import FileSaver from "file-saver";
import axios from "axios";
import AlbumPhoto from "../components/AlbumPhoto.vue";
import AlbumSearch from "../components/AlbumSearch.vue";
import Pagination from "../components/Pagination.vue";
import AlbumPagination from "../components/AlbumPagination.vue";
import { useProductStore } from "../stores/photos";

const photos = useProductStore();
export default {
    name: "Album",
    data() {
        return {
            listPhotos: [],
            totalPhoto: 0,
            totalPage: 0,
            page: 1,
            pager: 10,
            photoShow: [],
            albumPaginationKey: 0,
            timer: 0,
            albumId: 1
        };
    },
    components: {
        AlbumPhoto,
        AlbumSearch,
        Pagination,
        AlbumPagination
    },
    methods: {
        /** Search */
        async search(keySearch) {
            // Get data
            const token = localStorage.getItem("token");

            // Configuration for API
            const config = {
                headers: { Authorization: `Bearer ${token}` }
            };

            // Axios Get
            await axios
                .get(
                    import.meta.env.VITE_API_URI +
                        "/Album/" +
                        this.albumId +
                        "?query=" +
                        encodeURI(keySearch),
                    config
                )
                .then((response) => {
                    this.listPhotos = response.data.photos;
                })
                .catch((err) => {
                    console.log(err);
                });

            this.totalPhoto = this.listPhotos.length;

            this.updatePage(this.page);
        },

        /** Download */
        download() {
            const zip = new JSZip();

            const listCheck = photos.PhotosCheckUrl();
            for (let i = 0; i < listCheck.length; i++) {
                const fileName = listCheck[i].split("/").at(-1);
                const imageUrl = listCheck[i];

                let blob = fetch(imageUrl).then((r) => r.blob());
                zip.file(fileName, blob);
            }

            zip.generateAsync({ type: "blob" }).then(function (content) {
                FileSaver.saveAs(content, "download.zip");
            });
            photos.clearPhoto();
            if (this.timer) {
                clearTimeout(this.timer);
                this.timer = null;
            }
            this.timer = setTimeout(() => {
                this.albumPaginationKey += 1;
            }, 800);
        },
        clearPhoto() {
            // Clear photo checklist after download
            photos.clearPhoto();
        },

        /** Upload Photo */
        uploadPhoto(event) {
            // get input photos
            const inputPhotos = Array.from(event.target.files);

            // Post one by one
            inputPhotos.forEach((file) => {
                this.sendPostPhoto(file);
            });
            this.albumPaginationKey += 1;
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
                    import.meta.env.VITE_API_URI + "/Album/" + this.albumId,
                    formData,
                    configPhoto
                )
                .then((response) => console.log(response))
                .catch((err) => console.log(err));
        },
        uploadClicked() {
            document.getElementById("image").click();
        },

        /** Pagination */
        updatePage(n) {
            this.page = n;

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.photoShow = this.listPhotos.slice(start, end);
        },
        updatePager(n) {
            this.pager = n;
            this.totalPage = Math.ceil(this.totalPhoto / this.pager);

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.photoShow = this.listPhotos.slice(start, end);
        }
    },

    async created() {
        // Get data
        const token = localStorage.getItem("token");

        // Configuration for API
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };

        // Axios Get
        await axios
            .get(import.meta.env.VITE_API_URI + "/Album/" + this.albumId, config)
            .then((response) => {
                this.listPhotos = response.data.photos;
            })
            .catch((err) => {
                console.log(err);
            });

        this.totalPhoto = this.listPhotos.length;
        this.updatePage(this.page);
        this.updatePager(this.pager);
    }
};
</script>

<style scoped>
#box {
    display: grid;
    grid-template-columns: 1fr auto 1fr;
    grid-template-areas: "none search button";
}
#search {
    grid-area: search;
    justify-content: center;
}
#button {
    display: flex;
    grid-area: button;
    justify-content: end;
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
.upload {
    background-color: #353642;
    color: #ffffff;
    border-color: #000000;
    /* border-block-width: ; */
}
.upload:hover {
    background-color: #0c0c0f;
}
.download {
    border-color: #353642;
    background-color: #f3f2ee;
}
.download:hover {
    background-color: #deddd8;
}
</style>

<template>
    <div class="album-photo">
        <AdminSidebar class="sidebar"/>
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
                <div class="header">
                    <input type="checkbox" name="select-all">
                    <label for="select-all">select all</label>
                </div>
                <div class="album">
                    <AdminAlbumPagination :photosInput="photoShow" :key="albumPaginationKey" />
                </div>
                <pagination
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
            albumPaginationKey: 0,
        };
    },
    components: {
        AdminSidebar,
        AdminAlbumPagination,
        Pagination
},
    methods: {
      deleteClicked() {

      },
      uploadClicked() {
        document.getElementById("image").click();
      },
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
                    import.meta.env.VITE_API_URI + "/Album/" + this.id,
                    formData,
                    configPhoto
                )
                .then((response) => console.log(response))
                .catch((err) => console.log(err));
        },

        /** Pagination */
        updatePage(n) {
            this.page = n;

            let page = this.page;
            let pager = this.pager;

            let start = pager * (page - 1);
            let end = pager * page;
            this.photoShow = this.listPhotos.slice(start,end);
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

.header {
    width: 100%;
    height: auto;
    margin-top: 2%;
    text-align: left;
    margin-left: 2%;
}

.btn-container {
    position: absolute;
    top: 15px;
    right: 25px;
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

</style>
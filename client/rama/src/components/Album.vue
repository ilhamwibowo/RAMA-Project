<template>
    <div>
        <p class="title">Album Page</p>
        <AlbumSearch @searchInput="search"/>
        
        <div>
            <input type="file" id="image" accept="image/*" multiple="multiple" @change="uploadPhoto" style="display:none;"/>
            <button class="btn upload" @click="uploadClicked" >Upload</button>
            <button class="btn download" @click="download">Download</button>
        </div>

        <AlbumPagination :photosInput="photoShow" :key="albumPaginationKey" />
        <Pagination :totalPage="totalPage" :pager="pager" :page="page" @changePage="updatePage" @changePager="updatePager"/>
    </div>
</template>

<script>
import JSZip from 'jszip';
import FileSaver from 'file-saver';
import AlbumPhoto from '../components/AlbumPhoto.vue'
import AlbumSearch from '../components/AlbumSearch.vue'
import Pagination from '../components/Pagination.vue';
import AlbumPagination from '../components/AlbumPagination.vue';
import { useProductStore } from '../stores/photos';

const photos = useProductStore();
export default {
    name: "Album",
    data() {
        return {
            listPhotos: {
                photos: [
                    {
                        id: "1",
                        url: "/1.jpg"
                    },
                    {
                        id: "2",
                        url: "/2.jpg"
                    },
                    {
                        id: "3",
                        url: "/3.jpg"
                    },
                    {
                        id: "4",
                        url: "/4.jpg"
                    },
                    {
                        id: "5",
                        url: "/5.jpg"
                    },
                    {
                        id: "6",
                        url: "/6.jpg"
                    },
                    {
                        id: "7",
                        url: "/7.jpg"
                    },
                    {
                        id: "8",
                        url: "/8.jpg"
                    },
                    {
                        id: "9",
                        url: "/9.jpg"
                    },
                    {
                        id: "10",
                        url: "/10.jpg"
                    },
                    {
                        id: "11",
                        url: "/11.jpg"
                    },
                    {
                        id: "12",
                        url: "/12.jpg"
                    },
                    {
                        id: "13",
                        url: "https://www.animesoulking.com/wp-content/uploads/2020/12/Kakushigoto.jpg"
                    },
                    {
                        id: "14",
                        url: "https://cdn.myanimelist.net/images/characters/16/436260.jpg"
                    }
                ]
            },
            totalPhoto: 0,
            totalPage: 0,
            page: 1,
            pager: 10,
            photoShow: [],
            albumPaginationKey: 0,
            timer: 0
        }
    },
    components: {
        AlbumPhoto,
        AlbumSearch,
        Pagination,
        AlbumPagination,
        AlbumPhotoDetail
    },
    methods: {
        search(keySearch) {
            console.log("Key: ", keySearch);
            /** TODO: Fetch data according to keySearch */
        },
        download() {
            console.log("download");
            const zip = new JSZip();

            const listCheck = photos.PhotosCheckUrl();
            for (let i = 0; i < listCheck.length; i++) {
                const fileName = listCheck[i].split("/").at(-1);
                const imageUrl = listCheck[i];

                let blob = fetch(imageUrl).then(r => r.blob());
                zip.file(fileName, blob);
            }

            zip.generateAsync({ type: 'blob' }).then(function (content) {
                FileSaver.saveAs(content, 'download.zip');
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
        uploadPhoto(event) {
            const inputPhotos = event.target.files;
            console.log(inputPhotos);
        },
        clearPhoto() {
            photos.clearPhoto();
        },
        updatePage(n) {
            this.page = n;

            let page = this.page
            let pager = this.pager

            let start = (pager * (page-1))
            let end = (pager * page)
            this.photoShow = this.listPhotos.photos.slice(start, end)
        },
        updatePager(n) {
            this.pager = n;
            this.totalPage = Math.ceil(this.totalPhoto/this.pager)

            let page = this.page
            let pager = this.pager

            let start = (pager * (page-1))
            let end = (pager * page)
            this.photoShow = this.listPhotos.photos.slice(start, end)
        }
    },

    async created() {
        let page = this.page
        let pager = this.pager

        // Update Data
        this.totalPhoto = this.listPhotos.photos.length
        this.totalPage = Math.ceil(this.totalPhoto/pager)

        let start = (pager * (page-1))
        let end = (pager * page)
        this.photoShow = this.listPhotos.photos.slice(start, end)
        console.log(this.photoShow)
    }

}
</script>

<style scoped>
.photo-container {
    column-count: 3;
    column-gap: 25px;
}
.btn {
    font-family: "Bebas Neue";
    font-size: 20px;
    width: 112px;
    height: 49px;
    margin: 5px;
    padding:10px 15px;
    border-radius: 100px;
    border-width: 1px;
    cursor: pointer;
}
.upload {
    background-color: #353642;
    color: #FFFFFF;
    border-color: #000000;
    /* border-block-width: ; */
}
.upload:hover {
    background-color: #0C0C0F;
}
.download {
    border-color: #353642;
    background-color: #F3F2EE;
}
.download:hover {
    background-color: #DEDDD8;
}
</style>
<template>
    <div>
        <p class="title">Album Page</p>
        <AlbumSearch @searchInput="search"/>
        <div>
            <input type="file" id="image" accept="image/*" multiple="multiple" @change="uploadPhoto" style="display:none;"/>
            <button class="btn upload" @click="uploadClicked" >Upload</button>
            <button class="btn download" @click="download">Download</button>
        </div>
        <div class="photo-container">
            
            <AlbumPhoto 
                v-for="photo in listPhotos.photos" 
                :key="photo.id" 
                :photoId="photo.id" 
                :photoUrl="photo.url" 
                @updateCheck="updateListCheck"/>
        </div>
    </div>
</template>

<script>
import JSZip from 'jszip';
import FileSaver from 'file-saver';
import AlbumPhoto from '../components/AlbumPhoto.vue'
import AlbumSearch from '../components/AlbumSearch.vue'
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
            listCheck: []
        }
    },
    components: {
        AlbumPhoto,
        AlbumSearch
    },
    methods: {
        search(keySearch) {
            console.log("Key: ", keySearch);
            /** TODO: Fetch data according to keySearch */
        },
        updateListCheck(data) {
            if (data.isCheck) {
                this.listCheck.push(data.url);
            } else {
                this.listCheck = this.listCheck.filter((e) => { return e !== data.url});
            }
            console.log(this.listCheck);
        },
        download() {
            console.log("download");
            const zip = new JSZip();
            for (let i = 0; i < this.listCheck.length; i++) {
                const fileName = this.listCheck[i].split("/").at(-1);
                const imageUrl = this.listCheck[i];

                console.log("Name", fileName);

                let blob = fetch(imageUrl).then(r => r.blob());
                zip.file(fileName, blob);
            }

            zip.generateAsync({ type: 'blob' }).then(function (content) {
                FileSaver.saveAs(content, 'download.zip');
            });
        },
        uploadClicked() {
            document.getElementById("image").click();
        },
        uploadPhoto(event) {
            const inputPhotos = event.target.files;
            console.log(inputPhotos);
        }
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
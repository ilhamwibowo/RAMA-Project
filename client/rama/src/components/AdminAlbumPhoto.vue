<template>
    <div class="border">
        <div class="photo-list">
            <input
                id="check"
                class="checkbox"
                type="checkbox"
                :value="photoId"
                @change="onClickCheck($event)"
                :checked="isCheck"
            />
            <img :src="url">
            <p>{{ name }}</p>
        </div>
    </div>
</template>

<script>
import { useProductStore } from '../stores/photos';
const photos = useProductStore();

export default {
    name: "AdminAlbumPhoto",
    data() {
        return {
            name: "",
            isCheck: false,
            url: ""
        }
    },
    components: {

    },
    props: {
        photoUrl: String,
        photoId: Number
    },
    methods: {
        onClickCheck(event) {
            // console.log(event);
            let isCheck = event.target.checked;
            let photo = { id: history.photoId, url: this.photoUrl};
            this.isCheck = isCheck;
            if (isCheck) {
                photos.addPhoto(photo);
            } else {
                photos.deletePhoto(photo);
            }
        }
    },
    async created() {
        // check
        this.isCheck = photos.isFound({ id: this.photoId, url: this.photoUrl });

        // photo name
        let index = this.photoUrl.lastIndexOf("/") + 1;
        this.name = this.photoUrl.substring(index);
        
        // photo img url
        index = this.photoUrl.indexOf("upload/") + 7;
        this.url = this.photoUrl.substring(0, index) + "w_64/f_auto/" + this.photoUrl.substring(index);
    }
}
</script>

<style scoped>
.border {
    padding: 10px;
    border: solid 1px black;
    border-radius: 15px;
    width: 350px;
    height: 84px;
    align-items: center;
}
.photo-list {
    display: grid;
    grid-template-columns: 27px 64px auto;
    grid-template-rows: 64px;
    column-gap: 10px;
    align-items: center;
}

.checkbox {
    width: 27px;
    height: 27px;
}

img {
    width: 64px;
    height: auto;
}

p {
    font-family: "Darker Grotesque";
    font-size: 20px;
}
</style>
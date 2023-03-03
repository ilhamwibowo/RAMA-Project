<template>
    <figure>
        <img :src="photoUrl">
        <input id="check" class="checkbox" type="checkbox" :value=photoId @change="onClickCheck($event)" :checked="isCheck">
    </figure>
</template>

<script>
import { useProductStore } from '../stores/photos';
const photos = useProductStore();

export default {
    name: "AlbumPhoto",
    data() {
        return {
            isCheck: false
        }
    },
    props: {
        photoUrl: String,
        photoId: String,
    },
    methods: {
        onClickCheck(event) {
            // let isCheck = event.target.checked
            // let url = event.target.value
            // console.log("Clicked: ", isCheck, url)
            // this.$emit("updateCheck", {'isCheck': isCheck, 'url': url, 'id': id})

            let isCheck = event.target.checked;
            let photo = {'id': this.photoId, 'url': this.photoUrl};
            this.isCheck = isCheck
            if (isCheck) {
                photos.addPhoto(photo);
            } else {
                photos.deletePhoto(photo);
            }
        }
    },
    async created() {
        this.isCheck = (photos.isFound({'id': this.photoId, 'url': this.photoUrl})) 
    }
}
</script>

<style scoped>
figure {
    margin: 0;
    display: grid;
    grid-template-columns: 30px auto;
    grid-template-rows: 30px auto;
    margin-bottom: 25px;
    break-inside: avoid;
}

figure > img {
    grid-row: 1 / -1;
    grid-column: 1 / -1;
}

figure > .checkbox {
    grid-row: 1;
    grid-column: 1;
}

.checkbox {
    opacity: 0;
    transition: 0.3s;
}

img {
    display: block;
    max-width: 100%;
    border-radius: 5px;
    /* height: auto; */
}

img:hover ~ .checkbox {
    opacity: 1;
}

.checkbox:hover {
    opacity: 1;
}
.checkbox:checked {
    opacity: 1;
}
</style>
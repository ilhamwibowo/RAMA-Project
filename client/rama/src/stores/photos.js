import { defineStore } from "pinia";

export const useProductStore = defineStore("product", {
    state: () => ({
        photos: []
    }),
    getters: {
        GetCheckId(state) {
            var returnValue = [];
            for (let i = 0; i < state.photos.length; i++) {
                returnValue.push(state.photos[i].id);
            }
            return returnValue;
        }
    },
    actions: {
        addPhoto(photo) {
            this.photos.push(photo);
            console.log("Photos", this.photos);
        },
        deletePhoto(photo) {
            this.photos = this.photos.filter((e) => e.id !== photo.id);
            console.log("Photos", this.photos);
        },
        clearPhoto() {
            this.photos = [];
        },
        isFound(photo) {
            // console.log(this.photos.find(x => x.id === photo.id))
            if (this.photos.find((x) => x.id === photo.id)) return true;
            else return false;
        },
        PhotosCheckUrl() {
            var returnValue = [];
            for (let i = 0; i < this.photos.length; i++) {
                returnValue.push(this.photos[i].url);
            }
            console.log("Return", returnValue);
            return returnValue;
        },
        PhotosCheckId() {
            var returnValue = [];
            for (let i = 0; i < this.photos.length; i++) {
                returnValue.push(this.photos[i].id)
            } 
            console.log("Return", returnValue);
            return returnValue;
        }
    }
});

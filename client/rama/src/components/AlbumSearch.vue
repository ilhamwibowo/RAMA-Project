<template>
    <div class="search">
        <input type="text" class="search" v-model=keySearch @input="sendInput">
        <p>{{ text }}</p>
    </div>    
</template>

<script>
export default {
    name: 'AlbumSearch',
    data() {
        return {
            keySearch: "",
            text: "",
            timer: 0
        }
    }, 
    methods: {
        sendInput() {
            if (this.timer) {
                clearTimeout(this.timer);
                this.timer = null;
            }
            this.timer = setTimeout(() => {
                /** Search input */
                // console.log(this.keySearch);
                // this.text = this.keySearch;
                this.$emit("searchInput", this.keySearch)
            }, 800);
        }
    }
}
</script>

<style scoped>

</style>
<template>
    <div class="search">
        <input type="text" id="input-search" v-model="keySearch" @input="sendInput" />
        <img src="search.png" id="img-search" />
    </div>
</template>

<script>
export default {
    name: "AlbumSearch",
    data() {
        return {
            keySearch: "",
            text: "",
            timer: 0
        };
    },
    methods: {
        sendInput() {
            if (this.timer) {
                clearTimeout(this.timer);
                this.timer = null;
            }
            this.timer = setTimeout(() => {
                /** Search input */
                this.$emit("searchInput", this.keySearch);
            }, 800);
        }
    }
};
</script>

<style scoped>
.search {
    display: grid;
    justify-content: center;
    grid-template-columns: auto;
    grid-template-areas: "icon box";
}

#img-search {
    grid-area: icon;
    height: 50px;
    padding-top: 10px;
    padding-left: 15px;
}

#input-search {
    grid-area: icon;
    background-color: white;
    border: black 1px solid;
    padding: 20px 20px 20px 60px;
    height: 60px;
    width: 50vw;
    max-width: 650px;
    border-radius: 100px;
    font-size: 22px;
    font-family: "darker grotesque";
}
</style>

<template>
    <div class="list-sidebar">
        <img class="icon" :src="imageSrc">
        <p v-if="isPage" class="text" style="color: black;">{{ text }}</p>        
        <p v-else class="text" style="color: #A4A4A4;">{{ text }}</p>        
    </div>
</template>

<script>
export default {
    name: "AdminSidebarList",
    props: {
        iconSrc: String,
        text: String,
        isPage: Boolean
    },
    data() {
        return {
            imageSrc: "",
        }
    },
    created() {
        if (this.isPage)
        {
            let index = this.iconSrc.indexOf("-");
            this.imageSrc = this.iconSrc.substring(0, index) + "-page" + this.iconSrc.substring(index);
        } 
        else 
        {
            this.imageSrc = this.iconSrc;
        }
    }
}
</script>

<style scoped>
.list-sidebar {
    display: flex;
    color: black;
    column-gap: 16px;
    align-items: center;
}
.icon {
    height: 34px;
    width: 34px;
}
.text {
    font-family: 'Darker Grotesque';
    font-size: 28px;
    font-weight: bold;
}
</style>
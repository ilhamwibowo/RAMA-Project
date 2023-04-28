<template>
    <div class="navpage">
        <button class="button" @click="onClick"><img src="/navbar-profile.png" ></button>
        <div v-if="isOpen" class="popup">
            <RouterLink class="text" to="/profile">My Profile</RouterLink>
            <RouterLink class="text" to="/logout">Log Out</RouterLink>
        </div>
    </div>    
</template>

<script>

export default {
    name: "NavigationProfile",
    data() {
        return {
            isOpen: false
        }
    },
    methods: {
        onClick() {
            this.isOpen = !this.isOpen;
        },
        onClickOutside() {
            this.isOpen = false;
        }
    }
    
}
</script>

<style scoped>
.navpage {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
}


img {
    width: 80px;
}

.button {
    border: none;
    background-color: rgba(0,0,0,0)
}

.popup {
    position: absolute;
    width: 240px;
    padding: 38px;
    top: 100px;
    right: 0px;
    border-radius: 15px;
    background-color:white;
    box-shadow: 0px 4px 30px 7px rgba(0,0,0,0.1);
    z-index: 200;
    display: flex;
    flex-direction: column;
    row-gap: 10px;
}

.popup .text {
    text-decoration: none;
    color: black;
    font-family: "Bebas Neue";
    font-size: 30px;
}

</style>
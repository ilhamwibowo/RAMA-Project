<template>
    <div class="home">
        <p class="header-1">Run For You Freedom</p>
        <img class="image" src="/home-image.png" >
        <p class="subtext">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras est augue, tincidunt et lacus a, rutrum iaculis diam. Vestibulum ultrices viverra tellus, et tincidunt magna eleifend ac.</p>
    </div>
</template>

<script>
export default {
    name: "Home"
}
</script>

<style scoped>
.home {
    position: relative;
    text-align: center;
    margin: 0 200px 200px 200px;
}

.header-1 {
    position: absolute;
    left: 100px;
    width: 385px;
    height: 210px;
    font-family: "Bebas Neue";
    font-size: 72px;
    text-align: left;

}

.image {
    top: 150px;
    width: 50%;
}

.subtext {
    position: absolute;
    right: 10px;
    top: 280px;
    width: 322px;
    height: 227px;
    font-family: "Darker Grotesque";
    font-size: 24px;
    font-weight: 400;
    text-align: left;
}
</style>
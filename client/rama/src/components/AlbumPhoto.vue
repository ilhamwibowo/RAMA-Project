<template>
    <figure>
        <img :src="photoUrl">
        <input class="checkbox" type="checkbox" :value=photoId>
    </figure>
</template>

<script>
export default {
    name: "AlbumPhoto",
    props: {
        photoUrl: String,
        photoId: String,
    }
}
</script>

<style scoped>
figure {
    margin: 0;
    display: grid;
    grid-template-columns: 50px auto;
    grid-template-rows: 50px auto;
    margin-bottom: 45px;
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
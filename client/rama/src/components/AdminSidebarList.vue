<template>
    <div class="list-sidebar">
        <img src="iconSrc">
        <p class="text">{{ text }}</p>
    </div>
</template>

<script>
export default {
    name: "AdminSidebarList",
    props: {
        iconSrc: String,
        text: String
    }
}
</script>

<style scoped>
.list-sidebar {
    display: flex;
    color: black;
}
</style>
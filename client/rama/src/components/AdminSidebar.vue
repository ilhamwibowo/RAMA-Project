<template>
    <div class="sidebar">
        <div class="title">
            <h1>RAMA</h1>
        </div>
        <div class="list">
            <AdminSidebarList iconSrc="" text="Ikan" />
        </div>
    </div>
</template>

<script>
import AdminSidebarList from './AdminSidebarList.vue';
export default {
    name: "AdminSidebar",
    components: {
        AdminSidebarList
    }
}
</script>
<style scoped>
.sidebar {
    background-color: white;
    border: 1px #C0C0C0 solid;
    display: grid;
    grid-template-rows: 100px auto;
    grid-template-areas: 
    "title"
    "list";
}
.title {
    grid-area: title;
    color: black;
    font-size: 24px;
    display: flex;
    align-items: center;
    justify-content: center;
}
.list {
    grid-area: list;
}
</style>
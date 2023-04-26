<template>
    <div class="sidebar">
        <div class="title">
            <h1>RAMA</h1>
        </div>
        <div class="list">
            <AdminSidebarList iconSrc="/dashboard-icon.png" text="Dashboard" :isPage="dashboard" />
            <AdminSidebarList iconSrc="/accounts-icon.png" text="Accounts" :isPage="account" />
            <RouterLink class="link" to="/event"><AdminSidebarList iconSrc="/events-icon.png" text="Events" :isPage="event"/></RouterLink>
            <RouterLink class="link" :to="{ name: 'albumAdmin'}"><AdminSidebarList iconSrc="/album-icon.png" text="Album" :isPage="album"/></RouterLink>
        </div>
    </div>
</template>

<script>
import AdminSidebarList from './AdminSidebarList.vue';
export default {
    name: "AdminSidebar",
    components: {
        AdminSidebarList
    },
    props: {
        dashboard: false,
        account: false,
        event: false,
        album: false
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
    margin: 100px 70px;
    display: flex;
    flex-direction: column;
    row-gap: 38px;
}

.link {
    text-decoration: none;
}
</style>
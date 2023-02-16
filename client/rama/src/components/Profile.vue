<template>
    <div class="profile">
        <h1>Profile</h1>
        <div class="container-image">
            <img :src="user.image" />
        </div>
        <DetailProfile title="Name" :text="user.Name"/>
        <DetailProfile title="Email" :text="user.Email"/>
        <DetailProfile title="KTP" :text="user.KTP"/>
        <DetailProfile title="Phone Number" :text="user.No_HP"/>
        <DetailProfile title="Birthday" :text="user.Birthday"/>
        <router-link :to="{ path: '/profile/edit/'}">
            <button class="btn edit">Edit</button>
        </router-link>
    </div>
</template>

<script>
import DetailProfile from './DetailProfile.vue';
// let response = await fetch('http://localhost:5226/api/User/1',
//     {
//         method: 'GET',
//         mode: 'cors',
//         headers: {
//             'Content-Type': 'application/json'
//         },
//     }).then(response => response.json());
// const user = await fetch('http://localhost:5226/api/User/1')
// console.log(user)
export default {
    name: 'Profile',
    components: {
        DetailProfile
    },
    data() {
        return {
            user: {
                AccId: 1,
                Name: 'Muhammad Akmal Arifin',
                Email: 'akmal@gmail.com',
                Role: 'user',
                KTP: '3515070000000001',
                No_HP: '082200000001',
                Birthday: '25-09-2002',
                image: 'https://images.pexels.com/photos/771742/pexels-photo-771742.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500'
            }
        }
    },
}
</script>

<style scoped>
.container-image {
    display: flex;
    margin: 0 auto;
    width: 150px;
    height: 150px;
    position: relative;
    align-items: center;
    overflow: hidden;
    border-radius: 50%;
}

img {
    display: inline;
    margin: 0 auto;
    height: 100%;
    width: auto;
}
</style>
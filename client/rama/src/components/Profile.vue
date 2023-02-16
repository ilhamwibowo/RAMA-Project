<template>
    <div class="profile">
        <h1>Profile</h1>
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
            }
        }
    }
}
</script>
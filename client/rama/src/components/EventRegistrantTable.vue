<template>
    <div class="table-container">
        <div class="helper">
            <button v-if="isEdit" @click="saveChanges">Save Changes</button>
            <button v-if="isEdit" @click="cancel">Cancel</button>
            <button v-if="!isEdit" @click="beginEdit">Edit</button>
        </div>
        <br/>
        <table class="event-table">
            <thead class="table-head">
                <tr class="table-row-header">
                    <th class="table-header" scope="col">Name</th>
                    <th class="table-header" scope="col">Registered At</th>
                    <th class="table-header" scope="col">Paid At</th>
                    <th class="table-header" scope="col">Taken Kit At</th>
                    <th class="table-header" scope="col">RFID</th>
                    <th class="table-header" scope="col">Status</th>
                </tr>
            </thead>
            <tbody class="table-body">
                <tr class="table-row-body" v-for="(user, index) in data" :key="index">
                    <td class="table-data" v-text="names[index]"></td>
                    <td class="table-data" v-text="(new Date(user.registedAt)).toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',hour: 'numeric', minute:'numeric' })"></td>
                    <td class="table-data" v-if="user.paidAt == '0001-01-01T00:00:00'">Not Yet</td>
                    <td class="table-data" v-else v-text="(new Date(user.paidAt)).toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',hour: 'numeric', minute:'numeric' })"></td>

                    <td class="table-data" v-if="user.takenKitAt == '0001-01-01T00:00:00'">Not Yet</td>
                    <td class="table-data" v-else v-text="(new Date(user.takenKitAt)).toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',hour: 'numeric', minute:'numeric' })"></td>
                    <td class="table-data" v-if="this.isEdit" >
                        <input :id="'rfid_'+index" @change="this.rfidChange($event)">
                    </td>
                    <td class="table-data" v-else v-text="user.rfid"></td>

                    <td class="table-data" v-if="this.isEdit" >
                        <select size="1" :id="'status_'+index" name="row-1-office" @change="this.statusChange($event)">
                            <option value="Waiting" :selected="user.statusAsText=='Waiting'">
                                Waiting
                            </option>
                            <option value="Paid" :selected="user.statusAsText=='Paid'">
                                Paid
                            </option>
                            <option value="TakenKit" :selected="user.statusAsText=='TakenKit'">
                                TakenKit
                            </option>
                            <option value="Completed" :selected="user.statusAsText=='Completed'">
                                Completed
                            </option>
                        </select>
                    </td>
                    <td class="table-data" v-if="!this.isEdit" v-text="user.statusAsText"></td>
                </tr>
            </tbody>
        </table>
    </div>
    
</template>
<script>
import axios from 'axios';
import Pagination from "../components/Pagination.vue"

export default {
    name: "EventRegistrantTable",
    props: {
        id: String,
        names: Array,
        data: Array,
    },
    components: {
        Pagination,
    },
    data() {
        return {
            edited : {},
            rfid : {},
            isEdit : false,
        };
    },
    methods: {
            statusChange(event) {
                let id = this.data[event.target.id.split("_")[1]].accId
                let val = event.target.value
                this.edited[id] = val
            },
            rfidChange(event) {
                let id = this.data[event.target.id.split("_")[1]].accId
                let val = event.target.value
                this.rfid[id] = val
            },
            async saveChanges(){
                if (confirm("Kamu Yakin ingin mengapilkasikan perubahan?"))
                {
                    if (this.edited != {})
                    await Promise.all(
                        Object.keys(this.edited).map(
                            async (key) => await this.changeStatus(this.id, key, this.edited[key])
                        )
                    )
                    if (this.rfid != {})
                    await Promise.all(
                        Object.keys(this.rfid).map(
                            async (key) => await this.changeRfid(this.id, key, this.rfid[key])
                        )
                    )
                    location.reload()
                }
                
            },
        async changeStatus(raceid, accid, statusastext) {
            let status;
            switch (statusastext){
                case 'Waiting':
                    status = 0;
                    break;
                case 'Paid':
                    status = 1;
                    break;
                case 'TakenKit':
                    status = 2;
                    break;
                case 'Completed':
                    status = 3;
                    break;
                default:
                    return;
                
            }
            console.log(status)
            // get token
            const token = localStorage.getItem("token");

            // configuration for post api
            const config = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };
            await axios
                .put(
                    import.meta.env.VITE_API_URI + "/Registration/" + raceid + "/" + accid,
                    {
                        status: status
                    },
                    config
                )
                .then((response) => console.log(response))
                .catch((err) => console.log(err));
        },
        async changeRfid(raceid, accid, rfid){
            const token = localStorage.getItem("token");

            // configuration for post api
            const config = {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            };
            await axios
                .put(
                    import.meta.env.VITE_API_URI + "/Registration/" + raceid + "/" + accid,
                    {
                        "rFID": rfid
                    },
                    config
                )
                .then((response) => console.log(response))
                .catch((err) => console.log(err));
        },
        cancel(){
            location.reload();
        },
        beginEdit(){
            this.isEdit = true
        }

    },
}
</script>
    <style scoped>
    .helper {
        display: flex;
        justify-content: space-between;
    }

    .table-container{
        width: 80%;
        left: 10%;
        background: #fff;
        border-radius: 10px;
    }
    .event-table {
        border-collapse: collapse;
        text-align: center;

        margin-bottom: 1rem;
        color: #000;
        padding: 20px;

        width: 100%;
        table-layout: fixed;
    }
    .table-row-header {
        border-bottom: 2px solid #272626;
        font-family: "Montserrat", sans-serif;
        font-size: 120%;
    }
    .table-header {
        width: 100% / 5;
    }
</style>
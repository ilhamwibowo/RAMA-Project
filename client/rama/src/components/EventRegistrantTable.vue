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
                    <td class="table-data" v-if="this.isEdit">
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
                    <!-- <td class="table-data">h</td>
                    <td class="table-data">
                        <p class="open-regis" id="open-regis" v-if="event.isOpened">Opened</p>
                        <p class="close-regis" id="close-regis" v-if="!event.isOpened">Closed</p>
                    </td>
                    <td class="table-data">
                        <p class="status-publish" id="published" v-if="event.isPublished">
                            Published
                        </p>
                        <p class="status-publish-not" id="notPublished" v-if="!event.isPublished">
                            Unpublished
                        </p>
                    </td> -->
                </tr>
            </tbody>
        </table>
    </div>
    
</template>
<script>
import axios from 'axios';


export default {
    name: "EventRegistrantTable",
    props: {
        id: String,
        names: Array,
        data: Array,
    },
    data() {
        return {
            edited : {},
            isEdit : false,
        };
    },
    methods: {
            statusChange(event) {
                console.log(event.target)
                let id = this.data[event.target.id.split("_")[1]].accId
                let val = event.target.value
                console.log(this.edited)
                this.edited[id] = val
                console.log(this.edited)
            },
            saveChanges(){
                if (confirm("Kamu Yakin ingin mengapilkasikan perubahan?"))
                {
                    Object.keys(this.edited).forEach( k => 
                    {
                        this.changeStatus(this.id, k, this.edited[k])
                    })
                    location.reload()
                }
                
            },
        async changeStatus(raceid, accid, statusastext) {
            let status;
            switch (statusastext){
                case 'Waiting':
                    status = 0
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
    .search {
        display: flex;
    }
    .layout {
        height: 100%;
        width: 100%;
        grid-template-columns: 333px auto;
        grid-template-areas: 
        "sidebar main";
    }

    /********************** FORMS ****************/
    form {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: white;
    padding: 20px;
    z-index: 999;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
    text-align: left;
    }

    .overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 998;
    }

    .form-grid {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        grid-column-gap: 20px;
        grid-row-gap: 20px;

    }
    
    .form-grid .grid-item {
        display: grid;
        grid-template-columns: 1fr;
        grid-row-gap: 10px;
    }
    

    /* Optional styling for labels and inputs */
    label {
        font-weight: bold;
    }
    
    input {
        width: 100%;
        padding: 5px;
        border-radius: 5px;
        border: 1px solid #ccc;
    }

    .form-grid input[type=text], 
    .form-grid input[type=number],
    .form-grid input[type=datetime-local] {
        border: 2px solid #5f5f5f;
        padding: 5px 10px;
        width: 100%;
        border-radius: 20px;
        border: 2px solid grey;
        color: rgb(63, 62, 62);
        font-size: 20px;
    }

    .form-grid label {
        margin-left:10px;
    }
    
    .form-grid button {
        width: 100px;
        height: 40px;
        padding: 10px;
        border-radius: 30px;
        cursor: pointer;
        font-family: 'Montserrat', sans-serif;
        font-weight: bold;
        transition: background-color 0.2s;
        margin: 5px;
    }

    .btn-save {
        background-color: #1f2220;
        color: #fff;
    }
    .btn-cancel {
        background-color: transparent;
        color: #000000;
        border:2px solid grey;
    }
    .btn-save:hover  {
    background-color: #171817;
    }
    .btn-cancel:hover {
        background-color: #b5b9b5
    }
    .row {
    display: flex;
    align-items: center;
    flex-wrap: nowrap;
    gap:10px;
    }

    .button-container {
        display: flex;
        justify-content: flex-end;
        align-items: right;
        flex-wrap: nowrap;
        gap:10px;
    }
    .row label {
    margin-right: 10px;
    }
    .row input {
    flex-grow: 1;
    }


    /********************** Base (Table and else) ****************/
    .sidebar {
        grid-area: sidebar;
    }

    .main {
        grid-area: main;
    }

    .header {
        width: 100%;
        height: 10%;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-top: 1%;
        margin-bottom: 1%;
    }

    .title-page-container {
        width: 50%;
        height: 100%;
        text-align: left;
    }

    .title-page {
        left: 20%;
        height: 100%;
    }
    .button-add-container {
        width: 50%;
        height: 100%;
        text-align: right;
    }
    .add-button {
        width: 10%;
        height: 80%;
        right: 20%;
        top: 10%;
        background: #353642;
        border: 1px solid grey;
        border-radius: 15px;
        font-family: "Montserrat", sans-serif;
        font-weight: bold;
        letter-spacing: 2px;
    }

    .add-button:hover {
        background-color: #171817;
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
        width: 100% / 7;
    }

    .open-regis {
        background: #72e48b;
        border-radius: 15px;
        left: 25%;
        width: 50%;
        font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
    }

    .close-regis {
        background: #ec7b7b;
        border-radius: 15px;
        left: 25%;
        width: 50%;
        font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
    }

    .status-publish {
        background: #72e48b;
        border-radius: 15px;
        left: 25%;
        width: 50%;
        font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
    }

    .status-publish-not {
        background: #ec7b7b;
        border-radius: 15px;
        left: 25%;
        width: 60%;
        font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
    }

    .detail-button {
        width: 50%;
        background: #deddd8;
        border: 1px solid grey;
        border-radius: 15px;
        font-family: "Montserrat", sans-serif;
        font-weight: bold;
        letter-spacing: 2px;
    }
</style>
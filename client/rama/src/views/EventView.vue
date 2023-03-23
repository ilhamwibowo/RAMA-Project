<template>
    <div class="layout">
        <div class="header">
            <div class="title-page-container">
                <h1 class="title-page">EVENTS</h1>
            </div>
            <div class="button-add-container">
                <button class="add-button">ADD</button>
            </div>
        </div>
        <div class="table-container">
            <table class="event-table">
                <thead class="table-head">
                    <tr class="table-row-header">
                        <th class="table-header" scope="col">Title</th>
                        <th class="table-header" scope="col">Location</th>
                        <th class="table-header" scope="col">Date</th>
                        <th class="table-header" scope="col">Category</th>
                        <th class="table-header" scope="col">Registration</th>
                        <th class="table-header" scope="col">Status</th>
                        <th class="table-header" scope="col">Action</th>
                    </tr>
                </thead>
                <tbody class="table-body">
                    <tr class="table-row-body">
                        <td class="table-data">Judul</td>
                        <td class="table-data">Lokasi</td>
                        <td class="table-data">tanggal</td>
                        <td class="table-data">Umum</td>
                        <td class="table-data">
                            <p class="open-regis">Opened</p>
                        </td>
                        <td class="table-data">
                            <p class="status-publish">Published</p>
                        </td>
                        <td class="table-data">
                            <button class="detail-button">Detail</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>


<script>

</script>
<style scoped>
.layout{
    height: 100%;
    width: 100%;
}

.header{
    width: 100%;
    height: 30%;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-top: 1%;
    margin-bottom: 1%;
}

.title-page-container{
    width: 50%;
    height: 100%;
    text-align: left;
}

.title-page{
    left: 20%;
    height: 100%;
}
.button-add-container{
    width: 50%;
    height: 100%;
    text-align: right;
}
.add-button{
    width: 10%;
    height: 80%;
    right: 20%;
    top: 10%;
    background: #353642 ;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    letter-spacing: 2px;

}
.table-container{
    width: 80%;
    left: 10%;
    background: #fff;
    border-radius: 10px;
}

.event-table{
    border-collapse: collapse;
    text-align: center;

    margin-bottom: 1rem;
    color: #000;
    padding: 20px;

    width: 100%;
    table-layout: fixed;
}

.table-row-header{
    border-bottom: 2px solid #272626;
    font-family: 'Montserrat', sans-serif;
    font-size: 120%;
}

.table-header{
    width: 100% / 7;
}

.open-regis{
    background: #72E48B;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.status-publish{
    background: #72E48B;
    border-radius: 15px;
    left: 25%;
    width: 50%;
    font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
}

.detail-button{
    width: 50%;
    background: #DEDDD8 ;
    border: 1px solid grey;
    border-radius: 15px;
    font-family: 'Montserrat', sans-serif;
    font-weight: bold;
    letter-spacing: 2px;
}




</style>
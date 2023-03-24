<template>
    <h2>Leaderboard</h2>
    <table>
        <tr>
            <th class="narrower">Position</th>
            <th class="narrower">Bib Number</th>
            <th>Name</th>
            <th class="narrower">Time</th>
        </tr>
        <tr v-for="attendee in attendees" :key="attendee.position">
            <td>{{ attendee.position }}</td>
            <td>{{ attendee.bibNumber }}</td>
            <td>{{ attendee.name }}</td>
            <td>{{ attendee.duration }}</td>
        </tr>
    </table>
</template>

<script>
export default {
    name: "Leaderboard",

    props: {
        attendees: {
            type: Array,
            required: true
        }
    }
};
</script>

<style scoped>
table,
th,
td {
    border: 1px solid black;
}

table {
    margin-left: auto;
    margin-right: auto;
    width: 50%;
}

th {
    font-weight: bold;
}

tr:nth-child(1):hover {
    background-color: unset !important;
}

tr:hover {
    background-color: lightgreen;
}

td {
    text-align: center;
}

.narrower {
    width: 20%;
}
</style>

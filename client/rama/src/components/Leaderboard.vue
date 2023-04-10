<template>
    <div class="leaderboard">
        <LeaderboardTable :attendees="raceData" />
            <LeaderboardPagination
                :totalPage="this.pageCount"
                :perPage="this.perPage"
                :currentPage="currentPage"
                @pageChanged="onPageChange"
            />
    </div>
</template>

<script>
import axios from 'axios';

import LeaderboardPagination from './LeaderboardPagination.vue'
import LeaderboardTable from './LeaderboardTable.vue';
export default {
    name: "Leaderboard",
    components: {
        LeaderboardTable,
        LeaderboardPagination
    },
    data() {
        return {
            raceId: "",
            raceData: [],
            currentPage: 1,
            pageCount: 1,
            perPage: 20
        };
    },

    methods: {
        onPageChange(page) {
            this.currentPage = page;
            axios
                .get(
                    import.meta.env.VITE_API_URI +
                        "/Race/" +
                        this.raceId +
                        "/attendees?page=" +
                        this.currentPage +
                        "&pageSize=" +
                        this.perPage
                )
                .then((response) => {
                    this.raceData = response.data.raceAttendee;
                })
                .catch((error) => {
                    console.log(error);
                });
        }
    },

    async beforeMount() {
        this.pageCount = await axios
            .get(import.meta.env.VITE_API_URI + "/Race/" + this.raceId + "/attendance")
            .then((response) => {
                return Math.ceil(response.data.raceAttendee.length / this.perPage);
            })
            .catch(() => {
                return 10;
            });
        await axios
            .get(
                import.meta.env.VITE_API_URI +
                    "/Race/" +
                    this.raceId +
                    "/attendees?page=" +
                    this.currentPage +
                    "&pageSize=" +
                    this.perPage
            )
            .then((response) => {
                this.raceData = response.data.raceAttendee;
            })
            .catch((error) => {
                console.log(error);
            });
    }
}
</script>

<style scoped>

</style>
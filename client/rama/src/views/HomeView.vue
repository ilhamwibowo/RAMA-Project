<template>
    <main>
        <Leaderboard :attendees="raceData" />
        <LeaderboardPagination
            :totalPage="this.pageCount"
            :perPage="this.perPage"
            :currentPage="currentPage"
            @pageChanged="onPageChange"
        />
    </main>
</template>

<script>
import axios from "axios";

import Leaderboard from "@/components/Leaderboard.vue";
import LeaderboardPagination from "@/components/LeaderboardPagination.vue";

export default {
    name: "HomeView",

    components: {
        Leaderboard,
        LeaderboardPagination
    },

    data() {
        return {
            raceId: 1,
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
                        this.pageCount
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
                    this.pageCount
            )
            .then((response) => {
                this.raceData = response.data.raceAttendee;
            })
            .catch((error) => {
                console.log(error);
            });
    }
};
</script>

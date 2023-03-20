<template>
    <main>
        <LeaderboardPagination
            :totalPage="10"
            :perPage="10"
            :currentPage="currentPage"
            @pageChanged="onPageChange"
        />
    </main>
</template>

<script>
import LeaderboardPagination from "@/components/LeaderboardPagination.vue";

export default {
    name: "HomeView",

    components: {
        LeaderboardPagination
    },

    data() {
        return {
            currentPage: 1
        };
    },

    methods: {
        onPageChange(page) {
            this.currentPage = page;
        }
    }
};
</script>

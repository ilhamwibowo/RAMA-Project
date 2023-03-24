<template>
    <ul class="pagination">
        <li class="pagination-item utility-button">
            <button type="button" @click="onClickFirstPage" :disabled="isInFirstPage">First</button>
        </li>

        <li class="pagination-item utility-button">
            <button type="button" @click="onClickPreviousPage" :disabled="isInFirstPage">
                Previous
            </button>
        </li>

        <!-- Visible Buttons Start -->

        <li class="pagination-item page-button" v-for="page in pages" :key="page.name">
            <button
                :class="{ active: isPageActive(page.name) }"
                type="button"
                :disabled="page.isDisabled"
                @click="onClickPage(page.name)"
            >
                {{ page.name }}
            </button>
        </li>

        <!-- Visible Buttons End -->

        <li class="pagination-item utility-button">
            <button type="button" @click="onClickNextPage" :disabled="isInLastPage">Next</button>
        </li>

        <li class="pagination-item utility-button">
            <button type="button" @click="onClickLastPage" :disabled="isInLastPage">Last</button>
        </li>
    </ul>
</template>

<script>
export default {
    name: "LeaderboardPagination",

    props: {
        maxVisibleButtons: {
            type: Number,
            required: false,
            default: 3
        },
        totalPage: {
            type: Number,
            required: true
        },
        perPage: {
            type: Number,
            required: true
        },
        currentPage: {
            type: Number,
            required: true
        }
    },

    computed: {
        startPage() {
            // When on the first page
            if (this.currentPage === 1) {
                return 1;
            }

            // When on the last page
            if (this.currentPage === this.totalPage) {
                const start = this.totalPage - (this.maxVisibleButtons - 1);

                if (start === 0) {
                    return 1;
                } else {
                    return start;
                }
            }

            // When in between
            return this.currentPage - 1;
        },
        pages() {
            const range = [];

            for (
                let i = this.startPage;
                i <= Math.min(this.startPage + this.maxVisibleButtons - 1, this.totalPage);
                i++
            ) {
                range.push({ name: i, isDisabled: i === this.currentPage });
            }

            return range;
        },
        isInFirstPage() {
            return this.currentPage === 1;
        },
        isInLastPage() {
            return this.currentPage === this.totalPage;
        }
    },

    methods: {
        onClickFirstPage() {
            this.$emit("pageChanged", 1);
        },
        onClickPreviousPage() {
            this.$emit("pageChanged", this.currentPage - 1);
        },
        onClickPage(page) {
            this.$emit("pageChanged", page);
        },
        onClickNextPage() {
            this.$emit("pageChanged", this.currentPage + 1);
        },
        onClickLastPage() {
            this.$emit("pageChanged", this.totalPage);
        },
        isPageActive(page) {
            return this.currentPage === page;
        }
    }
};
</script>

<style scoped>
ul {
    margin-top: 2em;
    padding: 0 0 !important;
}

.pagination {
    list-style-type: none;
}

.pagination-item {
    display: inline-block;
}

.utility-button {
    margin: 0 0.2em;
}

.page-button {
    margin: 0 0.05em;
}

.active {
    background-color: #4aae9b;
    color: #ffffff;
}
</style>

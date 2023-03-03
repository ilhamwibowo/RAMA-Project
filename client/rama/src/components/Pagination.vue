<template>
    <div class="pagination">
        <span>
            <button @click="onClickPrev">Prev</button>
            <button v-for="n in this.totalPage" :key="n" class="btn-page" :id="n" @click="onClickPage(n)">{{ n }}</button>
            <button @click="onClickNext">Next</button>
            <select id="pager" @change="onClickPager">
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="50">50</option>
                <option value="100">100</option>
            </select>
        </span>
    </div>
</template>
<script>
export default {
    name: "Pagination",
    props: {
        totalPage: 0,
        pager: 0,
        page: 0
    },
    methods: {
        onClickPrev() {
            if (this.page !== 1) {
                this.$emit("changePage", this.page-1);
            }
        },
        onClickNext() {
            if (this.page !== this.totalPage) {
                this.$emit("changePage", this.page+1);
            }
        },
        onClickPage(n) {
            if (n !== this.page) {
                this.$emit("changePage", n);
            }
        },
        onClickPager() {
            var e = document.getElementById("pager");
            var value = e.value
            if (value !== this.pager)
                this.$emit("changePager", value);
        }
    }
}
</script>
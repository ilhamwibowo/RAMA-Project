<template>
    <div class="pagination">
        <button @click="onClickPrev" id="btn-prev"><img src="/left-arrow.png" /></button>
        <button
            v-for="n in this.totalPage"
            :key="n"
            class="btn-page"
            :id="n"
            @click="onClickPage(n)"
        >
            {{ n }}
        </button>
        <button @click="onClickNext" id="btn-next"><img src="/right-arrow.png" /></button>
        <select id="pager" @change="onClickPager">
            <option value="10">10</option>
            <option value="20">20</option>
            <option value="50">50</option>
            <option value="100">100</option>
        </select>
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
                this.$emit("changePage", this.page - 1);
            }
        },
        onClickNext() {
            if (this.page !== this.totalPage) {
                this.$emit("changePage", this.page + 1);
            }
        },
        onClickPage(n) {
            if (n !== this.page) {
                this.$emit("changePage", n);
            }
        },
        onClickPager() {
            var e = document.getElementById("pager");
            var value = e.value;
            if (value !== this.pager) this.$emit("changePager", value);
        }
    }
};
</script>

<style scoped>
.pagination {
    display: flex;
    justify-content: center;
    column-gap: 10px;
    align-items: center;
}
#btn-prev {
    border: 0px;
    background-color: rgba(0, 0, 0, 0);
}

#btn-next {
    border: 0px;
    background-color: rgba(0, 0, 0, 0);
}

.btn-page {
    padding: 5px;
    height: 36px;
    width: 36px;
    font-family: "Bebas Neue";
    font-size: 24px;
    border: 0px;
    background-color: #d9d9d9;
    border-radius: 5px;
    justify-content: center;
}
</style>

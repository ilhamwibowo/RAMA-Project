<template>
    <div class="toast-wrapper">
        <div class="toast" :id="type">
            {{ message }}
        </div>
    </div>
</template>

<script>
export default {
    name: "Toast",
    props: {
        type: String,
        message: String
    }

}
</script>

<style scoped>
.toast-wrapper {
    position: fixed;
    width: 100%;
    top: 20px;
    z-index: 10000;
}
.toast {
    font-family: "Darker Grotesque";
    font-size: 20px;
    padding: 20px;
    color: white;
    border-radius: 10px;
    box-shadow: 1px 3px 5px rgba(0,0,0,0.2);
    max-width: 400px;
    margin: 0 auto;
}

#error {
    background: #ff4444;
}

#success {
    background: #00C851;
}
</style>
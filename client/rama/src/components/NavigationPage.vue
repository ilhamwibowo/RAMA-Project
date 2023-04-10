<template>
    <div class="navpage">
        <button class="button" @click="onClick"><img src="/navbar-button.png"></button>
        <div v-if="isOpen" v-click-outside="onClickOutside" class="popup">
            <p>Home</p>
            <p>Leaderboard</p>
            <p>Album</p>
        </div>
    </div>    
</template>

<script>
Vue.directive('click-outside', {
    bind: function (el, binding, vnode) {
      el.clickOutsideEvent = function (event) {
        // here I check that click was outside the el and his children
        if (!(el == event.target || el.contains(event.target))) {
          // and if it did, call method provided in attribute value
          vnode.context[binding.expression](event);
        }
      };
      document.body.addEventListener('click', el.clickOutsideEvent)
    },
    unbind: function (el) {
      document.body.removeEventListener('click', el.clickOutsideEvent)
    },
});

export default {
    name: "NavigationPage",
    data() {
        return {
            isOpen: false
        }
    },
    methods: {
        onClick() {
            this.isOpen = true;
        },
        onClickOutside() {
            this.isOpen = false;
        }
    }
}
</script>

<style scoped>
.navpage {
    width: 100%;
    height: 100%;
    background-color: aqua;
    display: flex;
    justify-content: center;
    align-items: center;
}

.button {
    border: none;
    background-color: rgba(0,0,0,0)
}

.popup {
    position: absolute;
    width: 240px;
    padding: 38px;
    top: 100px;
    left: 50px;
    border-radius: 15px;
    background-color:white;
    box-shadow: 0px 4px 30px 7px rgba(0,0,0,0.1);
    z-index: 200;
    display: flex;
    flex-direction: column;
    row-gap: 10px;
}

.popup p {
    font-family: "Bebas Neue";
    font-size: 30px;
}

</style>
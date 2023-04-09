<template>
    <div class="modal" v-if="show">
      <div class="modal-backdrop" @click="close"></div>
      <div class="modal-content">
        <div class="modal-header">
          <h3>{{ title }}</h3>
        </div>
        <div class="modal-body">
          <p>{{ message }}</p>
        </div>
        <div class="modal-footer">
          <button class="yes-button" @click="confirm">Yes</button>
          <button class="no-button" @click="close">No</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      show: Boolean,
      title: String,
      message: String,
    },
    methods: {
      close() {
        this.$emit('close');
      },
      confirm() {
        this.$emit('confirm');
        this.close();
      },
    },
  };
  </script>
  
  
  <style scoped>
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 9999;
  }
  
  .modal-backdrop {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
  }
  
  .modal-content {
    background-color: #fff;
    border-radius: 5px;
    padding: 20px;
    max-width: 400px;
    width: 90%;
  }
  
  .modal-header {
    margin-bottom: 10px;
  }
  
  .modal-body {
    margin-bottom: 20px;
  }
  
  .modal-footer {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
  }

  button {
    width: 75px;
    height: 30px;
    font-family: "Montserrat", sans-serif;
    cursor: pointer;
    border-radius: 10px;
    transition: 0.3s;
  }

  .yes-button {
    color:white;
    border:none;
    background-color: #1f2220;
    color: #fff;
    border: none;

  }

  .no-button {
    background-color: white;
    border:2px solid grey;
  }

  .yes-button:hover {
    background-color: #161a17;
  }

  .no-button:hover {
    background-color: rgb(233, 228, 228);
  }
  </style>
  
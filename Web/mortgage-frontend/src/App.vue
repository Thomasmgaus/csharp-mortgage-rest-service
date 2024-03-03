<script setup lang="ts">
    import Mortgage from './components/MortgageView.vue';
</script>

<template>
  <Mortgage class="app"></Mortgage>
</template>

<style scoped>
  .app {
    position: fixed;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    overflow: auto;
    background-color: darkgreen;
  }
</style>

<template>
  <div class="home">
    <h1>Welcome to the Task Management Application</h1>
    <p>This is the home page where you can find an overview of the application.</p>
    <router-link to="/login">Login</router-link>
    <router-link to="/register">Register</router-link>
  </div>
</template>

<script>
export default {
  name: 'Home',
};
</script>

<style scoped>
.home {
  text-align: center;
  margin-top: 50px;
}
</style>
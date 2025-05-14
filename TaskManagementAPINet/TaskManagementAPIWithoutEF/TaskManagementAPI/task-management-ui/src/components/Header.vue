<template>
  <header class="header">
    <nav>
      <router-link to="/">Home</router-link>
      <router-link to="/login">Login</router-link>
      <router-link to="/register">Register</router-link>
    </nav>
  </header>
</template>

<script>
export default {
  name: "Header",
};
</script>

<style scoped>
.header {
  background-color: #333;
  color: white;
  padding: 10px;
  text-align: center;
}
.header nav a {
  color: white;
  margin: 0 10px;
  text-decoration: none;
}
.header nav a:hover {
  text-decoration: underline;
}
</style>
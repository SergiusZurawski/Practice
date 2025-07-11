<template>
  <aside class="sidebar-left">
    <!-- You can add navigation, user info, or any widgets here -->
    <h3>Left Sidebar</h3>
    <ul>
      <li><router-link to="/">Home</router-link></li>
      <li><router-link to="/tasks">Tasks</router-link></li>
      <li><router-link to="/addtask">Add Tasks</router-link></li>
    </ul>
  </aside>
</template>

<script>
export default {
  name: "SidebarLeft",
};
</script>

<style scoped>
.sidebar-left {
  width: 20%;
  background-color: #f4f4f4;
  padding: 10px;
  box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.1);
}
.sidebar-left ul {
  list-style: none;
  padding: 0;
}
.sidebar-left li {
  margin: 10px 0;
}
</style>

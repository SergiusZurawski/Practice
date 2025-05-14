<template>
  <div>
    <h1>Task Management</h1>
    <div v-if="tasks.length === 0">
      <p>No tasks available.</p>
    </div>
    <ul>
      <li v-for="task in tasks" :key="task.id">
        <h2>{{ task.title }}</h2>
        <p>{{ task.description }}</p>
        <p>Status: {{ task.isCompleted ? 'Completed' : 'Pending' }}</p>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Tasks',
  data() {
    return {
      tasks: []
    };
  },
  mounted() {
    this.fetchTasks();
  },
  methods: {
    async fetchTasks() {
      try {
        const response = await fetch('http://localhost:5037/api/v1/tasks');
        this.tasks = await response.json();
      } catch (error) {
        console.error('Error fetching tasks:', error);
      }
    }
  }
};
</script>

<style scoped>
h1 {
  color: #333;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  margin: 10px 0;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}
</style>
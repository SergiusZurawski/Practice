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
        <p v-if="task.taskDueDate">Due Date: {{ formatDate(task.taskDueDate) }}</p>
        <p v-if="task.taskPriority">Priority: {{ formatPriority(task.taskPriority) }}</p>
        <p>Status: {{ task.isCompleted ? 'Completed' : 'Pending' }}</p>
      </li>
    </ul>
  </div>
</template>

<script>

import { useStore } from 'vuex'; // Import useStore

export default {
  name: 'Tasks',
  mounted() {
    this.fetchTasks();
  },
  setup() {
    const store = useStore(); // Get the store instance
    return {
      store
    };
  },
  computed: {
    tasks() {
      return this.store.state.tasks; // Assuming tasks are stored in Vuex
    }
  },
  methods: {
    async fetchTasks() {
      try {
        await this.store.dispatch('fetchTasks'); // Use this.store
        console.log('Inside fetchTasks method from Tasks.vue');
        console.log('Tasks fetched (from computed property):', this.tasks);
      } catch (error) {
        console.error('Error fetching tasks:', error);
      }
    },
    formatDate(dateString) {
      const options = { year: 'numeric', month: 'short', day: 'numeric' };
      return new Date(dateString).toLocaleDateString(undefined, options);
    },
    formatPriority(priority) {
      const map = { 1: 'High', 2: 'Medium', 3: 'Low' };
      return map[priority] || 'Unknown';
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
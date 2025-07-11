<template>
  <div class="add-task-form">
    <h3>Add New Task</h3>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="title">Title:</label>
        <input type="text" id="title" v-model="title" required />
      </div>

      <div class="form-group">
        <label for="description">Description:</label>
        <textarea id="description" v-model="description" required></textarea>
      </div>

      <div class="form-group">
        <label for="task_due_date">Due Date:</label>
        <input type="date" id="task_due_date" v-model="taskDueDate" />
      </div>

      <div class="form-group">
        <label for="task_priority">Priority:</label>
        <select id="task_priority" v-model.number="taskPriority">
          <option value="1">High</option>
          <option value="2">Medium</option>
          <option value="3">Low</option>
        </select>
      </div>

      <button type="submit">Add Task</button>

      <div v-if="successMessage" class="success-message">{{ successMessage }}</div>
      <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useStore } from 'vuex';

const store = useStore();

const title = ref('');
const description = ref('');
const taskDueDate = ref('');
const taskPriority = ref(null);

const successMessage = ref('');
const errorMessage = ref('');

// ✅ Pull userId from store using getter
const userId = computed(() => store.getters.getUserId);

const handleSubmit = async () => {
  try {
    console.log('handleSubmit userId' + userId.value);
    await store.dispatch('addTask', {
      title: title.value,
      description: description.value,
      taskDueDate: taskDueDate.value || null,
      taskPriority: taskPriority.value,
      userId: userId.value,
      // ⚠️ No need to send taskStatusId — backend sets it
    });

    successMessage.value = 'Task added successfully!';
    errorMessage.value = '';

    title.value = '';
    description.value = '';
    taskDueDate.value = '';
    taskPriority.value = null;

    setTimeout(() => (successMessage.value = ''), 3000);
  } catch (error) {
    errorMessage.value = 'Failed to add task. ' + (error.message || '');
    successMessage.value = '';
    console.error('Error adding task:', error);
  }
};
</script>


<style scoped>
.add-task-form {
  max-width: 500px;
  margin: 20px auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}

.form-group input[type="text"],
.form-group textarea {
  width: 100%;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  box-sizing: border-box; /* So padding doesn't add to width */
}

button[type="submit"] {
  background-color: #007bff;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

button[type="submit"]:hover {
  background-color: #0056b3;
}

.success-message {
  margin-top: 10px;
  color: green;
  font-weight: bold;
}

.error-message {
  margin-top: 10px;
  color: red;
  font-weight: bold;
}
</style>
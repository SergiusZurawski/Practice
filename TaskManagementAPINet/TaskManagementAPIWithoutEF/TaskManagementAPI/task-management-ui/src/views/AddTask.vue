<template>
  <div class="add-task">
    <h1>Create Task</h1>
    <AddTaskForm />
  </div>
</template>

<script>
import AddTaskForm from '../components/AddTaskForm.vue';

export default {
  name: 'AddTask',
  components: {
    AddTaskForm
  },
};
</script>

<style scoped>
.add-task {
  text-align: center;
  margin-top: 50px;
}
</style>
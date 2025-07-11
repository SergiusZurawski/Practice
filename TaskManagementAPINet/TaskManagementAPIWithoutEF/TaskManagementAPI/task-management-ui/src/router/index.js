import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Tasks from '../views/Tasks.vue';
import Login from '../views/Login.vue';
import Register from '../views/Register.vue';
import AddTask from '../views/AddTask.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/tasks',
    name: 'Tasks',
    component: Tasks
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/addtask',
    name: 'AddTask',
    component: AddTask
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
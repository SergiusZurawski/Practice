// filepath: d:\WorkSpace\Practice\TaskManagementAPINet\TaskManagementAPIWithoutEF\task-management-ui\src\store\auth.js
import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
  state: {
    token: null,
    userId: null,
    tasks: [],
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    setUserId(state, userId) {
      state.userId = userId;
    },
    clearAuth(state) {
      state.token = null;
      state.userId = null;
    },
    setTasks(state, tasks) {
      state.tasks = tasks;
    },
    addTask(state, task) {
      state.tasks.push(task);
    },
  },
  actions: {
    async login({ commit }, { username, password }) {
      try {
        const response = await axios.post('/api/v1/auth/login', { username, password });
        const token = response.data.token;
        commit('setToken', token);
      } catch (error) {
        throw new Error('Login failed: ' + error.response.data.error);
      }
    },
    async register({ commit }, { username, password, role }) {
      try {
        const response = await axios.post('/api/v1/auth/register', { username, password, role });
        const userId = response.data.userId;
        commit('setUserId', userId);
      } catch (error) {
        throw new Error('Registration failed: ' + error.response.data.error);
      }
    },
    async fetchTasks({ commit }) {
      // Fetch tasks from the API and commit the mutation
      const response = await fetch('http://localhost:5037/api/v1/tasks');
      const data = await response.json();
      commit('setTasks', data);
    },
    async addTask({ commit }, task) {
      // Add a new task to the API and commit the mutation
      const response = await fetch('http://localhost:5037/api/v1/tasks', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(task),
      });
      const data = await response.json();
      commit('addTask', data);
    },
  },
  getters: {
    isAuthenticated(state) {
      return !!state.token;
    },
    getToken(state) {
      return state.token;
    },
    getUserId(state) {
      return state.userId;
    },
    allTasks: (state) => state.tasks,
    getTaskById: (state) => (id) => {
      return state.tasks.find((task) => task.id === id);
    },
  },
});

export default store;
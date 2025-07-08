// filepath: d:\WorkSpace\Practice\TaskManagementAPINet\TaskManagementAPIWithoutEF\task-management-ui\src\store\auth.js
import { createStore } from 'vuex';
import { parseJwt } from '../utils/jwt'; 
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
        console.log('Login successful:', response);
        const token = response.data.token;
        commit('setToken', token);
        localStorage.setItem('token', token); // <-- SAVE TOKEN TO LOCALSTORAGE
        // Set token in Axios default headers for subsequent requests
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        //Alternative
        //config.headers.Authorization = `Bearer ${token}`;
        const payload = parseJwt(token);
        if (payload && payload.nameid) {
          commit('setUserId', payload.nameid);
        }
        console.log('Login successful:', token);
      } catch (error) {
        throw new Error('Login failed: ' + (error.response?.data?.error || error.message));
      }
    },
    async register({ commit }, { username, password, role }) {
      try {
        const response = await axios.post('/api/v1/auth/register', { username, password, role });
        const userId = response.data.userId;
        commit('setUserId', userId);
      } catch (error) {
        throw new Error('Registration failed: ' + (error.response?.data?.error || error.message));
      }
    },
    async fetchTasks({ commit, state }) {
      try {
        // No need to manually set the header if it's set globally after login
        // However, for actions that might be called when the app loads and token is from localStorage,
        // it's good practice to ensure the header is set or get it from state.
        // For simplicity here, we rely on it being set after login or app initialization.
        // If axios.defaults.headers.common['Authorization'] is not set, this will fail if endpoint is protected.
        const response = await axios.get('/api/v1/tasks');
        commit('setTasks', response.data);
        console.log('Tasks fetched from store:', response.data);
      } catch (error) {
        console.error('Failed to fetch tasks:', error.response?.data || error.message);
        // Optionally, you could clear tasks or token if it's an auth error (e.g., 401)
        // if (error.response && error.response.status === 401) {
        //   commit('clearAuth');
        //   // redirect to login
        // }
        throw new Error('Failed to fetch tasks');
      }
    },
    async addTask({ commit, state }, task) {
      try {
        // Similar to fetchTasks, relies on global Axios header
        const response = await axios.post('/api/v1/tasks', task);
        console.log('Task added:', response.data);
        commit('addTask', response.data);
      } catch (error) {
        console.error('Failed to add task:', error.response?.data || error.message);
        throw new Error('Failed to add task');
      }
    },
    // It's good practice to have an action to initialize auth state from localStorage
    // and set the Axios header if a token exists.
    async tryAutoLogin({ commit, dispatch }) {
      console.log('Trying to auto-login...');
      const token = localStorage.getItem('token');
      console.log('Token from localStorage:', token);
      if (!token) {
        return false;
      }
      commit('setToken', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      // You might want to add a call here to validate the token with the backend
      // e.g., fetch user profile
      // For now, we'll assume if token exists, it's valid.
      // dispatch('fetchUserProfile'); // Example
      const payload = parseJwt(token);
      if (payload && payload.nameid) {
        console.log('User ID from token:', payload.nameid);
        commit('setUserId', payload.nameid);
      }
      return true;
    },
    logout({ commit }) {
      commit('clearAuth');
      localStorage.removeItem('token');
      delete axios.defaults.headers.common['Authorization'];
      // redirect to login or home page
      // router.push('/login'); // if router is accessible here or handle in component
    }
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
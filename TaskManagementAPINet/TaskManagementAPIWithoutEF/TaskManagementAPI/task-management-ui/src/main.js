import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import 'normalize.css';
import axios from 'axios'; // Import Axios

// Set the base URL for all Axios requests
// Read from environment variable, fallback to localhost if not set
axios.defaults.baseURL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5037';

createApp(App)
  .use(router)
  .use(store)
  .mount('#app');
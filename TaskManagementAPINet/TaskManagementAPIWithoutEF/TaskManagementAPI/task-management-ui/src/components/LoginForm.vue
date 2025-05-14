<template>
  <div class="login-form">
    <h2>Login</h2>
    <form @submit.prevent="handleLogin">
      <div>
        <label for="username">Username:</label>
        <input type="text" v-model="username" required />
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" v-model="password" required />
      </div>
      <button type="submit">Login</button>
      <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
import axios from 'axios';
import { ref } from 'vue';

export default {
  name: 'LoginForm',
  setup() {
    const username = ref('');
    const password = ref('');
    const errorMessage = ref('');

    const handleLogin = async () => {
      try {
        const response = await axios.post('/api/v1/auth/login', {
          username: username.value,
          password: password.value,
        });
        // Handle successful login (e.g., store token, redirect)
        console.log('Login successful:', response.data);
      } catch (error) {
        errorMessage.value = 'Invalid username or password.';
      }
    };

    return {
      username,
      password,
      errorMessage,
      handleLogin,
    };
  },
};
</script>

<style scoped>
.login-form {
  max-width: 400px;
  margin: auto;
  text-align: center;
}

.error {
  color: red;
}
</style>
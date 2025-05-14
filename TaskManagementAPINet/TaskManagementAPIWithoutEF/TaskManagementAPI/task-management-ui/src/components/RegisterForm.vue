<template>
  <div class="register-form">
    <h2>Register</h2>
    <form @submit.prevent="handleRegister">
      <div>
        <label for="username">Username:</label>
        <input type="text" v-model="username" required />
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" v-model="password" required />
      </div>
      <div>
        <label for="role">Role:</label>
        <input type="text" v-model="role" required />
      </div>
      <button type="submit">Register</button>
      <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'RegisterForm',
  data() {
    return {
      username: '',
      password: '',
      role: '',
      errorMessage: ''
    };
  },
  methods: {
    async handleRegister() {
      try {
        const response = await axios.post('/api/v1/auth/register', {
          username: this.username,
          password: this.password,
          role: this.role
        });
        alert(`User registered with ID: ${response.data.userId}`);
        this.username = '';
        this.password = '';
        this.role = '';
      } catch (error) {
        this.errorMessage = error.response.data.error || 'Registration failed';
      }
    }
  }
};
</script>

<style scoped>
.register-form {
  max-width: 400px;
  margin: auto;
  text-align: left;
}
.error {
  color: red;
}
</style>
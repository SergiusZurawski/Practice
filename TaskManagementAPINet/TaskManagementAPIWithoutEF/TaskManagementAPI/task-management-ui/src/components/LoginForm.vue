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
import { useRouter } from 'vue-router';
import { useStore } from 'vuex'; // Import useStore

export default {
  name: 'LoginForm',
  setup() {
    const username = ref('');
    const password = ref('');
    const errorMessage = ref('');
    const router = useRouter();
    const store = useStore(); // Get the store instance

    const handleLogin = async () => {
      try {
        await store.dispatch('login', {
          username: username.value,
          password: password.value,
        });
        router.push('/tasks');
      } catch (error) {
        errorMessage.value = error.message || 'Invalid username or password.';
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
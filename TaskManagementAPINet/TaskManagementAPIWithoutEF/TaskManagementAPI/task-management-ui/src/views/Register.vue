<template>
  <div class="register">
    <h1>Register</h1>
    <RegisterForm />
  </div>
</template>

<script>
import RegisterForm from '../components/RegisterForm.vue';

export default {
  name: 'Register',
  components: {
    RegisterForm,
  },
};
</script>

<style scoped>
.register {
  text-align: center;
  margin-top: 50px;
}
</style>
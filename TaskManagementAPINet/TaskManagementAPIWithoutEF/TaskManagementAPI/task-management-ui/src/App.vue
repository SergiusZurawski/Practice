<template>
  <div id="app">
    <Header />
    <div class="main-layout">
      <SidebarLeft />
      <main class="content">
        <router-view />
      </main>
      <SidebarRight />
    </div>
    <Footer />
  </div>
</template>

<script>
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import SidebarLeft from "./components/SidebarLeft.vue";
import SidebarRight from "./components/SidebarRight.vue";
import { useStore } from 'vuex';
import { onMounted } from 'vue';

export default {
  name: "App",
  setup() {
    const store = useStore();
    onMounted(() => {
      store.dispatch('tryAutoLogin');
    });
    // ...
  },
  components: {
    Header,
    Footer,
    SidebarLeft,
    SidebarRight,
  },
};
</script>

<style>
#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}
.main-layout {
  display: flex;
  flex: 1;
}
.content {
  flex: 1;
  padding: 10px;
}
</style>
<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary d-flex justify-content-between">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img
          alt="logo"
          src="../assets/img/logo.png"
          height="75"
        />
      </div>
    </router-link>

    <button
      class="btn btn-outline-secondary text-uppercase mr-5"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>
    <router-link v-else :to="{ name: 'Profiles', params: { id: account.id } }">
      <span class="navbar-text bg-dark user-card c-pointer py-2 px-3 rounded elevation-2 justify-content-center d-flex align-items-center">

        <i class=" text-white fa fa-user-o f-24" aria-hidden="true"></i>
        <span class="ml-3 mr-1 text-white f-14 user-name">{{ user.name }}</span>
      </span>
    </router-link>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  name: 'Navbar',
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped lang="scss">
@import '../assets/scss/_variables.scss';
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
@media only screen and (max-width: 600px) {
  .user-name {
  display: none;
  }
  .user-card {
    background-color: $gray !important;
    width: 50px;
    height: 50px;
  }
  .fa-user-o {
    color: $success !important;
  }
}
</style>

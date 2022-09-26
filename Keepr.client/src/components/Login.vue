<template>
  <span class="navbar-text">
    <button
      class="btn selectable text-dark text-uppercase my-2 my-lg-0"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>

    <div class="dropdown my-2 my-lg-0 smaller" v-else>
      <div
        class="dropdown-toggle selectable"
        data-bs-toggle="dropdown"
        aria-expanded="false"
        id="authDropdown"
      >
        <div class="user-card rounded" id="user-card-text" v-if="account.picture || user.picture">
          <img
            :src="account.picture || user.picture"
            alt="account photo"
            height="40"
            class="rounded"
          />
          <span class="mx-3 lighten-30">{{ account.name || user.name }}</span>
        </div>
        <!-- <div id="user-picture">
        <img
            :src="account.picture || user.picture"
            alt="account photo"
            height="40"
            class="rounded"
            v-if="account.picture || user.picture"
          />
        </div> -->
      </div>
      <div
        class="dropdown-menu p-0 list-group w-100"
        aria-labelledby="authDropdown"
      >
        <router-link :to="{ name: 'Account' }">
          <div class="list-group-item list-group-item-action hoverable">
            Manage Account
          </div>
        </router-link>
        <div
          class="list-group-item list-group-item-action hoverable text-danger"
          @click="logout"
        >
          <i class="mdi mdi-logout"></i>
          logout
        </div>
      </div>
    </div>
  </span>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { AuthService } from "../services/AuthService";
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      },
    };
  },
};
</script>


<style lang="scss" scoped>
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
.user-card {
  background-color: #636E72;
  color: white;
}
// @media screen and (max-width: 600px) {
//     #user-card-text {
//         visibility: hidden;
//     }
//     #user-picture {
//       visibility: visible;
//     }
// }
// @media screen and (min-width: 600px) {
//     #user-card-text {
//         visibility: visible;
//     }
//     #user-picture {
//       visibility: hidden;
//     }
// }
</style>

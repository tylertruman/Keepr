<template>
  <div class="container">
    <section class="row">
      <div class="col-2">
        <img class="rounded" :src="profile.picture" alt="">
      </div>
      <div class="col-5">
        <h1 class="pb-2">{{profile.name}}</h1>
        <p class="my-1">Vaults: {{vaults.length}}</p>
        <p class="my-1">Keeps: {{keeps.length}}</p>
      </div>
    </section>
    <section class="row">
      <h4 class="pt-5">Vaults <span v-if="profile.id == account.id" class="selectable text-success" @click="newVaultModal()" title="Create Vault">+</span></h4>
      <div class="col-5 col-sm-4 col-md-3 col-lg-2 pt-3" v-for="v in vaults" :key="v.id">
        <VaultCard :vault="v"/>
      </div>
    </section>
    <section class="row">
      <h4 class="pt-5">Keeps <span v-if="profile.id == account.id" class="selectable text-success" @click="newKeepModal()" title="Create Keep">+</span></h4>
      <div class="container-fluid">
        <div v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k"/>
        </div>
      </div>
    </section>
  </div>
  <NewKeepModal/>
  <NewVaultModal/>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState'
import VaultCard from '../components/VaultCard.vue'
import { keepsService } from '../services/KeepsService';
import { profilesService } from '../services/ProfilesService';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import KeepCard from '../components/KeepCard.vue';
import { Modal } from 'bootstrap';
import NewKeepModal from '../components/NewKeepModal.vue';
import NewVaultModal from '../components/NewVaultModal.vue';

export default {
    setup() {
      const route = useRoute();
      async function getProfileById() {
        try {
          await profilesService.getProfileById(route.params.profileId)
        } catch (error) {
          logger.error(error)
        }
      }
      async function getKeepsByProfile() {
        try {
          await keepsService.getKeepsByProfile(route.params.profileId)
        } catch (error) {
          logger.error(error)
        }
      }
      async function getVaultsByProfile() {
        try {
          await vaultsService.getVaultsByProfile(route.params.profileId)
        } catch (error) {
          logger.error(error)
        }
      }
      onMounted(() => {
        getProfileById();
        getKeepsByProfile();
        getVaultsByProfile();
      })
        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.profileKeeps),
            vaults: computed(() => AppState.vaults),
            async newVaultModal() {
              try {
                Modal.getOrCreateInstance(document.getElementById("newVaultModal")).toggle();
              } catch (error) {
                logger.error(error)
              }
            },
            async newKeepModal() {
              try {
                Modal.getOrCreateInstance(document.getElementById("newKeepModal")).toggle();
              } catch (error) {
                logger.error(error)
              }
            }
        };
    },
    components: { VaultCard, KeepCard, NewKeepModal, NewVaultModal }
};
</script>

<style scoped>
@media only screen and (max-width: 768px) {
  .container-fluid {
    column-count: 2;
    column-gap: 10px;
  }
}
@media only screen and (min-width: 769px) and (max-width: 991px){
  .container-fluid {
    column-count: 3;
    column-gap: 10px;
  }
}
@media only screen and (min-width: 992px) {
  .container-fluid {
    column-count: 4;
    column-gap: 10px;
  }
}
</style>
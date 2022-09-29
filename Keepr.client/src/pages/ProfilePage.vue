<template>
  <div class="container">
    <section class="row">
      <div class="col-3">
        <img class="rounded" :src="profile.picture" alt="">
      </div>
      <div class="col-9">
        <h1 class="pb-2">{{profile.name}}</h1>
        <p class="my-1">Vaults: {{vaults.length}}</p>
        <p class="my-1">Keeps: {{keeps.length}}</p>
      </div>
    </section>
    <section class="row">
      <h4 class="pt-5">Vaults <span class="selectable text-success">+</span></h4>
      <div class="col-4" v-for="v in vaults" :key="v.id">
        <VaultCard :vault="v"/>
      </div>
    </section>
  </div>
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
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.profileKeeps),
            vaults: computed(() => AppState.vaults)
        };
    },
    components: { VaultCard }
};
</script>

<style>

</style>
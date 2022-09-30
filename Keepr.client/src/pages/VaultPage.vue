<template>
<section class="row pt-5">
  <div class="col-5 offset-1 text-start px-5">
    <h1 class="px-4">{{vault.name}}</h1>
    <p class="px-4">Keeps: {{keeps.length}}</p>
  </div>
  <div class="col-5 text-end">
    <button class="btn btn-secondary" @click="deleteVault()">Delete Vault</button>
  </div>
</section>
<div class="container pt-3">
  <div v-for="k in keeps" :key="k.id">
    <KeepCard :keep="k"/>
  </div>
</div>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import KeepCard from '../components/KeepCard.vue';
import { router } from '../router';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
export default {
    setup() {
      const route = useRoute();
      async function getVaultById() {
        try {
          await vaultsService.getVaultById(route.params.vaultId);
        } catch (error) {
          logger.error(error)
        }
      }
      async function getKeepsByVaultId() {
        try {
          await keepsService.getKeepsByVaultId(route.params.vaultId);
        } catch (error) {
          logger.error(error)
        }
      }
      onMounted(() => {
        getVaultById();
        getKeepsByVaultId();
      })
        return {
          keeps: computed(() => AppState.activeVaultKeeps),
          vault: computed(() => AppState.activeVault),
          async deleteVault() {
            try {
              if(AppState.account.id != AppState.activeVault.creatorId){
                throw new Error('You are not the owner of this vault!')
              }
              const yes = await Pop.confirm('Delete the Vault?')
              if(!yes) {return}
              await vaultsService.delete(route.params.vaultId);
              Pop.success('Vault deleted successfully!')
            } catch (error) {
              logger.error(error)
              Pop.error(error)
            }
          }
        };
    },
    components: { KeepCard }
};
</script>

<style scoped>

@media only screen and (max-width: 768px) {
  .container {
    column-count: 2;
    column-gap: 10px;
  }
}
@media only screen and (min-width: 769px) and (max-width: 991px){
  .container {
    column-count: 3;
    column-gap: 10px;
  }
}
@media only screen and (min-width: 992px) {
  .container {
    column-count: 4;
    column-gap: 10px;
  }
}
</style>
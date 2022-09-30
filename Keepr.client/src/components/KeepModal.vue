<template>
<div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
        <div class="modal-header ">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body ">
          <div class="row">
            <div class="col-12 col-lg-6">
              <img class="img-fluid rounded" :src="keep?.img" alt="">
            </div>
            <div class="col-12 col-lg-6">
              <div class="text-center">
                <span class="mx-2">Views: {{keep?.views}}</span>
                <span class="mx-2">Saves: {{keep?.kept}}</span>
                <span class="mx-2">Shares: {{keep?.shares}}</span>
                <h1 class="pt-3">{{keep?.name}}</h1>
              <div class="pt-2">
                {{keep?.description}}
              </div>
              <hr class="mb-5">
              </div>
              <section class="row sticky-bottom">
              <!-- <div class="dropdown col-4 mt-1">
  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Add to Vault
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenu2"> -->
    <!-- <button class="dropdown-item" v-for="v in vaults" :key="v.id" :value="v.name" @click="addKeepToVault(v)"></button> -->
    <!-- <button class="dropdown-item" type="button" @click="addKeepToVault(vaults[0].id)">{{vaults[0]?.name}}</button>
    <button class="dropdown-item" type="button" @click="addKeepToVault(vaults[1].id)">{{vaults[1]?.name}}</button>
    <button class="dropdown-item" type="button" @click="addKeepToVault(vaults[2].id)">{{vaults[2]?.name}}</button>
  </div>
</div> -->

<div class="dropdown col-4">
  <button disabled class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
    Add To Vault
  </button>
  <ul class="dropdown-menu">
    <li class="dropdown-item selectable" @click="addKeepToVault()">Vault #1</li>
    <!-- <li class="dropdown-item selectable" @click="addKeepToVault(vaults[1])">Vault #2</li> -->
    <!-- <li class="dropdown-item selectable" @click="addKeepToVault(vaults[2])">Vault #3</li> -->
  </ul>
</div>

<span class="selectable text-danger align-self-center px-3 col-1 offset-1" v-if="keep.creatorId == account.id" @click="deleteKeep()" title="Delete Keep">X</span>
<img class="rounded p-0 selectable offset-1 col-1 mt-1" :src="keep.creator?.picture" alt="Keep Creator Info" height="35" @click="profilePush()" :title="keep.creator?.name">
              <span class="align-self-center col-3 selectable" @click="profilePush()" >{{keep.creator?.name}}</span>
</section>
            </div>
          </div>
          <!-- <div class="row justify-content-center align-content-center">
            <button class="btn btn-info col-4">Add To Vault</button>
            <span class="selectable text-danger col-1 align-self-center px-3">X</span>
            <img class="rounded col-2 p-0 selectable" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" >
            <span class="col-5 align-self-center">{{keep.creator?.name}}</span>
          </div> -->
          <!-- <div class="row justify-content-center align-content-center text-center"> -->
            
            <!-- <div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Add to Vault
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
    <button class="dropdown-item" v-for="v in vaults" :key="v.id" :value="v.name" @click="addKeepToVault(v)"></button> -->
    <!-- <button class="dropdown-item" type="button">Action</button>
    <button class="dropdown-item" type="button">Another action</button>
    <button class="dropdown-item" type="button">Something else here</button> -->
  <!-- </div> -->
<!-- </div> -->

            <!-- <span class="selectable text-danger align-self-center px-3 col-1" v-if="keep.creatorId == account.id" @click="deleteKeep()">X</span> -->
            <!-- <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
              <img class="rounded p-0 selectable col-3" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" >
              <span class="align-self-center col-3">{{keep.creator?.name}}</span>
            </router-link> -->
            <!-- <img class="rounded p-0 selectable col-3" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" @click="profilePush()" > -->
              <!-- <span class="align-self-center col-3" @click="profilePush()" >{{keep.creator?.name}}</span> -->
          <!-- </div> -->
        </div>
    </div>
  </div>
</div>
</template>

<script>
import { router } from '../router';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { onMounted } from 'vue';
import { Modal } from 'bootstrap';

export default {
setup() {
  // async function getVaultsByAccount() {
  //     try {
  //       await vaultsService.getVaultsByAccount();
  //     } catch (error) {
  //       logger.error(error)
  //       Pop.error(error)
  //     }
  //   }
  //   onMounted(() => {
  //     getVaultsByAccount();
  //   })
  return {
    vaults: computed(() => AppState.accountVaults),
    async profilePush() {
      try {
        router.push({name: 'Profile', params: { profileId: this.keep.creatorId}})
        Modal.getOrCreateInstance(document.getElementById('keepModal')).hide()
      } catch (error) {
        logger.error(error)
      }
    },
    async deleteKeep() {
      try {
        if(AppState.account.id != AppState.activeKeep.creatorId) {
          throw new Error('You are not the owner of this keep!')
        }
        const yes = await Pop.confirm('Delete the Keep?')
        if(!yes) {return}
        await keepsService.delete(AppState.activeKeep.id);
        Pop.success('Keep deleted successfully!')
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    },
    // async addKeepToVault() {
    //   try {
    //     await vaultsService.addKeepToVault(AppState.activeVault.id, AppState.activeKeep.id)
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.error(error)
    //   }
    // },
    keep: computed(() => AppState.activeKeep),
    account: computed(() => AppState.account)
  };
},
};
</script>

<style scoped>
.sticky-bottom {
  position: absolute;
  bottom: 7px;
}
</style>
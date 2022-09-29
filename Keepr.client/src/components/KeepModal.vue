<template>
<div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
        <div class="modal-header ">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body ">
          <div class="row">
            <div class="col-12 col-md-6">
              <img class="img-fluid rounded" :src="keep?.img" alt="">
            </div>
            <div class="col-12 col-md-6">
              <div class="text-center">
                <span class="mx-2">Views: {{keep?.views}}</span>
                <span class="mx-2">Saves: {{keep?.kept}}</span>
                <span class="mx-2">Shares: {{keep?.shares}}</span>
                <h1 class="pt-3">{{keep?.name}}</h1>
              <div class="pt-2">
                {{keep?.description}}
              </div>
              <hr>
              </div>
            </div>
          </div>
          <!-- <div class="row justify-content-center align-content-center">
            <button class="btn btn-info col-4">Add To Vault</button>
            <span class="selectable text-danger col-1 align-self-center px-3">X</span>
            <img class="rounded col-2 p-0 selectable" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" >
            <span class="col-5 align-self-center">{{keep.creator?.name}}</span>
          </div> -->
          <div class="row justify-content-center align-content-center text-center">
            <button class="btn btn-info col-3">Add To Vault</button>
            <span class="selectable text-danger align-self-center px-3 col-1">X</span>
            <!-- <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
              <img class="rounded p-0 selectable col-3" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" >
              <span class="align-self-center col-3">{{keep.creator?.name}}</span>
            </router-link> -->
            <img class="rounded p-0 selectable col-3" :src="keep.creator?.picture" alt="Keep Creator Info" height="50" @click="profilePush()" >
              <span class="align-self-center col-3" @click="profilePush()" >{{keep.creator?.name}}</span>
          </div>
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

export default {
setup() {
  return {
    profilePush() {
      try {
        router.push({name: 'Profile', params: { profileId: this.keep.creatorId}})
      } catch (error) {
        logger.error(error)
      }
    },
    keep: computed(() => AppState.activeKeep)
  };
},
};
</script>

<style scoped>
/* .modal {
  height: 100vh !important;
} */
</style>
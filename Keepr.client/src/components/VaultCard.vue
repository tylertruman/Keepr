<template>
  <div class="selectable rounded " @click="goTo()">
    <div class="">
      <h6 class="text-center">{{vault.name}}</h6>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState';
import { router } from '../router';
import { logger } from '../utils/Logger';

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
setup(props) {
  return {
    async goTo() {
      try {
        if(props.vault.isPrivate == true && AppState.account.id != props.vault.creatorId) {
          router.push({name: 'Home'})
        }
        router.push({ name: 'Vault', params: { vaultId: props.vault.id }})
      } catch (error) {
        logger.error(error)
      }
    }
  };
},
};
</script>

<style scoped>
div {
  height: 150px;
  width: 150px;
}
</style>
<template>
  <div class="container">
    <div v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k"/>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import KeepCard from '../components/KeepCard.vue';
import { logger } from '../utils/Logger';
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
export default {
    name: "Home",
    setup() {
      async function getKeeps() {
        try {
          await keepsService.getKeeps();
        } catch (error) {
          logger.error(error)
        }
      }
      onMounted(() => {
        getKeeps();
      })
      return {
        keeps: computed(() => AppState.keeps)
      };
    },
    components: { KeepCard }
}
</script>

<style scoped lang="scss">

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
// .container {
//     column-count: 3;
//     column-gap: 10px;
//   }
</style>

<template>
<figure class="selectable rounded" @click="setActive()">
  <img class="keep-img rounded" :src="keep.img" alt="keep image" :title="keep.name">
  <h6 class="keep-name text-light">{{keep.name}} </h6>
    <img class="profile-img rounded-circle" :src="keep.creator.picture" height="30">
</figure>
<KeepModal/>
</template>

<script>
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import KeepModal from './KeepModal.vue';
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        return {
            async setActive() {
                try {
                  Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                  await keepsService.getOne(props.keep.id)
                }
                catch (error) {
                  logger.error(error);
                }
            }
        };
    },
    components: { KeepModal }
};
</script>

<style scoped>
figure {
  margin: 0;
  display: grid;
  grid-template-rows: 1fr auto;
  margin-bottom: 10px;
  break-inside: avoid;
  overflow: auto;
}
figure > .keep-img {
  grid-row: 1 / -1;
  grid-column: 1;
}
.keep-img {
  max-width: 100%;
  display: block;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
}
.profile-img {
  grid-row: 2;
  grid-column: 1;
  padding: .2em .5em;
  justify-self: end;
}
.keep-name {
  grid-row: 2;
  grid-column: 1;
  padding: .2em .5em;
  justify-self: start;
  text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.699);
}

</style>
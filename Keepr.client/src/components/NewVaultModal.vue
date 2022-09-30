<template>
<div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">New Vault</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form @submit.prevent="createVault()">
  <div class="mb-3">
    <label for="name" class="form-label">Name</label>
    <input v-model="editable.name" type="text" class="form-control" id="name" aria-describedby="emailHelp">
  </div>
  <div class="mb-3">
  <label for="description" class="form-label">Description</label>
  <textarea v-model="editable.description" class="form-control" id="description" rows="3"></textarea>
</div>
  <div class="mb-3 form-check">
    <input v-model="editable.isPrivate" type="checkbox" class="form-check-input" id="isPrivate">
    <label class="form-check-label" for="exampleCheck1">Private?</label>
  </div>
  <button type="submit" class="btn btn-primary">Create</button>
</form>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import { ref } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
setup() {
  const editable = ref({});
  return {
    editable,
    async createVault() {
      try {
        if(!AppState.account.id) {
          throw new Error('You must be signed in to create a vault.')
        }
        await vaultsService.create(editable.value);
        editable.value = {};
        Pop.success("Vault created successfully!")
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
  };
},
};
</script>

<style>

</style>
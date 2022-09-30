<template>
<div class="modal fade" id="newKeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">New Keep</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form @submit.prevent="createKeep()">
  <div class="mb-3">
    <label for="name" class="form-label">Name</label>
    <input v-model="editable.name" type="text" class="form-control" id="name" aria-describedby="emailHelp" required>
  </div>
  <div class="mb-3">
    <label for="img" class="form-label">Image URL</label>
    <input v-model="editable.img" type="text" class="form-control" id="img" aria-describedby="emailHelp" required>
  </div>
  <div class="mb-3">
  <label for="description" class="form-label">Description</label>
  <textarea v-model="editable.description" class="form-control" id="description" rows="3" required></textarea>
</div>
  <button type="submit" class="btn btn-primary">Create</button>
</form>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import { Modal } from 'bootstrap';
import { ref } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
setup() {
  const editable = ref({});
  return {
    editable,
    async createKeep() {
      try {
        if(!AppState.account.id) {
          throw new Error('You must be signed in to create a keep.')
        }
        await keepsService.create(editable.value);
        editable.value = {};
        Pop.success("Keep created successfully!")
        Modal.getOrCreateInstance(document.getElementById("newKeepModal")).hide()
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
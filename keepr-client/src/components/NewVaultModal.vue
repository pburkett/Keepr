<template>
  <div class="modal fade"
       id="newVaultModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered " role="document">
      <div class="modal-content modal-border">
        <div class="modal-header">
          <h4 class="modal-title" id="exampleModalLongTitle">
            New Vault
          </h4>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true" class="text-danger f-24">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="col-12">
            <h6 class="fw-700">
              Title
              <span class="text-danger f-16">*</span>
            </h6>

            <input v-model="vaultForm.name" type="text" placeholder="Title..." class="form-inp">
            <h6 class="fw-700">
              Image URL
            </h6>
            <input v-model="vaultForm.img" type="text" placeholder="Image URL..." class="form-inp">
            <h6 class="fw-700 mt-3">
              Description
            </h6>
            <textarea v-model="vaultForm.description" type="text" placeholder="Description..." class="form-textarea"></textarea>
            <div class="row px-3 align-items-center my-2">
              <input v-model="vaultForm.IsPrivate" type="checkbox" class="form-check mr-3">
              <h6 class="fw-700 mb-0">
                Private?
              </h6>
            </div>
            <div class="row px-3">
              <i class="text-muted f-10">
                Private vaults can only be seen by you
              </i>
            </div>
            <div class="row px-3">
              <i class="text-muted f-10 mt-1">
                Fields marked with * are required
              </i>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" @click="submitForm">
            Post
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  name: 'NewVaultModal',
  setup() {
    return {
      vaultForm: {},
      async submitForm() {
        console.log(this.vaultForm)
        await vaultsService.create(this.vaultForm)
        $('#newVaultModal').modal('hide')
        $('.modal-backdrop').remove()
        $('body').removeClass('modal-open')
        this.vaultForm = {}
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>

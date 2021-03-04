<template>
  <div class="modal fade"
       id="newKeepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered " role="document">
      <div class="modal-content modal-border">
        <div class="modal-header">
          <h4 class="modal-title" id="exampleModalLongTitle">
            New Keep
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
            <input v-model="keepForm.name" type="text" placeholder="Title..." class="form-inp">
            <h6 class="fw-700">
              Image URL
              <span class="text-danger f-16">*</span>
            </h6>
            <input v-model="keepForm.img" type="text" placeholder="Image URL..." class="form-inp">
            <h6 class="fw-700 mt-3">
              Description
            </h6>
            <textarea v-model="keepForm.description" type="text" placeholder="Description..." class="form-textarea"></textarea>

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
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'NewKeepModal',
  setup() {
    return {
      keepForm: { description: '', name: '', img: '' },
      async submitForm() {
        console.log(this.keepForm)
        await keepsService.create(this.keepForm)
        $('#newKeepModal').modal('hide')
        $('.modal-backdrop').remove()
        $('body').removeClass('modal-open')
        this.keepForm = { description: '', name: '', img: '' }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>

<template>
  <div class="modal fade"
       id="exampleModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-lg  guttered modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-body container-fluid">
          <div class="row">
            <div class="col-lg-6 col-12 d-flex align-items-center">
              <img class="keep-img" :src="state.keep.img" alt="">
            </div>
            <div class="col-lg-6 col-12 keep-main ">
              <div class="keep-header">
                <div class="row x-row justify-content-end pr-3">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true" class="f-24">&times;</span>
                  </button>
                </div>
                <div class="row justify-content-center">
                  <img class="icon-sm ml-4 mr-1" src="../assets/img/views.png" alt="">
                  <p class="keep-data text-muted">
                    {{ state.keep.views }}
                  </p>
                  <img class="icon-sm ml-4 mr-1" src="../assets/img/keeps.png" alt="">
                  <p class="keep-data text-muted">
                    {{ state.keep.keeps }}
                  </p>
                  <img class="icon-sm ml-4 mr-1" src="../assets/img/shares.png" alt="">
                  <p class="keep-data text-muted">
                    {{ state.keep.shares }}
                  </p>
                </div>
                <h4 class=" text-center">
                  {{ state.keep.name }}
                </h4>
              </div>
              <div class="keep-body px-4">
                <p>{{ state.keep.description }}</p>
              </div>
              <div class="short-border mx-auto w-75">
              </div>
              <div class="keep-footer align-items-center row no-wrap w-100 justify-content-between">
                <div class="wrap">
                  <button type="button" @click="state.dropdown = !state.dropdown" class="add-btn btn btn-outline-primary">
                    ADD TO VAULT
                  </button>
                  <div class="dropdown" v-if="state.dropdown" @click="state.dropdown = !state.dropdown">
                    <add-to-vault v-for="v in state.vaults" :key="v.id" :v="v" :keep-id="state.keep.id" />
                  </div>
                </div>
                <i v-if="state.keep.creator.id == state.account.id" class="fa fa-trash-o trash-hover f-24" @click="remove" aria-hidden="true"></i>
                <div class="wrap d-flex justify-content-end">
                  <img class="avatar rounded mr-3 c-pointer hover-topleft" @click="pushToProfile" :src="state.keep.creator.picture" alt="">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import AddToVault from './AddToVault.vue'
import { alerts } from '../utils/Alerts'
export default {
  components: { AddToVault },
  name: 'KeepModal',

  setup() {
    const router = useRouter()

    const state = reactive({
      keep: computed(() => AppState.displayKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),

      dropdown: false
    })
    return {
      state,
      closeModal() {
        $('#exampleModal').modal('hide')
        $('.modal-backdrop').remove()
        $('body').removeClass('modal-open')
      },
      async pushToProfile() {
        await this.closeModal()
        router.push({ name: 'Profiles', params: { id: state.keep.creator.id } })
      },
      async remove() {
        if (await alerts.confirmAction('Are you sure you want to delete this Keep?')) {
          let divs
          if (screen.width < 768) {
            divs = 2
          } else {
            divs = 4
          }
          await keepsService.remove(state.keep.id, divs)
          await this.closeModal()
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
@import '../assets/scss/_variables.scss';
.keep-img {
  width: 100%;
  max-height: 80vh;}
.icon-sm{
  width: 28px;
  height: 28px;
}
.keep-footer{
position: absolute;
bottom: 0px;
}
// .x-row{
//   transform: translateY(-15px);
// }
.keep-body{
  min-height: 20%;
}
.short-border{
  background-color: $gray;
  height: 3px;
}
.keep-main{
  padding-bottom: 80px;
}
.avatar{
  width: 40px;
  height: 42px;
}
.add-btn{
  border-width: 3px !important;
}
.wrap{
  width: 30%;
}
.dropdown{
  width: 50%;
  border: 2px solid $primary;
  background: $white;
  position: absolute;
}
</style>

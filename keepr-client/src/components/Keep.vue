<template>
  <div class="keep c-pointer h-fc my-4 rounded-2 bottom-shadow hover-topright elevation-3" @click="openModal" :style="cssVars">
    <button v-if="removeFromVaultOpt" type="button" class="close" @click="removeKeep">
      <span aria-hidden="true" class="f-24 p-3">&times;</span>
    </button>
    <img class="v-hidden w-100" :src="k.img" alt="keep">
    <div class="row mb-1 no-wrap justify-content-between">
      <div class="col-8">
        <h5 class="keep-name text-white text-left ml-2">
          {{ k.name }}
        </h5>
      </div>

      <div @click="pushToProfile" class="hover-topleft p-3 c-pointer circle bg-dark mb-2 mr-2 d-flex align-items-center justify-content-center">
        <i class=" text-primary fa fa-user-o f-16 " aria-hidden="true"></i>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import $ from 'jquery'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import 'bootstrap'
import { alerts } from '../utils/Alerts'
export default {
  name: 'Keep',
  props: {
    k: {
      type: Object,
      required: true
    },
    removeFromVaultOpt: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    cssVars() {
      return {
        background: `linear-gradient(180.45deg, rgba(0, 0, 0, 0) 67.72%, rgba(0, 0, 0, .5) 99.61%), url(${this.k.img}) center  no-repeat`
      }
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      async removeKeep(e) {
        e.stopPropagation()
        if (await alerts.confirmAction('Are you sure you want to remove this Keep?')) {
          let divs
          if (screen.width < 768) {
            divs = 2
          } else {
            divs = 4
          }
          await vaultKeepsService.remove(props.k.vaultKeepId, divs)
          await this.closeModal()
        }
      },
      async openModal() {
        console.log('openModal')
        AppState.keepModal = await keepsService.getOne(props.k.id)
        console.log($('#exampleModal'))
        $('#exampleModal').modal('toggle')
      },

      pushToProfile(e) {
        e.stopPropagation()
        router.push({ name: 'Profiles', params: { id: props.k.creator.id } })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
@import '../assets/scss/_variables.scss';

.v-hidden {
  visibility: hidden;
}
.keep {
background-size: cover !important;
}

.h-fc{
  height: fit-content;
}
.circle{
  border-radius: 50%;
  height: 2rem !important;
  width: 2rem !important;
  position: relative;
  right: 15px;
  bottom: 0px;

}
.butn {
  position: absolute;
  width: 90%;
  height: 50%;
}
@media only screen and (max-width: 600px) {
.keep{
  margin-top: 8px !important;
  margin-bottom: 8px !important;
}
}
.close{
  position: relative;
  color: $danger;
}
</style>

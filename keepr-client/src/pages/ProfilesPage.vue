<template>
  <div class="profiles-page px-5 row flex-grow-1 d-flex  align-items-start justify-content-center">
    <div class="col-12">
      <div class="row w-100 justify-content-end">
        <button @click="logout" class="btn mr-3 mt-3 btn-outline-danger">
          LOGOUT
        </button>
      </div>
      <div class="row">
        <div class="col-lg-1 col-3 d-flex align-items-center">
          <img class="pfp" :src="state.profile.picture" alt="pfp">
        </div>
        <div class="col-12 col-lg-10">
          <h1 class="profile-name">
            {{ state.profile.name }}
          </h1>
          <h4>Keeps: {{ state.keepsCount }}</h4>
          <h4>Vaults: {{ state.vaultsCount }}</h4>
        </div>
      </div>
      <div class="col mt-5">
        <h3 class="w-fc border-bottom border-2">
          Vaults <i v-if="state.profile.id == state.account.id" class="fa fa-plus f-24 text-primary c-pointer pl-2 pr-5 hover-topleft" aria-hidden="true" data-toggle="modal" data-target="#newVaultModal"></i>
        </h3>

        <div class="row reverse" v-if="state.vaults != undefined">
          <vault-column v-for="col in state.vaults" :key="col" :col="col" />
        </div>
        <div class="row mt-3 justify-content-center">
          <div class="col">
            <h3 class="w-fc border-bottom border-2">
              Keeps <i v-if="state.profile.id == state.account.id" class="fa fa-plus f-24 text-primary c-pointer pl-2 pr-5 hover-topleft" aria-hidden="true" data-toggle="modal" data-target="#newKeepModal"></i>
            </h3>

            <div class="row reverse">
              <keep-column v-for="col in state.keeps" :key="col" :col="col" />
            </div>
          </div>
        </div>
      </div>
    </div>
    <keep-modal v-if="state.keep" />
    <new-keep-modal />
    <new-vault-modal />
  </div>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { onMounted, reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'ProfilesPage',
  watch: {
    'state.account'(newAcc, oldAcc) {
      if (screen.width < 768) {
        vaultsService.getAllByUser(2, AppState.routeParams)
      } else {
        vaultsService.getAllByUser(4, AppState.routeParams)
      }
    }
  },
  setup() {
    onMounted(() => {
      AppState.routeParams = state.route.params.id
      profilesService.getOne(state.route.params.id)
      console.log('MOUNTED')
      if (screen.width < 768) {
        vaultsService.getAllByUser(2, state.account.id)

        keepsService.getAllByUser(3, state.route.params.id)
      } else {
        vaultsService.getAllByUser(4, state.account.id)

        keepsService.getAllByUser(6, state.route.params.id)
      }
    })
    const state = reactive({
      keeps: computed(() => AppState.keepsArrs),
      keep: computed(() => AppState.displayKeep),
      vaults: computed(() => AppState.vaultsArrs),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      keepsCount: computed(() => AppState.keepsCount),
      vaultsCount: computed(() => AppState.vaultsCount),
      route: useRoute()
    })
    return {
      state,
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.reverse{
  flex-direction: row-reverse;
}
.pfp{
  width: 100%;
  border-radius: 20px;
}
@media only screen and (max-width: 600px) {

}
</style>

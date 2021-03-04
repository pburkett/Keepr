/* eslint-disable vue/html-quotes */
<template>
  <div class="px-5 flex-grow-1 d-flex container flex-column ">
    <div class="row w-100 justify-content-end">
      <button v-if="state.vault ? state.vault.creator.id == state.account.id : false" @click="deleteVault" class="btn mt-3 btn-outline-danger">
        DELETE VAULT
      </button>
    </div>
    <div class="row py-3" v-if="state.vault">
      <div class="col-lg-2 col-4 d-flex align-items-center">
        <img class="pfp" :src="state.vault.img" alt="pfp">
      </div>
      <div class="col-12 col-lg-10">
        <h1 class="profile-name">
          {{ state.vault.name }}
        </h1>
        <h4>Keeps: {{ state.keepsCount }}</h4>
      </div>
    </div>
    <div class="row vaults">
      <keep-column v-for="col in state.keeps" :remove-from-vault-opt="true" :key="col" :col="col" />
    </div>
  </div>
  <keep-modal v-if="state.keep" />
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { alerts } from '../utils/Alerts'

export default {
  name: 'VaultsPage',
  watch: {
    'state.account'(newAcc, oldAcc) {
      if (AppState.keeps === undefined) {
        console.log('keep get on login')
        if (screen.width < 768) {
          keepsService.getByVault(3, AppState.vaultId)
          vaultsService.getOne(AppState.vaultId)
        } else {
          keepsService.getByVault(6, AppState.vaultId)
          vaultsService.getOne(AppState.vaultId)
        }
      }
    }
  },
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keepsArrs),
      keep: computed(() => AppState.displayKeep),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.displayVault),
      keepsCount: computed(() => AppState.keepsCount)
    })
    onMounted(() => {
      AppState.vaultId = route.params.id
      if (screen.width < 768) {
        keepsService.getByVault(3, AppState.vaultId)
        vaultsService.getOne(route.params.id)
      } else {
        keepsService.getByVault(6, AppState.vaultId)
        vaultsService.getOne(route.params.id)
      }
    })

    return {
      state,
      async deleteVault() {
        if (await alerts.confirmAction('Are you sure you want to delete this Vault?')) {
          vaultsService.remove(state.vault.id)
          router.push({ name: 'Profiles', params: { id: state.account.id } })
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.vaults{
  text-align: center;
  user-select: none;
  overflow-y: hidden;
  flex-direction: row-reverse;
  > img{
    height: 200px;
    width: 200px;
  }
  }
@media only screen and (max-width: 600px) {
.vaults{
  padding-left: 0px !important;
  padding-right: 0px !important;
}
}
.pfp{
  width: 100%;
  border-radius: 20px;
}

</style>

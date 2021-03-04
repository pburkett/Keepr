/* eslint-disable vue/html-quotes */
<template>
  <div class="home px-5 flex-grow-1 d-flex  align-items-center justify-content-center">
    <keep-column v-for="col in state.keeps" :key="col" :col="col" />
  </div>
  <keep-modal v-if="state.keep" />
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Home',
  watch: {
    'state.account'(newAcc, oldAcc) {
      console.log('watcher')
      if (screen.width < 768) {
        vaultsService.getAllByUser(2, newAcc.id)
      } else {
        vaultsService.getAllByUser(4, newAcc.id)
      }
    }
  },
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keepsArrs),
      keep: computed(() => AppState.displayKeep),
      account: computed(() => AppState.account)

    })
    onMounted(() => {
      if (screen.width < 768) {
        vaultsService.getAllByUser(2, state.account.id)
        keepsService.getAll(2)
      } else {
        vaultsService.getAllByUser(4, state.account.id)

        keepsService.getAll(4)
      }
    }
    )
    return { state }
  }
}
</script>

<style scoped lang="scss">
.home{
  flex-direction: row-reverse;
  text-align: center;
  user-select: none;
  overflow-y: hidden;
  > img{
    height: 200px;
    width: 200px;
  }
  }
@media only screen and (max-width: 600px) {
.home{
  padding-left: 0px !important;
  padding-right: 0px !important;
}
}

</style>

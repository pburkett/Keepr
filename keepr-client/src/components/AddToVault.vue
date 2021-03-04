<template>
  <div class="add-to-vault row c-pointer " @click="addToVault">
    <div class="col-12">
      <h6 class="p-2 bg-white">
        + {{ v.name }}
      </h6>
    </div>
  </div>
</template>

<script>
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {
  name: 'AddToVault',
  props: {
    keepId: {
      type: Number,
      required: true
    },
    v: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async addToVault(vaultId) {
        console.log('addToVault')
        await vaultKeepsService.create({ vaultId: props.v.id, keepId: props.keepId })
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
@import '../assets/scss/_variables.scss';
h6{
  border-top: 3px solid $gray;
}
</style>

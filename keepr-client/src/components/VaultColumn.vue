<template>
  <div class="vault-column mt-3 col">
    <Vault v-for="v in col" :key="v.id" :v="v" />
  </div>
</template>

<script>
export default {
  name: 'VaultColumn',
  props: {
    col: {
      type: Array,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>
.keep-column{
  min-height: 100%;
}
@media only screen and (max-width: 600px) {
.vault-column{
  margin-top: 5px !important;
  padding-left: 5px !important;
  padding-right: 5px !important;
}
}
</style>

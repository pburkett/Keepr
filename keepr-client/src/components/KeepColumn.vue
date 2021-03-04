<template>
  <div class="keep-column mt-3 col">
    <Keep v-for="k in col" :remove-from-vault-opt="removeFromVaultOpt" :key="k.id" :k="k" />
  </div>
</template>

<script>
export default {
  name: 'KeepColumn',
  props: {
    col: {
      type: Array,
      required: true
    },
    removeFromVaultOpt: {
      type: Boolean,
      default: false
    }
  },
  setup() {
    return {}
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.keep-column{
  min-height: 100%;
}
@media only screen and (max-width: 600px) {
.keep-column{
  margin-top: 5px !important;
  padding-left: 5px !important;
  padding-right: 5px !important;
}
}
</style>

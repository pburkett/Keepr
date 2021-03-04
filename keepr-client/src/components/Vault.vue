<template>
  <router-link :to="{name: 'Vaults', params: {id: v.id}}">
    <div class="vault c-pointer h-fc my-4 rounded-2 bottom-shadow hover-topleft elevation-3" @click="openModal" :style="cssVars">
      <img class="v-hidden w-100" :src="v.img" alt="keep">

      <div class="row mb-1 no-wrap justify-content-between">
        <div class="col-8">
          <h5 class="keep-name text-white text-left ml-2">
            {{ v.name }}
          </h5>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script>

export default {
  name: 'Vault',
  props: {
    v: {
      type: Object,
      required: true
    }
  },
  computed: {
    cssVars() {
      return {
        background: `linear-gradient(180.45deg, rgba(0, 0, 0, 0) 67.72%, rgba(0, 0, 0, .5) 99.61%), url(${this.v.img}) center  no-repeat`
      }
    }
  },
  setup(props) {
    return {
    }
  }
}
</script>

<style lang="scss" scoped>
@import '../assets/scss/_variables.scss';

.v-hidden {
  visibility: hidden;
}
.vault {
background-size: cover !important;
max-height: 350px;
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
.vault{
  margin-top: 8px !important;
  margin-bottom: 8px !important;
}
}
</style>

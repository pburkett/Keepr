import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profile: {},
  keeps: [],
  keepsArrs: {},
  keepsCount: 0,
  displayKeep: null,
  vaults: [],
  vaultsArrs: {},
  vaultsCount: 0,
  displayVault: null,
  keepModal: {},
  routeParams: ''
})

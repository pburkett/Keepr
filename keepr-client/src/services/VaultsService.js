import { api } from './AxiosService.js'
import { AppState } from '../AppState.js'

class VaultsService {
  async getAllByUser(divisions, userId) {
    // const res = await api.get('profiles/' + userId + '/vaults')

    const res = await api.get('profiles/' + userId + '/vaults')
    AppState.vaults = res.data
    AppState.vaultsArrs = {}
    AppState.vaultsCount = 0

    // Splits the array into 4 pieces for display
    for (let i = 1; i <= res.data.length; i++) {
      const ratio = i / res.data.length
      for (let n = 1; n <= divisions; n++) {
        if (AppState.vaultsArrs[n - 1] === undefined) {
          AppState.vaultsArrs[n - 1] = []
        }
        if (ratio <= n / divisions && ratio > (n - 1) / divisions) {
          AppState.vaultsArrs[n - 1].push(res.data[i - 1])
          AppState.vaultsCount++
        }
      }
    }
  }

  async getOne(id) {
    const res = await api.get('vaults/' + id)
    AppState.displayVault = res.data
  }

  async create(vault) {
    const res = await api.post('vaults', vault)
    AppState.displayVault = res.data
  }

  async remove(id) {
    await api.delete('vaults/' + id)
    AppState.displayVault = null
  }
}
export const vaultsService = new VaultsService()

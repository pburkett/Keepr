import { api } from './AxiosService'
import { AppState } from '../AppState'
class VaultKeepsService {
  async create(vaultKeep) {
    const res = await api.post('vaultkeeps', vaultKeep)
    AppState.keeps.push(res.data)
  }

  async remove(vaultKeepId) {
    const res = await api.delete('vaultkeeps/' + vaultKeepId)
    AppState.keeps = AppState.keeps.filter(k => k.id !== res.keepId)
  }
}
export const vaultKeepsService = new VaultKeepsService()

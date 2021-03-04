import { api } from './AxiosService.js'
import { AppState } from '../AppState.js'

class KeepsService {
  async getAll(divisions) {
    const res = await api.get('keeps')
    AppState.keepsArrs = {}
    AppState.keepsCount = 0
    // Splits the array into 4 pieces for display
    for (let i = 1; i <= res.data.length; i++) {
      const ratio = i / res.data.length
      for (let n = 1; n <= divisions; n++) {
        if (AppState.keepsArrs[n - 1] === undefined) {
          AppState.keepsArrs[n - 1] = []
        }
        if (ratio <= n / divisions && ratio > (n - 1) / divisions) {
          AppState.keepsArrs[n - 1].push(res.data[i - 1])
          AppState.keepsCount++
        }
      }
    }
  }

  async getOne(id) {
    const res = await api.get('keeps/' + id)
    AppState.displayKeep = res.data
  }

  async getAllByUser(divisions, userId) {
    const res = await api.get('profiles/' + userId + '/keeps')
    AppState.keepsArrs = {}
    AppState.keepsCount = 0

    // Splits the array into 4 pieces for display
    for (let i = 1; i <= res.data.length; i++) {
      const ratio = i / res.data.length
      for (let n = 1; n <= divisions; n++) {
        if (AppState.keepsArrs[n - 1] === undefined) {
          AppState.keepsArrs[n - 1] = []
        }
        if (ratio <= n / divisions && ratio > (n - 1) / divisions) {
          AppState.keepsArrs[n - 1].push(res.data[i - 1])
          AppState.keepsCount++
        }
      }
    }
  }

  async getByVault(divisions, id) {
    const res = await api.get('vaults/' + id + '/keeps')
    AppState.keepsArrs = {}
    AppState.keepsCount = 0

    // Splits the array into 4 pieces for display
    for (let i = 1; i <= res.data.length; i++) {
      const ratio = i / res.data.length
      for (let n = 1; n <= divisions; n++) {
        if (AppState.keepsArrs[n - 1] === undefined) {
          AppState.keepsArrs[n - 1] = []
        }
        if (ratio <= n / divisions && ratio > (n - 1) / divisions) {
          AppState.keepsArrs[n - 1].push(res.data[i - 1])
          AppState.keepsCount++
        }
      }
    }
  }

  async create(keep) {
    const res = await api.post('keeps', keep)
    AppState.displayKeep = res.data
  }

  async remove(id) {
    await api.delete('keeps/' + id)
    AppState.displayKeep = null
    this.getAll(4)
  }
}

export const keepsService = new KeepsService()

import { api } from '../services/AxiosService'
import { AppState } from '../AppState'
class ProfilesService {
  async getOne(id) {
    const res = await api.get('profiles/' + id)
    AppState.profile = res.data
  }
}
export const profilesService = new ProfilesService()

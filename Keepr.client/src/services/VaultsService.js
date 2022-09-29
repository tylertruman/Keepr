import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService {
  async getVaultsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }
  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }
}

export const vaultsService = new VaultsService()
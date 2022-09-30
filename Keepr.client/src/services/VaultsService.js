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
  async create(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.vaults.push(res.data)
  }
  async delete(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)
  }
  async getVaultsByAccount() {
    const res = await api.get('account/vaults')
    AppState.accountVaults = res.data
  }
}

export const vaultsService = new VaultsService()
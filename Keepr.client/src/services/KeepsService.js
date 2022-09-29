import { AppState } from "../AppState";
import { api } from "./AxiosService";

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }
  async getOne(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }
  async getKeepsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.profileKeeps = res.data
  }
  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.activeVaultKeeps = res.data
  }
}

export const keepsService = new KeepsService()
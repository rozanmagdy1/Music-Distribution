import apiClient from './apiClient'
import type { Artist } from '../types'

export const artistService = {
  async list(): Promise<Artist[]> {
    const response = await apiClient.get<Artist[]>('/artists')
    return response.data
  },
}

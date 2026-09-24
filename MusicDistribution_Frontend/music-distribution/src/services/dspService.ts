import apiClient from './apiClient'
import type { Dsp } from '../types'

export const dspService = {
  async list(): Promise<Dsp[]> {
    const response = await apiClient.get<Dsp[]>('/dsps')
    return response.data
  },
}

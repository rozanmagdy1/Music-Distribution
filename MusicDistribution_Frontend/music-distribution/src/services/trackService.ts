import apiClient from './apiClient'
import type { DistributionResult, Track, TrackDistribution, TrackStatus } from '../types'

export interface TrackFilters {
  artistId?: number
  genre?: string
  status?: TrackStatus
}

export const trackService = {
  async list(filters: TrackFilters = {}): Promise<Track[]> {
    const response = await apiClient.get<Track[]>('/tracks', { params: filters })
    return response.data
  },
  async get(id: number): Promise<Track> {
    const response = await apiClient.get<Track>(`/tracks/${id}`)
    return response.data
  },
  async getDistributions(id: number): Promise<TrackDistribution[]> {
    const response = await apiClient.get<TrackDistribution[]>(`/tracks/${id}/distributions`)
    return response.data
  },
  async updateStatus(id: number, status: TrackStatus): Promise<Track> {
    const response = await apiClient.patch<Track>(`/tracks/${id}/status`, { status })
    return response.data
  },
  async distribute(id: number, dspIds: number[]): Promise<DistributionResult> {
    const response = await apiClient.post<DistributionResult>(`/tracks/${id}/distribute`, { dspIds })
    return response.data
  },
}

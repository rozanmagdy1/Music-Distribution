import apiClient from './apiClient'
import type { AuthToken } from '../types'

const TOKEN_KEY = 'music-distribution-token'

export const authService = {
  async login(email: string, password: string): Promise<AuthToken> {
    const response = await apiClient.post<AuthToken>('/auth/login', { email, password })
    localStorage.setItem(TOKEN_KEY, response.data.accessToken)
    return response.data
  },
  logout(): void {
    localStorage.removeItem(TOKEN_KEY)
  },
  hasToken(): boolean {
    return Boolean(localStorage.getItem(TOKEN_KEY))
  },
}

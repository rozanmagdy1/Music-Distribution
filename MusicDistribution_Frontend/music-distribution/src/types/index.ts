export type TrackStatus = 'Draft' | 'Submitted' | 'Distributed'
export type DistributionStatus = 'Pending' | 'Live' | 'Rejected'

export interface Track {
  id: number
  title: string
  artistId: number
  isrc: string
  releaseDate: string
  genre: string
  status: TrackStatus
}

export interface Artist {
  id: number
  name: string
  email: string
  country: string
}

export interface TrackDistribution {
  id: number
  trackId: number
  dspId: number
  submittedAt: string
  status: DistributionStatus
}

export interface DistributionResult {
  submitted: TrackDistribution[]
  alreadyDistributedDspIds: number[]
  error: 'None' | 'TrackNotFound' | 'DspNotFound'
}

export interface AuthToken {
  userId: number
  email: string
  accessToken: string
  tokenType: string
  expiresAtUtc: string
}

export interface Dsp {
  id: number
  name: string
}

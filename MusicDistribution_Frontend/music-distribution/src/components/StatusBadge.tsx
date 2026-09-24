import type { TrackStatus, DistributionStatus } from '../types'

export function StatusBadge({ status }: { status: TrackStatus | DistributionStatus }) {
  return <span className={`status-badge status-${status.toLowerCase()}`}>{status}</span>
}

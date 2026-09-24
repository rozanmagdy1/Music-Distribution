import type { Track } from '../../types'
import { TrackRow } from './TrackRow'

interface TrackTableProps {
  tracks: Track[]
  artistsById: Map<number, string>
  status: string
  onOpenTrack: (trackId: number) => void
}

export function TrackTable({ tracks, artistsById, status, onOpenTrack }: TrackTableProps) {
  if (tracks.length === 0) {
    return <div className="empty-state"><span className="empty-icon">♪</span><h2>No tracks found</h2><p>{status ? `There are no ${status.toLowerCase()} tracks right now.` : 'Your catalog is ready for its first release.'}</p></div>
  }

  return <div className="table-wrap"><table><thead><tr><th>TRACK</th><th>ARTIST</th><th>GENRE</th><th>STATUS</th><th aria-label="Open track" /></tr></thead><tbody>{tracks.map((track) => <TrackRow key={track.id} track={track} artistName={artistsById.get(track.artistId) ?? `Artist #${track.artistId}`} onOpen={onOpenTrack} />)}</tbody></table></div>
}

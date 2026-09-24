import { Link } from 'react-router-dom'
import type { Track } from '../../types'
import { StatusBadge } from '../StatusBadge'

interface TrackRowProps {
  track: Track
  artistName: string
  onOpen: (trackId: number) => void
}

export function TrackRow({ track, artistName, onOpen }: TrackRowProps) {
  return <tr onClick={() => onOpen(track.id)} className="clickable-row">
    <td><Link className="track-title" to={`/tracks/${track.id}`} onClick={(event) => event.stopPropagation()}><span className="track-art">♪</span><span>{track.title}<small>{track.isrc || 'Catalog release'}</small></span></Link></td>
    <td>{artistName}</td>
    <td>{track.genre}</td>
    <td><StatusBadge status={track.status} /></td>
    <td className="row-arrow">↗</td>
  </tr>
}

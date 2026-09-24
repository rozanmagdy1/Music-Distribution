import type { Artist } from '../../types'

export function ArtistRow({ artist }: { artist: Artist }) {
  return <article className="artist-row"><span className="artist-avatar">{artist.name.trim().charAt(0).toUpperCase()}</span><div className="artist-info"><strong>{artist.name}</strong><span>{artist.email}</span></div><span className="artist-country">{artist.country}</span></article>
}

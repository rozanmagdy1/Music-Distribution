import { useEffect, useState } from 'react'
import { ArtistRow } from '../components/artists/ArtistRow'
import { ErrorMessage, LoadingState } from '../components/Feedback'
import { Pagination } from '../components/Pagination'
import { getErrorMessage } from '../services/apiClient'
import { artistService } from '../services/artistService'
import type { Artist } from '../types'

export function ArtistsPage() {
  const pageSize = 10
  const [artists, setArtists] = useState<Artist[]>([])
  const [page, setPage] = useState(1)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    artistService.list()
      .then(setArtists)
      .catch((issue: unknown) => setError(getErrorMessage(issue, 'We couldn’t load artists. Please try again.')))
      .finally(() => setLoading(false))
  }, [])

  const totalPages = Math.max(1, Math.ceil(artists.length / pageSize))
  const pageArtists = artists.slice((page - 1) * pageSize, page * pageSize)

  return <section><div className="page-heading"><div><p className="eyebrow">YOUR PEOPLE</p><h1>Artists</h1><p className="page-subtitle">Artists connected to your releases.</p></div><span className="count-pill">{loading ? '—' : `${artists.length} ${artists.length === 1 ? 'artist' : 'artists'}`}</span></div>{error && <ErrorMessage>{error}</ErrorMessage>}{loading ? <LoadingState label="Loading artists…" /> : !error && !artists.length ? <div className="empty-state"><h2>No artists found</h2><p>Artists will appear here when available in the catalog.</p></div> : !error && <><div className="artist-list">{pageArtists.map((artist) => <ArtistRow key={artist.id} artist={artist} />)}</div><Pagination page={page} totalPages={totalPages} onPageChange={setPage} /></>}</section>
}

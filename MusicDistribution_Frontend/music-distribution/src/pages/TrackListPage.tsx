import { useEffect, useMemo, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { ErrorMessage, LoadingState } from '../components/Feedback'
import { Pagination } from '../components/Pagination'
import { TrackFilters, type TrackFilterValues } from '../components/tracks/TrackFilters'
import { TrackTable } from '../components/tracks/TrackTable'
import { getErrorMessage } from '../services/apiClient'
import { artistService } from '../services/artistService'
import { trackService, type TrackFilters as ApiTrackFilters } from '../services/trackService'
import type { Artist, Track, TrackStatus } from '../types'

const pageSize = 10
const emptyFilterValues: TrackFilterValues = { artistId: '', genre: '', status: '' }

export function TrackListPage() {
  const navigate = useNavigate()
  const [filterValues, setFilterValues] = useState<TrackFilterValues>(emptyFilterValues)
  const [appliedFilters, setAppliedFilters] = useState<ApiTrackFilters>({})
  const filterKey = JSON.stringify(appliedFilters)
  const [page, setPage] = useState(1)
  const [tracks, setTracks] = useState<Track[]>([])
  const [artists, setArtists] = useState<Artist[]>([])
  const [genres, setGenres] = useState<string[]>([])
  const [loadedFilterKey, setLoadedFilterKey] = useState<string | null>(null)
  const [artistsLoaded, setArtistsLoaded] = useState(false)
  const [trackError, setTrackError] = useState('')
  const [artistError, setArtistError] = useState('')
  const loadingTracks = loadedFilterKey !== filterKey
  const loading = loadingTracks || !artistsLoaded

  useEffect(() => {
    let active = true
    trackService.list(appliedFilters)
      .then((trackRows) => {
        if (!active) return
        setTracks(trackRows)
        setTrackError('')
        if (Object.keys(appliedFilters).length === 0) {
          setGenres([...new Set(trackRows.map((track) => track.genre).filter(Boolean))].sort((a, b) => a.localeCompare(b)))
        }
      })
      .catch((issue: unknown) => { if (active) setTrackError(getErrorMessage(issue, 'We couldn’t load the catalog. Please try again.')) })
      .finally(() => { if (active) setLoadedFilterKey(filterKey) })
    return () => { active = false }
  }, [appliedFilters, filterKey])

  useEffect(() => {
    let active = true
    artistService.list()
      .then((artistRows) => { if (active) { setArtists(artistRows); setArtistError('') } })
      .catch((issue: unknown) => { if (active) setArtistError(getErrorMessage(issue, 'We couldn’t load artists for filtering. Please try again.')) })
      .finally(() => { if (active) setArtistsLoaded(true) })
    return () => { active = false }
  }, [])

  const artistsById = useMemo(() => new Map(artists.map((artist) => [artist.id, artist.name])), [artists])
  const totalPages = Math.max(1, Math.ceil(tracks.length / pageSize))
  const pageTracks = tracks.slice((page - 1) * pageSize, page * pageSize)

  const applyFilters = () => {
    const nextFilters: ApiTrackFilters = {}
    if (filterValues.artistId) nextFilters.artistId = Number(filterValues.artistId)
    if (filterValues.genre) nextFilters.genre = filterValues.genre
    if (filterValues.status) nextFilters.status = filterValues.status as TrackStatus
    setAppliedFilters((current) => JSON.stringify(current) === JSON.stringify(nextFilters) ? current : nextFilters)
    setPage(1)
  }

  const clearFilters = () => {
    setFilterValues(emptyFilterValues)
    setAppliedFilters((current) => Object.keys(current).length === 0 ? current : {})
    setPage(1)
  }

  const error = trackError || artistError

  return <section>
    <div className="page-heading"><div><p className="eyebrow">YOUR CATALOG</p><h1>Tracks</h1><p className="page-subtitle">A clear view of every release in your catalog.</p></div><span className="count-pill">{loading ? '—' : `${tracks.length} ${tracks.length === 1 ? 'track' : 'tracks'}`}</span></div>
    <TrackFilters artists={artists} genres={genres} values={filterValues} onChange={setFilterValues} onApply={applyFilters} onClear={clearFilters} />
    {error && <ErrorMessage>{error}</ErrorMessage>}
    {loading ? <LoadingState label="Loading tracks…" /> : !error && <><TrackTable tracks={pageTracks} artistsById={artistsById} status={appliedFilters.status ?? ''} onOpenTrack={(trackId) => navigate(`/tracks/${trackId}`)} /><Pagination page={page} totalPages={totalPages} onPageChange={setPage} /></>}
  </section>
}

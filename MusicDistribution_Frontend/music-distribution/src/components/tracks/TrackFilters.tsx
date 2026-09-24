import type { Artist, TrackStatus } from '../../types'

const statuses: TrackStatus[] = ['Draft', 'Submitted', 'Distributed']

export interface TrackFilterValues {
  artistId: string
  genre: string
  status: string
}

interface TrackFiltersProps {
  artists: Artist[]
  genres: string[]
  values: TrackFilterValues
  onChange: (values: TrackFilterValues) => void
  onApply: () => void
  onClear: () => void
}

export function TrackFilters({ artists, genres, values, onChange, onApply, onClear }: TrackFiltersProps) {
  return <form className="toolbar track-filters" onSubmit={(event) => { event.preventDefault(); onApply() }}>
    <div className="track-filter-field"><label className="filter-label" htmlFor="artist-filter">Artist</label><select id="artist-filter" value={values.artistId} onChange={(event) => onChange({ ...values, artistId: event.target.value })}><option value="">All artists</option>{artists.map((artist) => <option key={artist.id} value={artist.id}>{artist.name}</option>)}</select></div>
    <div className="track-filter-field"><label className="filter-label" htmlFor="genre-filter">Genre</label><select id="genre-filter" value={values.genre} onChange={(event) => onChange({ ...values, genre: event.target.value })}><option value="">All genres</option>{genres.map((genre) => <option key={genre} value={genre}>{genre}</option>)}</select></div>
    <div className="track-filter-field"><label className="filter-label" htmlFor="status-filter">Status</label><select id="status-filter" value={values.status} onChange={(event) => onChange({ ...values, status: event.target.value })}><option value="">All statuses</option>{statuses.map((value) => <option key={value}>{value}</option>)}</select></div>
    <div className="filter-actions">
      <button className="button button-primary filter-apply" type="submit">Apply filters</button>
      <button className="button button-secondary filter-clear" type="button" onClick={onClear}>Clear</button>
    </div>
  </form>
}

import { useCallback, useEffect, useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom'
import { DspSelector } from '../components/distribution/DspSelector'
import { ErrorMessage, LoadingState } from '../components/Feedback'
import { StatusBadge } from '../components/StatusBadge'
import { artistService } from '../services/artistService'
import { authService } from '../services/authService'
import { getErrorMessage } from '../services/apiClient'
import { dspService } from '../services/dspService'
import { trackService } from '../services/trackService'
import type { Artist, DistributionResult, Dsp, Track, TrackDistribution, TrackStatus } from '../types'

const statuses: TrackStatus[] = ['Draft', 'Submitted', 'Distributed']

export function TrackDetailPage() {
  const { id } = useParams()
  const trackId = Number(id)
  const navigate = useNavigate()
  const location = useLocation()
  const [track, setTrack] = useState<Track | null>(null)
  const [artist, setArtist] = useState<Artist | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const [actionError, setActionError] = useState('')
  const [working, setWorking] = useState(false)
  const [nextStatus, setNextStatus] = useState<TrackStatus>('Draft')
  const [selectedDsps, setSelectedDsps] = useState<number[]>([])
  const [dsps, setDsps] = useState<Dsp[]>([])
  const [distributions, setDistributions] = useState<TrackDistribution[]>([])
  const [distributionResult, setDistributionResult] = useState<DistributionResult | null>(null)
  const signedIn = authService.hasToken()
  const invalidTrackId = !Number.isInteger(trackId) || trackId <= 0

  useEffect(() => {
    let active = true
    if (invalidTrackId) return
    Promise.all([trackService.get(trackId), artistService.list(), dspService.list(), trackService.getDistributions(trackId)])
      .then(([trackRow, artistRows, dspRows, distributionRows]) => { if (active) { setTrack(trackRow); setArtist(artistRows.find((item) => item.id === trackRow.artistId) ?? null); setNextStatus(trackRow.status); setDsps(dspRows); setDistributions(distributionRows); setSelectedDsps(distributionRows.map((item) => item.dspId)) } })
      .catch((issue: unknown) => { if (active) setError(getErrorMessage(issue, 'We couldn’t load this track. Please try again.')) })
      .finally(() => { if (active) setLoading(false) })
    return () => { active = false }
  }, [invalidTrackId, trackId])

  const requireLogin = useCallback(() => {
    navigate('/login', { state: { from: `${location.pathname}${location.search}` } })
  }, [location.pathname, location.search, navigate])

  const updateStatus = async () => {
    if (!track) return
    if (!signedIn) { requireLogin(); return }
    setWorking(true); setActionError('')
    try { setTrack(await trackService.updateStatus(track.id, nextStatus)) }
    catch (issue) { setActionError(getErrorMessage(issue, 'We couldn’t update the track status. Please try again.')) }
    finally { setWorking(false) }
  }

  const toggleDsp = (dspId: number) => {
    if (!signedIn) { requireLogin(); return }
    setSelectedDsps((selected) => selected.includes(dspId) ? selected.filter((item) => item !== dspId) : [...selected, dspId])
  }

  const distribute = async () => {
    if (!track) return
    if (!signedIn) { requireLogin(); return }
    if (!selectedDsps.length) { setActionError('Choose at least one DSP to continue.'); return }
    setWorking(true); setActionError(''); setDistributionResult(null)
    try {
      const result = await trackService.distribute(track.id, selectedDsps)
      setDistributionResult(result)
      setDistributions((existing) => [...existing, ...result.submitted.filter((item) => !existing.some((saved) => saved.dspId === item.dspId))])
    }
    catch (issue) { setActionError(getErrorMessage(issue, 'We couldn’t submit this track for distribution. Please try again.')) }
    finally { setWorking(false) }
  }

  if (loading && !invalidTrackId) return <LoadingState label="Loading track…" />
  if (error || invalidTrackId || !track) return <><Link className="back-link" to="/">← Back to tracks</Link><ErrorMessage>{error || 'This track could not be found.'}</ErrorMessage></>

  return <section>
    <Link className="back-link" to="/">← All tracks</Link>
    <div className="detail-heading"><div className="detail-art">♪</div><div className="detail-title"><p className="eyebrow">TRACK DETAILS</p><h1>{track.title}</h1><p>{artist?.name ?? `Artist #${track.artistId}`} <span className="dot">·</span> {track.genre}</p></div><StatusBadge status={track.status} /></div>
    <div className="detail-grid">
      <article className="panel"><div className="panel-heading"><div><p className="eyebrow">RELEASE</p><h2>Track information</h2></div></div><dl className="metadata"><div><dt>Artist</dt><dd>{artist?.name ?? `Artist #${track.artistId}`}</dd></div><div><dt>Genre</dt><dd>{track.genre}</dd></div><div><dt>Release date</dt><dd>{track.releaseDate}</dd></div><div><dt>ISRC</dt><dd className="monospace">{track.isrc}</dd></div><div><dt>Track ID</dt><dd className="monospace">{track.id}</dd></div><div><dt>Current status</dt><dd><StatusBadge status={track.status} /></dd></div></dl></article>
      <DspSelector dsps={dsps} selectedDspIds={selectedDsps} distributions={distributions} distributionResult={distributionResult} actionError={actionError} working={working} signedIn={signedIn} onToggleDsp={toggleDsp} onDistribute={distribute} />
      <article className="panel status-panel"><div className="panel-heading"><div><p className="eyebrow">WORKFLOW</p><h2>Update track status</h2></div></div><p className="panel-description">Keep your catalog status up to date.</p><div className="inline-form"><select value={nextStatus} onChange={(event) => setNextStatus(event.target.value as TrackStatus)} aria-label="New track status">{statuses.map((value) => <option key={value}>{value}</option>)}</select><button className="button button-secondary" onClick={updateStatus} disabled={working}>{working ? 'Saving…' : 'Save status'}</button></div>{!signedIn && <p className="login-hint">Log in to update track status.</p>}</article>
    </div>
  </section>
}

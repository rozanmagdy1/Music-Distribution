import { useState } from 'react'
import type { DistributionResult, Dsp, TrackDistribution } from '../../types'
import { ErrorMessage } from '../Feedback'
import { StatusBadge } from '../StatusBadge'
import { Pagination } from '../Pagination'

interface DspSelectorProps {
  dsps: Dsp[]
  selectedDspIds: number[]
  distributions: TrackDistribution[]
  distributionResult: DistributionResult | null
  actionError: string
  working: boolean
  signedIn: boolean
  onToggleDsp: (dspId: number) => void
  onDistribute: () => void
}

export function DspSelector({ dsps, selectedDspIds, distributions, distributionResult, actionError, working, signedIn, onToggleDsp, onDistribute }: DspSelectorProps) {
  const pageSize = 10
  const [page, setPage] = useState(1)
  const allDspsSelected = dsps.length > 0 && dsps.every((dsp) => selectedDspIds.includes(dsp.id))
  const totalPages = Math.max(1, Math.ceil(dsps.length / pageSize))
  const visibleDsps = dsps.slice((page - 1) * pageSize, page * pageSize)

  return <article className="panel distribution-panel"><div className="panel-heading"><div><p className="eyebrow">DELIVERY</p><h2>DSP distribution</h2></div><span className="panel-icon">↗</span></div>
    <div className="api-note"><strong>Existing destinations are preselected.</strong><p>Sign in to change destinations. Existing submissions can’t be removed through the current API.</p></div>
    <p className="form-label">Select DSPs</p><div className="dsp-options">{visibleDsps.map((dsp) => {
      const existing = distributions.find((item) => item.dspId === dsp.id)
      return <label key={dsp.id} className={`dsp-option ${selectedDspIds.includes(dsp.id) ? 'selected' : ''}`}><input type="checkbox" checked={selectedDspIds.includes(dsp.id)} disabled={!signedIn || Boolean(existing) || working} onChange={() => onToggleDsp(dsp.id)} /><span className="checkmark">✓</span><span>{dsp.name}</span>{existing && <span className="dsp-state"><StatusBadge status={existing.status} /></span>}</label>
    })}</div>
    <Pagination page={page} totalPages={totalPages} onPageChange={setPage} />
    {distributionResult && <div className="result-block"><strong>{distributionResult.submitted.length ? 'Submitted for distribution' : 'Already submitted'}</strong>{distributionResult.submitted.length > 0 && distributionResult.submitted.map((item) => <div className="result-line" key={item.id}>{dsps.find((dsp) => dsp.id === item.dspId)?.name ?? `DSP #${item.dspId}`}<StatusBadge status={item.status} /></div>)}{distributionResult.alreadyDistributedDspIds.length > 0 && <p className="result-hint">Already submitted to: {distributionResult.alreadyDistributedDspIds.map((dspId) => dsps.find((dsp) => dsp.id === dspId)?.name ?? `DSP #${dspId}`).join(', ')}.</p>}</div>}
    {actionError && <ErrorMessage>{actionError}</ErrorMessage>}
    {!allDspsSelected && <button className="button button-primary full-button" onClick={onDistribute} disabled={working}>{working ? 'Submitting…' : signedIn ? 'Submit for distribution' : 'Log in to distribute'}</button>}
  </article>
}

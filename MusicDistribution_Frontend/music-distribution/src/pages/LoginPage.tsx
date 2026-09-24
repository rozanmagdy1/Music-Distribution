import { useState, type FormEvent } from 'react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { ErrorMessage } from '../components/Feedback'
import { authService } from '../services/authService'
import { getErrorMessage } from '../services/apiClient'

interface LoginPageProps {
  onLogin: () => void
}

export function LoginPage({ onLogin }: LoginPageProps) {
  const navigate = useNavigate()
  const location = useLocation()
  const state = location.state as { from?: string; expired?: boolean } | null
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [working, setWorking] = useState(false)

  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault(); setWorking(true); setError('')
    try { await authService.login(email, password); onLogin(); navigate(state?.from || '/', { replace: true }) }
    catch (issue) { setError(getErrorMessage(issue, 'We couldn’t sign you in. Check your details and try again.')) }
    finally { setWorking(false) }
  }

  return <div className="login-layout"><div className="login-copy"><p className="eyebrow">NORTHSTAR MUSIC</p><h1>Good music<br />deserves a clear path.</h1><p>Sign in to manage release status and send tracks to your distribution partners.</p><div className="login-note"><span>♪</span><span>Your catalog, ready for the next step.</span></div></div><section className="login-panel"><div className="login-panel-heading"><span className="login-icon">N</span><p className="eyebrow">WELCOME BACK</p><h2>Sign in</h2><p>Use your account credentials to continue.</p></div>{state?.expired && <div className="alert alert-info">Your session expired. Sign in to continue where you left off.</div>}{error && <ErrorMessage>{error}</ErrorMessage>}<form onSubmit={submit}><label className="form-label" htmlFor="email">Email address</label><input id="email" type="email" autoComplete="email" required maxLength={254} value={email} onChange={(event) => setEmail(event.target.value)} placeholder="you@example.com" /><label className="form-label" htmlFor="password">Password</label><input id="password" type="password" autoComplete="current-password" required minLength={8} maxLength={128} value={password} onChange={(event) => setPassword(event.target.value)} placeholder="Enter your password" /><button className="button button-primary full-button" type="submit" disabled={working}>{working ? 'Signing in…' : 'Sign in'}</button></form><Link className="login-back" to={state?.from || '/'}>← Return to {state?.from?.startsWith('/tracks/') ? 'track' : 'catalog'}</Link></section></div>
}

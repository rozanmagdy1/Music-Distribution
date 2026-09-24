import { useEffect, useState } from 'react'
import { Route, Routes, useLocation, useNavigate } from 'react-router-dom'
import { authService } from '../../services/authService'
import { ArtistsPage } from '../../pages/ArtistsPage'
import { LoginPage } from '../../pages/LoginPage'
import { NotFound } from '../../pages/NotFound'
import { TrackDetailPage } from '../../pages/TrackDetailPage'
import { TrackListPage } from '../../pages/TrackListPage'
import { Footer } from './Footer'
import { Header } from './Header'

export function Shell() {
  const navigate = useNavigate()
  const location = useLocation()
  const [signedIn, setSignedIn] = useState(authService.hasToken())
  useEffect(() => {
    const sync = () => setSignedIn(authService.hasToken())
    const onUnauthorized = () => {
      sync()
      navigate('/login', { state: { from: `${location.pathname}${location.search}`, expired: true } })
    }
    window.addEventListener('storage', sync)
    window.addEventListener('auth:unauthorized', onUnauthorized)
    return () => {
      window.removeEventListener('storage', sync)
      window.removeEventListener('auth:unauthorized', onUnauthorized)
    }
  }, [location.pathname, location.search, navigate])

  const logout = () => {
    authService.logout()
    setSignedIn(false)
    navigate('/')
  }

  return <div className="app-shell">
    <Header signedIn={signedIn} currentPath={`${location.pathname}${location.search}`} onLogout={logout} />
    <main className="main-content"><Routes>
      <Route path="/" element={<TrackListPage />} />
      <Route path="/tracks/:id" element={<TrackDetailPage />} />
      <Route path="/artists" element={<ArtistsPage />} />
      <Route path="/login" element={<LoginPage onLogin={() => setSignedIn(true)} />} />
      <Route path="*" element={<NotFound />} />
    </Routes></main>
    <Footer />
  </div>
}

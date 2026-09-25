import { Link, NavLink } from 'react-router-dom'

interface HeaderProps {
  signedIn: boolean
  currentPath: string
  onLogout: () => void
}

export function Header({ signedIn, currentPath, onLogout }: HeaderProps) {
  return <header className="topbar">
    <Link className="brand" to="/" aria-label="Takwene / Music home"><span className="brand-mark">T</span><span>Takwene<span className="brand-light"> / Music</span></span></Link>
    <nav className="navigation" aria-label="Main navigation">
      <NavLink to="/" end>Tracks</NavLink>
      <NavLink to="/artists">Artists</NavLink>
    </nav>
    <div className="account-nav">{signedIn ? <><span className="signed-in"><i />Signed in</span><button className="text-button" onClick={onLogout}>Log out</button></> : <NavLink to="/login" state={{ from: currentPath }}>Log in</NavLink>}</div>
  </header>
}

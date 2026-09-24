import { Link } from 'react-router-dom'

export function NotFound() {
  return <div className="empty-state"><span className="empty-icon">♪</span><h2>Page not found</h2><p>The page you’re looking for isn’t here.</p><Link className="button button-secondary" to="/">Back to tracks</Link></div>
}

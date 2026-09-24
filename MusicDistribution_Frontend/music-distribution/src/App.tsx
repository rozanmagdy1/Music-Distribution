import { BrowserRouter } from 'react-router-dom'
import { Shell } from './components/layout/Shell'
import './App.css'

export default function App() {
  return <BrowserRouter>
    <Shell />
  </BrowserRouter>
}

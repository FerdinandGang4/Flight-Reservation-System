import { useEffect, useMemo, useState } from 'react'
import AppHeader from './components/AppHeader'
import AppFooter from './components/AppFooter'
import FlightFilters from './components/FlightFilters'
import FlightList from './components/FlightList'
import FlightSummary from './components/FlightSummary'
import { flights } from './data/flights'
import './App.css'

function App() {
  const [filters, setFilters] = useState({ origin: '', destination: '' })
  const [selectedFlightId, setSelectedFlightId] = useState(flights[0]?.id ?? null)
  const [user, setUser] = useState(() => {
    if (typeof window === 'undefined') return null

    const storedUser = window.localStorage.getItem('flightEaseUser')
    return storedUser ? JSON.parse(storedUser) : null
  })
  const [showLogin, setShowLogin] = useState(false)
  const [loginForm, setLoginForm] = useState({ email: '', password: '' })
  const [loginError, setLoginError] = useState('')

  useEffect(() => {
    if (!user) {
      window.localStorage.removeItem('flightEaseUser')
      return
    }

    window.localStorage.setItem('flightEaseUser', JSON.stringify(user))
  }, [user])

  const filteredFlights = useMemo(() => {
    return flights.filter((flight) => {
      const matchesOrigin =
        !filters.origin ||
        flight.origin.toLowerCase().includes(filters.origin.toLowerCase())
      const matchesDestination =
        !filters.destination ||
        flight.destination.toLowerCase().includes(filters.destination.toLowerCase())

      return matchesOrigin && matchesDestination
    })
  }, [filters])

  const selectedFlight =
    filteredFlights.find((flight) => flight.id === selectedFlightId) ??
    filteredFlights[0] ??
    flights[0] ??
    null

  const handleFilterChange = (field, value) => {
    setFilters((current) => ({ ...current, [field]: value }))
  }

  const handleLoginSubmit = (event) => {
    event.preventDefault()

    const email = loginForm.email.trim()
    const password = loginForm.password.trim()

    if (!email || !password) {
      setLoginError('Please enter both your email and password.')
      return
    }

    const name = email.split('@')[0] || 'Traveller'

    setUser({
      name: name.charAt(0).toUpperCase() + name.slice(1),
      email,
    })
    setLoginError('')
    setLoginForm({ email: '', password: '' })
    setShowLogin(false)
  }

  const handleLogout = () => {
    setUser(null)
  }

  return (
    <>
      <AppHeader user={user} onLoginClick={() => setShowLogin(true)} onLogout={handleLogout} />

      {showLogin && (
        <div className="login-overlay" onClick={() => setShowLogin(false)}>
          <div className="login-modal" onClick={(event) => event.stopPropagation()}>
            <div className="login-header">
              <h2>Welcome back</h2>
              <button type="button" className="close-button" onClick={() => setShowLogin(false)}>
                ×
              </button>
            </div>

            <form className="login-form" onSubmit={handleLoginSubmit}>
              <label>
                Email
                <input
                  type="email"
                  placeholder="you@example.com"
                  value={loginForm.email}
                  onChange={(event) =>
                    setLoginForm((current) => ({ ...current, email: event.target.value }))
                  }
                />
              </label>

              <label>
                Password
                <input
                  type="password"
                  placeholder="••••••••"
                  value={loginForm.password}
                  onChange={(event) =>
                    setLoginForm((current) => ({ ...current, password: event.target.value }))
                  }
                />
              </label>

              {loginError && <p className="login-error">{loginError}</p>}

              <button type="submit" className="primary-button full-width">
                Sign in to account
              </button>
            </form>
          </div>
        </div>
      )}

      <div className="flight-app">
        <header className="page-header">
          <div>
            <p className="eyebrow">Flight reservation</p>
            <h1>Browse flights</h1>
            {user && <p className="welcome-text">Welcome back, {user.name}</p>}
          </div>
          <button type="button" className="primary-button">
            Book now
          </button>
        </header>

        <FlightFilters filters={filters} onChange={handleFilterChange} />

        <main className="flight-layout">
          <section className="flight-list-panel">
            <div className="panel-header">
              <h2>Available flights</h2>
              <span>{filteredFlights.length} result(s)</span>
            </div>

            <FlightList
              flights={filteredFlights}
              selectedFlightId={selectedFlight?.id ?? null}
              onSelectFlight={setSelectedFlightId}
            />
          </section>

          <FlightSummary flight={selectedFlight} />
        </main>
      </div>

      <AppFooter />
    </>
  )
}

export default App

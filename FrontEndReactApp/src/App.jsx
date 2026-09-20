import { useMemo, useState } from 'react'
import FlightFilters from './components/FlightFilters'
import FlightList from './components/FlightList'
import FlightSummary from './components/FlightSummary'
import { flights } from './data/flights'
import './App.css'

function App() {
  const [filters, setFilters] = useState({ origin: '', destination: '' })
  const [selectedFlightId, setSelectedFlightId] = useState(flights[0]?.id ?? null)

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

  return (
    <div className="flight-app">
      <header className="page-header">
        <div>
          <p className="eyebrow">Flight reservation</p>
          <h1>Browse flights</h1>
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
  )
}

export default App

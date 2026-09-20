import { useMemo, useState } from 'react'
import './App.css'

const flights = [
  {
    id: 1,
    airline: 'SkyJet Airways',
    flightNo: 'SJ 204',
    origin: 'New York',
    destination: 'Chicago',
    departAt: '08:30 AM',
    arriveAt: '11:15 AM',
    duration: '2h 45m',
    stops: 0,
    price: 189,
    seatsLeft: 8,
    cabin: 'Economy'
  },
  {
    id: 2,
    airline: 'Blue Horizon',
    flightNo: 'BH 118',
    origin: 'New York',
    destination: 'Miami',
    departAt: '09:45 AM',
    arriveAt: '12:50 PM',
    duration: '3h 05m',
    stops: 1,
    price: 245,
    seatsLeft: 5,
    cabin: 'Economy'
  },
  {
    id: 3,
    airline: 'AeroNova',
    flightNo: 'AN 522',
    origin: 'Chicago',
    destination: 'Los Angeles',
    departAt: '01:10 PM',
    arriveAt: '04:25 PM',
    duration: '3h 15m',
    stops: 0,
    price: 310,
    seatsLeft: 12,
    cabin: 'Business'
  },
  {
    id: 4,
    airline: 'Violet Air',
    flightNo: 'VA 441',
    origin: 'Miami',
    destination: 'Seattle',
    departAt: '06:20 PM',
    arriveAt: '09:55 PM',
    duration: '3h 35m',
    stops: 1,
    price: 278,
    seatsLeft: 4,
    cabin: 'Economy'
  },
  {
    id: 5,
    airline: 'SkyJet Airways',
    flightNo: 'SJ 330',
    origin: 'Chicago',
    destination: 'New York',
    departAt: '07:15 AM',
    arriveAt: '10:05 AM',
    duration: '2h 50m',
    stops: 0,
    price: 205,
    seatsLeft: 9,
    cabin: 'Economy'
  },
  {
    id: 6,
    airline: 'NorthStar',
    flightNo: 'NS 901',
    origin: 'Seattle',
    destination: 'Los Angeles',
    departAt: '11:00 AM',
    arriveAt: '01:40 PM',
    duration: '2h 40m',
    stops: 0,
    price: 230,
    seatsLeft: 6,
    cabin: 'Premium'
  }
]

function App() {
  const [filters, setFilters] = useState({ origin: '', destination: '' })
  const [selectedFlightId, setSelectedFlightId] = useState(flights[0].id)

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
    filteredFlights.find((flight) => flight.id === selectedFlightId) ?? filteredFlights[0] ?? flights[0]

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

      <section className="search-panel">
        <div className="field-group">
          <label htmlFor="origin">From</label>
          <input
            id="origin"
            type="text"
            value={filters.origin}
            placeholder="Origin city"
            onChange={(event) =>
              setFilters((current) => ({ ...current, origin: event.target.value }))
            }
          />
        </div>

        <div className="field-group">
          <label htmlFor="destination">To</label>
          <input
            id="destination"
            type="text"
            value={filters.destination}
            placeholder="Destination city"
            onChange={(event) =>
              setFilters((current) => ({ ...current, destination: event.target.value }))
            }
          />
        </div>

        <div className="field-group">
          <label htmlFor="date">Departure</label>
          <input id="date" type="date" defaultValue="2026-09-21" />
        </div>
      </section>

      <main className="flight-layout">
        <section className="flight-list-panel">
          <div className="panel-header">
            <h2>Available flights</h2>
            <span>{filteredFlights.length} result(s)</span>
          </div>

          {filteredFlights.length > 0 ? (
            filteredFlights.map((flight) => (
              <article
                key={flight.id}
                className={`flight-card ${selectedFlight.id === flight.id ? 'selected' : ''}`}
              >
                <div className="flight-card-header">
                  <div>
                    <p className="airline">{flight.airline}</p>
                    <h3>
                      {flight.origin} to {flight.destination}
                    </h3>
                  </div>
                  <span className="badge">{flight.cabin}</span>
                </div>

                <div className="flight-times">
                  <div>
                    <span className="time">{flight.departAt}</span>
                    <small>{flight.origin}</small>
                  </div>
                  <div className="timeline">
                    <span>{flight.duration}</span>
                    <div className="line"></div>
                    <small>{flight.stops === 0 ? 'Non-stop' : `${flight.stops} stop`}</small>
                  </div>
                  <div>
                    <span className="time">{flight.arriveAt}</span>
                    <small>{flight.destination}</small>
                  </div>
                </div>

                <div className="flight-card-footer">
                  <div className="meta">
                    <span>Flight {flight.flightNo}</span>
                    <span>{flight.seatsLeft} seats left</span>
                  </div>

                  <div className="price-block">
                    <strong>${flight.price}</strong>
                    <button type="button" onClick={() => setSelectedFlightId(flight.id)}>
                      {selectedFlight.id === flight.id ? 'Selected' : 'Select'}
                    </button>
                  </div>
                </div>
              </article>
            ))
          ) : (
            <div className="empty-state">
              <h3>No flights match your filters.</h3>
              <p>Try a different departure or destination city.</p>
            </div>
          )}
        </section>

        <aside className="summary-panel">
          <h2>Selected flight</h2>

          <div className="summary-box">
            <p className="summary-route">
              {selectedFlight.origin} → {selectedFlight.destination}
            </p>
            <h3>{selectedFlight.airline}</h3>
            <p className="summary-flight">Flight {selectedFlight.flightNo}</p>

            <div className="summary-row">
              <span>Departure</span>
              <strong>{selectedFlight.departAt}</strong>
            </div>
            <div className="summary-row">
              <span>Arrival</span>
              <strong>{selectedFlight.arriveAt}</strong>
            </div>
            <div className="summary-row">
              <span>Duration</span>
              <strong>{selectedFlight.duration}</strong>
            </div>
            <div className="summary-row total-row">
              <span>Total</span>
              <strong>${selectedFlight.price}</strong>
            </div>
          </div>

          <button type="button" className="primary-button full-width">
            Continue booking
          </button>
        </aside>
      </main>
    </div>
  )
}

export default App

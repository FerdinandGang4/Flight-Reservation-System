export default function FlightList({ flights, selectedFlightId, onSelectFlight }) {
  if (flights.length === 0) {
    return (
      <div className="empty-state">
        <h3>No flights match your filters.</h3>
        <p>Try a different departure or destination city.</p>
      </div>
    )
  }

  return (
    <>
      {flights.map((flight) => (
        <article
          key={flight.id}
          className={`flight-card ${selectedFlightId === flight.id ? 'selected' : ''}`}
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
              <button type="button" onClick={() => onSelectFlight(flight.id)}>
                {selectedFlightId === flight.id ? 'Selected' : 'Select'}
              </button>
            </div>
          </div>
        </article>
      ))}
    </>
  )
}

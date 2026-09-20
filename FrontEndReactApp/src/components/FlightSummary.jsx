export default function FlightSummary({ flight }) {
  if (!flight) {
    return (
      <aside className="summary-panel">
        <h2>Selected flight</h2>
        <div className="summary-box empty-summary">
          <p>No flight selected yet.</p>
        </div>
      </aside>
    )
  }

  return (
    <aside className="summary-panel">
      <h2>Selected flight</h2>

      <div className="summary-box">
        <p className="summary-route">
          {flight.origin} → {flight.destination}
        </p>
        <h3>{flight.airline}</h3>
        <p className="summary-flight">Flight {flight.flightNo}</p>

        <div className="summary-row">
          <span>Departure</span>
          <strong>{flight.departAt}</strong>
        </div>
        <div className="summary-row">
          <span>Arrival</span>
          <strong>{flight.arriveAt}</strong>
        </div>
        <div className="summary-row">
          <span>Duration</span>
          <strong>{flight.duration}</strong>
        </div>
        <div className="summary-row total-row">
          <span>Total</span>
          <strong>${flight.price}</strong>
        </div>
      </div>

      <button type="button" className="primary-button full-width">
        Continue booking
      </button>
    </aside>
  )
}

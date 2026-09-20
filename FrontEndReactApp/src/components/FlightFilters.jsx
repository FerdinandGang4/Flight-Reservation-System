export default function FlightFilters({ filters, onChange }) {
  return (
    <section className="search-panel">
      <div className="field-group">
        <label htmlFor="origin">From</label>
        <input
          id="origin"
          type="text"
          value={filters.origin}
          placeholder="Origin city"
          onChange={(event) => onChange('origin', event.target.value)}
        />
      </div>

      <div className="field-group">
        <label htmlFor="destination">To</label>
        <input
          id="destination"
          type="text"
          value={filters.destination}
          placeholder="Destination city"
          onChange={(event) => onChange('destination', event.target.value)}
        />
      </div>

      <div className="field-group">
        <label htmlFor="date">Departure</label>
        <input id="date" type="date" defaultValue="2026-09-21" />
      </div>
    </section>
  )
}

export default function AppHeader({ user, onLoginClick, onLogout }) {
  return (
    <header className="topbar">
      <div className="brand-wrap">
        <div className="brand-mark">F</div>
        <div>
          <span className="brand-name">FlightEase</span>
          <small>Powered by FERDINAND DINGA GANG</small>
          <small>Sr. Software Engineer </small>
          <small>Tel: +1 (641) 23-323-57</small>
        </div>
      </div>

      <nav className="topnav" aria-label="Primary navigation">
        <a href="#">Home</a>
        <a href="#">Flights</a>
        <a href="#">Deals</a>
        <a href="#">Support</a>
      </nav>

      <button
        type="button"
        className="primary-button header-button"
        onClick={user ? onLogout : onLoginClick}
      >
        {user ? `Hi, ${user.name}` : 'Sign in'}
      </button>
    </header>
  )
}

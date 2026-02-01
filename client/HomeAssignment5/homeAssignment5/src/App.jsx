import { useState } from 'react'
import './App.css'
import Login from './Login'
import LegoList from './LegoList'

function App() {
  const [token, setToken] = useState(null)

  const handleLogout = () => {
    setToken(null)
  }
  return (
    <>
    {!token ? 
      (
        <Login onLogin={setToken} />
      ) : 
      (
        <>
          <button onClick={handleLogout} className='logoutBtn'>Logout</button>
          <LegoList token={token} />
        </>
      )}
    </>
  )
}

export default App

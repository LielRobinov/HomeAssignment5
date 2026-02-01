import React, { useState } from "react";

export default function Login({ onLogin }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  //טיפול בטופס התחברות
  const handleSubmit = async (e) => {
    e.preventDefault();
    const res = await fetch("https://localhost:7217/api/Auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username: username,password: password}),
    });

    if (!res.ok) {
      alert("Login failed");
      return;
    }

    const data = await res.json();
    console.log("Login successful, token:", data.token);
    //העברת הטוקן ל-App דרך הפונקציה onLogin
    onLogin(data.token);
  };

  return (
    <div
      className="loginContainer"
      style={{ border: "1px solid #ccc", padding: "20px", maxWidth: "300px" }}
    >
      <h3>Lego Login</h3>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="User"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <br />
        <input
          type="password"
          placeholder="Pass"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <br />
        <button type="submit">Login</button>
      </form>
    </div>
  );
}

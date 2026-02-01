import React, { useState, useEffect } from "react";

export default function LegoList({ token }) {

  const [legoSets, setLegoSets] = useState([]);
  const [newName, setNewName] = useState("");
  const [newPrice, setNewPrice] = useState("");

  //טעינת נתונים בעת עליית הקומפוננטה
  useEffect(() => {
    fetch("https://localhost:7217/api/Lego", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then((res) => res.json())
      .then((data) => setLegoSets(data))
      .catch((err) => console.error("Error fetching lego sets:", err));
  }, [token]);

//הוספת סט לגו חדש
  const handleAdd = async () => {
    if (!newName || !newPrice) return;

    const res = await fetch("https://localhost:7217/api/Lego", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: JSON.stringify({ name: newName, price: newPrice }),
    });

    if (!res.ok) {
      alert("Failed to add Lego set");
      return;
    }

    const addSet = await res.json();
    setLegoSets([...legoSets, addSet]);
    setNewName("");
    setNewPrice("");
  };

//מחיקת סט לגו
    const handleDelete = async (id) => {
    const res = await fetch(`https://localhost:7217/api/Lego/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    });

    if (!res.ok) {
      alert("Failed to delete Lego set");
      return;
    }

    // עדכון ה-state לאחר מחיקה
    setLegoSets(legoSets.filter((set) => set.id !== id));
  };

  return (
    <div className="addSetContainer">
      <h3>My Collection</h3>
      <div>
        <input
          placeholder="Name"
          type="text"
          value={newName}
          onChange={(e) => setNewName(e.target.value)}
        />
        <input
          placeholder="Price"
          type="number"
          value={newPrice}
          onChange={(e) => setNewPrice(e.target.value)}
        />
        <button onClick={handleAdd}>Add Set</button>
      </div>
      <ul className="legoList">
        {legoSets.map((set) => (
          <li key={set.id} className="legoCard">
            {set.name} - ${set.price}
            <button
              className="deleteBtn"
              onClick={() => handleDelete(set.id)}
              style={{ marginLeft: "10px", color: "red" }}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

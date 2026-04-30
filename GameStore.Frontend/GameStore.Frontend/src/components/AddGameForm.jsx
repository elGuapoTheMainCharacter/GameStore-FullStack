import { useState, useEffect } from "react";
import "../CSS/AddGameForm.css";

function AddGameForm({ closeForm, gameToEdit }) {
    const [name, setName] = useState("");
    const [genreName, setGenreName] = useState(""); // We store the Name string
    const [price, setPrice] = useState("");
    const [releaseDate, setReleaseDate] = useState("");
    const [genres, setGenres] = useState([]); // To hold the list from the API

    const API_URL = 'http://localhost:5084';

    // 1. Fetch available genres when the form loads
    useEffect(() => {
        fetch(`${API_URL}/genres`)
            .then(res => res.json())
            .then(data => setGenres(data))
            .catch(err => console.error("Error fetching genres:", err));

        if (gameToEdit) {
            setName(gameToEdit.name);
            setGenreName(gameToEdit.genre || ""); 
            setPrice(gameToEdit.price);
            setReleaseDate(gameToEdit.releaseDate);
        }
    }, [gameToEdit]);

    const handleSubmit = (e) => {
        e.preventDefault();
        
        // Use the genreName string in your DTO
        const gameData = { 
            name, 
            genreId: genreName, // Your DTO calls it genreId but expects a string name
            price: parseFloat(price),
            releaseDate 
        };

        const method = gameToEdit ? 'PUT' : 'POST';
        const url = gameToEdit ? `${API_URL}/games/${gameToEdit.id}` : `${API_URL}/games`;

        fetch(url, {
            method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(gameData)
        })
        .then(response => response.ok ? closeForm() : alert("Error saving game"))
        .catch(err => console.error('Error:', err));
    };

    return (
        <div className="addGameContainer">
            <div className="form-card">
                <form className="addGameForm" onSubmit={handleSubmit}>
                    <h3 className="form-title">{gameToEdit ? "Edit Game" : "Add New Game"}</h3>
                    
                    <div className="input-group">
                        <label>Game Title</label>
                        <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                    </div>
                    
                    <div className="input-row">
                        <div className="input-group">
                            <label>Genre</label>
                            {/* 2. THE DROPDOWN */}
                            <select 
                                className="genre-select"
                                value={genreName} 
                                onChange={(e) => setGenreName(e.target.value)} 
                                required
                            >
                                <option value="">Select a Genre</option>
                                {genres.map(g => (
                                    <option key={g.id} value={g.name}>
                                        {g.name}
                                    </option>
                                ))}
                            </select>
                        </div>
                        
                        <div className="input-group">
                            <label>Price ($)</label>
                            <input type="number" step="0.01" value={price} onChange={(e) => setPrice(e.target.value)} required />
                        </div>
                    </div>

                    <div className="input-group">
                        <label>Release Date</label>
                        <input type="date" value={releaseDate} onChange={(e) => setReleaseDate(e.target.value)} required />
                    </div>

                    <div className="formBtns">
                        <button type="submit" className="saveBtn">{gameToEdit ? "Update" : "Add"}</button>
                        <button type="button" className="cancelBtn" onClick={closeForm}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default AddGameForm;

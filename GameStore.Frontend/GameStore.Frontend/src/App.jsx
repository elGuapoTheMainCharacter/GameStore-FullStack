import "./CSS/App.css"
import React, { useState, useEffect } from 'react';
import AddGameForm from "./components/AddGameForm"

function App() {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [games, setGames] = useState([]); 
  const [gameToEdit, setGameToEdit] = useState(null); 
  const [isLoading, setIsLoading] = useState(true); // New loading state

  const API_URL = 'https://gamestore-fullstack-3.onrender.com';

  const fetchGames = () => {
    setIsLoading(true); // Start loading
    fetch(`${API_URL}/games`)
      .then(response => response.json())
      .then(data => {
        setGames(data);
        setIsLoading(false); // Stop loading
      })
      .catch(error => {
        console.error('Error fetching games:', error);
        setIsLoading(false);
      });
  };

  useEffect(() => {
    fetchGames();
  }, []);   

  // ... (deleteGame, handleAddClick, handleEditClick stay the same)

  return (
    <div className="container">
      <header className="header">
        <h1 className="heading">Game Store</h1>
        {!isFormVisible && (
          <button className="addGameBtn" onClick={handleAddClick}>
            + Add New Game
          </button>
        )}   
      </header>
      
      {!isFormVisible && (
        <div className="table-container">
          {isLoading ? (
            /* PROFESSIONAL LOADING MESSAGE FOR RECRUITERS */
            <div className="loading-state">
              <div className="spinner"></div>
              <p>Waking up the server...</p>
              <span>Note: Using Render's free tier. This may take 30-50 seconds on the first load.</span>
            </div>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Genre</th>
                  <th>Price</th>
                  <th>Release Date</th>
                  <th className="text-right">Actions</th>
                </tr>
              </thead>
              <tbody>
                {games.length > 0 ? (
                  games.map(game => (
                    <tr key={game.id}>
                      <td className="game-name">{game.name}</td>
                      <td><span className="genre-badge">{game.genre}</span></td> 
                      <td className="price-tag">${game.price}</td>
                      <td>{game.releaseDate}</td>
                      <td>
                        <div className="actionsTd">
                          <button className="editBtn" onClick={() => handleEditClick(game)}>Edit</button>
                          <button className="deleteBtn" onClick={() => deleteGame(game.id)}>Delete</button>
                        </div>
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan="5" style={{textAlign: 'center', padding: '40px'}}>
                      No games found. Try adding one!
                    </td>
                  </tr>
                )}
              </tbody>   
            </table>
          )}
        </div>
      )}

      {isFormVisible && (
        <div className="form-overlay">
            <AddGameForm 
              closeForm={() => {
                setIsFormVisible(false);
                fetchGames(); 
              }}
              gameToEdit={gameToEdit}
            />
        </div>
      )}
    </div>
  );
}

export default App;

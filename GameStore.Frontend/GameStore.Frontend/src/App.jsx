import "./CSS/App.css"
import React, { useState, useEffect } from 'react';
import AddGameForm from "./components/AddGameForm"

function App() {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [games, setGames] = useState([]); 
  const [gameToEdit, setGameToEdit] = useState(null); 

  const API_URL = 'https://gamestore-fullstack-3.onrender.com/';

  const fetchGames = () => {
    fetch(API_URL)
      .then(response => response.json())
      .then(data => setGames(data))
      .catch(error => console.error('Error fetching games:', error));
  };

  useEffect(() => {
    fetchGames();
  }, []);   

  const deleteGame = (id) => {
    if (window.confirm("Are you sure you want to delete this game?")) {
      fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
      })
      .then(response => {
        if (response.ok) {
          setGames(games.filter(game => game.id !== id));
        }
      })
      .catch(error => console.error('Error deleting game:', error));
    }
  };

  const handleAddClick = () => {
    setGameToEdit(null);
    setIsFormVisible(true);
  };

  const handleEditClick = (game) => {
    setGameToEdit(game);
    setIsFormVisible(true);
  };

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
              {games.map(game => (
                <tr key={game.id}>
                  <td className="game-name">{game.name}</td>
                  <td><span className="genre-badge">{game.genre || game.genreId}</span></td> 
                  <td className="price-tag">${game.price}</td>
                  <td>{game.releaseDate}</td>
                  <td>
                    <div className="actionsTd">
                      <button className="editBtn" onClick={() => handleEditClick(game)}>Edit</button>
                      <button className="deleteBtn" onClick={() => deleteGame(game.id)}>Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>   
          </table>
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

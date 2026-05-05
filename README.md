# 🎮 GameStore — Full-Stack Web Application

A production-style **full-stack Game Store platform** built with **ASP.NET Core (.NET 10)** and **React**, showcasing end-to-end development, API design, and cloud deployment.

This project demonstrates real-world engineering practices including **RESTful API design, database integration, cloud hosting, and frontend-backend communication**.

---

## 🚀 Live Demo

*   **🌐 Frontend:** [https://game-store-full-stack-bwbe.vercel.app](https://game-store-full-stack-bwbe.vercel.app)
*   **🔌 Backend API:** [https://onrender.com](https://onrender.com)

---

## ✨ Key Features

*   **🎯 Dynamic Management:** Add, edit, and delete games in real-time.
*   **📂 Genre Integration:** Dynamic genre selection fetched directly from the database.
*   **🔍 RESTful Architecture:** Fully decoupled API powering all frontend interactions.
*   **⚡ Modern UI:** Fast, responsive interface built with React and polished CSS.
*   **🌐 Cloud Native:** Fully deployed frontend (**Vercel**) and backend (**Render**).
*   **🗄️ Persistent Storage:** Cloud-hosted PostgreSQL database (**Neon**) ensuring data survives server restarts.

---

## 🧠 Technical Highlights

*   Built a **scalable ASP.NET Core Web API** using Minimal APIs and clean architecture.
*   Integrated **Entity Framework Core with PostgreSQL (Neon)** for persistent data.
*   Implemented **robust connection handling with retry logic** to manage cloud database "cold starts."
*   Configured **CORS policies** for secure cross-origin communication between Vercel and Render.
*   Containerized the backend using **Docker** for consistent deployment environments.
*   Used **Environment Variables** to manage sensitive connection strings securely.

---

## 🧱 Project Structure

```bash
GameStore/
 ├── GameStore.Api/        # ASP.NET Core Web API (.NET 10)
 │    └── GameStore.Api/   # Project Logic, DTOs, and Entities
 └── GameStore.Frontend/   # React (Vite) Single Page Application
```

---

## 🛠️ Tech Stack

**Backend**
*   ASP.NET Core (.NET 10)
*   Entity Framework Core
*   PostgreSQL (Neon)
*   Npgsql Driver

**Frontend**
*   React
*   Vite
*   JavaScript (ES6+)
*   CSS3

**DevOps & Hosting**
*   **Render:** Backend Hosting (Docker Runtime)
*   **Vercel:** Frontend Hosting
*   **Neon:** Managed PostgreSQL Database

---

## 📦 API Overview


| Method | Endpoint | Description |
| :--- | :--- | :--- |
| **GET** | `/games` | Retrieve all games with genre details |
| **GET** | `/genres` | Fetch available genres for dropdowns |
| **POST** | `/games` | Add a new game to the catalog |
| **PUT** | `/games/{id}` | Update existing game details |
| **DELETE** | `/games/{id}` | Remove a game from the database |

---

## ⚙️ Getting Started (Local Setup)

1. **Clone the repo**
   ```bash
   git clone https://github.com
   cd GameStore-FullStack
   ```

2. **Backend setup**
   ```bash
   cd GameStore.Api/GameStore.Api
   dotnet restore
   dotnet run
   ```

3. **Frontend setup**
   ```bash
   cd GameStore.Frontend/GameStore.Frontend
   npm install
   npm run dev
   ```

---

## 📈 Engineering Competencies Demonstrated
*   Full-stack development lifecycle from UI → API → Database.
*   Implementation of **DTO (Data Transfer Object)** patterns for secure data handling.
*   Expertise in **Cloud Deployment pipelines** and environment configuration.
*   Writing clean, maintainable, and scalable C# and JavaScript code.

---

## 📌 Future Improvements
*   🔐 **Authentication:** Implementing JWT for secure admin access.
*   🛒 **Cart System:** Adding a local-storage based shopping cart.
*   ⭐ **Ratings:** User-generated reviews and star ratings.
*   📊 **Dashboard:** Analytics for game sales and inventory levels.

---

## 👨‍💻 Author

**Zipo Siposenkosi Nkefe**  
*Aspiring Full-Stack Developer passionate about building scalable web applications and solving real-world problems through code.*

[![GitHub](https://shields.io)](https://github.com)

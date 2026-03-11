# SmartWheel

SmartWheel is a full-stack reward wheel application where users answer riddles and spin a wheel to win prizes. The backend controls all game logic, ensuring fairness by calculating the score, determining the prize, and returning the exact wheel segment where the wheel must stop.

The frontend only handles the user interface and wheel animation.

## Tech Stack

Frontend
- React
- Vite
- Tailwind CSS

Backend
- ASP.NET Core Minimal API (.NET 9)
- Entity Framework Core
- SQLite

Hosting
- Azure App Service

## How It Works

1. The user answers five riddles.
2. The answers are sent to the backend.
3. The backend calculates the score and determines the prize.
4. The backend returns the wheel segment index.
5. The frontend spins the wheel to that exact segment and displays the result.

## API Example

**POST /spin**

Request:

```json
{
  "userId": "GUID",
  "answers": ["echo", "shadow", "time", "fire", "water"]
}
```

Response:

```json
{
  "score": 5,
  "prizeAmount": 100,
  "wheelIndex": 5
}
```

The frontend uses `wheelIndex` to animate the wheel and shows the prize returned by the backend.

## Run Locally

Backend:

```bash
dotnet run
```

Frontend:

```bash
cd SmartWheel.Client
npm install
npm run dev
```

Frontend runs at:

```
http://localhost:5173
```

## Author

Msekeli

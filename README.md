# FinTracker
**A multi-project .NET solution for tracking your daily expenses — with both a Console App and a REST API.**

---

## 📌 Overview

FinTracker helps you manage your expenses by allowing you to add, view, filter, delete, and summarize your spending. Built as a multi-project solution with shared core logic, it exposes two interfaces: a console app for local use and a REST API for programmatic access.

---

## 🏗️ Project Structure

```
FinTracker/
├── FinTracker.Core/          # Shared business logic (models, repository, service)
├── FinTracker.Console/       # Console application interface
└── FinTracker.API/           # ASP.NET Core REST API
```

- **FinTracker.Core** — contains the `Expense` model, `ExpenseRepository` (JSON persistence), and `ExpenseService` (business logic). Referenced by both the Console and API projects.
- **FinTracker.Console** — a terminal-based interface with an interactive menu.
- **FinTracker.API** — an ASP.NET Core Web API exposing expenses over HTTP with JSON responses.

---

## ✨ Features

- **Add Expenses** — log expenses with a name, amount, and category
- **View All Expenses** — display a list of all recorded expenses
- **Filter by Category** — view expenses for a specific category
- **Delete Expenses** — remove an expense by ID
- **Summary Statistics** — total spent, most expensive item, top category, and average expense
- **Data Persistence** — expenses saved to a JSON file, shared across both interfaces
- **REST API** — full HTTP access to all features with proper status codes

---

## 🛠️ Built With

- **.NET 8**
- **ASP.NET Core** (Web API)
- **C#** — OOP, LINQ, exception handling
- **System.Text.Json** — serialization and persistence
- **Swagger / Swashbuckle** — API documentation and testing UI

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Soufianesaliki/FinTracker.git
   cd FinTracker
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Running the Console App

```bash
dotnet run --project FinTracker.Console
```

You'll see an interactive menu to add, view, filter, delete, and summarize expenses.

### Running the API

```bash
dotnet run --project FinTracker.API
```

The API will start listening on `http://localhost:<port>`. Open Swagger UI to explore and test all endpoints:

```
http://localhost:<port>/swagger/index.html
```

---

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/expenses` | Get all expenses |
| `GET` | `/expenses/{id}` | Get a specific expense by ID |
| `GET` | `/expenses/category/{category}` | Get expenses by category |
| `GET` | `/expenses/summary` | Get summary statistics |
| `POST` | `/expenses` | Add a new expense |
| `DELETE` | `/expenses/{id}` | Delete an expense by ID |

### Example POST body

```json
{
  "name": "Coffee",
  "amount": 3.50,
  "category": "Food"
}
```

### Example GET /expenses/summary response

```json
{
  "totalExpenses": 4,
  "totalSpent": 87.40,
  "averageExpense": 21.85,
  "mostExpensive": "Grocery run",
  "topCategory": "Food"
}
```

---

## 💾 Data Storage

Expenses are persisted to a `expenses.json` file. Both the Console app and the API share the same file, so data added through one interface is immediately visible in the other.
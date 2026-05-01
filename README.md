# Asset Tracker - .NET 10 Web API

This project is a high-performance **.NET 10 Web API** designed to manage personal financial portfolios. The system allows users to securely track their assets (stocks and cryptocurrencies), record transactions, and retrieve real-time market data through seamless integrations with external financial APIs. It is built with a strong focus on clean architecture, security, and scalability.

## 🚀 Key Features

### 🔒 Robust Security & Identity
* **JWT Authentication:** Implements stateless authentication using JSON Web Tokens (JWT) for secure API access.
* **Resource-Based Authorization:** Ensures strict data privacy—users can only access, update, or delete portfolios and transactions that belong to their specific `UserId`.
* **ASP.NET Core Identity:** Handles secure user registration, password hashing, and token validation.

### 🌐 Live Market Data Integrations
* **Multi-Source Fetching:** Utilizes typed `HttpClient` services to fetch real-time data from **Finnhub** (Stocks), **CoinGecko** (Cryptocurrencies), and **NewsAPI** (Market News).
* **Smart Data Normalization:** Separates raw external JSON responses (e.g., extracting CoinGecko's dynamic keys or Finnhub's `c`, `d`, `dp` fields) and maps them into a unified, clean `AssetPriceDto` for the frontend.

### 🏗️ Enterprise-Grade Architecture
* **Clean & Scalable Codebase:** Adheres strictly to **SOLID principles** utilizing N-Tier architecture (Controllers, Services, Data Access).
* **"No Magic Strings" Policy:** All validation messages, API endpoints, and system constants are centralized in `AppMessages` and `AppConstants` for extreme maintainability.
* **Unified API Responses:** Wraps all controller outputs in a generic `ApiResponse<T>` object, ensuring a predictable contract (Success status, Data payload, Messages, Errors) for frontend consumers.

### 🗄️ Advanced Data Management
* **Entity Framework Core:** Uses EF Core with SQL Server for robust relational data mapping.
* **Financial Precision:** Implements strict database-level precision constraints (`18,8` for crypto quantities, `18,2` for fiat prices) directly within the `OnModelCreating` configuration to ensure financial accuracy.

## 🛠 Technologies & Tools

| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 10 (ASP.NET Core Web API) |
| **Database** | SQL Server via Entity Framework Core |
| **Authentication** | ASP.NET Core Identity & JWT Bearer |
| **External APIs** | Finnhub, CoinGecko, NewsAPI |
| **Docs** | Swashbuckle / Swagger UI (Configured for OpenAPI v10 w/ JWT Lock) |

## 📦 Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/yourusername/AssetTracker-WebAPI.git](https://github.com/yourusername/AssetTracker-WebAPI.git)
   ```

2. **Navigate to the directory:**
   ```bash
   cd AssetTracker-WebAPI
   ```

3. **Configure Database & API Keys:**
   Update the `appsettings.json` with your SQL Server connection string, or set up **User Secrets** for your external API keys to keep them out of version control:
   ```bash
   dotnet user-secrets set "ApiSettings:Finnhub:ApiKey" "your_finnhub_key"
   dotnet user-secrets set "ApiSettings:CoinGecko:ApiKey" "your_coingecko_key"
   dotnet user-secrets set "JwtSettings:Secret" "your_super_secret_jwt_key_here"
   ```

4. **Apply Database Migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Build and Run:**
   ```bash
   dotnet run
   ```

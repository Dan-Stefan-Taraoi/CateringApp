# CateringApp

A learning-focused ASP.NET Core MVC application simulating a restaurant and catering management system. Built incrementally to explore EF Core, design patterns, authentication, and CI/CD in a realistic domain context.

## Overview

CateringApp manages inventory, a dynamic menu, and orders for two distinct service contexts — dine-in restaurant service and off-site catering events — sharing a single central kitchen. The project evolved from a simple CRUD app into a multi-layered system exploring several classic design patterns applied to a real-world-style domain.

## Features

- **Inventory management** — ingredients and hardware items with category-based organization
- **Menu management** — dishes with cooking methods, serving sizes, and recipe-style ingredient requirements
- **Order placement** — staff-facing flow supporting both Restaurant and Catering service types
- **Kitchen simulation** — dish preparation with live visual timers driven by per-dish preparation time
- **Inventory deduction** — stock automatically adjusted when orders are placed
- **Order history** — expandable order details with line items and payment status
- **Payment tracking** — separate payment records supporting multiple payment methods
- **Authentication & roles** — ASP.NET Core Identity with Staff, Manager, and Admin roles
- **Unit tests** — xUnit test suite covering factories, builders, and services
- **CI pipeline** — GitHub Actions workflow running build and test on every push

## Architecture & Design Patterns

This project intentionally applies several design patterns to solve real structural problems rather than as academic exercises:

| Pattern | Where | Purpose |
|---|---|---|
| **Factory Method** | `RestaurantKitchenFactory`, `CateringKitchenFactory` | Produce context-specific dishes from a `MenuItem` |
| **Strategy / Resolver** | `KitchenFactoryResolver` | Select the correct factory at runtime based on service type |
| **Builder** | `OrderDetailsBuilder` | Construct the runtime order context fluently |
| **Static Factory Method** | `Order.Create()`, `OrderDetails.ForRestaurant/ForCatering()` | Encapsulate object construction logic, avoid duplication |
| **Observer** | `IOrderEventObserver`, `OrderEventPublisher` | Decouple order-placement side effects (inventory, kitchen, billing, notifications) from the controller |

### Runtime vs. Persistence separation

A core architectural decision in this project: **`OrderDetails` and `IDish` (and its implementations `RestaurantDish`/`CateringDish`) exist only at runtime** — they orchestrate kitchen production for the duration of a single request and are never persisted. **`Order` and `MenuOrderEntry`** are the persisted record of what was sold, including price/name snapshots so historical orders remain accurate even if the menu changes later.

```
MenuItem (DB)
    ↓ staff places order
DishService.CreateDish() → IKitchenFactory → IDish (runtime)
    ↓
OrderEventPublisher → IOrderEventObserver(s)
    ├── InventoryObserver  → deducts stock
    ├── KitchenObserver    → starts preparation (fire-and-forget)
    └── BillingObserver    → creates PaymentRecord
    ↓
Order + MenuOrderEntry (persisted)
```

## Tech Stack

- **.NET 8** / ASP.NET Core MVC
- **Entity Framework Core** with SQL Server (LocalDB for development)
- **ASP.NET Core Identity** for authentication and role-based authorization
- **xUnit** for unit testing
- **GitHub Actions** for CI

## Project Structure

```
CateringApp/
├── Controllers/        Inventory, MenuItems, Orders, Account, UserManagement
├── Models/
│   ├── Builders/        OrderDetailsBuilder
│   ├── Observers/        IOrderEventObserver implementations + publisher
│   ├── Interfaces/        IDish, IKitchenFactory, IOrderEventObserver
│   └── Enums/             CookingMethod, PaymentMethod
├── Services/            DishService, KitchenFactoryResolver, RoleSeeder
├── Data/                MyAppContext (EF Core, Identity)
├── Views/               Razor views, warm-themed shared styles
└── Migrations/          EF Core migrations

CateringApp.Tests/
├── Factories/           Kitchen factory and resolver tests
├── Builders/             OrderDetailsBuilder tests
├── Models/               Order, DishBase tests
├── Services/             DishService tests
└── Observers/            InventoryObserver tests
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server LocalDB (included with Visual Studio) or a SQL Server instance

### Setup

```bash
git clone https://github.com/Dan-Stefan-Taraoi/CateringApp.git
cd CateringApp
dotnet restore
```

Update the connection string in `appsettings.json` if needed, then apply migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run --project CateringApp
```

On first run, `RoleSeeder` creates the `Admin`, `Manager`, and `Staff` roles along with a default admin account.

### Running Tests

```bash
dotnet test
```

## Domain Model

```
Category 1──* Item
Item     1──* KitchenItem *──1 MenuItem
Client   1──* Order
Order    1──* MenuOrderEntry
Order    1──0..1 PaymentRecord
```

`KitchenItem` resolves the many-to-many relationship between `Item` (inventory) and `MenuItem` (menu), carrying the `QuantityRequired` for that recipe — the link that drives automatic inventory deduction.

## Roadmap

- [x] Inventory & Menu CRUD
- [x] Order placement with kitchen factory pattern
- [x] Inventory deduction via Observer pattern
- [x] Authentication & role-based authorization
- [x] Payment tracking
- [x] Unit tests & CI pipeline
- [ ] Minimal API layer for external/mobile consumption
- [ ] SignalR-driven live kitchen display
- [ ] Deployment pipeline (CD)

## License

This is a personal learning project, not intended for production use.

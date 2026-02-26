**CHUK'S KITCHEN API**

**TECHNICAL DESCRIPTION**

**OVERVIEW**

Chuks Kitchen API is a C# ASP.NET Core Web API project that demonstrates backend functionality for a kitchen management system. It uses in‑memory data storage (no external database required) and exposes endpoints for Authentication, Menu, and Order.

**Environment Setup**

-.NET SDK 6.0 or higher

-Visual Studio 2022 or Visual Studio Code

-Works on Windows, macOS, or Linux

**Dependencies**

-ASP.NET Core Web API

-Microsoft.EntityFrameworkCore.InMemory

-Swashbuckle.AspNetCore (Swagger UI)

-xUnit/NUnit (for testing)

Install via NuGet:

bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Swashbuckle.AspNetCore
Endpoints
**AUTHENTICATION**

POST /api/auth/login → User login

POST /api/auth/register → Register new user

**MENU**

GET /api/menu → Retrieve all menu items

POST /api/menu → Add new menu item

PUT /api/menu/{id} → Update menu item

DELETE /api/menu/{id} → Remove menu item

**ORDER**

POST /api/order → Place a new order

GET /api/order/{id} → Retrieve order details

GET /api/order → List all orders

Running the Project
bash
git clone https://github.com/OkongwuChioma/ChucksKitchen.git
cd ChucksKitchen
dotnet build
dotnet run

Base URL: http://localhost:5000/api/

Swagger UI: http://localhost:5000/swagger

Testing
bash
dotnet test

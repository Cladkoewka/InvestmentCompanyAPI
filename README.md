---

# Investment Projects Management API

## Description

This API powers the Investment Projects Management platform. It provides endpoints for managing investment projects, customers, employees, departments, and more. The API is built using ASP.NET Core 8 and follows a clean architecture with distinct layers for application logic, domain models, and infrastructure.


---
Frontend Repository: [https://github.com/Cladkoewka/InvestmentCompanyFrontend](https://github.com/Cladkoewka/InvestmentCompanyFrontend)

## Technologies

- **Backend Framework**:
  - **ASP.NET Core 8** — The primary framework for building the API.
  - **Entity Framework Core** — ORM for database access and management.
  - **PostgreSQL** — Relational database for storing project, customer, and related data.

- **Authentication & Authorization**:
  - **JWT (JSON Web Tokens)** — Used for securing API endpoints.
  - **ASP.NET Core Identity** — Manages authentication and user roles.
  - **Roles**: Admin, Viewer.

- **Architecture**:
  - **Clean Architecture** — Separation of concerns into distinct layers: API, Application, Domain, Infrastructure.
  - **DTOs (Data Transfer Objects)** — For data mapping and communication between layers.

- **Dependencies**:
  - **AutoMapper** — For mapping entities to DTOs.
  - **FluentValidation** — For validation of incoming requests.

---

## Endpoints

### AuthController
- **POST /api/auth/register**: Register a new user.
- **POST /api/auth/login**: Authenticate and log in a user.

### ProjectController
- **GET /api/projects**: Get all projects (Admin, Viewer).
- **GET /api/projects/{id}**: Get a project by ID (Admin, Viewer).
- **GET /api/projects/customer/{customerId}**: Get projects by customer ID (Admin, Viewer).
- **GET /api/projects/editor/{editorId}**: Get projects by editor ID (Admin, Viewer).
- **POST /api/projects**: Create a new project (Admin).
- **PUT /api/projects/{id}**: Update a project (Admin).
- **DELETE /api/projects/{id}**: Delete a project (Admin).

### CustomerController
- **GET /api/customers**: Get all customers (Admin, Viewer).
- **GET /api/customers/{id}**: Get a customer by ID (Admin, Viewer).
- **GET /api/customers/by-name/{name}**: Get a customer by name (Admin, Viewer).
- **POST /api/customers**: Add a new customer (Admin).
- **PUT /api/customers/{id}**: Update a customer (Admin).
- **DELETE /api/customers/{id}**: Delete a customer (Admin).

### FunctionalController
- **GET /api/functional/totalprofit**: Get the total profit from all projects (Admin).
- **GET /api/functional/projects/{customerName}**: Get projects by customer name (Admin).

### DepartmentController, EditorController, EmployeeController, RiskController, ProjectAssetLinkController
- These controllers manage respective entities and provide endpoints for CRUD operations (Admin, Viewer).

---

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Cladkoewka/InvestmentCompanyAPI.git
   ```

2. Install dependencies:
   ```bash
   cd InvestmentCompanyAPI
   dotnet restore
   ```

3. Configure PostgreSQL connection in `appsettings.json`.

4. Run the application:
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5000`.

---

## Authentication

- Use JWT tokens for authentication.
- Include the token in the `Authorization` header with the prefix `Bearer` for protected routes.
- Example header: 
  ```bash
  Authorization: Bearer <your-token>
  ```

---

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

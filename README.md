## Employee Management API (ERP-Style)


## ⚙️ Tech Stack

- ASP.NET Core 8 Web API
- Entity Framework Core (SQL Server)
- AutoMapper, Swagger
- Clean Architecture + Repository Pattern

---

## 🚀 Setup Instructions

1. **Clone & Navigate**
   ```bash
   git clone https://github.com/your-username/employee-management-api.git
   cd employee-management-api
2. **Set up DB in appsettings.json**
   "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb);Database=EmployeeManagementDB;Trusted_Connection=True;"
}
3. **Run Migrations**
   
   dotnet ef migrations add InitialCreate
   
   dotnet ef database update
   
5. **Start API**
   
   dotnet run
   
   Access at : http://localhost:5186/swagger

7. **Project Structure**
   
  ├── Controllers/        → API endpoints
  ├── DTOs/               → Request/response models
  ├── Entities/           → Domain models (Employee, Department)
  ├── Repositories/       → Interfaces + Implementations
  ├── Data/               → DbContext + Seed/Migration
  ├── Program.cs          → App config + DI
  
9. **Features**
  Employee Operations
  CRUD endpoints
  Server-side validation (DataAnnotations)
  Pagination for employees list
  Unique email enforced
11. **Filtering & Sorting**
  GET /api/employee/filter
  Supports:
  name, departmentId, status, hireDateFrom, hireDateTo
  Sorting by name or hireDate
  Example : /api/employee/filter?status=Active&sortBy=hireDate&sortOrder=desc&pageNumber=1&pageSize=5

12. **Log History**
  Tracks Created, Updated, Deleted actions
  View logs: GET /api/logs

13. **Sample Request**
    POST /api/employee
  {
    "name": "Jane Doe",
    "email": "jane@example.com",
    "departmentId": 1,
    "hireDate": "2024-05-01",
    "status": "Active"
  }
  





